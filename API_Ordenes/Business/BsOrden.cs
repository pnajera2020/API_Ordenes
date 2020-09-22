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
    public class BsOrden : IBsOrden
    {
        private readonly ApiDBContext context = null;
        public BsOrden(ApiDBContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Inserta un registro de Orden en base de datos
        /// </summary>
        /// <param name="Orden">Objeto de tipo Orden con la información ingresada</param>
        /// <returns>Variable de tipo entero indicando el identificador de Orden si se insertó correctamente</returns>
        public async Task<long> AgregaOrdenJsonAsync(Orden Orden)
        {
            long resultado = 0;
            try
            {
                var itemOrden = new TbOrden
                {
                    Fecha = Orden.Fecha,
                    Total = Orden.Total,
                    FKIdMetodoPago = Orden.IdMetodoPago,
                    FKIdEstatus = Orden.IdEstatus,
                    FKIdUsuario = Orden.IdUsuario
                };
                context.TbOrden.Add(itemOrden);
                await context.SaveChangesAsync();
                resultado = itemOrden.PKIdOrden;
            }
            catch (Exception e)
            {
                var message = $"Ocurrió un error al registrar la Orden";
                throw new IOException(message, e);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene todos los registros de Orden 
        /// </summary>
        /// <returns>Devuelve una lista de objetos de tipo Orden</returns>
        public async Task<IList<Orden>> ObtenerOrdensAsync()
        {
            Task<List<Orden>> listaOrden;
            try
            {
                //listaOrden = context.TbOrden.Where(x => x.Activo).Select(x => new Orden
                listaOrden = context.TbOrden.Select(x => new Orden
                {
                    IdOrden = x.PKIdOrden,
                    Fecha = x.Fecha,
                    Total = x.Total,
                    IdMetodoPago = x.FKIdMetodoPago,
                    IdEstatus = x.FKIdEstatus,
                    IdUsuario = x.FKIdUsuario
                }).OrderBy(x => x.IdOrden).ToListAsync();
            }
            catch (Exception ex)
            {
                var message = $"Ocurrió un error al obtener las Ordenes.";
                throw new IOException(message, ex);
            }
            return await listaOrden;
        }

        /// <summary>
        /// Obtiene Orden por Id
        /// </summary>
        /// <param name="idOrden">Identificador de la Orden</param>
        /// <returns>Devuelve el objeto Orden seleccionado por ID</returns>
        public async Task<Orden> ObtenerOrdenPorIdAsync(int idOrden)
        {
            Task<Orden> Orden;
            try
            {
                //Consulta para obtener Orden
                Orden = context.TbOrden
                    .Where(x => x.PKIdOrden == idOrden)
                    .Select(x => new Orden
                    {
                        IdOrden = x.PKIdOrden,
                        Fecha = x.Fecha,
                        Total = x.Total,
                        IdMetodoPago = x.FKIdMetodoPago,
                        IdEstatus = x.FKIdEstatus,
                        IdUsuario = x.FKIdUsuario
                    }).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                var message = $"Ocurrió un error al obtener la Orden.";
                throw new IOException(message, ex);
            }
            return await Orden;
        }

        /// <summary>
        /// Obtiene Orden por clave
        /// </summary>
        /// <param name="sku">SKU del Orden</param>
        /// <returns>Devuelve el objeto Orden seleccionado por SKU</returns>
        //public async Task<Orden> ObtenerOrdenPorSKUAsync(string sku)
        //{
        //    Task<Orden> Orden;
        //    try
        //    {
        //        //Consulta para obtener Orden
        //        Orden = context.TbOrden
        //             .Where(x => x.SKU == sku)
        //             .Select(x => new Orden
        //             {
        //                 IdOrden = x.PKIdOrden,
        //                 Fecha = x.Fecha,
        //                 Total = x.Total,
        //                 IdMetodoPago = x.FKIdMetodoPago,
        //                 IdEstatus = x.FKIdEstatus,
        //                 IdUsuario = x.FKIdUsuario
        //             }).FirstOrDefaultAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        var message = $"Ocurrió un error al obtener la Orden.";
        //        throw new IOException(message, ex);
        //    }
        //    return await Orden;
        //}

        /// <summary>
        /// Realiza la actualización de datos de un registro de Orden
        /// </summary>
        /// <param name="Orden">Objeto de tipo Orden con la información actualizada</param>
        /// <returns>Regresa el identificador del registro actualizado en caso exitoso</returns>
        public async Task<long> EditarOrdenJsonAsync(Orden Orden)
        {
            long resultado = 0;
            try
            {
                TbOrden objOrden = context.TbOrden.Where(x => x.PKIdOrden == Orden.IdOrden).FirstOrDefault();
                objOrden.Fecha = Orden.Fecha;
                objOrden.Total = Orden.Total;
                objOrden.FKIdMetodoPago = Orden.IdMetodoPago;
                objOrden.FKIdEstatus = Orden.IdEstatus;
                objOrden.FKIdUsuario = Orden.IdUsuario;

                await context.SaveChangesAsync();
                resultado = objOrden.PKIdOrden;
            }
            catch (Exception e)
            {
                var message = $"Ocurrió un error al actualizar al Orden.";
                throw new IOException(message, e);
            }
            return resultado;
        }

        public async Task<long> CambiarEstatusOrdenJsonAsync(Orden Orden)
        {
            long resultado = 0;
            try
            {
                TbOrden objOrden = context.TbOrden.Where(x => x.PKIdOrden == Orden.IdOrden).FirstOrDefault();
                //objOrden.Fecha = Orden.Fecha;
                //objOrden.Total = Orden.Total;
                //objOrden.FKIdMetodoPago = Orden.IdMetodoPago;
                objOrden.FKIdEstatus = Orden.IdEstatus;
                //objOrden.FKIdUsuario = Orden.IdUsuario;

                await context.SaveChangesAsync();
                resultado = objOrden.PKIdOrden;
            }
            catch (Exception e)
            {
                var message = $"Ocurrió un error al actualizar al Orden.";
                throw new IOException(message, e);
            }
            return resultado;
        }

        /// <summary>
        /// Realiza una baja lógica de Orden
        /// <param name="idOrden"/>Id de Orden a eliminar
        /// </summary>
        /// <returns>Regresa un 1 en caso exitoso, 0 si ocurre algún error</returns>
        //public async Task<int> EliminarOrdenAsync(int idOrden)
        //{
        //    //Se inicializan variables
        //    int resultado = 0;

        //    try
        //    {
        //        TbOrden objDelete = context.TbOrden.Where(o => o.PKIdOrden == idOrden).FirstOrDefault();

        //        if (objDelete != null)
        //        {
        //            objDelete.Activo = false;
        //            await context.SaveChangesAsync();
        //            resultado = 1;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var message = $"Ocurrió un error al eliminar al Orden.";
        //        throw new IOException(message, ex);
        //    }

        //    //Devuelve resultado
        //    return await Task.FromResult<int>(resultado);
        //}
    }
}
