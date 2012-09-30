/* Model Validation
 * 
 * Holds the Error Information associated with the validation.
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NiCris.Core.Validation
{
    public class ErrorInfo
    {
        public ErrorInfo(string propertyName, string errorMessage, object instance)
        {
            PropertyName = propertyName;
            ErrorMessage = errorMessage;
            Instance = instance;
        }

        public string PropertyName { get; set; }
        public string ErrorMessage { get; set; }
        public object Instance { get; set; }
    }
}
