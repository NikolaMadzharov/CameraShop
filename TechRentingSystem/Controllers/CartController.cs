using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TechRentingSystem.Data;
using TechRentingSystem.Models.Cart;
using TechRentingSystem.Repository.IRepository;

namespace TechRentingSystem.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
      

        private readonly IUnitOfWork _unitOfWork;
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
                ListCart = _unitOfWork.ShoppingCart.GetAll(x => x.ApplicationUserId == claim.Value)
            };
            foreach( var cart in shoppingCartVM.ListCart)
            {
                cart.Price = GetPriceBasedOnQuantity(cart.Count, cart.Camera.Price);
                shoppingCartVM.CartTotal += (cart.Price * cart.Count);
            }
            return View(shoppingCartVM);
        }

        public IActionResult Minus(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.id == cartId);
            if (cart.Count <=1)
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

        public IActionResult Plus (int cartID)
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

        private decimal GetPriceBasedOnQuantity(double quantity, decimal price)
        {
            return price;
        }
    
    }

    
}
