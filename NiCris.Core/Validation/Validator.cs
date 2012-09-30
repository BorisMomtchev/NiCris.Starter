/* Model Validation
 * 
 * Provides the public access to the DataAnnotationsValidationRunner.
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NiCris.Core.Validation
{
    public static class Validator
    {
        public static IEnumerable<ErrorInfo> Check(object instance)
        {
            IEnumerable<ErrorInfo> errors = DataAnnotationsValidationRunner.GetErrors(instance);
            return errors;
        }
    }
}
