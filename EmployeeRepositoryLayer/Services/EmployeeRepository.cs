using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using EmployeeCommonLayer;
using EmployeeRepositoryLayer.Interface;

namespace EmployeeRepositoryLayer.Services
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private static readonly string connectionVariable = "Server=DESKTOP-EUJ5D3D;Database=EmployeeDatabase;Trusted_Connection=true;MultipleActiveResultSets=True";

        SqlConnection sqlConnection = new SqlConnection(connectionVariable);

        public async Task<bool> AddEmployee(EmployeeModel employeeModel)
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand("spInsertRecord", this.sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@FirstName", employeeModel.FirstName);
                sqlCommand.Parameters.AddWithValue("@LastName", employeeModel.LastName);
                sqlCommand.Parameters.AddWithValue("@EmailId", employeeModel.EmailId);
                sqlCommand.Parameters.AddWithValue("@MobileNumber", employeeModel.MobileNumber);
                sqlCommand.Parameters.AddWithValue("@EmployeeAddress", employeeModel.EmployeeAddress);
               
                this.sqlConnection.Open();
                var response = await sqlCommand.ExecuteNonQueryAsync();

                if (response > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IList<EmployeeModel> ReadEmployee()
        {
            try
            {
                IList<EmployeeModel> employeeModelsList = new List<EmployeeModel>();
                SqlCommand sqlCommand = new SqlCommand("spReadRecord", this.sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                this.sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    EmployeeModel employeeModel = new EmployeeModel();
                    employeeModel.EmployeeId = Convert.ToInt32(sqlDataReader["EmployeeId"]);
                    employeeModel.FirstName = sqlDataReader["FirstName"].ToString();
                    employeeModel.LastName = sqlDataReader["LastName"].ToString();
                    employeeModel.EmailId = sqlDataReader["EmailId"].ToString();
                    employeeModel.MobileNumber = sqlDataReader["MobileNumber"].ToString();
                    employeeModel.EmployeeAddress = sqlDataReader["EmployeeAddress"].ToString();
                    employeeModelsList.Add(employeeModel);
                }
                this.sqlConnection.Close();
                return employeeModelsList;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public async Task<bool> UpdateEmployee(EmployeeModel employeeModel)
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand("spUpdateRecord", this.sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@EmployeeId", employeeModel.EmployeeId);
                sqlCommand.Parameters.AddWithValue("@FirstName", employeeModel.FirstName);
                sqlCommand.Parameters.AddWithValue("@LastName", employeeModel.LastName);
                sqlCommand.Parameters.AddWithValue("@EmailId", employeeModel.EmailId);
                sqlCommand.Parameters.AddWithValue("@MobileNumber", employeeModel.MobileNumber);
                sqlCommand.Parameters.AddWithValue("@EmployeeAddress", employeeModel.EmployeeAddress);

                this.sqlConnection.Open();
                var response = await sqlCommand.ExecuteNonQueryAsync();
                if (response > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public async Task<bool> DeleteEmployee(EmployeeModel employeeModel)
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand("spDeleteRecord", this.sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@EmployeeId", employeeModel.EmployeeId);
                this.sqlConnection.Open();
                var response = await sqlCommand.ExecuteNonQueryAsync();
                if (response > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }

        public IList<EmployeeModel> SearchEmployee(EmployeeModel employeeModel)
        {
            try
            {
                IList<EmployeeModel> employeeModelsList = new List<EmployeeModel>();
                SqlCommand sqlCommand = new SqlCommand("spSearchEmployee", this.sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@EmployeeId", employeeModel.EmployeeId);
                this.sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    employeeModel.EmployeeId = Convert.ToInt32(sqlDataReader["EmployeeId"]);
                    employeeModel.FirstName = sqlDataReader["FirstName"].ToString();
                    employeeModel.LastName = sqlDataReader["LastName"].ToString();
                    employeeModel.EmailId = sqlDataReader["EmailId"].ToString();
                    employeeModel.MobileNumber = sqlDataReader["MobileNumber"].ToString();
                    employeeModel.EmployeeAddress = sqlDataReader["EmployeeAddress"].ToString();
                    employeeModelsList.Add(employeeModel);
                }
                this.sqlConnection.Close();
                return employeeModelsList;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}
