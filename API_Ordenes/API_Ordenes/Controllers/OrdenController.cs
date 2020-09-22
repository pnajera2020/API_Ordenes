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
    public class OrdenController : ControllerBase
    {
        private readonly ILogger<OrdenController> _logger = null;
        private readonly IBsOrden _bsOrden = null;

        public OrdenController(ILogger<OrdenController> logger, IBsOrden bsOrden)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _bsOrden = bsOrden ?? throw new ArgumentNullException(nameof(bsOrden));
        }

        /// <summary>
        /// Inserta un registro de Orden en base de datos
        /// </summary>
        /// <param name="Orden">Objeto de tipo Orden con la información ingresada</param>
        /// <returns>Mensaje "Registro exitoso" si la información se almacenó correctamente
        /// Mensaje "Intente nuevamente" en caso de algún error</returns>
        [HttpPost("agregarOrden")]
        public async Task<ActionResult> AgregarOrden([FromBody]Orden Orden)
        {
            ObjectResult result;
            long resultado;
            try
            {
                resultado = await _bsOrden.AgregaOrdenJsonAsync(Orden);
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
        /// Obtiene todos los registros de Orden 
        /// </summary>
        /// <returns>Devuelve una lista de objetos de tipo Orden</returns>
        [HttpGet("obtenerOrdens")]
        public async Task<ActionResult<IList<Orden>>> ObtenerOrdenesAsync()
        {
            ObjectResult valRet;
            _logger.LogInformation("ObtenerOrdensAsync: INICIO");

            IList<Orden> listaOrdenes = null;
            try
            {
                listaOrdenes = await _bsOrden.ObtenerOrdensAsync();
                valRet = Ok(listaOrdenes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                valRet = StatusCode(StatusCodes.Status500InternalServerError, null);
            }
            _logger.LogInformation("ObtenerOrdensAsync: FIN");
            return valRet;
        }

        /// <summary>
        /// Obtiene Orden por Id
        /// </summary>
        /// <param name="idOrden">Identificador del Orden</param>
        /// <returns>Devuelve el objeto Orden seleccionado por ID</returns>
        [HttpGet("obtenerOrdenPorId/{idOrden}")]
        public async Task<ActionResult<Orden>> ObtenerOrdenPorIdAsync(int idOrden)
        {
            ObjectResult respuesta;
            _logger.LogInformation("ObtenerOrdenPorId: INICIO");
            _logger.LogDebug("idOrden={idOrden}", idOrden);
            try
            {
                var resultado = await _bsOrden.ObtenerOrdenPorIdAsync(idOrden);
                respuesta = Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                respuesta = StatusCode(StatusCodes.Status500InternalServerError, false);
            }
            _logger.LogInformation("ObtenerOrdenPorId: FIN");
            return respuesta;
        }

        /// <summary>
        /// Obtiene Orden por SKU
        /// </summary>
        /// <param name="sku">SKU del Orden</param>
        /// <returns>Devuelve el objeto Orden seleccionado por SKU</returns>
        //[HttpGet("obtenerOrdenPorSKU/{sku}")]
        //public async Task<ActionResult<Orden>> ObtenerOrdenPorSKUAsync(string sku)
        //{
        //    ObjectResult respuesta;
        //    _logger.LogInformation("ObtenerOrdenPorSKU: INICIO");
        //    _logger.LogDebug("sku={sku}", sku);
        //    try
        //    {
        //        var resultado = await _bsOrden.ObtenerOrdenPorSKUAsync(sku);
        //        respuesta = Ok(resultado);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, ex.Message);
        //        respuesta = StatusCode(StatusCodes.Status500InternalServerError, false);
        //    }
        //    _logger.LogInformation("ObtenerOrdenPorSKU: FIN");
        //    return respuesta;
        //}

        /// <summary>
        /// Realiza la actualización de datos de un registro de Orden
        /// </summary>
        /// <param name="Orden">Objeto de tipo Orden con la información actualizada</param>
        /// <returns>Mensaje "Registro exitoso" si la información se almacenó correctamente
        /// Mensaje "Intente nuevamente" en caso de algún error</returns>
        [HttpPut("editarOrdenI/{idOrden}")]
        public async Task<ActionResult> EditarOrdenI(int idOrden, [FromBody]Orden Orden)
        {
            ObjectResult result;
            long resultado;
            try
            {
                Orden.IdOrden = idOrden;
                resultado = await _bsOrden.EditarOrdenJsonAsync(Orden);
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
        /// Realiza la actualización de datos de un registro de Orden
        /// </summary>
        /// <param name="Orden">Objeto de tipo Orden con la información actualizada</param>
        /// <returns>Mensaje "Registro exitoso" si la información se almacenó correctamente
        /// Mensaje "Intente nuevamente" en caso de algún error</returns>
        [HttpPut("editarOrden")]
        public async Task<ActionResult> EditarOrden([FromBody]Orden Orden)
        {
            ObjectResult result;
            long resultado;
            try
            {
                resultado = await _bsOrden.EditarOrdenJsonAsync(Orden);
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
        /// Realiza una baja lógica del Orden
        /// </summary>
        /// <param name="idOrden"/>Id del Orden a eliminar
        /// <returns>Mensaje "Borrado exitoso" si la baja lógica se realizó correctamente
        /// Mensaje "Intente nuevamente" en caso de algún error</returns>
        //[HttpGet("eliminarOrden/{idOrden}")]
        //public async Task<ActionResult<bool>> EliminarOrdenAsync(int idOrden)
        //{
        //    ObjectResult respuesta;
        //    _logger.LogInformation("EliminarOrden: INICIO");
        //    _logger.LogDebug("idOrden={idOrden}", idOrden);
        //    try
        //    {
        //        var resultado = await _bsOrden.EliminarOrdenAsync(idOrden);
        //        respuesta = Ok(resultado == 1 ? "Borrado exitoso" : "Intente nuevamente");
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, ex.Message);
        //        respuesta = StatusCode(StatusCodes.Status500InternalServerError, false);
        //    }
        //    _logger.LogInformation("EliminarOrden: FIN");
        //    return respuesta;
        //}
    }
}