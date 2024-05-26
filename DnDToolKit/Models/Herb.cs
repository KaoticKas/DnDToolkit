using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DnDToolKit.Models
{
    public class Herb
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Herb Name")]
        public string? name { get; set; }

        public string? place { get; set; }
        public string? description { get; set; }

        [Column(TypeName ="Decimal(18, 2)")]
        public int Price { get; set; }

    }
}

