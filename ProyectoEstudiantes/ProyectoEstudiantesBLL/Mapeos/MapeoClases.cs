using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ProyectoEstudiantesBLL.Dtos;
using ProyectoEstudiantesDAL.Entidades;

namespace ProyectoEstudiantesBLL.Mapeos
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