using ApiEstudiantes.BLL.Dtos;
using ApiEstudiantes.BLL.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace ApiEstudiantes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EstudiantesController : Controller
    {
        private readonly IEstudiantesServicio _estudiantesServicio;

        public EstudiantesController(IEstudiantesServicio estudiantesServicio)
        {
            _estudiantesServicio = estudiantesServicio;
        }

        [HttpGet(Name = "ObtenerEstudiantes")] // ObtenerTodos
        public async Task<IActionResult> ObtenerEstudiantes()
        {
            var respuesta = await _estudiantesServicio.ObtenerEstudiantesAsync();
            return Ok(respuesta);
        }

        [HttpGet("{id}", Name = "ObtenerEstudiantePorId")] // ObtenerPorId
        public async Task<IActionResult> ObtenerEstudiantePorId(int id)
        {
            var respuesta = await _estudiantesServicio.ObtenerEstudiantePorIdAsync(id);
            if (respuesta.Data is null)
            {
                return NotFound("Estudiante no encontrado");
            }
            return Ok(respuesta);
        }

        [HttpPost(Name = "CrearEstudiante")] // Crear
        public async Task<IActionResult> CrearEstudiante(EstudianteDto estudiante)
        {
            var respuesta = await _estudiantesServicio.AgregarEstudianteAsync(estudiante);
            if (respuesta.EsError)
            {
                return BadRequest(respuesta.Mensaje);
            }
            return Ok(respuesta);
        }

        [HttpPut(Name = "ActualizarEstudiante")] // Actualizar
        public async Task<IActionResult> ActualizarEstudiante(EstudianteDto estudiante)
        {
            var respuesta = await _estudiantesServicio.ActualizarEstudianteAsync(estudiante);
            return Ok(respuesta);
        }

        [HttpDelete("{id}", Name = "EliminarEstudiante")] // Eliminar
        public async Task<IActionResult> EliminarEstudiante(int id)
        {
            var respuesta = await _estudiantesServicio.EliminarEstudianteAsync(id);
            return Ok(respuesta);
        }
    }
}