using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoEstudiantesDAL.RespuestasAPIS
{
    public class RespuestaApiEstudiantes<T>
    {
        public bool EsError { get; set; }

        public string Mensaje { get; set; }

        public T Data { get; set; }
    }
}