using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoEstudiantesBLL.Dtos;

namespace ProyectoEstudiantesBLL.Servicios
{
    public interface IEstudiantesServicio
    {
        Task<CustomResponse<EstudianteDto>> ObtenerEstudiantePorIdAsync(int id);
        Task<CustomResponse<List<EstudianteDto>>> ObtenerEstudiantesAsync();
        Task<CustomResponse<EstudianteDto>> AgregarEstudianteAsync(EstudianteDto estudianteDto);
        Task<CustomResponse<EstudianteDto>> ActualizarEstudianteAsync(EstudianteDto estudianteDto);
        Task<CustomResponse<EstudianteDto>> EliminarEstudianteAsync(int id);
    }
}