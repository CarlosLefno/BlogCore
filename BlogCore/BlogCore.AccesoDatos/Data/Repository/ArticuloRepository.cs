using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Data;
using BlogCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.AccesoDatos.Data.Repository
{
    internal class ArticuloRepository : Repository<Articulo>, IArticuloRepository
    {
        private readonly ApplicationDbContext _contexto;

        public ArticuloRepository(ApplicationDbContext db) : base(db)
        {
            _contexto = db;
        }

        public void Update(Articulo articulo)
        {
            var objDesdeDd = _contexto.Articulo.FirstOrDefault(s => s.Id == articulo.Id);
            if (objDesdeDd != null)
            {
                objDesdeDd.Nombre = articulo.Nombre;
                objDesdeDd.Descripcion = articulo.Descripcion;
                objDesdeDd.FechaCreacion = articulo.FechaCreacion;
                objDesdeDd.UrlImagen = articulo.UrlImagen;
                objDesdeDd.CategoriaId = articulo.CategoriaId;
            }

        }
    }
}
