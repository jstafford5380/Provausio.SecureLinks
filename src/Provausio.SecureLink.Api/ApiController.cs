using System.Linq;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using RiskFirst.Hateoas;

namespace Provausio.SecureLink.Api
{
    public class ApiController: Controller
    {
        private IMediator _mediator;
        private ILinksService _linkService;

        protected IMediator Mediator =>
            _mediator ?? (_mediator = Request.HttpContext.RequestServices.GetRequiredService<IMediator>());

        protected ILinksService LinksService =>
            _linkService ?? (_linkService = Request.HttpContext.RequestServices.GetRequiredService<ILinksService>());

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var apiError = new ApiError();
                foreach (var (key, value) in context.ModelState)
                {
                    var errors = value.Errors.Select(error => error.ErrorMessage);
                    apiError.Errors.Add($"{key} -> {string.Join(';', errors)}");
                }

                context.Result = BadRequest(apiError);
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
