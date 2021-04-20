namespace Imagegram.Core.Domain
{
    public interface IDomainRule
    {
        bool IsValid();

        string Message { get; }
    }
}
