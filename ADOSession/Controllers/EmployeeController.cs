using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using ADOSession.Models;

namespace ADOSession.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Save(Employee emp)
        {
            // 1. Details of Database 
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "data source=localhost; integrated security=true;initial catalog=ADOSession2102";

            // 2. Details of command
            SqlCommand cmd = new SqlCommand();
            //cmd.CommandType = CommandType.Text;
            //cmd.CommandText = "Insert into Employees values(1,ahmad,1000)";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Employees_Insert";

            SqlParameter Name=cmd.Parameters.Add("@P_Name",SqlDbType.VarChar);
            Name.Direction = ParameterDirection.Input;
            Name.Value = emp.Name;


            SqlParameter Salary=cmd.Parameters.Add("@P_Salary",SqlDbType.Float);
            Salary.Direction = ParameterDirection.Input;
            Salary.Value = emp.Salary;

            //3. Connect Command with Database
            cmd.Connection = con;

            // 4. Execute Command
            con.Open();
            cmd.ExecuteNonQuery(); // insert, update,delete
            con.Close();
            return View("Index");
        }

        public IActionResult LoadAll()
        {
            // 1. Details of Database 
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "data source=localhost; integrated security=true;initial catalog=ADOSession2102";

            // 2
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Employee_LoadAll";

            //3. 
            cmd.Connection = con;

            //4.
            con.Open();
            List<Employee> liEmp = new List<Employee>();

            SqlDataReader r= cmd.ExecuteReader();
            while (r.Read())
            {
                Employee emp = new Employee();
                emp.Name = r["Name"].ToString();
                emp.Salary = Convert.ToDouble(r["Salary"]);
                liEmp.Add(emp);
            }

            con.Close();


            return View("Index");
        }
    }
}

