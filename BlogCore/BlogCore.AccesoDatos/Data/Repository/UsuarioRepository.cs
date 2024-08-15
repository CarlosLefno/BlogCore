using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Data;
using BlogCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.AccesoDatos.Data.Repository
{
    public class UsuarioRepository : Repository<ApplicationUser>, IUsuarioRepository

    {
        private readonly ApplicationDbContext _db;

        public UsuarioRepository(ApplicationDbContext context) : base(context)
        {
            _db = context;
        }

        public void BloquearUsuario(string IdUsuario)
        {
            var UsuarioDesdeBd = _db.ApplicationUser.FirstOrDefault(u => u.Id == IdUsuario);
            if (UsuarioDesdeBd != null)
            {
                UsuarioDesdeBd.LockoutEnd = DateTime.Now.AddYears(1000);
                _db.SaveChanges();
            }           
        }

        public void DesbloquearUsuario(string IdUsuario)
        {
            var UsuarioDesdeBd = _db.ApplicationUser.FirstOrDefault(u => u.Id == IdUsuario);
            if (UsuarioDesdeBd != null)
            {
                UsuarioDesdeBd.LockoutEnd = DateTime.Now;
                _db.SaveChanges();
            }
        }
    }
}

