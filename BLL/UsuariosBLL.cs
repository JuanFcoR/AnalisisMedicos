using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using AnalisisMedicos.Entidades;
using AnalisisMedicos.DAL;
using System.Linq.Expressions;

namespace AnalisisMedicos.BLL
{
    public class UsuariosBLL
    {
        public static bool Guardar(Usuarios Usuario)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                if (contexto.Usuarios.Add(Usuario) != null)
                {
                    contexto.SaveChanges();
                    paso = true;
                }
                contexto.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return paso;
        }


        public static bool Editar(Usuarios Usuario)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                contexto.Entry(Usuario).State = EntityState.Modified;
                if (contexto.SaveChanges() > 0)
                {

                    paso = true;
                }
                contexto.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return paso;
        }

        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto contexto = new Contexto();


            try
            {
                Usuarios Usuario = contexto.Usuarios.Find(id);
                contexto.Usuarios.Remove(Usuario);
                if (contexto.SaveChanges() > 0)
                {
                    paso = true;
                }
                contexto.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return paso;
        }

        public static Usuarios Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Usuarios Usuario = new Usuarios();

            try
            {
                Usuario = contexto.Usuarios.Find(id);
                contexto.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return Usuario;
        }

        public static List<Usuarios> GetList(Expression<Func<Usuarios, bool>> expression)
        {
            List<Usuarios> Usuario = new List<Usuarios>();
            Contexto contexto = new Contexto();
            try
            {
                Usuario = contexto.Usuarios.Where(expression).ToList();
                contexto.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return Usuario;
        }
    }
}
