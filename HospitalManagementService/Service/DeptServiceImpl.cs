using Dapper;
using HospitalManagementService.Context;
using HospitalManagementService.DTO;
using HospitalManagementService.Entity;
using HospitalManagementService.Interface;
using System.Data;

namespace HospitalManagementService.Service
{
    public class DeptServiceImpl(DapperContext context) : IDepartment
    {
        public object? CreateDept(DeptRequest deptRequest)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@DeptName", deptRequest.DeptName);

            using (var connection = context.CreateConnection())
            {
                // Use CommandType.Text for inline SQL query
                // Use QueryFirstOrDefault<long> to match the type of SCOPE_IDENTITY()
                var result = connection.QueryFirstOrDefault<long>(
                    @"INSERT INTO Department (DeptName) 
              VALUES (@DeptName);
              SELECT SCOPE_IDENTITY();",
                    parameters,
                    commandType: CommandType.Text);

                return result;
            }
        }


        public Department? getByDeptId(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@DeptId", id);

            using (var connection = context.CreateConnection())
            {
                // Use CommandType.Text for inline SQL query
                var result = connection.QueryFirstOrDefault<Department>(
                    @"SELECT * FROM Department WHERE DeptId = @DeptId;",
                    parameters,
                    commandType: CommandType.Text);

                return result;
            }
        }
        public async Task<int> DeleteDepartmentById(int Depid)
        {
            var query = "DELETE FROM Department WHERE DeptId = @depid";
            using (var connection = context.CreateConnection())
            {
                var affectedRows = await connection.ExecuteAsync(query, new { Depid });
                return affectedRows;
            }
        }

        public async Task<int> updateDepartment(int Depid, DeptRequest Department)
        {
            var query = $"update Department set DeptName=@deptname where DeptId = @depid";
            using (var connection = context.CreateConnection())
            {
                var Dep = await connection.ExecuteAsync(query, new { deptname = Department.DeptName, depid = Depid });
                return Dep;
            }
        }


        public Department? getByDeptName(string name)
        {
            String query = "Select * from Department where DeptName=@dname";
            return context.CreateConnection().Query<Department>(query, new { dname = name }).FirstOrDefault();
        }
        private Department MapToEntity(DeptRequest request) => new Department { DeptName = request.DeptName };

    }
}
