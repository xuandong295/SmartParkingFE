using Final.LoginPage.Helper;
using Final.LoginPage.Model;
using Final.LoginPage.Model.Configs;
using Final.LoginPage.Model.Entities;
using Final.LoginPage.Model.Persistence;
using Final.LoginPage.Model.RabbitMQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Prism.Mvvm;
using RabbitMQ.Client;
using RestSharp;
using System.Threading.Tasks;
using System.Windows;

namespace Final.LoginPage.ViewModel
{
    public class HomeViewModel : BindableBase
    {
        private readonly IPersistenceFactory PersistenceFactory;
        public string Title { get; } = "Home";
        private string frontImageLink;
        public string FrontImageLink
        {
            get { return frontImageLink; }

            set
            {
                if (frontImageLink != value)
                {
                    frontImageLink = value;
                    RaisePropertyChanged("FrontImageLink");
                }
            }
        }
        private string backImageLink;
        public string BackImageLink
        {
            get { return backImageLink; }

            set
            {
                if (backImageLink != value)
                {
                    backImageLink = value;
                    RaisePropertyChanged("BackImageLink");
                }
            }
        }
        private string licensePlateNumber;
        public string LicensePlateNumber
        {
            get { return licensePlateNumber; }

            set
            {
                if (licensePlateNumber != value)
                {
                    licensePlateNumber = value;
                    RaisePropertyChanged("LicensePlateNumber");
                }
            }
        }
        private string parkingAreaName;
        public string ParkingAreaName
        {
            get { return parkingAreaName; }

            set
            {
                if (parkingAreaName != value)
                {
                    parkingAreaName = value;
                    RaisePropertyChanged("ParkingAreaName");
                }
            }
        }
        private string timeIn;
        public string TimeIn
        {
            get { return timeIn; }

            set
            {
                if (timeIn != value)
                {
                    timeIn = value;
                    RaisePropertyChanged("TimeIn");
                }
            }
        }
        private string timeOut;
        public string TimeOut
        {
            get { return timeOut; }

            set
            {
                if (timeOut != value)
                {
                    timeOut = value;
                    RaisePropertyChanged("TimeOut");
                }
            }
        }
        public HomeViewModel()
        {
            //get env variables
            var configBuilder = new ConfigurationBuilder()
              .SetBasePath(System.IO.Directory.GetCurrentDirectory())
              .AddJsonFile(@"C:\Users\Admin\Desktop\FinalProject\Final.LoginPage\Final.LoginPage\appsettings.json", optional: false, reloadOnChange: true);
            var configuration = configBuilder.Build();


            //set init runtime 
            var loggerFactory = new LoggerFactory();
            ILogger logger = loggerFactory.CreateLogger<ILogger>();
            //set rabbitMQ server
            var rabbitMqConfig = new RabbitMqConfiguration(configuration["RabbitMQ:Server"], configuration["RabbitMQ:Port"], configuration["RabbitMQ:Username"],
                                       configuration["RabbitMQ:Password"], configuration["RabbitMQ:QueueName"], configuration["RabbitMQ:VHost"]);
            var messageDispatcher = new RabbitMqDispatcher(rabbitMqConfig, loggerFactory);
            var preprocessQueue = configuration["RabbitMQ:Queues:WPFManageQueue"];
            //RabbitMQ queue
            var rabbitMqQueues = new AppConfig
            {
                WPFManageQueue = configuration["RabbitMQ:Queues:WPFManageQueue"]
                
            };
            PersistenceFactory = new PersistenceFactory(loggerFactory, rabbitMqConfig, rabbitMqQueues);

            Task.Factory.StartNew(() =>
            {
                System.Threading.Thread.Sleep(10);

                while (true)
                {
                    //var message = messageDispatcher.Dequeue<RabbitMQMessage>(preprocessQueue);
                    var message = new RabbitMQMessage
                    {
                        BackImageLink = "https://drive.google.com/uc?export=view&id=1Ja6hGb0HkeP3X0ueLXyL7wO-I2Xqzh00",
                        FrontImageLink = "https://drive.google.com/uc?export=view&id=1XtQM_BqEKipAgODymSTW_gmXDF8kQ28s",
                        LicensePlateNumber = "29A08129",
                        ParkingAreaName = "A",
                        TimeIn = 1657990820,
                        TimeOut = 1658005220,
                    };
                    if (message != null)
                    {
                        BackImageLink = message.BackImageLink;
                        FrontImageLink = message.FrontImageLink;
                        LicensePlateNumber = message.LicensePlateNumber;
                        ParkingAreaName = message.ParkingAreaName;
                        TimeIn = UnixTimestamp.UnixTimestampToDateTime(message.TimeIn).ToString();
                        TimeOut = UnixTimestamp.UnixTimestampToDateTime(message.TimeOut).ToString();
                    }

                    System.Threading.Thread.Sleep(1000);
                }

            });
        }
        //private async Task LoadDataAsync(RabbitMqDispatcher messageDispatcher, string preprocessQueue)
        //{
        //    while (true)
        //    {
        //        var message = messageDispatcher.Dequeue<RabbitMQMessage>(preprocessQueue);
        //        //var message = new RabbitMQMessage
        //        //{
        //        //    BackImageLink = "https://drive.google.com/uc?export=view&id=1ojh3ABPB6LEh1tCQTyYt_kfjkn4otWa2",
        //        //    FrontImageLink = "https://drive.google.com/uc?export=view&id=1ojh3ABPB6LEh1tCQTyYt_kfjkn4otWa2",
        //        //    LicensePlateNumber = "60A-999.99",
        //        //    ParkingAreaName = "A",
        //        //    TimeIn = 1657990820,
        //        //    TimeOut = 1658005220,
        //        //};
        //        if (message != null)
        //        {
        //            BackImageLink = message.BackImageLink;
        //            FrontImageLink = message.FrontImageLink;
        //            LicensePlateNumber = message.LicensePlateNumber;
        //            ParkingAreaName = message.ParkingAreaName;
        //            TimeIn = UnixTimestamp.UnixTimestampToDateTime(message.TimeIn).ToString();
        //            TimeOut = UnixTimestamp.UnixTimestampToDateTime(message.TimeOut).ToString();
        //            //gửi message mở barrier
        //            using (var messageDispatcher = PersistenceFactory.GetMessageDispatcher())
        //            {
        //                var scheduleMessage = new RabbitMQMessage
        //                {
        //                    CarId = "",
        //                    BackImageLink = "https://drive.google.com/uc?export=view&id=1ojh3ABPB6LEh1tCQTyYt_kfjkn4otWa2",
        //                    FrontImageLink = "https://drive.google.com/uc?export=view&id=1ojh3ABPB6LEh1tCQTyYt_kfjkn4otWa2",
        //                    LicensePlateNumber = "60A-999.99",
        //                    ParkingAreaName = "A",
        //                    TimeIn = 1657990820,
        //                    TimeOut = 1658005220,
        //                };

        //                using (var rabbitMqQueues = PersistenceFactory.GetAppConfig())
        //                {
        //                    //"wpf-manage-queue"
        //                    messageDispatcher.Enqueue<RabbitMQMessage>(rabbitMqQueues.GetAppConfig().WPFManageQueue, scheduleMessage);
        //                }
        //            }

        //            //update lại database
        //            var car = new Car()
        //            {
        //                Id = message.CarId,
        //                BackImageLink = message.BackImageLink,
        //                FrontImageLink = message.FrontImageLink,
        //                LicensePlateNumber = message.LicensePlateNumber,
        //                ParkingAreaId = message.ParkingAreaId,
        //                TimeIn = message.TimeIn,
        //                TimeOut = message.TimeOut
        //            };
        //            var client = new RestClient();
        //            //var barrer = "BQBuTEaFoPc9n13ioks_CssZL70RHwu3DtC3KxOPH5_kLVwdlrZXIBGY5e5DJGUPiuWsiD1kF5nhmTYgEJ_JmZn6gyuK1p_st6C_yFbdZ3ZqeSYrMLBTwZ3i-foLdedmpYQZliEwh2lOz54EPQ0znX9FGYD5xUv8PEg";
        //            //client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(barrer, "Bearer");
        //            var request = new RestRequest("http://smartparking.local:5555/api/car/out", Method.Post);
        //            request.AddHeader("Accept", "application/json");
        //            request.AddHeader("Content-Type", "application/json");
        //            request.AddParameter("application/json", car, ParameterType.RequestBody);
        //            var response = await client.PostAsync(request);
        //            if (!response.IsSuccessful)
        //            {
        //                MessageBox.Show("Update data false!");
        //            }
        //        }

        //        System.Threading.Thread.Sleep(1000);
        //    }

        //}
    }
}
