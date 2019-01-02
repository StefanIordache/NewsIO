using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsIO.Api.Utils
{
    public class JsonWithFilesFormDataModelBinder : IModelBinder
    {
        private readonly IOptions<MvcJsonOptions> JsonOptions;
        private readonly FormFileModelBinder FormFileModelBinder;

        public JsonWithFilesFormDataModelBinder(IOptions<MvcJsonOptions> jsonOptions, ILoggerFactory loggerFactory)
        {
            JsonOptions = jsonOptions;
            FormFileModelBinder = new FormFileModelBinder(loggerFactory);
        }

        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));

            // Retrieve the form part containing the JSON
            var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.FieldName);
            if (valueResult == ValueProviderResult.None)
            {
                // The JSON was not found
                var message = bindingContext.ModelMetadata.ModelBindingMessageProvider.MissingBindRequiredValueAccessor(bindingContext.FieldName);
                bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, message);
                return;
            }

            var rawValue = valueResult.FirstValue;

            // Deserialize the JSON
            var model = JsonConvert.DeserializeObject(rawValue, bindingContext.ModelType, JsonOptions.Value.SerializerSettings);

            // Now, bind each of the IFormFile properties from the other form parts
            foreach (var property in bindingContext.ModelMetadata.Properties)
            {
                if (property.ModelType != typeof(IFormFile))
                    continue;

                var fieldName = property.BinderModelName ?? property.PropertyName;
                var modelName = fieldName;
                var propertyModel = property.PropertyGetter(bindingContext.Model);
                ModelBindingResult propertyResult;
                using (bindingContext.EnterNestedScope(property, fieldName, modelName, propertyModel))
                {
                    await FormFileModelBinder.BindModelAsync(bindingContext);
                    propertyResult = bindingContext.Result;
                }

                if (propertyResult.IsModelSet)
                {
                    // The IFormFile was sucessfully bound, assign it to the corresponding property of the model
                    property.PropertySetter(model, propertyResult.Model);
                }
                else if (property.IsBindingRequired)
                {
                    var message = property.ModelBindingMessageProvider.MissingBindRequiredValueAccessor(fieldName);
                    bindingContext.ModelState.TryAddModelError(modelName, message);
                }
            }

            // Set the successfully constructed model as the result of the model binding
            bindingContext.Result = ModelBindingResult.Success(model);
        }
    }
}
