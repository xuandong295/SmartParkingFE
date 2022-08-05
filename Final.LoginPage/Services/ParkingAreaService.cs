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
    public class ParkingAreaService : IParkingAreaService
    {
        public async Task<tblParkingArea> LoadParkingDataAsync(string parkingAreaName)
        {
            try
            {
                var parkingSpaces = new List<tblParkingArea>();
                var client = new RestClient();
                //var barrer = "BQBuTEaFoPc9n13ioks_CssZL70RHwu3DtC3KxOPH5_kLVwdlrZXIBGY5e5DJGUPiuWsiD1kF5nhmTYgEJ_JmZn6gyuK1p_st6C_yFbdZ3ZqeSYrMLBTwZ3i-foLdedmpYQZliEwh2lOz54EPQ0znX9FGYD5xUv8PEg";
                //client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(barrer, "Bearer");
                var request = new RestRequest("http://smartparking.local:5555/api/parkingarea", Method.Get);
                //request.AddHeader("Accept", "application/json");
                //request.AddHeader("Content-Type", "application/json");
                var response = await client.GetAsync(request);
                var data = JsonConvert.DeserializeObject<InternalAPIResponseCode>(response.Content);
                var smt = data.Data.ToString();
                var data2 = JsonConvert.DeserializeObject<List<tblParkingArea>>(smt);
                var data3 = data2.Where(o => o.Area == parkingAreaName).FirstOrDefault();
                return data3;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
