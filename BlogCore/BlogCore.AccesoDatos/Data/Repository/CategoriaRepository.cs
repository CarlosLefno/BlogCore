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
    internal class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        private readonly ApplicationDbContext _contexto;

        public CategoriaRepository(ApplicationDbContext db) : base(db)
        {
            _contexto = db;
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
