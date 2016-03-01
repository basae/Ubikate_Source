using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VModels;
using Data;

namespace Business
{
    public class BusinessBusiness
    {
        private BusinessRepository _repository;

        public BusinessBusiness()
        {
            _repository = new BusinessRepository();
        }

        public async Task<ResponseService<bool>> Save(VMBusiness _business, int userid)
        {
            ResponseService<bool> Response = new ResponseService<bool>();
            try
            {
                if (_business == null)
                    throw new Exception("Error en Objeto");
                if (_business.businessid.HasValue && _business.businessid.Value < 1)
                    throw new Exception("Id Invalido");
                if (string.IsNullOrWhiteSpace(_business.name))
                    throw new Exception("Nombre del Negocio Requerido");
                if(_business.proprietaryid==null || _business.proprietaryid<1)
                    throw new Exception("ID de Propietario Invalido");
                if(_business.businesstype==null || _business.businesstype<1)
                    throw new Exception("Tipo de Negocio Invalido");
                if(_business.latitude==null || _business.latitude==0)
                    throw new Exception("Latitud Invalida");
                if(_business.longitude==null || _business.longitude==0)
                    throw new Exception("Longitud Invalida");

                Response = await _repository.Save(_business, userid);
                if (Response.Error)
                    throw new Exception(Response.Message);
            }
            catch (Exception ex)
            {
                Response.Error = true;
                Response.Message = ex.Message;
            }
            return Response;
        }
        
        public async Task<ResponseService<bool>> Update(VMBusiness _business, int userid)
        {
            ResponseService<bool> Response = new ResponseService<bool>();
            try
            {
                if (_business == null)
                    throw new Exception("Error en Objeto");
                if (!_business.businessid.HasValue || _business.businessid.Value < 1)
                    throw new Exception("Id Invalido");
                if (string.IsNullOrWhiteSpace(_business.name))
                    throw new Exception("Nombre del Negocio Requerido");
                if(_business.proprietaryid==null || _business.proprietaryid<1)
                    throw new Exception("ID de Propietario Invalido");
                if(_business.businesstype==null || _business.businesstype<1)
                    throw new Exception("Tipo de Negocio Invalido");
                if(_business.latitude==null || _business.latitude==0)
                    throw new Exception("Latitud Invalida");
                if(_business.longitude==null || _business.longitude==0)
                    throw new Exception("Longitud Invalida");

                Response = await _repository.Save(_business, userid);
                if (Response.Error)
                    throw new Exception(Response.Message);
            }
            catch (Exception ex)
            {
                Response.Error = true;
                Response.Message = ex.Message;
            }
            return Response;
        }

        public async Task<ResponseService<VMBusiness>> Get(int? bussinessid)
        {
            ResponseService<VMBusiness> Response = new ResponseService<VMBusiness>();
            try
            {
                Response = await _repository.Get(bussinessid);
                if (Response.Error)
                    throw new Exception(Response.Message);
            }
            catch (Exception ex)
            {
                Response.Error = true;
                Response.Message = ex.Message;
            }
            return Response;
        }

        public async Task<ResponseService<bool>> Delete(int bussinessid)
        {
            ResponseService<bool> Response = new ResponseService<bool>();
            try
            {
                if (bussinessid == null)
                    throw new Exception("El Id Es Invalido");
                Response = await _repository.Delete(bussinessid);
                if (Response.Error)
                    throw new Exception(Response.Message);
            }
            catch (Exception ex)
            {
                Response.Error = true;
                Response.Message = ex.Message;
            }

            return Response;
        }

        public async Task<ResponseService<BusinessMap>> getBusinessMap()
        {
            ResponseService<BusinessMap> Response = new ResponseService<BusinessMap>();
            try
            {
                Response = await _repository.GetBussinessMap();
                if (Response.Error)
                    throw new Exception(Response.Message);
            }
            catch (Exception ex)
            {
                Response.Error = true;
                Response.Message = ex.Message;
            }
            return Response;
        }

        public async Task<ResponseService<DetailBusinessMap>> getBusinessMapId(int id)
        {
            ResponseService<DetailBusinessMap> Response = new ResponseService<DetailBusinessMap>();
            try
            {
                Response = await _repository.GetBussinessMapId(id);
                if (Response.Error)
                    throw new Exception(Response.Message);
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
