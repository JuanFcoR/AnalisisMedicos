using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using AnalisisMedicos.DAL;
using AnalisisMedicos.Entidades;
using System.Linq.Expressions;

namespace AnalisisMedicos.BLL
{
    public class TiposAnalisisBLL
    {
        public static bool Guardar(TiposAnalisis TiposAnalisi)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                if (contexto.TiposAnalisis.Add(TiposAnalisi) != null)
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


        public static bool Editar(TiposAnalisis TiposAnalisi)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                contexto.Entry(TiposAnalisi).State = EntityState.Modified;
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
                TiposAnalisis TiposAnalisi = contexto.TiposAnalisis.Find(id);
                contexto.TiposAnalisis.Remove(TiposAnalisi);
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

        public static TiposAnalisis Buscar(int id)
        {
            Contexto contexto = new Contexto();
            TiposAnalisis TiposAnalisi = new TiposAnalisis();

            try
            {
                TiposAnalisi = contexto.TiposAnalisis.Find(id);
                contexto.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return TiposAnalisi;
        }

        public static List<TiposAnalisis> GetList(Expression<Func<TiposAnalisis, bool>> expression)
        {
            List<TiposAnalisis> TiposAnalisi = new List<TiposAnalisis>();
            Contexto contexto = new Contexto();
            try
            {
                TiposAnalisi = contexto.TiposAnalisis.Where(expression).ToList();
                contexto.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return TiposAnalisi;
        }
    }
}


        