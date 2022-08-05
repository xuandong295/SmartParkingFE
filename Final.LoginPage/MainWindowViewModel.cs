using Final.LoginPage.Common.Command;
using Final.LoginPage.Helper;
using Final.LoginPage.Model;
using Final.LoginPage.ViewModel;
using Newtonsoft.Json;
using Prism.Mvvm;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static Final.LoginPage.Helper.ConstantHelper;

namespace Final.LoginPage
{
    public class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel()
        {
            NavCommand = new CommandBase<string>(OnNavAsync);
        }

        private MainViewModel mainViewModel = new MainViewModel();

        private RegisterViewModel registerViewModelModel = new RegisterViewModel();

        private BindableBase _CurrentViewModel;

        public BindableBase CurrentViewModel
        {
            get { return _CurrentViewModel; }
            set { SetProperty(ref _CurrentViewModel, value); }
        }
        private string Bearer { get; set; }

        public CommandBase<string> NavCommand { get; private set; }

        private void OnNavAsync(string destination)
        {
            switch (destination)
            {
                case "login":
                    try
                    {
                        var client = new RestClient();
                        //var barrer = "BQBuTEaFoPc9n13ioks_CssZL70RHwu3DtC3KxOPH5_kLVwdlrZXIBGY5e5DJGUPiuWsiD1kF5nhmTYgEJ_JmZn6gyuK1p_st6C_yFbdZ3ZqeSYrMLBTwZ3i-foLdedmpYQZliEwh2lOz54EPQ0znX9FGYD5xUv8PEg";
                        //client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(barrer, "Bearer");
                        var request = new RestRequest($"http://smartparking.local:5555/api/User/sign-in?username={Username}&password={Password}", Method.Post);
                        request.AddHeader("Accept", "application/json");
                        request.AddHeader("Content-Type", "application/json");
                        //var body = new tblUser() { UserName = "admin", Password = "admin" };
                        //request.AddParameter("application/json", body, ParameterType.RequestBody);
                        var response = client.Post(request);
                        var data = JsonConvert.DeserializeObject<InternalAPIResponseCode>(response.Content);
                        var user = data.Data.ToString();
                        var data2 = JsonConvert.DeserializeObject<tblUser>(user);
                        var requestBearer = new RestRequest("http://smartparking.local:5555/api/user", Method.Post);
                        requestBearer.AddHeader("Accept", "application/json");
                        requestBearer.AddHeader("Content-Type", "application/json");
                        var body = new tblUser() { UserName = "admin", Password = "admin" };
                        requestBearer.AddParameter("application/json", body, ParameterType.RequestBody);
                        var responseBearer = client.Post(requestBearer);
                        var bearer = JsonConvert.DeserializeObject<Bearer>(responseBearer.Content);
                        Bearer = bearer.Token;
                        CurrentViewModel = mainViewModel;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("The email address or password is incorrect.");
                        return;
                    }
                    break;
                case "register":
                default:
                    CurrentViewModel = registerViewModelModel;
                    break;
            }
        }

        private string password = "admin";

        public string Password
        {
            get { return password; }

            set
            {
                if (password != value)
                {
                    password = value;
                    RaisePropertyChanged("Password");
                }
            }
        }

        private string username = "admin";
        public string Username
        {
            get { return username; }

            set
            {
                if (username != value)
                {
                    username = value;
                    RaisePropertyChanged("Username");
                }
            }
        }
    }
}
