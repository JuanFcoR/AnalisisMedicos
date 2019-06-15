using AnalisisMedicos.DAL;
using AnalisisMedicos.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AnalisisMedicos.BLL
{
    public class AnalisisBLL
    {
        public static bool Guardar(Analisis analisis)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                if (contexto.Analisis.Add(analisis) != null)
                    paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }

        //Este es el metodo para modificar en la base de datos
        public static bool Modificar(Analisis analisis)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                //buscar las entidades que no estan para removerlas
                var Anterior = contexto.Analisis.Find(analisis.AnalisisId);
                foreach (var item in Anterior.AnalisisDetalles)
                {
                    if (!analisis.AnalisisDetalles.Exists(d => d.ID == item.ID))
                        contexto.Entry(item).State = EntityState.Deleted;
                }

                contexto.Entry(analisis).State = EntityState.Modified;
                paso = (contexto.SaveChanges() > 0);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }

        //Este es el metodo para eliminar en la base de datos
        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                var eliminar = contexto.Analisis.Find(id);
                contexto.Entry(eliminar).State = System.Data.Entity.EntityState.Deleted;

                paso = (contexto.SaveChanges() > 0);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }

        //Este es el metodo para buscar en la base de datos
        public static Analisis Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Analisis analisis = new Analisis();
            try
            {
                analisis = contexto.Analisis.Find(id);
                // El Count() lo que hace es engañar al lazyloading y obligarlo a cargar los detalles 
                analisis.AnalisisDetalles.Count();

                //analisis = contexto.Analisis
                //     .Include(x => x.Telefonos.Select(c => c.analisisId))
                //             .Where(p => p.analisisId == id)
                //             .FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return analisis;
        }

        //Este es el metodo para listar o consultar lo que tenemos en la base de datos
        public static List<Analisis> GetList(Expression<Func<Analisis, bool>> analisis)
        {
            List<Analisis> Lista = new List<Analisis>();
            Contexto contexto = new Contexto();
            try
            {
                Lista = contexto.Analisis.Where(analisis).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return Lista;
        }
    }
}
