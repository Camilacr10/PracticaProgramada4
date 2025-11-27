using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiEstudiantes.BLL.Dtos;
using ApiEstudiantes.DLL.Entidades;
using AutoMapper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApiEstudiantes.BLL.Mapeos
{
    public class MapeoClases : Profile
    {
        public MapeoClases()
        {
            CreateMap<Estudiante, EstudianteDto>();
            CreateMap<EstudianteDto, Estudiante>();
        }
    }
}