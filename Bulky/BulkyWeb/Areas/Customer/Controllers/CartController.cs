﻿using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BulkyBookWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;

        public ShoppingCartVM ShoppingCartVM { get; set; }

        public CartController (IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Action Methods
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            ShoppingCartVM = new()
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(
                    u => u.ApplicationUserId == userId,
                    includeProperties: "Product"
                    ),
            };

            foreach (var cart in ShoppingCartVM.ShoppingCartList)
            {
                cart.Price = GetPriceBasedOnQuantity(cart);
                ShoppingCartVM.OrderTotal += (cart.Price * cart.Count);
            }

            return View(ShoppingCartVM);
        }

        public IActionResult Plus (int cartId)
        {
            var cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId);
            cartFromDb.Count += 1;
            _unitOfWork.ShoppingCart.Update(cartFromDb);
            _unitOfWork.Save();
            TempData["success"] = "Cart details updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Minus(int cartId)
        {
            var cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId);
            
            if (cartFromDb.Count <= 1)
            {
                // Remove item from cart
                _unitOfWork.ShoppingCart.Remove(cartFromDb);
                _unitOfWork.Save();
                TempData["success"] = "Item removed from cart successfully.";

            }
            else
            {
                // Update cart
                cartFromDb.Count -= 1;
                _unitOfWork.ShoppingCart.Update(cartFromDb);
                _unitOfWork.Save();
                TempData["success"] = "Cart details updated successfully.";
            }            
            
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int cartId)
        {
            var cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId);
            _unitOfWork.ShoppingCart.Remove(cartFromDb);
            _unitOfWork.Save();
            TempData["success"] = "Item removed from cart successfully.";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Summary ()
        {
            return View();
        }
        #endregion

        #region Helper Methods

        private double GetPriceBasedOnQuantity(ShoppingCart shoppingCart)
        {
            if (shoppingCart.Count <= 50)
            {
                return shoppingCart.Product.Price;
            }
            else
            {
                if (shoppingCart.Count <= 100)
                {
                    return shoppingCart.Product.Price50;
                }

                else
                {
                    return shoppingCart.Product.Price100;
                }
            }
        }
        #endregion
    }
}