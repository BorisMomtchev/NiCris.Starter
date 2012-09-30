using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace NiCris.BusinessObjects.Validation
{
    /// <summary>
    /// Email Attribute class used for email validation. Used similar to the System.ComponentModel.DataAnnotations.RegularExpressionAttribute.
    /// </summary>
    public class EmailAttribute : RegularExpressionAttribute
    {
        #region Ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailAttribute"/> class.
        /// </summary>
        public EmailAttribute()
            : base(
                @"^([\w\!\#$\%\&\'\*\+\-\/\=\?\^\`{\|\}\~]+\.)*[\w\!\#$\%\&\'\*\+\-\/\=\?\^\`{\|\}\~]+@((((([a-zA-Z0-9]{1}[a-zA-Z0-9\-]{0,62}[a-zA-Z0-9]{1})|[a-zA-Z])\.)+[a-zA-Z]{2,6})|(\d{1,3}\.){3}\d{1,3}(\:\d{1,5})?)$"
                )
        {
        }

        #endregion Ctors
    }
}
