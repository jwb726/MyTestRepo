using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using kBank.Data.Repositories;
using kBank.Domain.Core;
using kBank.Web.Api.Utils;

namespace kBank.Web.Api.Controllers
{
    public class UsersController : ApiController
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public HttpResponseMessage GetAllUsers()
        {
            var users = _userRepository.GetAllUsers();
            return Request.CreateResponse(HttpStatusCode.OK, users);
        }

        public HttpResponseMessage Delete(int id)
        {
            _userRepository.DeleteUser(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage GetSingleUser(int id)
        {
            var user = _userRepository.GetUser(id);
            if (user != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, user);
            }

            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        public HttpResponseMessage Put(User user)
        {
            if (ModelState.IsValid)
            {
                var updateResult = _userRepository.UpdateUser(user);
                if (updateResult.Succeeded)
                {
                    var response = Request.CreateResponse(HttpStatusCode.OK, updateResult.Result);
                    return response;
                }
                
                if (updateResult.Result == null)
                {
                    var response = Request.CreateResponse(HttpStatusCode.NotFound);
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

        public HttpResponseMessage PostNewUser(User user)
        {
            if (ModelState.IsValid)
            {
                var newUser = _userRepository.CreateUser(user);
                var response = Request.CreateResponse(HttpStatusCode.OK, newUser);

                var uri = Url.Route(null, new { id = newUser.Id });
                response.Headers.Location = new Uri(Request.RequestUri, uri);

                return response;
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState.GetErrors());
        }
    }
}