using Domain.Models.DoctorDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ReponseModel<T>
    {
        private DoctorCreateDTO doctorDTO;

        public ReponseModel(DoctorCreateDTO doctorDTO)
        {
            this.doctorDTO = doctorDTO;
        }

        public ReponseModel(T result, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            Result = result;
            StatusCode = statusCode;
        }

        public ReponseModel(string error, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            Error = error;
            StatusCode = statusCode;
        }
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        public string Error { get; set; }
        public T Result { get; set; }
    }
}
