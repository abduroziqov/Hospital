using Application.Services;
using Domain.Entities;
using Infrastructure.DataAccess;

namespace Infrastructure.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly HospitalDbContext _context;

        public DoctorService(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<Doctor> CreateAsync(Doctor entity)
        {
            await _context.Doctors.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            Doctor? entity = await _context.Doctors.FindAsync(Id);
            if (entity == null)
                return false;

            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public void Get()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Doctor>> GetAllAsync()
        {
            //IEnumerable<Doctor> doctors = _context.Doctors.Include(x => x.Doctors).AsNoTracking().AsEnumerable();
            //return Task.FromResult(doctors);

            return null;
        }

        public async Task<Doctor> GetByIdAsync(int Id)
        {
            Doctor? doctorEntity = await _context.Doctors.FindAsync(Id);
            return doctorEntity;
        }

        public async Task<bool> UpdateAsync(Doctor entity)
        {
            _context.Doctors.Update(entity);
            var executedRows = await _context.SaveChangesAsync();

            return executedRows > 0;
        }
    }
}
