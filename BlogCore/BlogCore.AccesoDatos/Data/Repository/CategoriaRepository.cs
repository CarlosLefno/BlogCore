using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Data;
using BlogCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
//using System.Web.Mvc;

namespace BlogCore.AccesoDatos.Data.Repository
{
    internal class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        private readonly ApplicationDbContext _contexto;

        public CategoriaRepository(ApplicationDbContext db) : base(db)
        {
            _contexto = db;
        }

        public IEnumerable<SelectListItem> GetListaCategorias()
        {
            return _contexto.Categoria.Select(i => new SelectListItem()
            {
                Text = i.Nombre,
                Value = i.Id.ToString()
            });
        }

        public void Update(Categoria categoria)
        {
            var objDesdeDd = _contexto.Categoria.FirstOrDefault(s => s.Id == categoria.Id);
            objDesdeDd.Nombre = categoria.Nombre;
            objDesdeDd.Orden = categoria.Orden;
         //   _contexto.SaveChanges();

        }
    }
}
