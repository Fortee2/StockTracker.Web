using System;
using System.ComponentModel.DataAnnotations;

namespace StockTracker.Core.Entities
{
    public class JobStatus
    {
        public JobStatus()
        {
            JobName = string.Empty;
            ActivityTime = DateTime.Now;
            ActivityDescription = string.Empty;
        }
        
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string JobName { get; set; }

        public DateTime ActivityTime { get; set; }

        [MaxLength(500)]
        public string ActivityDescription { get; set; }
    }
}

