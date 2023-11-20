using Application.Repositories;
using Domain.Entities;

namespace Application.Services
{
    public interface IDoctorService : IRepository<Doctor>
    {
        //void Get();
    }
}
