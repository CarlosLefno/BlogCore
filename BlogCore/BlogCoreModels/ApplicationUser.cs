using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage="El nombre es ogligatio")]
        public string  Nombre { get; set; }

        [Required(ErrorMessage = "La dirección es ogligatia")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "La ciudad es ogligatia")]
        public string Ciudad { get; set; }

        [Required(ErrorMessage = "El pais es ogligatio")]
        public string Pais { get; set; }

    }
}
