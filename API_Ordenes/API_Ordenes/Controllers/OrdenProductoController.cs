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
    public class OrdenProductoController : ControllerBase
    {
        private readonly ILogger<OrdenProductoController> _logger = null;
        private readonly IBsOrdenProducto _bsOrdenProducto = null;

        public OrdenProductoController(ILogger<OrdenProductoController> logger, IBsOrdenProducto bsOrdenProducto)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _bsOrdenProducto = bsOrdenProducto ?? throw new ArgumentNullException(nameof(bsOrdenProducto));
        }

        /// <summary>
        /// Inserta un registro de OrdenProducto en base de datos
        /// </summary>
        /// <param name="OrdenProducto">Objeto de tipo OrdenProducto con la información ingresada</param>
        /// <returns>Mensaje "Registro exitoso" si la información se almacenó correctamente
        /// Mensaje "Intente nuevamente" en caso de algún error</returns>
        [HttpPost("agregarOrdenProducto")]
        public async Task<ActionResult> AgregarOrdenProducto([FromBody]OrdenProducto OrdenProducto)
        {
            ObjectResult result;
            long resultado;
            try
            {
                resultado = await _bsOrdenProducto.AgregaOrdenProductoJsonAsync(OrdenProducto);
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
        /// Obtiene todos los registros de OrdenProducto 
        /// </summary>
        /// <returns>Devuelve una lista de objetos de tipo OrdenProducto</returns>
        [HttpGet("obtenerOrdenProductos")]
        public async Task<ActionResult<IList<OrdenProducto>>> ObtenerOrdenProductoesAsync()
        {
            ObjectResult valRet;
            _logger.LogInformation("ObtenerOrdenProductosAsync: INICIO");

            IList<OrdenProducto> listaOrdenProductoes = null;
            try
            {
                listaOrdenProductoes = await _bsOrdenProducto.ObtenerOrdenProductosAsync();
                valRet = Ok(listaOrdenProductoes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                valRet = StatusCode(StatusCodes.Status500InternalServerError, null);
            }
            _logger.LogInformation("ObtenerOrdenProductosAsync: FIN");
            return valRet;
        }

        /// <summary>
        /// Obtiene OrdenProducto por Id
        /// </summary>
        /// <param name="idOrdenProducto">Identificador del OrdenProducto</param>
        /// <returns>Devuelve el objeto OrdenProducto seleccionado por ID</returns>
        [HttpGet("obtenerOrdenProductoPorId/{idOrdenProducto}")]
        public async Task<ActionResult<OrdenProducto>> ObtenerOrdenProductoPorIdAsync(int idOrdenProducto)
        {
            ObjectResult respuesta;
            _logger.LogInformation("ObtenerOrdenProductoPorId: INICIO");
            _logger.LogDebug("idOrdenProducto={idOrdenProducto}", idOrdenProducto);
            try
            {
                var resultado = await _bsOrdenProducto.ObtenerOrdenProductoPorIdAsync(idOrdenProducto);
                respuesta = Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                respuesta = StatusCode(StatusCodes.Status500InternalServerError, false);
            }
            _logger.LogInformation("ObtenerOrdenProductoPorId: FIN");
            return respuesta;
        }

        /// <summary>
        /// Realiza la actualización de datos de un registro de OrdenProducto
        /// </summary>
        /// <param name="OrdenProducto">Objeto de tipo OrdenProducto con la información actualizada</param>
        /// <returns>Mensaje "Registro exitoso" si la información se almacenó correctamente
        /// Mensaje "Intente nuevamente" en caso de algún error</returns>
        [HttpPut("editarOrdenProductoI/{idOrdenProducto}")]
        public async Task<ActionResult> EditarOrdenProductoI(int idOrdenProducto, [FromBody]OrdenProducto OrdenProducto)
        {
            ObjectResult result;
            long resultado;
            try
            {
                OrdenProducto.IdOrdenProducto = idOrdenProducto;
                resultado = await _bsOrdenProducto.EditarOrdenProductoJsonAsync(OrdenProducto);
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
        /// Realiza la actualización de datos de un registro de OrdenProducto
        /// </summary>
        /// <param name="OrdenProducto">Objeto de tipo OrdenProducto con la información actualizada</param>
        /// <returns>Mensaje "Registro exitoso" si la información se almacenó correctamente
        /// Mensaje "Intente nuevamente" en caso de algún error</returns>
        [HttpPut("editarOrdenProducto")]
        public async Task<ActionResult> EditarOrdenProducto([FromBody]OrdenProducto OrdenProducto)
        {
            ObjectResult result;
            long resultado;
            try
            {
                resultado = await _bsOrdenProducto.EditarOrdenProductoJsonAsync(OrdenProducto);
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
        /// Realiza una baja lógica del OrdenProducto
        /// </summary>
        /// <param name="idOrdenProducto"/>Id del OrdenProducto a eliminar
        /// <returns>Mensaje "Borrado exitoso" si la baja lógica se realizó correctamente
        /// Mensaje "Intente nuevamente" en caso de algún error</returns>
        [HttpGet("eliminarOrdenProducto/{idOrdenProducto}")]
        public async Task<ActionResult<bool>> EliminarOrdenProductoAsync(int idOrdenProducto)
        {
            ObjectResult respuesta;
            _logger.LogInformation("EliminarOrdenProducto: INICIO");
            _logger.LogDebug("idOrdenProducto={idOrdenProducto}", idOrdenProducto);
            try
            {
                var resultado = await _bsOrdenProducto.EliminarOrdenProductoAsync(idOrdenProducto);
                respuesta = Ok(resultado == 1 ? "Borrado exitoso" : "Intente nuevamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                respuesta = StatusCode(StatusCodes.Status500InternalServerError, false);
            }
            _logger.LogInformation("EliminarOrdenProducto: FIN");
            return respuesta;
        }
    }
}