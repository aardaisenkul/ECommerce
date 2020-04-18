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
        public IActionResult ProfileSaveAction([FromBody]Data.DTO.Account_ProfileSaveAction_Request dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Bad Boy");
            }
            int? userId = HttpContext.Session.GetInt32("UserId");
            var user = _unitOfwork.UserRepository.Get((int)userId);   

            user.Name = dto.Name;
            user.Surname = dto.Surname;
            user.Email = dto.Email;
            _unitOfwork.UserRepository.Update(user);
            _unitOfwork.Complete();

            return new JsonResult(user);
        }
        public IActionResult ChangePassword()
        {
            return View();
        }
        public IActionResult ChangePasswordAction([FromBody] Data.DTO.Account_ChangePasswordAction_Request dto)
        {
            if(!ModelState.IsValid) return BadRequest("Kötü Çocuk");
            
            int userId = HttpContext.Session.GetInt32("UserId").Value;
            var user = _unitOfwork.UserRepository.GetById(userId);
            if(user.Password == Helper.CryptoHelper.Sha1(dto.Password))
            {
                user.Password = Helper.CryptoHelper.Sha1(dto.NewPassword);
                _unitOfwork.Complete();
            }
            else
            {
                return BadRequest("Şifre mevcut şifreniz ile aynı değil.");
            }
            return new JsonResult("ok");
        }
    }
}