using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.LoginPage.Model.Entities
{
    public class RabbitMQMessage
    {
        public string CarId { get; set; }
        public string FrontImageLink { get; set; }
        public string BackImageLink { get; set; }
        public string LicensePlateNumber { get; set; }
        public string ParkingAreaName { get; set; }
        public string ParkingAreaId { get; set; }
        public long TimeIn { get; set; }
        public long TimeOut { get; set; }
        public int Occupied { get; set; }
        public int Available { get; set; }
    }
}
