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
    public class ProprietaryRepository:BaseRepository
    {
        public ProprietaryRepository() { }

        public async Task<ResponseService<long>> Save(VMproprietary proprietary,long userid)
        {
            ResponseService<long> Response = new ResponseService<long>();
            try
            {
                using (SqlConnection con = new SqlConnection(sqlConnection))
                {
                    await con.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.CommandText = "SavePropietary";
                        cmd.Parameters.Add("@proprietaryid", SqlDbType.BigInt, 64).Direction = ParameterDirection.InputOutput;
                        cmd.Parameters["@proprietaryid"].Value = proprietary.proprietaryid;
                        cmd.Parameters.AddWithValue("@rfc", proprietary.rfc);
                        cmd.Parameters.AddWithValue("@lastname", proprietary.lastname);
                        cmd.Parameters.AddWithValue("@firstname", proprietary.firstname);
                        cmd.Parameters.AddWithValue("@insertuser", userid);
                        cmd.Parameters.AddWithValue("@subscribed", proprietary.subscribed);
                        cmd.ExecuteNonQuery();
                        Response.Result = new long[] { Convert.ToInt64((cmd.Parameters["@proprietaryid"].Value == DBNull.Value) ? 0 : cmd.Parameters["@proprietaryid"].Value) };
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

        public async Task<ResponseService<VMproprietary>> Get(long? id)
        {
            ResponseService<VMproprietary> Response = new ResponseService<VMproprietary>();
            try
            {
                using (SqlConnection con = new SqlConnection(sqlConnection))
                {
                    await con.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;

                        cmd.CommandText = "getProprietarys";
                        cmd.Parameters.AddWithValue("@id", id);
                        await cmd.ExecuteNonQueryAsync();
                        SqlDataAdapter bindData = new SqlDataAdapter(cmd);
                        DataTable getData = new DataTable();
                        bindData.Fill(getData);
                        if (getData.Rows.Count > 0)
                        {
                            Response.Result = from row in getData.Rows.Cast<DataRow>() as IEnumerable<DataRow>
                                              select new VMproprietary
                                              {
                                                  proprietaryid = Convert.ToInt32(row["proprietaryid"]),
                                                  rfc = Convert.ToString(row["rfc"]),
                                                  lastname = Convert.ToString(row["lastname"]),
                                                  firstname = Convert.ToString(row["firstname"]),
                                                  subscribed = Convert.ToBoolean(row["subscribed"]),
                                                  regDate = DateTime.Parse(row["inserdate"].ToString())
                                              };
                            if (Response.Result.OfType<Exception>().Count() > 0)
                                throw new Exception(Response.Result.OfType<Exception>().FirstOrDefault().Message);
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

        public async Task<ResponseService<bool>> Delete(long proprietaryid)
        {
            ResponseService<bool> Response = new ResponseService<bool>();
            try
            {
                using (SqlConnection con = new SqlConnection(sqlConnection))
                {
                    await con.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "DeleteProprietary";
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@proprietaryid", proprietaryid);
                        int result=await cmd.ExecuteNonQueryAsync();
                        if (result > 0)
                            Response.Result = new bool[] { true };
                        else
                            throw new Exception("No se Encontro Ningun Registro para Eliminar");

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
