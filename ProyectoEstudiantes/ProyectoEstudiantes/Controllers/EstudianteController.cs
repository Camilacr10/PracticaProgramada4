using Microsoft.AspNetCore.Mvc;
using ProyectoEstudiantesBLL.Dtos;
using ProyectoEstudiantesBLL.Servicios;

namespace ProyectoEstudiantes.Controllers
{
    public class EstudianteController : Controller
    {
        private readonly ILogger<EstudianteController> _logger;
        private readonly IEstudiantesServicio _estudiantesServicio;

        public EstudianteController(ILogger<EstudianteController> logger, IEstudiantesServicio estudiantesServicio)
        {
            _logger = logger;
            _estudiantesServicio = estudiantesServicio;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Listado de Estudiantes";
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerEstudiantes()
        {
            var respuesta = await _estudiantesServicio.ObtenerEstudiantesAsync();
            return Json(respuesta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearEstudiante(EstudianteDto estudianteDto)
        {
            if (!ModelState.IsValid)
            {
                return Json(new CustomResponse<EstudianteDto> { EsError = true, Mensaje = "Error de validación" });
            }

            var response = await _estudiantesServicio.AgregarEstudianteAsync(estudianteDto);
            return Json(response);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerEstudiantePorId(int id)
        {
            var respuesta = await _estudiantesServicio.ObtenerEstudiantePorIdAsync(id);
            return Json(respuesta);
        }

        [HttpPost]
        public async Task<IActionResult> EditarEstudiante(EstudianteDto estudianteDto)
        {
            if (!ModelState.IsValid)
            {
                return Json(new CustomResponse<EstudianteDto> { EsError = true, Mensaje = "Error de validación" });
            }

            var respuesta = await _estudiantesServicio.ActualizarEstudianteAsync(estudianteDto);
            return Json(respuesta);
        }

        //POST: Estudiante/Delete/5
        [HttpPost]
        public async Task<IActionResult> EliminarEstudiante(int id)
        {
            var respuesta = await _estudiantesServicio.EliminarEstudianteAsync(id);
            return Json(respuesta);
        }

        public IActionResult ConsultaPorId()
        {
            return View();
        }
    }
}