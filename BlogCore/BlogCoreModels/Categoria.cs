using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.Models
{
    public class Categoria
    {
        [Key] 
        public int Id { get; set; }
        
        [Required(ErrorMessage ="Ingresa un nombre para la categoria")]
        [Display(Name ="Nombre de Categoria")]
        public string Nombre { get; set; }

        public int? Orden { get; set; }


    }
}
