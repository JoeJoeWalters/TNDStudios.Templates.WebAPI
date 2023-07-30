using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.DependencyChecker.Infrastructure
{
    public class DependencyCheckResults
    {
        public List<IDependencyCheckResult> Checks { get; set; } = new List<IDependencyCheckResult>();
    }

    public interface IDependencyCheckResult
    {
        Boolean Success { get; set; }
    }

    public class HttpCheckResult : IDependencyCheckResult
    {
        public Boolean Success { get; set; } = false;

        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
    }
}
