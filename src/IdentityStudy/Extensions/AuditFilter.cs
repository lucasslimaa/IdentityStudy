using KissLog;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityStudy.Extensions
{
    public class AuditFilter : IActionFilter
    {
        private readonly ILogger _logger;

        public AuditFilter(ILogger logger)
        {
            _logger = logger;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                var message = context.HttpContext.User.Identity.Name + "accessed" +
                              context.HttpContext.Request.GetDisplayUrl();

                _logger.Info(message);
            }
        }
    }
}
