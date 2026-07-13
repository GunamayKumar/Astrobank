using Astrobank.Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Astrobank.Web.Filters;

/// <summary>
/// Catches ValidationExceptions thrown by the Application layer and translates them into MVC ModelState errors.
/// </summary>
public class ValidationExceptionFilterAttribute : ExceptionFilterAttribute
{
    private readonly IModelMetadataProvider _modelMetadataProvider;

    public ValidationExceptionFilterAttribute(IModelMetadataProvider modelMetadataProvider)
    {
        _modelMetadataProvider = modelMetadataProvider;
    }

    public override void OnException(ExceptionContext context)
    {
        if (context.Exception is ValidationException validationException)
        {
            // Store errors in TempData so they survive a redirect
            var controller = context.RouteData.Values["controller"]?.ToString();
            var action = context.RouteData.Values["action"]?.ToString();

            var tempDataFactory = context.HttpContext.RequestServices.GetRequiredService<Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataDictionaryFactory>();
            var tempData = tempDataFactory.GetTempData(context.HttpContext);

            // Serialize errors and store them to be picked up by the controller/view
            tempData["ValidationErrors"] = System.Text.Json.JsonSerializer.Serialize(validationException.Errors);

            // Redirect back to the same GET action to display the form again
            context.Result = new RedirectToActionResult(action, controller, null);
            context.ExceptionHandled = true;
        }
        else
        {
            // For other exceptions, log them (Serilog integration comes later) and let the default error handler take over.
            // A simple placeholder for now.
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<ValidationExceptionFilterAttribute>>();
            logger.LogError(context.Exception, "An unhandled exception occurred.");
        }

        base.OnException(context);
    }
}
