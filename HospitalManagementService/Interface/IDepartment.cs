using HospitalManagementService.DTO;
using HospitalManagementService.Entity;

namespace HospitalManagementService.Interface
{
    public interface IDepartment
    {

        object? CreateDept(DeptRequest deptRequest);
        Department? getByDeptId(int id);
        Department? getByDeptName(string name);
        public Task<int> DeleteDepartmentById(int Depid);

        public Task<int> updateDepartment(int Depid, DeptRequest Department);
    }
}
