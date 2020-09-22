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
    public class MetodoPagoController : ControllerBase
    {
        private readonly ILogger<MetodoPagoController> _logger = null;
        private readonly IBsMetodoPago _bsMetodoPago = null;

        public MetodoPagoController(ILogger<MetodoPagoController> logger, IBsMetodoPago bsMetodoPago)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _bsMetodoPago = bsMetodoPago ?? throw new ArgumentNullException(nameof(bsMetodoPago));
        }

        /// <summary>
        /// Inserta un registro de MetodoPago en base de datos
        /// </summary>
        /// <param name="MetodoPago">Objeto de tipo MetodoPago con la información ingresada</param>
        /// <returns>Mensaje "Registro exitoso" si la información se almacenó correctamente
        /// Mensaje "Intente nuevamente" en caso de algún error</returns>
        [HttpPost("agregarMetodoPago")]
        public async Task<ActionResult> AgregarMetodoPago([FromBody]MetodoPago MetodoPago)
        {
            ObjectResult result;
            long resultado;
            try
            {
                resultado = await _bsMetodoPago.AgregaMetodoPagoJsonAsync(MetodoPago);
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
        /// Obtiene todos los registros de MetodoPago activos
        /// </summary>
        /// <returns>Devuelve una lista de objetos de tipo MetodoPago</returns>
        [HttpGet("obtenerMetodoPagos")]
        public async Task<ActionResult<IList<MetodoPago>>> ObtenerMetodoPagosAsync()
        {
            ObjectResult valRet;
            _logger.LogInformation("ObtenerMetodoPagosAsync: INICIO");

            IList<MetodoPago> listaMetodoPagoes = null;
            try
            {
                listaMetodoPagoes = await _bsMetodoPago.ObtenerMetodoPagosAsync();
                valRet = Ok(listaMetodoPagoes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                valRet = StatusCode(StatusCodes.Status500InternalServerError, null);
            }
            _logger.LogInformation("ObtenerMetodoPagosAsync: FIN");
            return valRet;
        }

        /// <summary>
        /// Obtiene MetodoPago por Id
        /// </summary>
        /// <param name="idMetodoPago">Identificador del MetodoPago</param>
        /// <returns>Devuelve el objeto MetodoPago seleccionado por ID</returns>
        [HttpGet("obtenerMetodoPagoPorId/{idMetodoPago}")]
        public async Task<ActionResult<MetodoPago>> ObtenerMetodoPagoPorIdAsync(int idMetodoPago)
        {
            ObjectResult respuesta;
            _logger.LogInformation("ObtenerMetodoPagoPorId: INICIO");
            _logger.LogDebug("idMetodoPago={idMetodoPago}", idMetodoPago);
            try
            {
                var resultado = await _bsMetodoPago.ObtenerMetodoPagoPorIdAsync(idMetodoPago);
                respuesta = Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                respuesta = StatusCode(StatusCodes.Status500InternalServerError, false);
            }
            _logger.LogInformation("ObtenerMetodoPagoPorId: FIN");
            return respuesta;
        }

        /// <summary>
        /// Realiza la actualización de datos de un registro de MetodoPago
        /// </summary>
        /// <param name="MetodoPago">Objeto de tipo MetodoPago con la información actualizada</param>
        /// <returns>Mensaje "Registro exitoso" si la información se almacenó correctamente
        /// Mensaje "Intente nuevamente" en caso de algún error</returns>
        [HttpPut("editarMetodoPagoI/{idMetodoPago}")]
        public async Task<ActionResult> EditarMetodoPagoI(int idMetodoPago, [FromBody]MetodoPago MetodoPago)
        {
            ObjectResult result;
            long resultado;
            try
            {
                MetodoPago.IdMetodoPago = idMetodoPago;
                resultado = await _bsMetodoPago.EditarMetodoPagoJsonAsync(MetodoPago);
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
        /// Realiza la actualización de datos de un registro de MetodoPago
        /// </summary>
        /// <param name="MetodoPago">Objeto de tipo MetodoPago con la información actualizada</param>
        /// <returns>Mensaje "Registro exitoso" si la información se almacenó correctamente
        /// Mensaje "Intente nuevamente" en caso de algún error</returns>
        [HttpPut("editarMetodoPago")]
        public async Task<ActionResult> EditarMetodoPago([FromBody]MetodoPago MetodoPago)
        {
            ObjectResult result;
            long resultado;
            try
            {
                resultado = await _bsMetodoPago.EditarMetodoPagoJsonAsync(MetodoPago);
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
        /// Realiza una baja lógica del MetodoPago
        /// </summary>
        /// <param name="idMetodoPago"/>Id del MetodoPago a eliminar
        /// <returns>Mensaje "Borrado exitoso" si la baja lógica se realizó correctamente
        /// Mensaje "Intente nuevamente" en caso de algún error</returns>
        [HttpGet("eliminarMetodoPago/{idMetodoPago}")]
        public async Task<ActionResult<bool>> EliminarMetodoPagoAsync(int idMetodoPago)
        {
            ObjectResult respuesta;
            _logger.LogInformation("EliminarMetodoPago: INICIO");
            _logger.LogDebug("idMetodoPago={idMetodoPago}", idMetodoPago);
            try
            {
                var resultado = await _bsMetodoPago.EliminarMetodoPagoAsync(idMetodoPago);
                respuesta = Ok(resultado == 1 ? "Borrado exitoso" : "Intente nuevamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                respuesta = StatusCode(StatusCodes.Status500InternalServerError, false);
            }
            _logger.LogInformation("EliminarMetodoPago: FIN");
            return respuesta;
        }
    }
}