using Final.LoginPage.Model;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Final.LoginPage.Helper.ConstantHelper;

namespace Final.LoginPage.Services
{
    public class UserService : IUserService
    {
        public bool Register(tblUser user)
        {
            try
            {
                var client = new RestClient();
                //var barrer = "BQBuTEaFoPc9n13ioks_CssZL70RHwu3DtC3KxOPH5_kLVwdlrZXIBGY5e5DJGUPiuWsiD1kF5nhmTYgEJ_JmZn6gyuK1p_st6C_yFbdZ3ZqeSYrMLBTwZ3i-foLdedmpYQZliEwh2lOz54EPQ0znX9FGYD5xUv8PEg";
                //client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(barrer, "Bearer");
                var request = new RestRequest("https://localhost:44307/api/User/register", Method.Post);
                request.AddHeader("Content-type", "application/json");
                user.Id = Guid.NewGuid().ToString();
                request.AddBody(user);
                var response = client.Execute(request);
                var data = JsonConvert.DeserializeObject<InternalAPIResponseCode>(response.Content);
                if (data.Code != 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
