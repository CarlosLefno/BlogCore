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
    internal class ProductoRepository : Repository<Producto>, IProductoRepository
    {
        private readonly ApplicationDbContext _contexto;

        public ProductoRepository(ApplicationDbContext db) : base(db)
        {
            _contexto = db;
        }

        public void Update(Producto producto)
        {
            var objDesdeDd = _contexto.Producto.FirstOrDefault(s => s.ProductoId == producto.ProductoId);
            objDesdeDd.Nombre = producto.Nombre;
            objDesdeDd.Precio = producto.Precio;
            _contexto.SaveChanges();

        }
    }
}
