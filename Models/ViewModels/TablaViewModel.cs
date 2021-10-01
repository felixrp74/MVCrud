using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCrud.Models.ViewModels
{
    public class TablaViewModel
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength (50)]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Apellido")]
        public string Apellido{ get; set; }
        
        [Required]
        [DataType(DataType.Date)]        
        [Display(Name = "Fecha Nacimiento")]
        public DateTime FechaNacimiento { get; set; }
    }
}