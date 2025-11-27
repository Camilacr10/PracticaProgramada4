using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiEstudiantes.DLL.Entidades;
using Microsoft.EntityFrameworkCore;

namespace ApiEstudiantes.DLL.Repositorio
{
    public class EstudiantesRepositorio : IEstudiantesRepositorio
    {
        private readonly ApiContext _context;

        public EstudiantesRepositorio(ApiContext context)
        {
            _context = context;
        }

        /*****COPY PASTE****/
        public async Task<bool> ActualizarEstudianteAsync(Estudiante estudiante)
        {
            var estudianteActualizar = _context.Estudiantes.FirstOrDefault(v => v.Id == estudiante.Id); // recupera el objeto

            estudianteActualizar.Nombre = estudiante.Nombre;   // actualiza la información
            estudianteActualizar.Apellido = estudiante.Apellido;
            estudianteActualizar.Edad = estudiante.Edad;

            var result = await _context.SaveChangesAsync();     // Aplica los cambios

            return result > 0; // check si funcionó
        }

        public async Task<bool> AgregarEstudianteAsync(Estudiante estudiante)
        {
            await _context.Estudiantes.AddAsync(estudiante);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> EliminarEstudianteAsync(int id)
        {
            var estudiante = _context.Estudiantes.FirstOrDefault(v => v.Id == id);

            _context.Estudiantes.Remove(estudiante);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<Estudiante> ObtenerEstudiantePorIdAsync(int id)
        {
            //SP //API // ETC
            var estudiante = _context.Estudiantes.FirstOrDefault(v => v.Id == id);
            return estudiante;
        }

        public async Task<List<Estudiante>> ObtenerEstudiantesAsync()
        {
            return await _context.Estudiantes.ToListAsync();
        }
    }
}