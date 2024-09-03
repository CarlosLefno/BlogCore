using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrador")]
    [Area("Admin")]
    public class ArticulosController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ArticulosController(IContenedorTrabajo contenedorTrabajo, IWebHostEnvironment hostingEnvironment)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult Create()
        {
            ArticuloVM ArtiVM = new ArticuloVM()
            {
                Articulo = new Models.Articulo(),
                ListaCategorias = _contenedorTrabajo.Categoria.GetListaCategorias()
            };

            return View(ArtiVM);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            ArticuloVM ArtiVM = new ArticuloVM()
            {
                Articulo = new Models.Articulo(),
                ListaCategorias = _contenedorTrabajo.Categoria.GetListaCategorias()
            };

            if (id is not null) 
            { 
                ArtiVM.Articulo = _contenedorTrabajo.Articulo.Get(id.GetValueOrDefault());

            }
            return View(ArtiVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ArticuloVM ArtiVM)
        {
            if (ModelState.IsValid)
            {
                string rutaPrincipal = _hostingEnvironment.WebRootPath; ;
                var archivos = HttpContext.Request.Form.Files;
                if (ArtiVM.Articulo.Id == 0 && archivos.Count() > 0)
                {
                    //Nuevo Articulo
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\articulos");
                    var extension = Path.GetExtension(archivos[0].FileName);

                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }

                    ArtiVM.Articulo.UrlImagen = @"\imagenes\articulos\" + nombreArchivo + extension;
                    ArtiVM.Articulo.FechaCreacion = DateTime.Now.ToString();

                    _contenedorTrabajo.Articulo.Add(ArtiVM.Articulo);
                    _contenedorTrabajo.Save();

                    return RedirectToAction(nameof(Index));
                }
                else {
                    ModelState.AddModelError("Imagen", "Debes seleccionar una imagen");
                }
            }
            ArtiVM.ListaCategorias =_contenedorTrabajo.Categoria.GetListaCategorias();

            return View(ArtiVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ArticuloVM ArtiVM)
        {
            if (ModelState.IsValid)
            {
                string rutaPrincipal = _hostingEnvironment.WebRootPath; ;
                var archivos = HttpContext.Request.Form.Files;
                var articuloDesdeBd = _contenedorTrabajo.Articulo.Get(ArtiVM.Articulo.Id);

                if (archivos.Count() > 0)
                {
                    //Nuevo Articulo
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\articulos");
                    var extension = Path.GetExtension(archivos[0].FileName);
                    var nuevaExtension = Path.GetExtension(archivos[0].FileName);
                    var rutaImagen = Path.Combine(rutaPrincipal, articuloDesdeBd.UrlImagen.TrimStart('\\'));

                    if (System.IO.File.Exists(rutaImagen))
                    { 
                        System.IO.File.Delete(rutaImagen);
                    }

                    //Nuevamente subimos el archivo
                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }

                    ArtiVM.Articulo.UrlImagen = @"\imagenes\articulos\" + nombreArchivo + extension;
                    ArtiVM.Articulo.FechaCreacion = DateTime.Now.ToString();

                    _contenedorTrabajo.Articulo.Update(ArtiVM.Articulo);
                    _contenedorTrabajo.Save();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    //Aqui seria cuando seria cuando la imagen existe y aw conserva 
                    ArtiVM.Articulo.UrlImagen = articuloDesdeBd.UrlImagen;
                    _contenedorTrabajo.Articulo.Update(ArtiVM.Articulo);
                    _contenedorTrabajo.Save();

                    return RedirectToAction(nameof(Index));
                }
            }
            ArtiVM.ListaCategorias = _contenedorTrabajo.Categoria.GetListaCategorias();

            return View(ArtiVM);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        #region  Llamada a la API

        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _contenedorTrabajo.Articulo.GetAll(includeProperties: "Categoria") });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {            
            var articuloDesdeBd = _contenedorTrabajo.Articulo.Get(id);
            string rutaDirectorioPrincipal = _hostingEnvironment.WebRootPath;
            var rutaImagen = Path.Combine(rutaDirectorioPrincipal,articuloDesdeBd.UrlImagen.TrimStart('\\'));

            if (System.IO.File.Exists(rutaImagen)) 
            {
                System.IO.File.Delete(rutaImagen);
            }

            if (articuloDesdeBd is null)
                return Json(new { success = false, message = "Error borrando articulo" });
            _contenedorTrabajo.Articulo.Remove(articuloDesdeBd);
            _contenedorTrabajo.Save();

            return Json(new { success = true, message = "Articulo Borrado Correctamente" });
        }

        #endregion
    }

}
