//using CameraShop.Core.Models.Order;
//using CameraShop.Core.Repository.IRepository;
//using CameraShop.Infrastructure.Data;
//using CameraShop.Infrastructure.Data.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Hosting;
//using System.Security.Claims;
//using TechRentingSystem.Infrastructure;

//namespace CameraShop.Controllers
//{
//    public class OrderController : Controller
//    {
//        private readonly IUnitOfWork _unitOfWork;
//        private readonly TechRentingDbContext _data;
//        [BindProperty]
//        public OrderDetailsVM OrderVM { get; set; }

//        public OrderController(IUnitOfWork unitOfWork,
//             TechRentingDbContext data)
//        {
//            _unitOfWork = unitOfWork;
//            _data = data;
         
//        }

//        public IActionResult Index()
//        {
            

//            return View();
//        }

//        //public IActionResult Details(int id)
//        //{
//        //    OrderVM = new OrderDetailsVM()
//        //    {
//        //        OrderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(u => u.Id == id,
//        //                                        includeProperties: "ApplicationUser"),
//        //        OrderDetails = _unitOfWork.OrderDetail.GetAll(o => o.OrderId == id, includeProperties: "Product")

//        //    };
//        //    return View(OrderVM);
//        //}
//        #region API CALLS
//        [HttpGet]
//        public IActionResult GetAll()
//        {
//            IEnumerable<OrderHeader> orderHeaders;
//             orderHeaders = _unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser");
//            return Json(new { data = orderHeaders });
//        }
//        #endregion

//    }
//}
