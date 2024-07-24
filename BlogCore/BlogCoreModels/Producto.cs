using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.Models
{
    public class Producto
    {

        [Key]
        public int ProductoId { get; set; }

        [Required(ErrorMessage = "Ingresa un nombre para el producto")]
        [Display(Name = "Nombre de el Producto")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Ingresa precio para el producto")]
        public decimal Precio { get; set; }

    }
}
