using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TechRentingSystem.Data.Models.Account;
//using TechRentingSystem.Models.Review;
//using TechRentingSystem.Repository.Repository;

//namespace TechRentingSystem.Controllers
//{
//    public class ReviewController : BaseController
//    {
//        private readonly IUnitOfWork _unitOfWork;
//        public ReviewController(IUnitOfWork unitOfWork)
//        {
//            _unitOfWork = unitOfWork;
//        }

//        public IActionResult CreateReview(int id)
//        {
//            var claimsIdentity = (ClaimsIdentity)User.Identity;
//            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

//            var UserId = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == claim.Value);


//            var model = new AddReviewViewModel()
//            {
//              ApplicationUserId = UserId,
//                CameraId = id,
//            };

//            return this.View(model);
//        }
//    }
//}
