using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SantaShop.Api.Extensions;

public static class ModelExtensions
{

    public static void AddToModelState(this ValidationResult result, ModelStateDictionary modelState)
    {
        foreach (var error in result.Errors)
        {
            modelState.AddModelError(error.PropertyName, error.ErrorMessage);
        }
    }

}