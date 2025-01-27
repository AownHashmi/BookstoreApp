using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace BookstoreApp.Filters
{
    public class RequestLoggingFilter : IActionFilter
    {
        private readonly ILogger<RequestLoggingFilter> _logger;

        public RequestLoggingFilter(ILogger<RequestLoggingFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var request = context.HttpContext.Request;
            _logger.LogInformation($"📢 Incoming Request: {request.Method} {request.Path}");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var response = context.HttpContext.Response;
            _logger.LogInformation($"✅ Response Status: {response.StatusCode}");
        }
    }
}
