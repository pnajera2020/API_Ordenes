using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business
{
    public interface IBsOrdenProducto
    {
        /// <summary>
        /// Inserta un registro de OrdenProducto en base de datos
        /// </summary>
        /// <param name="OrdenProducto">Objeto de tipo OrdenProducto con la información ingresada</param>
        /// <returns>Variable de tipo entero indicando el identificador de OrdenProducto si se insertó correctamente</returns>
        Task<long> AgregaOrdenProductoJsonAsync(OrdenProducto OrdenProducto);

        /// <summary>
        /// Obtiene todos los registros de OrdenProducto 
        /// </summary>
        /// <returns>Devuelve una lista de objetos de tipo OrdenProducto</returns>
        Task<IList<OrdenProducto>> ObtenerOrdenProductosAsync();

        /// <summary>
        /// Obtiene OrdenProducto por Id
        /// </summary>
        /// <param name="idOrdenProducto">Identificador de la OrdenProducto</param>
        /// <returns>Devuelve el objeto OrdenProducto seleccionado por ID</returns>
        Task<OrdenProducto> ObtenerOrdenProductoPorIdAsync(int idOrdenProducto);

        /// <summary>
        /// Realiza la actualización de datos de un registro de OrdenProducto
        /// </summary>
        /// <param name="OrdenProducto">Objeto de tipo OrdenProducto con la información actualizada</param>
        /// <returns>Regresa el identificador del registro actualizado en caso exitoso</returns>
        Task<long> EditarOrdenProductoJsonAsync(OrdenProducto OrdenProducto);

        /// <summary>
        /// Realiza una baja lógica de OrdenProducto
        /// <param name="idOrdenProducto"/>Id de OrdenProducto a eliminar
        /// </summary>
        /// <returns>Regresa un 1 en caso exitoso, 0 si ocurre algún error</returns>
        Task<int> EliminarOrdenProductoAsync(int idOrdenProducto);
    }
}
