using Final.LoginPage.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Final.LoginPage.ViewModel
{
    public class ParkingSpaceViewModel : INotifyPropertyChanged
    {
        #region Data Members

        /// <summary>
        /// The list of rectangles that is displayed in the ListBox.
        /// </summary>
        private ObservableCollection<RectangleViewModel> rectangles = new ObservableCollection<RectangleViewModel>();
        private int currentCarInParkingArea;
        public int CurrentCarInParkingArea
        {
            get
            {
                return currentCarInParkingArea;
            }
            set
            {
                if (currentCarInParkingArea == value)
                {
                    return;
                }

                currentCarInParkingArea = value;

                OnPropertyChanged("CurrentCarInParkingArea");
            }
        }
        private int maxCarInParkingSpace;
        public int MaxCarInParkingSpace
        {
            get
            {
                return maxCarInParkingSpace;
            }
            set
            {
                if (maxCarInParkingSpace == value)
                {
                    return;
                }

                maxCarInParkingSpace = value;

                OnPropertyChanged("MaxCarInParkingSpace");
            }
        }


        #endregion Data Members
        IParkingSpaceService parkingSpaceService = new ParkingSpaceService();
        IParkingAreaService parkingAreaService = new ParkingAreaService();
        public ParkingSpaceViewModel()
        {
            //
            // Populate the view model with some example data.
            //
            for (int i = 0; i < 12; i++)
            {
                var r1 = new RectangleViewModel(44 + i * 26, 30 , 20, 20, Colors.Green);
                r1.Id = i + 1;
                rectangles.Add(r1);
            }
            //while (true)
            //{
            //Task task = LoadDataAsync();
            ////Task.WhenAll(task);
            //Thread.Sleep(TimeSpan.FromSeconds(10));
            //}
            Task.Factory.StartNew(async () =>
            {
                System.Threading.Thread.Sleep(10);
                await LoadDataAsync();
            });

        }
        private async Task LoadDataAsync()
        {
            while (true)
            {
                try
                {
                    var parkingSpaces = await parkingSpaceService.LoadParkingDataAsync();
                    var parkingArea = await parkingAreaService.LoadParkingDataAsync("A");
                    CurrentCarInParkingArea = parkingArea.Current;
                    MaxCarInParkingSpace = parkingArea.Maximum;
                    foreach (var item in parkingSpaces)
                    {
                        var obj = rectangles.Where(o => o.Id.ToString() == item.Position.ToString()).FirstOrDefault();
                        obj.Color = item.State == 0 ? Colors.Green : Colors.Red;
                    }
                    System.Threading.Thread.Sleep(10000);
                }
                catch (Exception)
                {

                }
                
            }

        }

        /// <summary>
        /// The list of rectangles that is displayed in the ListBox.
        /// </summary>
        public ObservableCollection<RectangleViewModel> Rectangles
        {
            get
            {
                return rectangles;
            }
        }

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Raises the 'PropertyChanged' event when the value of a property of the view model has changed.
        /// </summary>
        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        /// <summary>
        /// 'PropertyChanged' event that is raised when the value of a property of the view model has changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }

}
