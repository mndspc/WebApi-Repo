using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using WebApiDemo.Models;
namespace WebApiDemo.Controllers
{
    
    public class ValuesController : ApiController
    {
        #region Database Objects
        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString);
        SqlCommand sqlCommand = new SqlCommand();
        SqlDataReader sqlDataReader;
        #endregion

        
        // GET api/values
        //public HttpResponseMessage Get()
        //{
        //    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
        //    DataSet ds = new DataSet();
        //    List<EmpMaster> empList = new List<EmpMaster>();
        //    try
        //    {
        //        sqlCommand.Connection = sqlConnection;
        //        sqlCommand.CommandText = "Select * from EmpMaster";
        //        sqlCommand.CommandType = CommandType.Text;
        //        sqlDataAdapter.SelectCommand = sqlCommand;
        //        sqlDataAdapter.Fill(ds, "Emp");
        //        if (ds.Tables["Emp"].Rows.Count > 0)
        //        {
        //            foreach (DataRow dataRow in ds.Tables["Emp"].Rows)
        //            {
        //                EmpMaster empMaster = new EmpMaster();
        //                empMaster.EmpCode = Convert.ToInt32(dataRow["EmpCode"]);
        //                empMaster.EmpName = Convert.ToString(dataRow["EmpName"]);
        //                empMaster.EmpDob = DateTime.Parse(dataRow["DateOfBirth"].ToString());
        //                empMaster.Email = Convert.ToString(dataRow["Email"]);
        //                empMaster.DeptCode = Convert.ToInt32(dataRow["DeptCode"]);
        //                empList.Add(empMaster);
        //            }
        //        }
        //        return Request.CreateResponse(HttpStatusCode.OK, empList);
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //    finally
        //    {
        //        sqlConnection.Close();
        //    }
           
        //}

        // GET api/values/5

        public HttpResponseMessage Get()
        {
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            List<string> empNames = new List<string>();
            try
            {
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "Select distinct(EmpName) from EmpMaster";
                sqlCommand.CommandType = CommandType.Text;
                sqlDataAdapter.SelectCommand = sqlCommand;
                sqlDataAdapter.Fill(ds, "Emp");
                if (ds.Tables["Emp"].Rows.Count > 0)
                {
                    foreach (DataRow dataRow in ds.Tables["Emp"].Rows)
                    {
                        empNames.Add(dataRow["EmpName"].ToString());                      
                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK, empNames);
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        public HttpResponseMessage Get([FromUri]int id)
        {
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            List<EmpMaster> empList = new List<EmpMaster>();
            try
            {
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "Select * from EmpMaster where EmpCode="+ id+" ";
                sqlCommand.CommandType = CommandType.Text;
                sqlDataAdapter.SelectCommand = sqlCommand;
                sqlDataAdapter.Fill(ds, "Emp");
                if (ds.Tables["Emp"].Rows.Count > 0)
                {
                    foreach (DataRow dataRow in ds.Tables["Emp"].Rows)
                    {
                        EmpMaster empMaster = new EmpMaster();
                        empMaster.EmpCode = Convert.ToInt32(dataRow["EmpCode"]);
                        empMaster.EmpName = Convert.ToString(dataRow["EmpName"]);
                        empMaster.EmpDob = DateTime.Parse(dataRow["DateOfBirth"].ToString());
                        empMaster.Email = Convert.ToString(dataRow["Email"]);
                        empMaster.DeptCode = Convert.ToInt32(dataRow["DeptCode"]);
                        empList.Add(empMaster);
                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK, empList);
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                sqlConnection.Close();
            }

        }

        // POST api/values
        public HttpResponseMessage Post([FromBody] EmpMaster entity)
        {
            try
            {
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "SaveEmployee";
                sqlCommand.Parameters.Add("@EmpCode", SqlDbType.Int).Value = entity.EmpCode;
                sqlCommand.Parameters.Add("@EmpName", SqlDbType.VarChar, 50).Value = entity.EmpName;
                sqlCommand.Parameters.Add("@DateOfBirth", SqlDbType.DateTime).Value = entity.EmpDob;
                sqlCommand.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = entity.Email;
                sqlCommand.Parameters.Add("@DeptCode", SqlDbType.Int).Value = entity.DeptCode;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                sqlCommand.ExecuteNonQuery();
                return Request.CreateResponse(HttpStatusCode.Created);
            }
            catch (SqlException ex)
            {
                return null;
            }
            finally
            {
                sqlConnection.Close();
            }
            
        }

        // PUT api/values/5
        public HttpResponseMessage Put(int id, [FromBody] string value)
        {
            //strings[id] = value;
            return Request.CreateResponse(HttpStatusCode.Accepted);
        }

        // DELETE api/values/5
        public HttpResponseMessage Delete(int id)
        {
            //strings.RemoveAt(id);
            return Request.CreateResponse(HttpStatusCode.Gone);
        }
    }
}
