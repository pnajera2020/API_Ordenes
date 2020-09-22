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
    public class BsMetodoPago : IBsMetodoPago
    {
        private readonly ApiDBContext context = null;
        public BsMetodoPago(ApiDBContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Inserta un registro de MetodoPago en base de datos
        /// </summary>
        /// <param name="MetodoPago">Objeto de tipo MetodoPago con la información ingresada</param>
        /// <returns>Variable de tipo entero indicando el identificador de MetodoPago si se insertó correctamente</returns>
        public async Task<long> AgregaMetodoPagoJsonAsync(MetodoPago MetodoPago)
        {
            long resultado = 0;
            try
            {
                var itemMetodoPago = new CtMetodoPago
                {
                    Descripcion = MetodoPago.Descripcion,
                    Activo = MetodoPago.Activo
                };
                context.CtMetodoPago.Add(itemMetodoPago);
                await context.SaveChangesAsync();
                resultado = itemMetodoPago.PKIdMetodoPago;
            }
            catch (Exception e)
            {
                var message = $"Ocurrió un error al registrar la MetodoPago : {MetodoPago.Descripcion}";
                throw new IOException(message, e);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene todos los registros de MetodoPago activos
        /// </summary>
        /// <returns>Devuelve una lista de objetos de tipo MetodoPago</returns>
        public async Task<IList<MetodoPago>> ObtenerMetodoPagosAsync()
        {
            Task<List<MetodoPago>> listaMetodoPago;
            try
            {
                listaMetodoPago = context.CtMetodoPago.Select(x => new MetodoPago
                {
                    IdMetodoPago = x.PKIdMetodoPago,
                    Descripcion = x.Descripcion,
                    Activo = x.Activo
                }).OrderBy(x => x.IdMetodoPago).ToListAsync();
            }
            catch (Exception ex)
            {
                var message = $"Ocurrió un error al obtener las MetodoPagos.";
                throw new IOException(message, ex);
            }
            return await listaMetodoPago;
        }

        /// <summary>
        /// Obtiene MetodoPago por Id
        /// </summary>
        /// <param name="idMetodoPago">Identificador de la MetodoPago</param>
        /// <returns>Devuelve el objeto MetodoPago seleccionado por ID</returns>
        public async Task<MetodoPago> ObtenerMetodoPagoPorIdAsync(int idMetodoPago)
        {
            Task<MetodoPago> MetodoPago;
            try
            {
                //Consulta para obtener MetodoPago
                MetodoPago = context.CtMetodoPago
                    .Where(x => x.PKIdMetodoPago == idMetodoPago)
                    .Select(x => new MetodoPago
                    {
                        IdMetodoPago = x.PKIdMetodoPago,
                        Descripcion = x.Descripcion,
                        Activo = x.Activo
                    }).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                var message = $"Ocurrió un error al obtener la MetodoPago.";
                throw new IOException(message, ex);
            }
            return await MetodoPago;
        }

        /// <summary>
        /// Realiza la actualización de datos de un registro de MetodoPago
        /// </summary>
        /// <param name="MetodoPago">Objeto de tipo MetodoPago con la información actualizada</param>
        /// <returns>Regresa el identificador del registro actualizado en caso exitoso</returns>
        public async Task<long> EditarMetodoPagoJsonAsync(MetodoPago MetodoPago)
        {
            long resultado = 0;
            try
            {
                CtMetodoPago objMetodoPago = context.CtMetodoPago.Where(x => x.PKIdMetodoPago == MetodoPago.IdMetodoPago).FirstOrDefault();
                objMetodoPago.PKIdMetodoPago = MetodoPago.IdMetodoPago;
                objMetodoPago.Descripcion = MetodoPago.Descripcion;
                objMetodoPago.Activo = MetodoPago.Activo;

                await context.SaveChangesAsync();
                resultado = objMetodoPago.PKIdMetodoPago;
            }
            catch (Exception e)
            {
                var message = $"Ocurrió un error al actualizar al MetodoPago.";
                throw new IOException(message, e);
            }
            return resultado;
        }

        /// <summary>
        /// Realiza una baja lógica de MetodoPago
        /// <param name="idMetodoPago"/>Id de MetodoPago a eliminar
        /// </summary>
        /// <returns>Regresa un 1 en caso exitoso, 0 si ocurre algún error</returns>
        public async Task<int> EliminarMetodoPagoAsync(int idMetodoPago)
        {
            //Se inicializan variables
            int resultado = 0;

            try
            {
                CtMetodoPago objDelete = context.CtMetodoPago.Where(o => o.PKIdMetodoPago == idMetodoPago).FirstOrDefault();

                if (objDelete != null)
                {
                    objDelete.Activo = false;
                    await context.SaveChangesAsync();
                    resultado = 1;
                }
            }
            catch (Exception ex)
            {
                var message = $"Ocurrió un error al eliminar al MetodoPago.";
                throw new IOException(message, ex);
            }

            //Devuelve resultado
            return await Task.FromResult<int>(resultado);
        }
    }
}
