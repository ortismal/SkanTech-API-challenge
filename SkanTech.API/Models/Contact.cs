using System;
using System.ComponentModel.DataAnnotations;

namespace SkanTech.API.Models
{
    public class Contact
    {

        // Look at issue where GUID only sets 0-bits
        public Guid ContactId { get; set; }
        public string ContactFullName { get; set; }
        [Key]
        public string Email { get; set; }
        public string Company { get; set; }
        public DateTime CreatedDate { get; } = DateTime.UtcNow;
        public DateTime LastUpdatedDate { get; set; } = DateTime.UtcNow;
        public DateTime LastQueriedDirectDate { get; set; } = DateTime.UtcNow;
    }
}