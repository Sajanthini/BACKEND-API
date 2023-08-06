using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public EmployeesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]

        public JsonResult Get()
        {
            string query = @"select Id, Name, MobileNo, Email from Employees";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Myappcon");
            SqlDataReader myReader;
            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (SqlCommand mycmd = new SqlCommand(query, mycon))
                {
                    myReader = mycmd.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult(table);

        }
        [HttpPost]

        public JsonResult Post(Employee emp)
        {
            string query = @"Insert into Employees (Name,MobileNo,Email) Values(@Name, @MobileNo, @Email)";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Myappcon");
            SqlDataReader myReader;
            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (SqlCommand mycmd = new SqlCommand(query, mycon))
                {
                    mycmd.Parameters.AddWithValue("@Name", emp.Name);
                    mycmd.Parameters.AddWithValue("@MobileNo", emp.MobileNo);
                    mycmd.Parameters.AddWithValue("@Email", emp.Email);
                    myReader = mycmd.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Added Successfully");

        }

        [HttpPut]
        public JsonResult Put(Employee emp)
        {
            string query = @"update Employees
                             set Name = @Name,
                             MobileNo = @MobileNo,
                             Email = @Email
                             where Id = @Id";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Myappcon");
            SqlDataReader myReader;
            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (SqlCommand mycmd = new SqlCommand(query, mycon))
                {
                    mycmd.Parameters.AddWithValue("@Id", emp.Id);
                    mycmd.Parameters.AddWithValue("@Name", emp.Name);
                    mycmd.Parameters.AddWithValue("@MobileNo", emp.MobileNo);
                    mycmd.Parameters.AddWithValue("@Email", emp.Email);
                    myReader = mycmd.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Edited Successfully");

        }

        [HttpDelete("{Id}")]
        public JsonResult Delete(int Id)
        {
            string query = @"Delete from Employees
                             where Id = @Id";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Myappcon");
            SqlDataReader myReader;
            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (SqlCommand mycmd = new SqlCommand(query, mycon))
                {
                    mycmd.Parameters.AddWithValue("@ID", Id);
                    myReader = mycmd.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Delete Successfully");

        }
    }
}
