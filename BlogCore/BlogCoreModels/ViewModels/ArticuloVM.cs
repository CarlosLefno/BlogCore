
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Web.WebPages.Html;
//using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogCore.Models.ViewModels
{
    public class ArticuloVM
    {
        public Articulo Articulo { get; set; }

        public IEnumerable<SelectListItem> ListaCategorias { get; set; }
    }
}
