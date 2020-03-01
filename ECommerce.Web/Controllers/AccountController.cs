using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUnitOfWork _unitOfwork;
        public AccountController(IUnitOfWork unitOfWork)
        {
            _unitOfwork = unitOfWork;
        }
        public IActionResult Profile()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            var user = _unitOfwork.UserRepository.GetById((int)userId);
            return View(user);
        }
        public IActionResult ProfileSaveAction([FromBody]Data.DTOs.Account_ProfileSaveAction_Request account_ProfileSaveAction_Request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Bad Boy");
            }
            int? userId = HttpContext.Session.GetInt32("UserId");
            var user = _unitOfwork.UserRepository.Get((int)userId);   

            user.Name = account_ProfileSaveAction_Request.Name;
            user.Surname = account_ProfileSaveAction_Request.Surname;
            user.Email = account_ProfileSaveAction_Request.Email;
            _unitOfwork.UserRepository.Update(user);
            _unitOfwork.Complete();

            return new JsonResult(user);
        }
    }
}