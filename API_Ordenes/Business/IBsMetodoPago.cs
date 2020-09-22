using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business
{
    public interface IBsMetodoPago
    {
        /// <summary>
        /// Inserta un registro de MetodoPago en base de datos
        /// </summary>
        /// <param name="MetodoPago">Objeto de tipo MetodoPago con la información ingresada</param>
        /// <returns>Variable de tipo entero indicando el identificador de MetodoPago si se insertó correctamente</returns>
        Task<long> AgregaMetodoPagoJsonAsync(MetodoPago MetodoPago);

        /// <summary>
        /// Obtiene todos los registros de MetodoPago 
        /// </summary>
        /// <returns>Devuelve una lista de objetos de tipo MetodoPago</returns>
        Task<IList<MetodoPago>> ObtenerMetodoPagosAsync();

        /// <summary>
        /// Obtiene MetodoPago por Id
        /// </summary>
        /// <param name="idMetodoPago">Identificador de la MetodoPago</param>
        /// <returns>Devuelve el objeto MetodoPago seleccionado por ID</returns>
        Task<MetodoPago> ObtenerMetodoPagoPorIdAsync(int idMetodoPago);

        /// <summary>
        /// Realiza la actualización de datos de un registro de MetodoPago
        /// </summary>
        /// <param name="MetodoPago">Objeto de tipo MetodoPago con la información actualizada</param>
        /// <returns>Regresa el identificador del registro actualizado en caso exitoso</returns>
        Task<long> EditarMetodoPagoJsonAsync(MetodoPago MetodoPago);

        /// <summary>
        /// Realiza una baja lógica de MetodoPago
        /// <param name="idMetodoPago"/>Id de MetodoPago a eliminar
        /// </summary>
        /// <returns>Regresa un 1 en caso exitoso, 0 si ocurre algún error</returns>
        Task<int> EliminarMetodoPagoAsync(int idMetodoPago);
    }
}
