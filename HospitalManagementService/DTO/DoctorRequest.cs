﻿namespace HospitalManagementService.DTO
{
    public class DoctorRequest
    {
        public int DoctorId { get; set; }
        public int DeptId { get; set; }
        public int DoctorAge { get; set; }

        public string DoctorName { get; set; }
        public string DoctorAvailable { get; set; }
        public string Specialization { get; set; }
        public string Qualifications { get; set; }

    }
}
