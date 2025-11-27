using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ProyectoEstudiantesBLL.Dtos;
using ProyectoEstudiantesDAL.Entidades;
using ProyectoEstudiantesDAL.Repositorios;

namespace ProyectoEstudiantesBLL.Servicios
{
    public class EstudiantesServicio : IEstudiantesServicio
    {
        //Inyección de dependencias
        private readonly IEstudiantesRepositorio _estudiantesRepositorio;
        private readonly IMapper _mapper;

        public EstudiantesServicio(IEstudiantesRepositorio estudiantesRepositorio, IMapper mapper)
        {
            _estudiantesRepositorio = estudiantesRepositorio;
            _mapper = mapper;
        }

        public async Task<CustomResponse<EstudianteDto>> AgregarEstudianteAsync(EstudianteDto estudianteDto)
        {
            var respuesta = new CustomResponse<EstudianteDto>();


            //El repositorio me indica si pudo o no agregar el estudiante
            if (!await _estudiantesRepositorio.AgregarEstudianteAsync(_mapper.Map<Estudiante>(estudianteDto)))
            {
                respuesta.EsError = true;
                respuesta.Mensaje = "No fue posible agregar el registro";
                return respuesta;
            }

            return respuesta;
        }

        public async Task<CustomResponse<EstudianteDto>> ActualizarEstudianteAsync(EstudianteDto estudianteDto)
        {
            var respuesta = new CustomResponse<EstudianteDto>();

            var estudiante = _mapper.Map<Estudiante>(estudianteDto);


            if (!await _estudiantesRepositorio.ActualizarEstudianteAsync(estudiante))
            {
                respuesta.EsError = true;
                respuesta.Mensaje = "No fue posible actualizar el registro";
                return respuesta;
            }

            return respuesta;
        }

        public async Task<CustomResponse<EstudianteDto>> EliminarEstudianteAsync(int id)
        {
            var respuesta = new CustomResponse<EstudianteDto>();
            if (!await _estudiantesRepositorio.EliminarEstudianteAsync(id))
            {
                respuesta.EsError = true;
                respuesta.Mensaje = "No fue posible eliminar el registro";
                return respuesta;
            }
            return respuesta;
        }

        public async Task<CustomResponse<EstudianteDto>> ObtenerEstudiantePorIdAsync(int id)
        {
            var respuesta = new CustomResponse<EstudianteDto>();
            var estudiante = await _estudiantesRepositorio.ObtenerEstudiantePorIdAsync(id);
            respuesta.Data = _mapper.Map<EstudianteDto>(estudiante);
            return respuesta;
        }

        public async Task<CustomResponse<List<EstudianteDto>>> ObtenerEstudiantesAsync()
        {
            var respuesta = new CustomResponse<List<EstudianteDto>>();
            var estudiantes = await _estudiantesRepositorio.ObtenerEstudiantesAsync();
            var estudiantesDto = _mapper.Map<List<EstudianteDto>>(estudiantes);
            respuesta.Data = estudiantesDto;
            return respuesta;
        }
    }
}