using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VModels;

namespace Data
{
    public class TokenRepository : BaseRepository
    {
        public TokenRepository() 
        { 
        
        }

        public async Task<ResponseService<VMlogin>> getSubscriberByToken(string token)
        {
            ResponseService<VMlogin> Response = new ResponseService<VMlogin>();
            try
            {
                byte[] getbyteToken = Convert.FromBase64String(token);
                string[] parameters = Encoding.UTF8.GetString(getbyteToken, 0, getbyteToken.Length).Split(' ');
                using (SqlConnection con = new SqlConnection(sqlConnection))
                {
                    await con.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.CommandText = "getUserByToken";
                        cmd.Parameters.AddWithValue("username", parameters[0]);
                        cmd.Parameters.AddWithValue("password", parameters[1]);
                        cmd.ExecuteNonQuery();
                        SqlDataAdapter bindData = new SqlDataAdapter(cmd);
                        DataTable getData = new DataTable();
                        bindData.Fill(getData);

                        Response.Result = from row in getData.Rows.Cast<DataRow>() as IEnumerable<DataRow>
                                     select new VMlogin
                                     {
                                         userId = Convert.ToInt64(row["userid"]),
                                         username = Convert.ToString(row["username"]),
                                         realName = Convert.ToString(row["firstname"]) + " " + Convert.ToString(row["lastname"])
                                     };
                       
                    }

                }
            }
            catch (Exception ex)
            {
                Response.Error = true;
                Response.Message = ex.Message;
            }

            return Response;

        }

        public async Task<ResponseService<VMlogin>> getTokenForUser(string username, string password)
        {
            ResponseService<VMlogin> Response = new ResponseService<VMlogin>();
            try
            {
                using (SqlConnection con = new SqlConnection(sqlConnection))
                {
                    await con.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.CommandText = "getUserByToken";
                        cmd.Parameters.AddWithValue("username", username);
                        cmd.Parameters.AddWithValue("password", password);
                        cmd.ExecuteNonQuery();
                        SqlDataAdapter bindData = new SqlDataAdapter(cmd);
                        DataTable getData = new DataTable();
                        bindData.Fill(getData);
                        if (getData.Rows.Count > 0)
                        {
                            Response.Result = from row in getData.Rows.Cast<DataRow>() as IEnumerable<DataRow>
                                              select new VMlogin
                                              {
                                                  userId = Convert.ToInt64(row["userid"]),
                                                  username = Convert.ToString(row["username"]),
                                                  password = Convert.ToString(row["password"]),
                                                  realName = Convert.ToString(row["firstname"]) + " " + Convert.ToString(row["lastname"]),
                                              };
                            if (Response.Result.OfType<Exception>().Count() > 0)
                                throw new Exception(Response.Result.OfType<Exception>().FirstOrDefault().Message);
                        }
                        else
                        {
                            throw new Exception("No se Encontro, El usuario");
                        }

                        
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Error = true;
                Response.Message = ex.Message;
            }

            return Response;
        }
    }
}
