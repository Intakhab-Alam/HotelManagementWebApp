using HotelManagementWebApp.Models;
using HotelManagementWebApp.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelManagementWebApp.Controllers
{
    public class RoomController : Controller
    {
        private HotelDBEntities objHotelDBEntities;
        public RoomController()
        {
            objHotelDBEntities = new HotelDBEntities();
        }
        // GET: Room
        public ActionResult Index()
        {
            RoomViewModel objRoomViewModel = new RoomViewModel();
            objRoomViewModel.ListOfBookingStatus = (from obj in objHotelDBEntities.BookingStatus
                                                    select new SelectListItem()
                                                    {
                                                        Text = obj.BookingStatus,
                                                        Value = obj.BookingStatusId.ToString()
                                                    }).ToList();
            objRoomViewModel.ListOfRoomType = (from obj in objHotelDBEntities.RoomTypes
                                               select new SelectListItem()
                                               {
                                                   Text = obj.RoomTypeName,
                                                   Value = obj.RoomTypeId.ToString()
                                               }).ToList();

            return View(objRoomViewModel);
        }
        [HttpPost]
        public ActionResult Index(RoomViewModel objRoomViewModel)
        {
            string message = String.Empty;
            string ImageUniqueName = String.Empty;
            string ActualImageName = String.Empty;

            if(objRoomViewModel.RoomId == 0)
            {
                ImageUniqueName = Guid.NewGuid().ToString();
                ActualImageName = ImageUniqueName + Path.GetExtension(objRoomViewModel.Image.FileName);

                objRoomViewModel.Image.SaveAs(Server.MapPath("~/RoomImages/" + ActualImageName));

                //objHotelDBEntities
                Room objRoom = new Room()
                {
                    RoomNumber = objRoomViewModel.RoomNumber,
                    RoomImage = ActualImageName,
                    RoomPrice = objRoomViewModel.RoomPrice,
                    BookingStatusId = objRoomViewModel.BookingStatusId,
                    RoomTypeId = objRoomViewModel.RoomTypeId,
                    RoomCapacity = objRoomViewModel.RoomCapacity,
                    RoomDescription = objRoomViewModel.RoomDescription,
                    IsActive = true
                };

                objHotelDBEntities.Rooms.Add(objRoom);
                message = "Added.";
            }
            else
            {
                Room objRoom = objHotelDBEntities.Rooms.Single(model => model.RoomId == objRoomViewModel.RoomId);
                if (objRoomViewModel.Image != null)
                {
                    ImageUniqueName = Guid.NewGuid().ToString();
                    ActualImageName = ImageUniqueName + Path.GetExtension(objRoomViewModel.Image.FileName);
                    objRoomViewModel.Image.SaveAs(Server.MapPath("~/RoomImages/" + ActualImageName));
                    objRoom.RoomImage = ActualImageName;
                }

                objRoom.RoomNumber = objRoomViewModel.RoomNumber;
                objRoom.RoomPrice = objRoomViewModel.RoomPrice;
                objRoom.BookingStatusId = objRoomViewModel.BookingStatusId;
                objRoom.RoomTypeId = objRoomViewModel.RoomTypeId;
                objRoom.RoomCapacity = objRoomViewModel.RoomCapacity;
                objRoom.RoomDescription = objRoomViewModel.RoomDescription;
                objRoom.IsActive = true;
                message = "Updated.";
            }

            objHotelDBEntities.SaveChanges();

            return Json( new { message = "Room Successfully " + message, success = true }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetAllRooms()
        {
            IEnumerable<RoomDetailsViewModel> listOfRoomDetailsViewModels =
               (from objRoom in objHotelDBEntities.Rooms
                join objBooking in objHotelDBEntities.BookingStatus on objRoom.BookingStatusId equals objBooking.BookingStatusId
                join objRoomType in objHotelDBEntities.RoomTypes on objRoom.RoomTypeId equals objRoomType.RoomTypeId
                where objRoom.IsActive == true
                select new RoomDetailsViewModel()
                {
                    RoomId = objRoom.RoomId,
                    RoomNumber = objRoom.RoomNumber,
                    RoomImage = objRoom.RoomImage,
                    RoomPrice = objRoom.RoomPrice,
                    BookingStatus = objBooking.BookingStatus,
                    RoomType = objRoomType.RoomTypeName,
                    RoomCapacity = objRoom.RoomCapacity,
                    RoomDescription = objRoom.RoomDescription
                }).ToList();

            return PartialView("_RoomDetailsPartial", listOfRoomDetailsViewModels);
        }
        [HttpGet]
        public JsonResult EditRoomDetails(int roomId)
        {
            var result = objHotelDBEntities.Rooms.Single(model => model.RoomId == roomId);
            return Json( result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult DeleteRoomDetails(int roomId)
        {
            Room objRoom = objHotelDBEntities.Rooms.Single(model => model.RoomId == roomId);
            objRoom.IsActive = false;
            objHotelDBEntities.SaveChanges();
            return Json( new { message = "Record Successfully Deleted", success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}