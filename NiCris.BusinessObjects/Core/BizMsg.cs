using System;
using System.ComponentModel.DataAnnotations;

namespace NiCris.BusinessObjects
{
    public class BizMsg
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        public DateTime Date { get; set; }              

        [Required(ErrorMessage = "User is required.")]
        public string User { get; set; }

        // Optional
        public string Description { get; set; }
        public string AppId { get; set; }
        public string ServiceId { get; set; }
        public string StyleId { get; set; }
        public string Roles { get; set; }

        /// <summary>
        /// Time Stamp
        /// </summary>
        public string RowVersion { get; set; }
    }
}
