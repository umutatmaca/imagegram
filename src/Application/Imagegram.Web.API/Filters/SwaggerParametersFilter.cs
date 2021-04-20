using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace Imagegram.Web.API.Filters
{
    public class SwaggerParametersFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters ??= Enumerable.Empty<OpenApiParameter>().ToList();

            if (context.ApiDescription.RelativePath == "api/accounts" && context.ApiDescription.HttpMethod == "POST")
            {
                //do not add account header key
            }
            else
            {
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "X-Account-Id",
                    In = ParameterLocation.Header,
                    AllowEmptyValue = false,
                    Style = ParameterStyle.Simple,
                    Schema = new OpenApiSchema
                    {
                        Type = "Guid"
                    },
                    Required = true
                });
            }
        }
    }
}
