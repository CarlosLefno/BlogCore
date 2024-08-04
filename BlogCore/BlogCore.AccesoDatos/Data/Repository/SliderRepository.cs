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
    internal class SliderRepository : Repository<Slider>, ISliderRepository
    {
        private readonly ApplicationDbContext _contexto;

        public SliderRepository(ApplicationDbContext db) : base(db)
        {
            _contexto = db;
        }

        public void Update(Slider slider)
        {
            var objDesdeDd = _contexto.Slider.FirstOrDefault(s => s.Id == slider.Id);
            objDesdeDd.Nombre = slider.Nombre;
            objDesdeDd.Estado = slider.Estado;         
            objDesdeDd.UrlImagen = slider.UrlImagen;
         
        }
    }
}
