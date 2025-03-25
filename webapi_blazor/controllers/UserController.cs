using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi_blazor.Helper;
using webapi_blazor.models.EbayDB;
//using webapi_blazor.Models;

namespace webapi_blazor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly EbayContext _context;
        private readonly JwtAuthService _jwtService;
        public UserController(EbayContext db, JwtAuthService jwt)
        {
            _context = db;
            _jwtService = jwt;
        }

        [HttpPost("/user/login")]
        public async Task<IActionResult> Login(UserLoginVM userLogin)
        {
            //Tìm user trong db có username hoặc email
            var userCheckLogin = await _context.Users.SingleOrDefaultAsync(us => us.Username == userLogin.Account || us.Email == userLogin.Account);
            if (userLogin != null && PasswordHelper.VerifyPassword(userLogin.Password, userCheckLogin.PasswordHash)) //Nếu account có trong db (account có thể username hoặc email)
            {
                //Tạo token
                string token = _jwtService.GenerateToken(userCheckLogin);
                UserLoginResultVM usLoginResult = new UserLoginResultVM();
                usLoginResult.AccessToken = token;
                usLoginResult.Account = userLogin.Account;

                //Trả về kết quả là username và token
                return Ok(usLoginResult);
            }
            return BadRequest("Login fail");
        }

        [Authorize]
        [HttpGet("/user/GetProfile")]
        public async Task<IActionResult> GetProfile([FromHeader] string authorization)
        {
            string token = authorization.Replace("Bearer ", "");
            // string token = HttpContext.Request.Headers["Authorization"];
            string account = _jwtService.DecodePayloadToken(token);
            var user = _context.Users.SingleOrDefault(us => us.Username == account || us.Email == account);
            return Ok(user);
        }
        [Authorize(Roles = "Buyer,Seller")]
        [HttpGet("/user/PostNew")]
        public async Task<IActionResult> PostNew()
        {
            return Ok();
        }



        // [HttpGet("/user/getall")]
        // public IActionResult Get()
        // {
        //     return Ok(_context.UserGroups.Skip(0).Take(10));
        // }
        // [HttpGet("/user/getView")]
        // public IActionResult GetView()
        // {
        //     return BadRequest(_context.UserGroups.Skip(0).Take(10));
        // }

    }
}