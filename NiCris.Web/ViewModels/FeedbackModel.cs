using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using NiCris.BusinessObjects.Validation;

namespace NiCris.Web.ViewModels
{
    public class FeedbackModel
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(128)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [Email(ErrorMessage = "Please enter a valid Email Address.")]
        [StringLength(128)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Subject is required.")]
        [StringLength(256)]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Message is required.")]
        [StringLength(1024)]
        public string Message { get; set; }
    }
}