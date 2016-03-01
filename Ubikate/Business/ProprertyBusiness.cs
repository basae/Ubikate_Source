using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using VModels;

namespace Business
{
    public class ProprietaryBusiness
    {
        private ProprietaryRepository _repository;
        public ProprietaryBusiness()
        {
            _repository = new ProprietaryRepository();
        }

        public async Task<ResponseService<long>> Save(VMproprietary _proprietary,long userid)
        {
            ResponseService<long> Response = new ResponseService<long>();
            try
            {
                if (_proprietary == null)
                    throw new Exception("Error en el Objeto");
                if (_proprietary.proprietaryid.HasValue && _proprietary.proprietaryid.Value != 0)
                    throw new Exception("El ID debe ser nulo o 0");
                if (string.IsNullOrWhiteSpace(_proprietary.rfc))
                    throw new Exception("El RFC es requerido");
                if (string.IsNullOrWhiteSpace(_proprietary.firstname))
                    throw new Exception("El Nombre es Requerido");
                if (string.IsNullOrWhiteSpace(_proprietary.lastname))
                    throw new Exception("Apellidos Requeridos");

                Response = await _repository.Save(_proprietary,userid);
            }
            catch (Exception ex)
            {
                Response.Error = true;
                Response.Message = ex.Message;
            }

            return Response;
        }

        public async Task<ResponseService<long>> Update(VMproprietary _proprietary, long userid)
        {
            ResponseService<long> Response = new ResponseService<long>();
            try
            {
                if (_proprietary == null)
                    throw new Exception("Error en el Objeto");
                if (!_proprietary.proprietaryid.HasValue || _proprietary.proprietaryid.Value <= 0)
                    throw new Exception("El ID debe No puede ser Nulo");
                if (string.IsNullOrWhiteSpace(_proprietary.rfc))
                    throw new Exception("El RFC es requerido");
                if (string.IsNullOrWhiteSpace(_proprietary.firstname))
                    throw new Exception("El Nombre es Requerido");
                if (string.IsNullOrWhiteSpace(_proprietary.lastname))
                    throw new Exception("Apellidos Requeridos");

                Response = await _repository.Save(_proprietary, userid);
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
            ResponseService<VMproprietary> Response=new ResponseService<VMproprietary>();
            try
            {
                Response=await _repository.Get(id);
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

        public async Task<ResponseService<bool>> Delete(int proprietaryid)
        {
            ResponseService<bool> Response = new ResponseService<bool>();
            try
            {
                if (proprietaryid == null || proprietaryid < 1)
                    throw new Exception("Id Invalido");
                Response = await _repository.Delete(proprietaryid);
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
