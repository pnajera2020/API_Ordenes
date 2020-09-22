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
    public class BsOrdenProducto : IBsOrdenProducto
    {
        private readonly ApiDBContext context = null;
        public BsOrdenProducto(ApiDBContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Inserta un registro de OrdenProducto en base de datos
        /// </summary>
        /// <param name="OrdenProducto">Objeto de tipo OrdenProducto con la información ingresada</param>
        /// <returns>Variable de tipo entero indicando el identificador de OrdenProducto si se insertó correctamente</returns>
        public async Task<long> AgregaOrdenProductoJsonAsync(OrdenProducto OrdenProducto)
        {
            long resultado = 0;
            try
            {
                var itemOrdenProducto = new TbOrdenProducto
                {
                    FKIdOrden = OrdenProducto.IdOrden,
                    FKIdProducto = OrdenProducto.IdProducto,
                    Cantidad = OrdenProducto.Cantidad,
                    Activo = OrdenProducto.Activo
                };
                context.TbOrdenProducto.Add(itemOrdenProducto);
                await context.SaveChangesAsync();
                resultado = itemOrdenProducto.PKIdOrdenProducto;
            }
            catch (Exception e)
            {
                var message = $"Ocurrió un error al registrar la OrdenProducto";
                throw new IOException(message, e);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene todos los registros de OrdenProducto activos
        /// </summary>
        /// <returns>Devuelve una lista de objetos de tipo OrdenProducto</returns>
        public async Task<IList<OrdenProducto>> ObtenerOrdenProductosAsync()
        {
            Task<List<OrdenProducto>> listaOrdenProducto;
            try
            {
                listaOrdenProducto = context.TbOrdenProducto.Select(x => new OrdenProducto
                {
                    IdOrdenProducto = x.PKIdOrdenProducto,
                    IdOrden = x.FKIdOrden,
                    IdProducto = x.FKIdProducto,
                    Cantidad = x.Cantidad,
                    Activo = x.Activo
                }).OrderBy(x => x.IdOrdenProducto).ToListAsync();
            }
            catch (Exception ex)
            {
                var message = $"Ocurrió un error al obtener las OrdenProductos.";
                throw new IOException(message, ex);
            }
            return await listaOrdenProducto;
        }

        /// <summary>
        /// Obtiene OrdenProducto por Id
        /// </summary>
        /// <param name="idOrdenProducto">Identificador de la OrdenProducto</param>
        /// <returns>Devuelve el objeto OrdenProducto seleccionado por ID</returns>
        public async Task<OrdenProducto> ObtenerOrdenProductoPorIdAsync(int idOrdenProducto)
        {
            Task<OrdenProducto> OrdenProducto;
            try
            {
                //Consulta para obtener OrdenProducto
                OrdenProducto = context.TbOrdenProducto
                    .Where(x => x.PKIdOrdenProducto == idOrdenProducto)
                    .Select(x => new OrdenProducto
                    {
                        IdOrdenProducto = x.PKIdOrdenProducto,
                        IdOrden = x.FKIdOrden,
                        IdProducto = x.FKIdProducto,
                        Cantidad = x.Cantidad,
                        Activo = x.Activo
                    }).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                var message = $"Ocurrió un error al obtener la OrdenProducto.";
                throw new IOException(message, ex);
            }
            return await OrdenProducto;
        }

        /// <summary>
        /// Realiza la actualización de datos de un registro de OrdenProducto
        /// </summary>
        /// <param name="OrdenProducto">Objeto de tipo OrdenProducto con la información actualizada</param>
        /// <returns>Regresa el identificador del registro actualizado en caso exitoso</returns>
        public async Task<long> EditarOrdenProductoJsonAsync(OrdenProducto OrdenProducto)
        {
            long resultado = 0;
            try
            {
                TbOrdenProducto objOrdenProducto = context.TbOrdenProducto.Where(x => x.PKIdOrdenProducto == OrdenProducto.IdOrdenProducto).FirstOrDefault();
                objOrdenProducto.PKIdOrdenProducto = OrdenProducto.IdOrdenProducto;
                objOrdenProducto.FKIdOrden = OrdenProducto.IdOrden;
                objOrdenProducto.FKIdProducto = OrdenProducto.IdProducto;
                objOrdenProducto.Cantidad = OrdenProducto.Cantidad;
                objOrdenProducto.Activo = OrdenProducto.Activo;

                await context.SaveChangesAsync();
                resultado = objOrdenProducto.PKIdOrdenProducto;
            }
            catch (Exception e)
            {
                var message = $"Ocurrió un error al actualizar al OrdenProducto.";
                throw new IOException(message, e);
            }
            return resultado;
        }

        /// <summary>
        /// Realiza una baja lógica de OrdenProducto
        /// <param name="idOrdenProducto"/>Id de OrdenProducto a eliminar
        /// </summary>
        /// <returns>Regresa un 1 en caso exitoso, 0 si ocurre algún error</returns>
        public async Task<int> EliminarOrdenProductoAsync(int idOrdenProducto)
        {
            //Se inicializan variables
            int resultado = 0;

            try
            {
                TbOrdenProducto objDelete = context.TbOrdenProducto.Where(o => o.PKIdOrdenProducto == idOrdenProducto).FirstOrDefault();

                if (objDelete != null)
                {
                    objDelete.Activo = false;
                    await context.SaveChangesAsync();
                    resultado = 1;
                }
            }
            catch (Exception ex)
            {
                var message = $"Ocurrió un error al eliminar al OrdenProducto.";
                throw new IOException(message, ex);
            }

            //Devuelve resultado
            return await Task.FromResult<int>(resultado);
        }
    }
}
