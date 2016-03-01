using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using VModels;

namespace Business
{
    public class TokenBusiness
    {
        TokenRepository _respository;
        public TokenBusiness()
        {
            _respository = new TokenRepository();
        }

        public async Task<ResponseService<UserResponse>> getTokenForUser(VMlogin _user)
        {
            ResponseService<UserResponse> Response = new ResponseService<UserResponse>();
            try
            {
                if (_user == null)
                    throw new Exception("Error en Objeto de Entrada");
                if(string.IsNullOrWhiteSpace(_user.username))
                    throw new Exception("Nombre de Usuario Requerido");
                if (string.IsNullOrWhiteSpace(_user.password))
                    throw new Exception("Contraseña Requerida");
                ResponseService<VMlogin> isUser=await _respository.getTokenForUser(_user.username, _user.password);
                if (isUser.Error)
                    throw new Exception(isUser.Message);
                    List<UserResponse> Result = new List<UserResponse>();
                    Result.Add(new UserResponse
                    {
                        id = isUser.Result.FirstOrDefault().userId,
                        accessToken = Convert.ToBase64String(Encoding.ASCII.GetBytes(isUser.Result.FirstOrDefault().username + " " + _user.password)),
                        username = isUser.Result.FirstOrDefault().username,
                        name = isUser.Result.FirstOrDefault().realName
                    });
                    Response.Result = Result;

            }
            catch (Exception ex)
            {
                Response.Error = true;
                Response.Message = ex.Message;
            }
            return Response;
        }

        public async Task<ResponseService<VMlogin>> getSubscriberByToken(string token)
        {
            ResponseService<VMlogin> Response=new ResponseService<VMlogin>();
            try
            {
                if (string.IsNullOrWhiteSpace(token))
                    throw new Exception("Token Invalido");
                Response=await _respository.getSubscriberByToken(token);
                if (Response.Error)
                    throw new Exception(Response.Message);

            }
            catch(Exception ex)
            {
                Response.Error=true;
                Response.Message=ex.Message;
            }

            return Response;
   
        }
    }
}
