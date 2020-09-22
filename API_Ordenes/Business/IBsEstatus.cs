using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business
{
    public interface IBsEstatus
    {
        /// <summary>
        /// Inserta un registro de Estatus en base de datos
        /// </summary>
        /// <param name="Estatus">Objeto de tipo Estatus con la información ingresada</param>
        /// <returns>Variable de tipo entero indicando el identificador de Estatus si se insertó correctamente</returns>
        Task<long> AgregaEstatusJsonAsync(Estatus Estatus);

        /// <summary>
        /// Obtiene todos los registros de Estatus 
        /// </summary>
        /// <returns>Devuelve una lista de objetos de tipo Estatus</returns>
        Task<IList<Estatus>> ObtenerEstatussAsync();

        /// <summary>
        /// Obtiene Estatus por Id
        /// </summary>
        /// <param name="idEstatus">Identificador de la Estatus</param>
        /// <returns>Devuelve el objeto Estatus seleccionado por ID</returns>
        Task<Estatus> ObtenerEstatusPorIdAsync(int idEstatus);

        /// <summary>
        /// Realiza la actualización de datos de un registro de Estatus
        /// </summary>
        /// <param name="Estatus">Objeto de tipo Estatus con la información actualizada</param>
        /// <returns>Regresa el identificador del registro actualizado en caso exitoso</returns>
        Task<long> EditarEstatusJsonAsync(Estatus Estatus);

        /// <summary>
        /// Realiza una baja lógica de Estatus
        /// <param name="idEstatus"/>Id de Estatus a eliminar
        /// </summary>
        /// <returns>Regresa un 1 en caso exitoso, 0 si ocurre algún error</returns>
        Task<int> EliminarEstatusAsync(int idEstatus);
    }
}
