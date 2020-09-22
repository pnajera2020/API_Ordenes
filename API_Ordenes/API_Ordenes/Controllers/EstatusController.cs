using Business;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API_Ordenes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstatusController : ControllerBase
    {
        private readonly ILogger<EstatusController> _logger = null;
        private readonly IBsEstatus _bsEstatus = null;

        public EstatusController(ILogger<EstatusController> logger, IBsEstatus bsEstatus)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _bsEstatus = bsEstatus ?? throw new ArgumentNullException(nameof(bsEstatus));
        }

        /// <summary>
        /// Inserta un registro de Estatus en base de datos
        /// </summary>
        /// <param name="Estatus">Objeto de tipo Estatus con la información ingresada</param>
        /// <returns>Mensaje "Registro exitoso" si la información se almacenó correctamente
        /// Mensaje "Intente nuevamente" en caso de algún error</returns>
        [HttpPost("agregarEstatus")]
        public async Task<ActionResult> AgregarEstatus([FromBody]Estatus Estatus)
        {
            ObjectResult result;
            long resultado;
            try
            {
                resultado = await _bsEstatus.AgregaEstatusJsonAsync(Estatus);
                result = Ok(resultado >= 1 ? "Registro exitoso " : "Intente nuevamente");
            }
            catch (Exception ex)
            {
                resultado = 0;
                result = StatusCode(StatusCodes.Status500InternalServerError, resultado);
                _logger.LogInformation($"Ha ocurrido un error: {ex.Message.ToString()}");
            }
            return result;
        }

        /// <summary>
        /// Obtiene todos los registros de Estatus activos
        /// </summary>
        /// <returns>Devuelve una lista de objetos de tipo Estatus</returns>
        [HttpGet("obtenerEstatuss")]
        public async Task<ActionResult<IList<Estatus>>> ObtenerEstatussAsync()
        {
            ObjectResult valRet;
            _logger.LogInformation("ObtenerEstatussAsync: INICIO");

            IList<Estatus> listaEstatuses = null;
            try
            {
                listaEstatuses = await _bsEstatus.ObtenerEstatussAsync();
                valRet = Ok(listaEstatuses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                valRet = StatusCode(StatusCodes.Status500InternalServerError, null);
            }
            _logger.LogInformation("ObtenerEstatussAsync: FIN");
            return valRet;
        }

        /// <summary>
        /// Obtiene Estatus por Id
        /// </summary>
        /// <param name="idEstatus">Identificador del Estatus</param>
        /// <returns>Devuelve el objeto Estatus seleccionado por ID</returns>
        [HttpGet("obtenerEstatusPorId/{idEstatus}")]
        public async Task<ActionResult<Estatus>> ObtenerEstatusPorIdAsync(int idEstatus)
        {
            ObjectResult respuesta;
            _logger.LogInformation("ObtenerEstatusPorId: INICIO");
            _logger.LogDebug("idEstatus={idEstatus}", idEstatus);
            try
            {
                var resultado = await _bsEstatus.ObtenerEstatusPorIdAsync(idEstatus);
                respuesta = Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                respuesta = StatusCode(StatusCodes.Status500InternalServerError, false);
            }
            _logger.LogInformation("ObtenerEstatusPorId: FIN");
            return respuesta;
        }

        /// <summary>
        /// Realiza la actualización de datos de un registro de Estatus
        /// </summary>
        /// <param name="Estatus">Objeto de tipo Estatus con la información actualizada</param>
        /// <returns>Mensaje "Registro exitoso" si la información se almacenó correctamente
        /// Mensaje "Intente nuevamente" en caso de algún error</returns>
        [HttpPut("editarEstatusI/{idEstatus}")]
        public async Task<ActionResult> EditarEstatusI(int idEstatus, [FromBody]Estatus Estatus)
        {
            ObjectResult result;
            long resultado;
            try
            {
                Estatus.IdEstatus = idEstatus;
                resultado = await _bsEstatus.EditarEstatusJsonAsync(Estatus);
                result = Ok(resultado >= 1 ? "Registro exitoso " : "Intente nuevamente");
            }
            catch (Exception ex)
            {
                resultado = 0;
                result = StatusCode(StatusCodes.Status500InternalServerError, resultado);
                _logger.LogInformation($"Ha ocurrido un error: {ex.Message.ToString()}");
            }
            return result;
        }



        /// <summary>
        /// Realiza la actualización de datos de un registro de Estatus
        /// </summary>
        /// <param name="Estatus">Objeto de tipo Estatus con la información actualizada</param>
        /// <returns>Mensaje "Registro exitoso" si la información se almacenó correctamente
        /// Mensaje "Intente nuevamente" en caso de algún error</returns>
        [HttpPut("editarEstatus")]
        public async Task<ActionResult> EditarEstatus([FromBody]Estatus Estatus)
        {
            ObjectResult result;
            long resultado;
            try
            {
                resultado = await _bsEstatus.EditarEstatusJsonAsync(Estatus);
                result = Ok(resultado >= 1 ? "Registro exitoso " : "Intente nuevamente");
            }
            catch (Exception ex)
            {
                resultado = 0;
                result = StatusCode(StatusCodes.Status500InternalServerError, resultado);
                _logger.LogInformation($"Ha ocurrido un error: {ex.Message.ToString()}");
            }
            return result;
        }

        /// <summary>
        /// Realiza una baja lógica del Estatus
        /// </summary>
        /// <param name="idEstatus"/>Id del Estatus a eliminar
        /// <returns>Mensaje "Borrado exitoso" si la baja lógica se realizó correctamente
        /// Mensaje "Intente nuevamente" en caso de algún error</returns>
        [HttpGet("eliminarEstatus/{idEstatus}")]
        public async Task<ActionResult<bool>> EliminarEstatusAsync(int idEstatus)
        {
            ObjectResult respuesta;
            _logger.LogInformation("EliminarEstatus: INICIO");
            _logger.LogDebug("idEstatus={idEstatus}", idEstatus);
            try
            {
                var resultado = await _bsEstatus.EliminarEstatusAsync(idEstatus);
                respuesta = Ok(resultado == 1 ? "Borrado exitoso" : "Intente nuevamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                respuesta = StatusCode(StatusCodes.Status500InternalServerError, false);
            }
            _logger.LogInformation("EliminarEstatus: FIN");
            return respuesta;
        }
    }
}