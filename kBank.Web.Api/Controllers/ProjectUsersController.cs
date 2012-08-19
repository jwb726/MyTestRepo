using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using kBank.Data.Repositories;
using kBank.Web.Api.Models;

namespace kBank.Web.Api.Controllers
{
    public class ProjectUsersController : ApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IProjectRepository _projectRepository;

        public ProjectUsersController(IUserRepository userRepository, IProjectRepository projectRepository)
        {
            _userRepository = userRepository;
            _projectRepository = projectRepository;
        }

        public HttpResponseMessage GetAllUsersForProject(int projectId)
        {
            var users = _userRepository.GetUsersForProject(projectId);
            return Request.CreateResponse(HttpStatusCode.OK, users);
        }

        public async Task<HttpResponseMessage> PostNewProjectUser(int projectId, ProjectUserInfo projectUserInfo)
        {
            var operation = await _projectRepository.AddUserToProject(projectId, projectUserInfo.UserId, projectUserInfo.UserTypeId);
            if (operation.Succeeded)
            {
                return Request.CreateResponse(HttpStatusCode.Created, operation.Result);
            }
            
            if (operation.ExceptionMessage != null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, operation.Message);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, operation.Message);
        }
    }
}