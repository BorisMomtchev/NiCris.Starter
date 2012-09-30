/* Model Validation
 * 
 * In .NET 3.5 SP1, the ASP.NET team introduced a new DLL named System.ComponentModel.DataAnnotations.
 * The purpose of this DLL is to provide UI-agnostic ways of annotating your data models with semantic attributes like [Required] and [Range].
 * DataAnnotations enables validation to be easily performed on both client- and server-side.
 * 
 * Many frameworks (MVC, Silverlight, etc) have model binders and are using DataAnnotations to provide validation.
 * This is an example of a custom model binder/know-how of how the DataAnnotations can be used otside of those frameworks.
 * 
 */
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace NiCris.Core.Validation
{
    internal static class DataAnnotationsValidationRunner
    {
        public static IEnumerable<ErrorInfo> GetErrors(object instance)
        {
            return from prop in TypeDescriptor.GetProperties(instance).Cast<PropertyDescriptor>()
                   from attribute in prop.Attributes.OfType<ValidationAttribute>()
                   where !attribute.IsValid(prop.GetValue(instance))
                   select new ErrorInfo(prop.Name, attribute.FormatErrorMessage(string.Empty), instance);
        }
    }
}
