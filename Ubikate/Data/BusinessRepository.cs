using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VModels;
using System.Data.SqlClient;
using System.Data;

namespace Data
{
    public class BusinessRepository:BaseRepository
    {
        public async Task<ResponseService<bool>> Save(VMBusiness _business, int userid)
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
                        cmd.CommandText = "sp_IU_SaveBusiness";
                        cmd.Connection = con;

                        cmd.Parameters.AddWithValue("@businessid", _business.businessid);
                        cmd.Parameters.AddWithValue("@name", _business.name);
                        if(_business.address!=null)
                        {
                            cmd.Parameters.AddWithValue("@number", _business.address.number);
                            cmd.Parameters.AddWithValue("@street", _business.address.street);
                            cmd.Parameters.AddWithValue("@city", _business.address.city);
                            cmd.Parameters.AddWithValue("@country", _business.address.country);
                        }
                        cmd.Parameters.AddWithValue("@proprietaryid", _business.proprietaryid);
                        cmd.Parameters.AddWithValue("@businesstypeid", _business.businesstype);
                        cmd.Parameters.AddWithValue("@insertuser",userid);
                        cmd.Parameters.AddWithValue("@latitude", _business.latitude);
                        cmd.Parameters.AddWithValue("@longitude", _business.longitude);

                        int result=await cmd.ExecuteNonQueryAsync();
                        if (result > 0)
                            Response.Result = new bool[] { true };
                        else
                            throw new Exception("No se inserto o actualizo el registro");
                        
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

        public async Task<ResponseService<VMBusiness>> Get(int? id)
        {
            ResponseService<VMBusiness> Response = new ResponseService<VMBusiness>();
            try
            {
                using (SqlConnection con = new SqlConnection(sqlConnection))
                {
                    await con.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("SP_S_GetBusiness", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@businessid", id);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            Response.Result = from r in dt.Rows.Cast<DataRow>()
                                              select new VMBusiness
                                              {
                                                  businessid = (int)r["businessid"],
                                                  name = (string)r["name"],
                                                  address = new Address()
                                                  {
                                                      number=(string)r["number"],
                                                      street=(string)r["street"],
                                                      city=(string)r["city"],
                                                      country=(string)r["country"]
                                                  },
                                                  proprietaryid=(int)r["proprietaryid"],
                                                  businesstype = (int)r["businessTypeid"],
                                                  latitude=(decimal)r["latitude"],
                                                  longitude=(decimal)r["longitude"]
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

        public async Task<ResponseService<bool>> Delete(int? businessid)
        {
            ResponseService<bool> Response = new ResponseService<bool>();
            try
            {
                using (SqlConnection con = new SqlConnection(sqlConnection))
                {
                    await con.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("SP_U_DeleteBusiness", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@businessid", businessid);
                        int result=await cmd.ExecuteNonQueryAsync();
                        if (result > 0)
                            Response.Result = new bool[] { true };
                        else
                            throw new Exception("No se Encontro el Registro a Eliminar");
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

        public async Task<ResponseService<BusinessMap>> GetBussinessMap()
        {
            ResponseService<BusinessMap> Response = new ResponseService<BusinessMap>();
            try
            {
                using (SqlConnection con = new SqlConnection(sqlConnection))
                {
                    await con.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("SP_S_GetBussinessMap", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.AddWithValue("@businessid", id);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            Response.Result = from r in dt.Rows.Cast<DataRow>()
                                              select new BusinessMap
                                              {
                                                  businessid = (int)r["businessid"],
                                                  name = (string)r["name"],
                                                  businessType = (int)r["businessTypeid"],
                                                  lat = (decimal)r["latitude"],
                                                  lng = (decimal)r["longitude"]
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

        public async Task<ResponseService<DetailBusinessMap>> GetBussinessMapId(int id)
        {
            ResponseService<DetailBusinessMap> Response = new ResponseService<DetailBusinessMap>();
            try
            {
                using (SqlConnection con = new SqlConnection(sqlConnection))
                {
                    await con.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("SP_S_GetBussinessMapID", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", id);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            Response.Result = from r in dt.Rows.Cast<DataRow>()
                                              select new DetailBusinessMap
                                              {
                                                  name = (string)r["name"],
                                                  address = new Address()
                                                  {
                                                      number=(string)r["number"],
                                                      street=(string)r["street"],
                                                      city=(string)r["city"],
                                                      country=(string)r["country"]
                                                  }
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
    }
}
