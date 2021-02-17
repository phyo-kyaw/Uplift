using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uplift.DataAccess.Data.Repository;
using Uplift.DataAccess.Data.Repository.IRepository;
using Uplift.Extensions;
using Uplift.Models;
using Uplift.Models.ViewModels;
using Uplift.Utility;

namespace Uplift.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public CartVM Cart_VM { get; set; }
        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            Cart_VM = new CartVM()
            {
                OrderHeader = new Models.OrderHeader(),
                ServiceList = new List<Service>()
            };
        }
        public IActionResult Index()
        {
            if(HttpContext.Session.GetObject<List<int>>(SD.SessionCart) != null)
            {
                List<int> sessionList = new List<int>();
                sessionList = HttpContext.Session.GetObject<List<int>>(SD.SessionCart);
                foreach(int serviceId in sessionList)
                {
                    Cart_VM.ServiceList.Add(_unitOfWork.Service.GetFirstOrDefault(u => u.Id == serviceId, includeProperties: "Frequency,Category"));
                }
            }
            return View(Cart_VM);
        }

        public IActionResult Summary()
        {
            if (HttpContext.Session.GetObject<List<int>>(SD.SessionCart) != null)
            {
                List<int> sessionList = new List<int>();
                sessionList = HttpContext.Session.GetObject<List<int>>(SD.SessionCart);
                foreach (int serviceId in sessionList)
                {
                    Cart_VM.ServiceList.Add(_unitOfWork.Service.GetFirstOrDefault(u => u.Id == serviceId, includeProperties: "Frequency,Category"));
                }
            }
            return View(Cart_VM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Summary")]
        public IActionResult SummaryPOST()
        {
            if (HttpContext.Session.GetObject<List<int>>(SD.SessionCart) != null)
            {
                List<int> sessionList = new List<int>();
                sessionList = HttpContext.Session.GetObject<List<int>>(SD.SessionCart);
                Cart_VM.ServiceList = new List<Service>();
                foreach (int serviceId in sessionList)
                {
                    Cart_VM.ServiceList.Add(_unitOfWork.Service.GetFirstOrDefault(u => u.Id == serviceId, includeProperties: "Frequency,Category"));
                }
            }
            Cart_VM.OrderHeader.OrderDate = DateTime.Now;
            Cart_VM.OrderHeader.Status = SD.StatusSubmitted;
            Cart_VM.OrderHeader.Comments = "Just comments";
            Cart_VM.OrderHeader.ServiceCount = Cart_VM.ServiceList.Count;

            

            if (!ModelState.IsValid)
            {
                return View(Cart_VM);
            }
            else
            {

                _unitOfWork.OrderHeader.Add(Cart_VM.OrderHeader);
                _unitOfWork.save();
                foreach(var item in Cart_VM.ServiceList)
                {
                    OrderDetails orderDetails = new OrderDetails()
                    {
                        ServiceId = item.Id,
                        OrderHeaderId = Cart_VM.OrderHeader.Id,
                        ServiceName = item.Name,
                        Price = item.Price
                    };
                    _unitOfWork.OrderDetails.Add(orderDetails);
                    _unitOfWork.save();
                }
                HttpContext.Session.SetObject(SD.SessionCart, new List<int>());
                return RedirectToAction("OrderConfirmation", "Cart", new { id = Cart_VM.OrderHeader.Id });
            }
            
        }

        public IActionResult OrderConfirmation(int id)
        {
            return View(id);
        }

        public IActionResult Remove(int serviceId)
        {
            List<int> sessionList = new List<int>();
            sessionList = HttpContext.Session.GetObject<List<int>>(SD.SessionCart);
            sessionList.Remove(serviceId);
            HttpContext.Session.SetObject(SD.SessionCart, sessionList);

            return RedirectToAction(nameof(Index));

        }
    }
}
