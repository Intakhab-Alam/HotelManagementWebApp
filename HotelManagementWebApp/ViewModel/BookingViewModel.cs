using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HotelManagementWebApp.ViewModel
{
    public class BookingViewModel
    {
        [Display(Name = "Customer Name")]
        [Required(ErrorMessage = "Customer Name is required.")]
        public string CustomerName { get; set; }
        [Display(Name = "Customer Address")]
        [Required(ErrorMessage = "Customer Address is required.")]
        public string CustomerAddress { get; set; }
        [Display(Name = "Customer Phone Number")]
        [Required(ErrorMessage = "Customer Phone is required.")]
        public string CustomerPhone { get; set; }
        [Display(Name = "Booking From")]
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Booking From is required.")]
        public System.DateTime BookingFrom { get; set; }
        [Display(Name = "Booking To")]
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Booking To is required.")]
        public System.DateTime BookingTo { get; set; }
        [Display(Name = "Assign Room")]
        [Required(ErrorMessage = "Assign Room is required.")]
        public int AssignRoomId { get; set; }
        [Display(Name = "No Of Member")]
        [Required(ErrorMessage = "No Of Member is required.")]
        public int NoOfMembers { get; set; }
        public IEnumerable<SelectListItem> ListOfRooms { get; set; }
    }
}