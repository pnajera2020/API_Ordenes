using Data;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Business
{
    public class BsEstatus : IBsEstatus
    {
        private readonly ApiDBContext context = null;
        public BsEstatus(ApiDBContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Inserta un registro de Estatus en base de datos
        /// </summary>
        /// <param name="Estatus">Objeto de tipo Estatus con la información ingresada</param>
        /// <returns>Variable de tipo entero indicando el identificador de Estatus si se insertó correctamente</returns>
        public async Task<long> AgregaEstatusJsonAsync(Estatus Estatus)
        {
            long resultado = 0;
            try
            {
                var itemEstatus = new CtEstatus
                {
                    Descripcion = Estatus.Descripcion,
                    Activo = Estatus.Activo
                };
                context.CtEstatus.Add(itemEstatus);
                await context.SaveChangesAsync();
                resultado = itemEstatus.PKIdEstatus;
            }
            catch (Exception e)
            {
                var message = $"Ocurrió un error al registrar la Estatus : {Estatus.Descripcion}";
                throw new IOException(message, e);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene todos los registros de Estatus activos
        /// </summary>
        /// <returns>Devuelve una lista de objetos de tipo Estatus</returns>
        public async Task<IList<Estatus>> ObtenerEstatussAsync()
        {
            Task<List<Estatus>> listaEstatus;
            try
            {
                listaEstatus = context.CtEstatus.Select(x => new Estatus
                {
                    IdEstatus = x.PKIdEstatus,
                    Descripcion = x.Descripcion,
                    Activo = x.Activo
                }).OrderBy(x => x.IdEstatus).ToListAsync();
            }
            catch (Exception ex)
            {
                var message = $"Ocurrió un error al obtener las Estatuss.";
                throw new IOException(message, ex);
            }
            return await listaEstatus;
        }

        /// <summary>
        /// Obtiene Estatus por Id
        /// </summary>
        /// <param name="idEstatus">Identificador de la Estatus</param>
        /// <returns>Devuelve el objeto Estatus seleccionado por ID</returns>
        public async Task<Estatus> ObtenerEstatusPorIdAsync(int idEstatus)
        {
            Task<Estatus> Estatus;
            try
            {
                //Consulta para obtener Estatus
                Estatus = context.CtEstatus
                    .Where(x => x.PKIdEstatus == idEstatus)
                    .Select(x => new Estatus
                    {
                        IdEstatus = x.PKIdEstatus,
                        Descripcion = x.Descripcion,
                        Activo = x.Activo
                    }).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                var message = $"Ocurrió un error al obtener la Estatus.";
                throw new IOException(message, ex);
            }
            return await Estatus;
        }

        /// <summary>
        /// Realiza la actualización de datos de un registro de Estatus
        /// </summary>
        /// <param name="Estatus">Objeto de tipo Estatus con la información actualizada</param>
        /// <returns>Regresa el identificador del registro actualizado en caso exitoso</returns>
        public async Task<long> EditarEstatusJsonAsync(Estatus Estatus)
        {
            long resultado = 0;
            try
            {
                CtEstatus objEstatus = context.CtEstatus.Where(x => x.PKIdEstatus == Estatus.IdEstatus).FirstOrDefault();
                objEstatus.PKIdEstatus = Estatus.IdEstatus;
                objEstatus.Descripcion = Estatus.Descripcion;
                objEstatus.Activo = Estatus.Activo;

                await context.SaveChangesAsync();
                resultado = objEstatus.PKIdEstatus;
            }
            catch (Exception e)
            {
                var message = $"Ocurrió un error al actualizar al Estatus.";
                throw new IOException(message, e);
            }
            return resultado;
        }

        /// <summary>
        /// Realiza una baja lógica de Estatus
        /// <param name="idEstatus"/>Id de Estatus a eliminar
        /// </summary>
        /// <returns>Regresa un 1 en caso exitoso, 0 si ocurre algún error</returns>
        public async Task<int> EliminarEstatusAsync(int idEstatus)
        {
            //Se inicializan variables
            int resultado = 0;

            try
            {
                CtEstatus objDelete = context.CtEstatus.Where(o => o.PKIdEstatus == idEstatus).FirstOrDefault();

                if (objDelete != null)
                {
                    objDelete.Activo = false;
                    await context.SaveChangesAsync();
                    resultado = 1;
                }
            }
            catch (Exception ex)
            {
                var message = $"Ocurrió un error al eliminar al Estatus.";
                throw new IOException(message, ex);
            }

            //Devuelve resultado
            return await Task.FromResult<int>(resultado);
        }
    }
}
