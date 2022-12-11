using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using System.Security.Claims;
using TechRentingSystem.Infrastructure;
using CameraShop.Core.Repository.IRepository;
using CameraShop.Core.Models.Cart;
using CameraShop.Infrastructure.Data.Models;

namespace TechRentingSystem.Controllers
{


    public class CartController : BaseController
    {


        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public ShoppingCartViewModel shoppingCartVM { get; set; }
        public CartController(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
        }

        

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);



            shoppingCartVM = new ShoppingCartViewModel()
            {


                ListCart = _unitOfWork.ShoppingCart.GetAll(x => x.ApplicationUserId == claim.Value, includeProperties: "Camera"),
                OrderHeader = new()

            };
          
            foreach (var cart in shoppingCartVM.ListCart)
            {
                cart.Price = GetPriceBasedOnQuantity(cart.Count, cart.Camera.Price);
                shoppingCartVM.OrderHeader.OrderTotal = (cart.Price * cart.Count);

            }
            return View(shoppingCartVM);
        }



        public IActionResult Summary()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);



            shoppingCartVM = new ShoppingCartViewModel()
            {

                ListCart = _unitOfWork.ShoppingCart.GetAll(x => x.ApplicationUserId == claim.Value, includeProperties: "Camera"),
                OrderHeader = new()


            };
            shoppingCartVM.OrderHeader.ApplicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == claim.Value);

            shoppingCartVM.OrderHeader.FirstName = shoppingCartVM.OrderHeader.ApplicationUser.FirstName;
            shoppingCartVM.OrderHeader.LastName = shoppingCartVM.OrderHeader.ApplicationUser.LastName;
            shoppingCartVM.OrderHeader.Emal = shoppingCartVM.OrderHeader.ApplicationUser.Email;

            foreach (var cart in shoppingCartVM.ListCart)
            {
                cart.Price = GetPriceBasedOnQuantity(cart.Count, cart.Camera.Price);
                shoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
            }
            return View(shoppingCartVM);

        }

        [HttpPost]
        [ActionName("Summary")]
        [ValidateAntiForgeryToken]
        public IActionResult SummaryPost()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            shoppingCartVM.ListCart = _unitOfWork.ShoppingCart.GetAll(x => x.ApplicationUserId == claim.Value, includeProperties: "Camera");

            shoppingCartVM.OrderHeader.PaymentStatus = SD.PaymentStatusPending;
            shoppingCartVM.OrderHeader.OrderStatus = SD.StatusPending;
            shoppingCartVM.OrderHeader.OrderDate = System.DateTime.Now;
            shoppingCartVM.OrderHeader.ApplicationUserId = claim.Value;
            foreach (var cart in shoppingCartVM.ListCart)
            {
                cart.Price = GetPriceBasedOnQuantity(cart.Count, cart.Camera.Price);
                shoppingCartVM.OrderHeader.OrderTotal = cart.Price * cart.Count;
            }

            _unitOfWork.OrderHeader.Add(shoppingCartVM.OrderHeader);
            _unitOfWork.Save();
            foreach (var cart in shoppingCartVM.ListCart)
            {
                OrderDetails orderDetail = new()
                {
                    ProductId = cart.CameraId,
                    OrderId = shoppingCartVM.OrderHeader.Id,
                    Price = cart.Price,
                    Count = cart.Count
                };
                _unitOfWork.OrderDetail.Add(orderDetail);
                _unitOfWork.Save();
            }
            var domain = "https://localhost:7145/";
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                    "card",
                },
                LineItems = new List<SessionLineItemOptions>(),


                Mode = "payment",
                SuccessUrl = domain + $"cart/OrderConfirmation?id={shoppingCartVM.OrderHeader.Id}",
                CancelUrl = domain + $"cart/index",
            };

            foreach (var item in shoppingCartVM.ListCart)
            {
                var sessionLineItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.Price * 100),
                        Currency = "bgn",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Camera.Brand,
                        },
                    },
                    Quantity = item.Count,
                };
                options.LineItems.Add(sessionLineItem);

            }

            var service = new SessionService();
            Session session = service.Create(options);
            shoppingCartVM.OrderHeader.SessionId = session.Id;
            shoppingCartVM.OrderHeader.PaymentIntendId = session.PaymentIntentId;
            _unitOfWork.OrderHeader.UpdateStripePaymendId(shoppingCartVM.OrderHeader.Id, session.Id, session.PaymentIntentId);
            _unitOfWork.Save();
            Response.Headers.Add("Location", session.Url);
            return RedirectToAction("OrderConfirmation", "Cart", new { id = shoppingCartVM.OrderHeader.Id });
            //_unitOfWork.ShoppingCart.RemoveRange(shoppingCartVM.ListCart);
            //_unitOfWork.Save();
            //return RedirectToAction("Index", "Home");

        }

        public IActionResult OrderConfirmation(int id)
        {
            OrderHeader orderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(u => u.Id == id);
            var service = new SessionService();
            Session session = service.Get(orderHeader.SessionId);

            if (session.PaymentStatus.ToLower() == "paid")
            {
                _unitOfWork.OrderHeader.UpdateStatus(id, SD.StatusApproved, SD.PaymentStatusApproved);
                _unitOfWork.Save();
            }
            List<ShoppingCart> shoppingCarts = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == orderHeader.ApplicationUserId).ToList();
            _unitOfWork.ShoppingCart.RemoveRange(shoppingCarts);
            _unitOfWork.Save();
            return View(id);

        }


        public IActionResult Minus(int cartId)
        {

            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault
                            (c => c.id == cartId);

            if (cart.Count <=1 )
            {
           
                _unitOfWork.ShoppingCart.Remove(cart);
              
            
            }
            else
            {

                _unitOfWork.ShoppingCart.DecrementCount(cart, 1);
            }
            _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Plus(int cartID)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.id == cartID);
            _unitOfWork.ShoppingCart.IncrementCount(cart, 1);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Remove(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.id == cartId);
            _unitOfWork.ShoppingCart.Remove(cart);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }


        private decimal GetPriceBasedOnQuantity(decimal quantity, decimal price)
        {
            return price;
        }
    }

}




   


    


