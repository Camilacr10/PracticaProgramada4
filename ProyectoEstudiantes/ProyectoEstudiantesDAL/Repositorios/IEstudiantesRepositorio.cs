using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoEstudiantesDAL.Entidades;

namespace ProyectoEstudiantesDAL.Repositorios
{
    public interface IEstudiantesRepositorio
    {
        Task<List<Estudiante>> ObtenerEstudiantesAsync();
        Task<Estudiante> ObtenerEstudiantePorIdAsync(int id);
        Task<bool> AgregarEstudianteAsync(Estudiante estudiante);
        Task<bool> ActualizarEstudianteAsync(Estudiante estudiante);
        Task<bool> EliminarEstudianteAsync(int id);
    }
}
