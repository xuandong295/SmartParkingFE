using Final.LoginPage.Common.Command;
using Final.LoginPage.Helper;
using Final.LoginPage.Model;
using Newtonsoft.Json;
using Prism.Mvvm;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static Final.LoginPage.Helper.ConstantHelper;

namespace Final.LoginPage.ViewModel
{
    public class HistoryViewModel : BindableBase
    {
        public const string NEWEST = "Newest";
        public const string ALL_CAR_ON_DATE = "All Car On Date";
        public const string ALL_THE_TIME = "All The Time";
        public ICommand ButtonCommand { get; set; }
        public string licensePlate;
        public string LicensePlate
        {
            get { return licensePlate; }

            set
            {
                if (licensePlate != value)
                {
                    licensePlate = value;
                    RaisePropertyChanged("LicensePlate");
                }
            }
        }
        public string selectedDate = DateTime.Now.AddDays(1).ToString();
        public string SelectedDate
        {
            get { return selectedDate; }

            set
            {
                if (selectedDate != value)
                {
                    selectedDate = value;
                    RaisePropertyChanged("SelectedDate");
                }
            }
        }
        public string selectedTypeSearch { get; set; }
        public string SelectedTypeSearch
        {
            get { return selectedTypeSearch; }

            set
            {
                if (selectedTypeSearch != value)
                {
                    selectedTypeSearch = value;
                    RaisePropertyChanged("SelectedTypeSearch");
                }
            }
        }
        private ObservableCollection<string> option;

        public ObservableCollection<string> Options
        {
            get { return option; }
            set { option = value; }
        }
        private Visibility tableVisibility;

        public Visibility TableVisibility
        {
            get { return tableVisibility; }
            set
            {
                if (tableVisibility != value)
                {
                    tableVisibility = value;
                    RaisePropertyChanged("TableVisibility");
                }
            }
        }

        public HistoryViewModel()
        {
            ButtonCommand = new RelayCommand(o => MainButtonClick(selectedTypeSearch));
            listDataGrid = new ObservableCollection<Car>();
            Options = new ObservableCollection<string>();
            Options.Add(NEWEST);
            Options.Add(ALL_CAR_ON_DATE);
            Options.Add(ALL_THE_TIME);
            SelectedTypeSearch = ALL_CAR_ON_DATE;
            TableVisibility = Visibility.Hidden;

        }

        private void MainButtonClick(object sender)
        {
            TableVisibility = Visibility.Visible;
            if (selectedTypeSearch == NEWEST)
            {
                var client = new RestClient();
                var request = new RestRequest($"http://smartparking.local:5555/api/Car/car-information?licensePlate={LicensePlate}", Method.Get);
                request.AddHeader("Accept", "application/json");
                request.AddHeader("Content-Type", "application/json");
                //var body = new tblUser() { UserName = "admin", Password = "admin" };
                //request.AddParameter("application/json", body, ParameterType.RequestBody);
                var response = client.Get(request);
                var data = JsonConvert.DeserializeObject<InternalAPIResponseCode>(response.Content);
                var carJson = data.Data.ToString();
                var currentCar = JsonConvert.DeserializeObject<Car>(carJson);
                currentCar.DateTimeIn = UnixTimestamp.UnixTimestampToDateTime(currentCar.TimeIn);
                currentCar.DateTimeOut = UnixTimestamp.UnixTimestampToDateTime(currentCar.TimeOut);
                listDataGrid.Clear();
                listDataGrid.Add(currentCar);
            }
            if (selectedTypeSearch == ALL_CAR_ON_DATE)
            {
                var client = new RestClient();

                var index = SelectedDate.IndexOf(" ");
                //var date = SelectedDate.Substring(index);
                var date = SelectedDate.ToString().Remove(index).Replace("/", "-");
                var request = new RestRequest($"http://smartparking.local:5555/api/Car/car-information-on-date?date={date}", Method.Get);
                request.AddHeader("Accept", "application/json");
                request.AddHeader("Content-Type", "application/json");
                //var body = new tblUser() { UserName = "admin", Password = "admin" };
                //request.AddParameter("application/json", body, ParameterType.RequestBody);
                var response = client.Get(request);
                var data = JsonConvert.DeserializeObject<InternalAPIResponseCode>(response.Content);
                var listCarJson = data.Data.ToString();
                var listCar = JsonConvert.DeserializeObject<List<Car>>(listCarJson);
                listDataGrid.Clear();
                foreach (var item in listCar)
                {
                    item.DateTimeIn = UnixTimestamp.UnixTimestampToDateTime(item.TimeIn);
                    item.DateTimeOut = UnixTimestamp.UnixTimestampToDateTime(item.TimeOut);
                    listDataGrid.Add(item);
                }
            }
            if (selectedTypeSearch == ALL_THE_TIME)
            {
                var client = new RestClient();
                var request = new RestRequest($"http://smartparking.local:5555/api/Car/car-history?licensePlate={LicensePlate}", Method.Get);
                request.AddHeader("Accept", "application/json");
                request.AddHeader("Content-Type", "application/json");
                //var body = new tblUser() { UserName = "admin", Password = "admin" };
                //request.AddParameter("application/json", body, ParameterType.RequestBody);
                var response = client.Get(request);
                var data = JsonConvert.DeserializeObject<InternalAPIResponseCode>(response.Content);
                var listCarJson = data.Data;

                var listCar = JsonConvert.DeserializeObject<List<Car>>(listCarJson.ToString());
                listDataGrid.Clear();
                foreach (var item in listCar)
                {
                    item.DateTimeIn = UnixTimestamp.UnixTimestampToDateTime(item.TimeIn);
                    
                        item.DateTimeOut = UnixTimestamp.UnixTimestampToDateTime(item.TimeOut);
                    
                    
                    listDataGrid.Add(item);
                }
            }
        }

        private ObservableCollection<Car> listDataGrid { get; set; }

        public ObservableCollection<Car> ListDataGrid
        {
            get { return listDataGrid; }

            set
            {
                if (listDataGrid != value)
                {
                    listDataGrid = value;
                    RaisePropertyChanged("ListDataGrid");
                }
            }
        }
    }
}
