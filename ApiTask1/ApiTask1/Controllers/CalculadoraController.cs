using ApiTask1.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ApiTask1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalculadoraController: ControllerBase
    {
        private readonly ICalculadoraService _calculadora;

        public CalculadoraController(ICalculadoraService calculadora)
        {
            //Cremos las inyecciones de dependencias de nuestros servicios
            _calculadora = calculadora;
        }

        //EndPoit de Sumar
        [HttpGet("sumar")]
        public IActionResult Sumar(float num1, float num2)
        {
            try
            {
                var resultado = _calculadora.Sumar(num1, num2);
                return Ok(new { Resultado = resultado });
            }
            catch (Exception ex)
            {
                // En caso de excepción, devolver un error 500 con el mensaje de la excepción
                return StatusCode(500, new { Error = $"Error al realizar la suma: {ex.Message}" });
            }
        }

        //EndPoint de resta
        [HttpGet("resta")]
        public IActionResult Restar(float num1, float num2)
        {
            try
            {
                var resultado = _calculadora.Resta(num1, num2);
                return Ok(new { Resultado = resultado });
            }
            catch (Exception ex)
            {
                // En caso de excepción, devolver un error 500 con el mensaje de la excepción
                return StatusCode(500, new { Error = $"Error al realizar la resta: {ex.Message}" });
            }
        }
    }
}
