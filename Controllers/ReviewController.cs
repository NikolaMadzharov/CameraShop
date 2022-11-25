﻿using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TechRentingSystem.Data.Models.Account;
using TechRentingSystem.Models.Review;
using TechRentingSystem.Repository.Repository;

namespace TechRentingSystem.Controllers
{
    using System.Security.Claims;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;

    public class ReviewController : BaseController
    {
       
        private readonly IUnitOfWork _unitOfWork;
        public ReviewController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult CreateReview(int productId)
        {


            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);


            var model = new AddReviewViewModel()
            {
                ApplicationUserId = UserId,
                CameraId = productId,
            };

            return this.View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddReview(AddReviewViewModel model)
        {
            model.DateOfPublication = DateTime.Now;

            if (!this.ModelState.IsValid)
            {
                return this.View("CreateReview", model);
            }

            await this._unitOfWork.Review.AddReview(model);
              
            return this.RedirectToAction("Details", "Camera", new { productId = model.CameraId });
        }


    }
}
