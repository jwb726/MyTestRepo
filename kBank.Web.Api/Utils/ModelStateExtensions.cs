using System.Text;
using System.Web.Http.ModelBinding;

namespace kBank.Web.Api.Utils
{
    public static class ModelStateExtensions
    {
        public static string GetErrors(this ModelStateDictionary modelState)
        {
            var builder = new StringBuilder();

            foreach (ModelState state in modelState.Values)
            {
                foreach (ModelError error in state.Errors)
                {
                    builder.AppendLine(error.ErrorMessage);
                }
            }

            return builder.ToString();
        }
    }
}