using Dapper;
using HospitalManagementService.Context;
using HospitalManagementService.DTO;
using HospitalManagementService.Entity;
using HospitalManagementService.Interface;
using System.Data;
using System.Net.Http;

namespace HospitalManagementService.Service
{
    public class DoctorServiceImpl(DapperContext context) : IDoctor
    {
        public object? CreateDoctor(DoctorRequest request)
        {
            Doctor e = MapToEntity(request);
                //getUserById(request.DoctorId));

            var parameters = new DynamicParameters();
            parameters.Add("@DoctorId", e.DoctorId);
            parameters.Add("@DeptId", e.DeptId);
            parameters.Add("@DoctorName", e.DoctorName);
            parameters.Add("@DoctorAge", e.DoctorAge);
            parameters.Add("@DoctorAvailable", e.DoctorAvailable);
            parameters.Add("@Specialization", e.Specialization);
            parameters.Add("@Qualifications", e.Qualifications);

            using (var connection = context.CreateConnection())
            {
                // Use parameterized query and CommandType.Text
                var result = connection.Execute(
                    @"INSERT INTO Doctor (DoctorId, DeptId, DoctorName, DoctorAge, DoctorAvailable, Specialization, Qualifications)
              VALUES (@DoctorId, @DeptId, @DoctorName, @DoctorAge, @DoctorAvailable, @Specialization, @Qualifications);",
                    parameters,
                    commandType: CommandType.Text);
                return result;
            }
        }


        private Doctor MapToEntity(DoctorRequest request)
        {
            return new Doctor
            {
                DoctorId = request.DoctorId,
                DeptId = request.DeptId,
                DoctorName = request.DoctorName,
                DoctorAge = request.DoctorAge,
                DoctorAvailable = request.DoctorAvailable,
                Specialization = request.Specialization,
                Qualifications = request.Qualifications
            };
        }
        public Doctor? GetDoctorById(int doctorId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@DoctorId", doctorId);

            using (var connection = context.CreateConnection())
            {
                // Use CommandType.Text for inline SQL query
                var result = connection.QueryFirstOrDefault<Doctor>(
                    @"SELECT * FROM Doctor WHERE DoctorId = @DoctorId;",
                    parameters,
                    commandType: CommandType.Text);

                return result;
            }
        }


        public List<Doctor>? GetAllDoctors()
        {
            string query = "SELECT * FROM Doctor;";
            return context.CreateConnection().Query<Doctor>(query).ToList();
        }


        public object? UpdateDoctor(int doctorId, DoctorRequest request)
        {
            Doctor existingDoctor = GetDoctorById(doctorId);
            if (existingDoctor == null)
                return null; // Doctor not found
            existingDoctor.DeptId = request.DeptId;
            existingDoctor.Specialization = request.Specialization;
            existingDoctor.Qualifications = request.Qualifications;
            existingDoctor.DoctorAge = request.DoctorAge;
            existingDoctor.DoctorAvailable = request.DoctorAvailable;
            string query = "update Doctor set Specialization=@Specialization,Qualifications=@Qualifications,DoctorName=@Doctorname,DoctorAge=@doctorAge where DoctorId=@DoctorId and DeptId=@DeptId";
            var parameters = new DynamicParameters();
            parameters.Add("@DoctorId", doctorId);
            parameters.Add("@DeptId", request.DeptId);
            parameters.Add("@Doctorname", request.DoctorName);
            parameters.Add("@doctorAge", request.DoctorAge);
            parameters.Add("@Specialization", request.Specialization);
            parameters.Add("@Qualifications", request.Qualifications);
            int rowsAffected = context.CreateConnection().Execute(query, parameters);
            return rowsAffected;
        }
        public int DeleteDoctor(int doctorId)
        {
            string query = "DELETE FROM Doctor WHERE DoctorId = @DoctorId;";
            return context.CreateConnection().Execute(query, new { DoctorId = doctorId });
        }
      
        public List<Doctor>? GetDoctorsBySpecialization(string specialization)
        {
            string query = "SELECT * FROM Doctor WHERE Specialization = @Specialization;";
            return context.CreateConnection().Query<Doctor>(query, new { Specialization = specialization }).ToList();
        }
    }
}
