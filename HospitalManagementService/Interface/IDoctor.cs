using HospitalManagementService.DTO;
using HospitalManagementService.Entity;

namespace HospitalManagementService.Interface
{
    public interface IDoctor
    {


        object? CreateDoctor(DoctorRequest request);
        Doctor? GetDoctorById(int doctorId);
        List<Doctor>? GetAllDoctors();
        object? UpdateDoctor(int doctorId, DoctorRequest request);
        int DeleteDoctor(int doctorId);
        public List<Doctor>? GetDoctorsBySpecialization(string specialization);
    }
}
