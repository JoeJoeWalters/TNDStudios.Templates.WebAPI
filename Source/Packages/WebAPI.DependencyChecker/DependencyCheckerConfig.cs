using System.Net;
using WebAPI.DependencyChecker.Infrastructure;

namespace WebAPI.DependencyChecker
{
    public interface IDependencyCheck
    {
        string Id { get; set; }

        Action<IDependencyCheckResult> OnChange { get; set; }
    }

    public class HttpCheck : IDependencyCheck
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Path { get; set; } = string.Empty;

        public List<HttpStatusCode> ExpectedResults { get; set; } = new List<HttpStatusCode>() { HttpStatusCode.OK, HttpStatusCode.Created };

        public Action<IDependencyCheckResult>? OnChange { get; set; }
    }

    public class DependencyCheckerConfig
    {
        /// <summary>
        /// List of checks to perform
        /// </summary>
        public List<IDependencyCheck> Checks { get; set; } = new List<IDependencyCheck>();

        /// <summary>
        /// Frequency of checks (in milliseconds)
        /// </summary>
        public int Frequency { get; set; } = 1000;
    }
}
