using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeanScene.Tests.Infrastructure
{
    internal class ObjectValidator : IObjectModelValidator
    {
        public void Validate(
            ActionContext actionContext,
            ValidationStateDictionary? validationState,
            string prefix,
            object? model)
        {
            //Exit if no model 
            if (model == null) return;

            //Initialise validation context and results
            var context = new ValidationContext(model, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            //Validate model
            bool isValid = Validator.TryValidateObject(
                model, context, results, validateAllProperties: true
                );
            // if not valid, add errors to context
            if (!isValid)
            {
                results.ForEach(r =>
                {
                    //add validation errors to the modelstate

                    actionContext.ModelState.AddModelError("", r.ErrorMessage ?? "");

                });
            }
        
        }

    }
}
