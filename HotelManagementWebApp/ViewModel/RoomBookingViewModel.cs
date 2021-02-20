using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagementWebApp.ViewModel
{
    public class RoomBookingViewModel
    {
        public int BookingId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhone { get; set; }
        public System.DateTime BookingFrom { get; set; }
        public System.DateTime BookingTo { get; set; }
        public decimal RoomPrice { get; set; }
        public string RoomNumber { get; set; }
        public int NoOfMembers { get; set; }
        public decimal TotalAmount { get; set; }
        public int NumberOfDays { get; set; }
    }
}