using Api;
using MediatR;

namespace Infrastructure.Services
{
    public class CreateMediatrService : IRequest<DoctorMediatr>
    {
        public DoctorMediatr? DoctorMediatr { get; set; }
    }
}
