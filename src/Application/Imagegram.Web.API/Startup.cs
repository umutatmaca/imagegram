using AutoMapper;
using Azure.Storage.Blobs;
using FluentValidation;
using Hellang.Middleware.ProblemDetails;
using Imagegram.Application;
using Imagegram.Application.Behaviors;
using Imagegram.Application.Handlers;
using Imagegram.Application.Validators;
using Imagegram.Core.Caching;
using Imagegram.Core.Domain;
using Imagegram.Core.Exceptions;
using Imagegram.Domain.Repositories;
using Imagegram.Domain.Services;
using Imagegram.Infrastructure;
using Imagegram.Infrastructure.Cache;
using Imagegram.Infrastructure.Database;
using Imagegram.Infrastructure.Database.Repositories;
using Imagegram.Infrastructure.File;
using Imagegram.Web.API.Filters;
using Imagegram.Web.API.Problems;
using Imagegram.Web.API.Services;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.IO;
using System.Reflection;

namespace Imagegram.Web.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add<AuthenticateAccountAttribute>();
                options.EnableEndpointRouting = true;
            }).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind;
                options.SerializerSettings.DateParseHandling = DateParseHandling.None;
            });

            var dbConnectionString = Configuration.GetConnectionString("ImagegramDB");
            services.AddDbContext<ImagegramContext>(options =>
            {
                if (!string.IsNullOrWhiteSpace(dbConnectionString))
                {
                    options.UseSqlServer(dbConnectionString, dbOptions =>
                    {
                        dbOptions.MigrationsAssembly("Imagegram.Infrastructure");
                    });
                }
                else
                {
                    options.UseInMemoryDatabase(databaseName: "ImagegramDB");
                }
            });
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddMemoryCache();
            services.AddSingleton<ICacheService, MemoryCacheService>();

            //upload to azure
            services.AddSingleton<BlobServiceClient>(x => new BlobServiceClient(Configuration.GetValue<string>("AzureBlobStorage")));
            services.AddSingleton<IImageUploader, FormFileImageUploader>();
            services.AddSingleton<IImageConverter, ImageConverter>();

            //local self hosted file uploader
            services.AddHttpContextAccessor();

            var blobConnectionStr = Configuration.GetValue<string>("AzureBlobStorage");
            if (!string.IsNullOrWhiteSpace(blobConnectionStr))
            {
                services.AddSingleton<IFileUploader, AzureBlobFileUploader>();
            }
            else
            {
                services.AddSingleton<IFileUploader, SelfHostedFileUploader>();
            }

            MapperConfiguration mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
                options.AppendTrailingSlash = false;
            });

            services.AddSwaggerGen(c =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.OperationFilter<SwaggerParametersFilter>();
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Imagegram.Web.API",
                    Version = "v1",
                    Description = "A system that allows you to upload images and comment on them",
                    Contact = new OpenApiContact
                    {
                        Email = "umut.atmaca.89@gmail.com",
                        Name = "Umut Atmaca",
                        Url = new Uri("https://www.linkedin.com/in/umut-atmaca-35511b20/")
                    }
                });
            });
            services.AddSwaggerGenNewtonsoftSupport();
            services.AddMediatR(typeof(CreateAccountCommandHandler).GetTypeInfo().Assembly);
            AssemblyScanner.FindValidatorsInAssembly(typeof(CreateAccountCommandValidator).Assembly).ForEach(item => services.AddScoped(item.InterfaceType, item.ValidatorType));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(CommandValidationBehavior<,>));



            services.AddProblemDetails(opts =>
            {
                opts.Map<ValidationException>(ex => new BadRequestProblemDetails(ex));
                opts.Map<DomainRuleFailedException>(ex => new DomainRuleFailedProblemDetails(ex));
                opts.Map<InvalidCommandException>(ex => new BadRequestProblemDetails(ex));
                opts.Map<NotFoundException>(ex => new ResourceNotFoundProblemDetails(ex));
                opts.Map<FormatException>(ex => new BadRequestProblemDetails(ex));
                opts.Map<AuthenticationException>(ex => new UnauthorizedRequestProblemDetails(ex));
                // Control when an exception is included
                opts.IncludeExceptionDetails = (ctx, ex) =>
                {
                    var env = ctx.RequestServices.GetRequiredService<IHostEnvironment>();
                    return env.IsDevelopment() || env.IsStaging();
                };
            });
            services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseProblemDetails();
            /*
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            */
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.WebRootPath ?? Directory.GetCurrentDirectory(), @"Content")),
                RequestPath = new PathString("/Content")
            });
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllerRoute(name: "Default", pattern: "api/{controller}");
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Imagegram.Web.API v1");
                c.DocExpansion(DocExpansion.None);
                c.DisplayRequestDuration();
                c.EnableFilter();
                c.ShowExtensions();
            });
        }
    }
}
