using System.Net;

namespace WebAPI.DependencyChecker.Infrastructure
{
    public interface IDependencyCheckResult
    {
        IDependencyCheck Origin { get; set; }

        Boolean SuccessState { get; set; }

        Boolean PreviousState { get; set; }
    }

    public class HttpCheckResult : IDependencyCheckResult
    {
        public IDependencyCheck Origin { get; set; }

        public Boolean SuccessState { get; set; } = false;

        public Boolean PreviousState { get; set; } = false;

        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
    }
}
