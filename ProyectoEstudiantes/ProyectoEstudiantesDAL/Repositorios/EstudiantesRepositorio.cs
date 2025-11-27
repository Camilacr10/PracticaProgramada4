using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using ProyectoEstudiantesDAL.Entidades;
using ProyectoEstudiantesDAL.RespuestasAPIS;

namespace ProyectoEstudiantesDAL.Repositorios
{
    public class EstudiantesRepositorio : IEstudiantesRepositorio
    {

        private List<Estudiante> estudiantes = new List<Estudiante>()
        {
            new Estudiante { Id = 1, Nombre = "Ana", Apellido = "López", Edad = 20 },
            new Estudiante { Id = 2, Nombre = "Carlos", Apellido = "Ramírez", Edad = 22 }
        };

        private readonly HttpClient _httpClient;
        public EstudiantesRepositorio(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> ActualizarEstudianteAsync(Estudiante estudiante)
        {

            var informacion = new StringContent(
                   System.Text.Json.JsonSerializer.Serialize(estudiante),
                   Encoding.UTF8,
                   "application/json"
               );

            var response = await _httpClient.PutAsync("https://localhost:7265/Estudiantes", informacion);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> AgregarEstudianteAsync(Estudiante estudiante)
        {
            var informacion = new StringContent(
                System.Text.Json.JsonSerializer.Serialize(estudiante),
                Encoding.UTF8,
                "application/json"
            );

           
            var response = await _httpClient.PostAsync("https://localhost:7265/Estudiantes", informacion);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> EliminarEstudianteAsync(int id)
        {

            var response = await _httpClient.DeleteAsync($"https://localhost:7265/Estudiantes/{id}");

            return response.IsSuccessStatusCode;
        }

        public async Task<Estudiante> ObtenerEstudiantePorIdAsync(int id)
        {
            var response = await _httpClient
        .GetFromJsonAsync<RespuestaApiEstudiantes<Estudiante>>($"https://localhost:7265/Estudiantes/{id}");

            return response?.Data;
        }

        public async Task<List<Estudiante>> ObtenerEstudiantesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<RespuestaApiEstudiantes<List<Estudiante>>>("https://localhost:7265/Estudiantes");

            return response?.Data ?? new List<Estudiante>();
        }
    }
}