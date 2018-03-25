using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Gen8.Ledger.Api.Requests
{
    public class BadRequestActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                actionContext.Result = new BadRequestObjectResult(new ApiResourceValidationErrorWrapper(actionContext.ModelState));
            }
            base.OnActionExecuting(actionContext);
        }
    }
    public class ApiResourceValidationErrorWrapper
    {
        private const string ErrorMessage = "The request is invalid.";

        private const string MissingPropertyError = "Undefined error.";

        public ApiResourceValidationErrorWrapper(ModelStateDictionary modelState)
        {
            Message = ErrorMessage;
            SerializeModelState(modelState);
        }

        public ApiResourceValidationErrorWrapper(string message, ModelStateDictionary modelState)
        {
            Message = message;
            SerializeModelState(modelState);
        }

        public string Message { get; private set; }

        public IDictionary<string, IEnumerable<string>> Errors { get; private set; }

        private void SerializeModelState(ModelStateDictionary modelState)
        {
            Errors = new Dictionary<string, IEnumerable<string>>();

            foreach (var keyModelStatePair in modelState)
            {
                var key = keyModelStatePair.Key;

                var errors = keyModelStatePair.Value.Errors;

                if (errors != null && errors.Count > 0)
                {
                    IEnumerable<string> errorMessages = errors.Select(
                        error => string.IsNullOrEmpty(error.ErrorMessage)
                                    ? MissingPropertyError
                                    : error.ErrorMessage).ToArray();

                    Errors.Add(key, errorMessages);
                }
            }
        }
    }
}
