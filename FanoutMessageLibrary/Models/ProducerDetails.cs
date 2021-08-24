using System;
using System.ComponentModel.DataAnnotations;

namespace FanoutMessageLibrary.Models
{
    public class ProducerDetails
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Display(Name = "Case Name")]
        [Required(ErrorMessage = "Case name is mandatory.")]
        [StringLength(500, ErrorMessage = "Case name must maximum length of 11.")]
        public string ProducerName { get; set; }

        [Display(Name = "Case Type")]
        [Required(ErrorMessage = "Case type is mandatory.")]
        public string ProducerMessage { get; set; }

        public ProducerDetails() { }
    }
}
