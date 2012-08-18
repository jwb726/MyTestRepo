using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using kBank.Data.Repositories;
using kBank.Domain.Core;

namespace kBank.Web.Api.Controllers
{
    public class ProjectsController : ApiController
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectsController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public HttpResponseMessage GetAllProjects()
        {
            var projects = _projectRepository.GetAllProjects();
            return Request.CreateResponse(HttpStatusCode.OK, projects);
        }

        public HttpResponseMessage Delete(int id)
        {
            _projectRepository.DeleteProject(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage GetSingleProject(int id)
        {
            var project = _projectRepository.GetProject(id);

            if (project != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, project);
            }

            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        public HttpResponseMessage Put(Project project)
        {
            if (ModelState.IsValid)
            {
                var updateResult = _projectRepository.UpdateProject(project);

                if (updateResult.Succeeded)
                {
                    var response = Request.CreateResponse(HttpStatusCode.OK, updateResult.Result);
                    return response;
                }
                else
                {
                    var response = Request.CreateResponse(HttpStatusCode.InternalServerError, updateResult.ExceptionMessage);
                    return response;
                }
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState.GetErrors());
        }

        public HttpResponseMessage PostNewProject(Project project)
        {
            if (ModelState.IsValid)
            {
                var newProject = _projectRepository.CreateProject(project);
                var response = Request.CreateResponse(HttpStatusCode.OK, newProject);
                
                var uri = Url.Route(null, new { id = newProject.Id });
                response.Headers.Location = new Uri(Request.RequestUri, uri); 

                return response;
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState.GetErrors());
        }
    }
}
