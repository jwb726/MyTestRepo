using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace kBank.Web.Api.Controllers
{
    public class ProjectUsersController : ApiController
    {
        public HttpResponseMessage GetAllUsersForProject(int projectId)
        {
            return Request.CreateResponse(HttpStatusCode.SeeOther);
        }
    }
}