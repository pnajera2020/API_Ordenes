using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business
{
    public interface IBsOrden
    {
        /// <summary>
        /// Inserta un registro de Orden en base de datos
        /// </summary>
        /// <param name="Orden">Objeto de tipo Orden con la información ingresada</param>
        /// <returns>Variable de tipo entero indicando el identificador de Orden si se insertó correctamente</returns>
        Task<long> AgregaOrdenJsonAsync(Orden Orden);

        /// <summary>
        /// Obtiene todos los registros de Orden activos
        /// </summary>
        /// <returns>Devuelve una lista de objetos de tipo Orden</returns>
        Task<IList<Orden>> ObtenerOrdensAsync();

        /// <summary>
        /// Obtiene Orden por Id
        /// </summary>
        /// <param name="idOrden">Identificador de la Orden</param>
        /// <returns>Devuelve el objeto Orden seleccionado por ID</returns>
        Task<Orden> ObtenerOrdenPorIdAsync(int idOrden);

        /// <summary>
        /// Obtiene Orden por SKU
        /// </summary>
        /// <param name="sku">SKU del Orden</param>
        /// <returns>Devuelve el objeto Orden seleccionado por SKU</returns>
        //Task<Orden> ObtenerOrdenPorSKUAsync(string sku);

        /// <summary>
        /// Realiza la actualización de datos de un registro de Orden
        /// </summary>
        /// <param name="Orden">Objeto de tipo Orden con la información actualizada</param>
        /// <returns>Regresa el identificador del registro actualizado en caso exitoso</returns>
        Task<long> EditarOrdenJsonAsync(Orden Orden);

        Task<long> CambiarEstatusOrdenJsonAsync(Orden Orden);

        /// <summary>
        /// Realiza una baja lógica de Orden
        /// <param name="idOrden"/>Id de Orden a eliminar
        /// </summary>
        /// <returns>Regresa un 1 en caso exitoso, 0 si ocurre algún error</returns>
        //Task<int> EliminarOrdenAsync(int idOrden);
    }
}
