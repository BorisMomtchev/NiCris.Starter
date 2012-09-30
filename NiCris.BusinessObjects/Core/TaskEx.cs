using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace NiCris.BusinessObjects
{
    public class TaskEx
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }

        public string Resolver { get; set; }
        public DateTime? ResolvedDate { get; set; }

        public string Status { get; set; }
    }
}
