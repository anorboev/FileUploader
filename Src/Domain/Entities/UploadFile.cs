using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities
{
    public class UploadFile : BaseEntity
    {
        public UploadFile() : base()
        {
        }

        [Key]
        [Column("FileID")]
        public int FileId { get; set; }

        [Required]
        public string Extension { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string SystemName { get; set; }

        [Required]
        [Range(0, 5)]
        public double Size { get; set; }
    }
}
