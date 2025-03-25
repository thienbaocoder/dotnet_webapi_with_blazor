//api-controller-async
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi_blazor.Models;
//using webapi_blazor.Models;

namespace webapi_blazor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [EnableCors("allow_origin")]
    public class DemoController : ControllerBase
    {
        private readonly StoreCybersoftContext _context = new StoreCybersoftContext();
        public DemoController()
        {
            // _context = db;
        }
        [HttpGet("HandleUser")]
        public async Task<IActionResult> HandleUser()
        {
            int b = 0;
            int res = 10 / b;
            return Ok(res);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll()
        {
            List<User> lstUser = _context.Users.ToList();
            return Ok(lstUser);
        }
        [HttpGet("GetAllSQLRaw")]
        public async Task<ActionResult> GetAllSQLRaw()
        {
            List<User> lstUser = _context.Users.FromSqlRaw("Select * From Users").ToList();
            return Ok(lstUser);
        }

        [HttpPost("AddUser")]
        public async Task<ActionResult> AddUser([FromBody] User newUser)
        {
            //linq:
            _context.Users.Add(newUser);
            _context.SaveChanges(); //Lưu vào db thật
            return Ok(_context.Users.ToList());
        }
        [HttpPost("AddUserRaw")]
        public async Task<ActionResult> AddUserRaw([FromBody] User newUser)
        {
            await _context.Database.ExecuteSqlRawAsync(
                $"INSERT INTO Users (Name, Age, Email) VALUES (N'{newUser.Name}', '{newUser.Age}', '{newUser.Email}')"
            );

            return Ok(await _context.Users.ToListAsync());
        }

        //Hoặc
        // [HttpPost("AddUserRaw")]
        // public async Task<ActionResult> AddUserRaw([FromBody] User newUser)
        // {

        //     string sqlCommand = $"INSERT INTO Users(Name,Age,Email,Additional) values(N'{newUser.Name}','{newUser.Age}','{newUser.Email}','{newUser.Additional}')";
        //     //linq:
        //     _context.Database.ExecuteSqlRaw(sqlCommand);

        //     return Ok(_context.Users.ToList());
        // }

        [HttpDelete("/delete/{id}")]
        public async Task<ActionResult> DeleteUser([FromRoute] int id)
        {
            User? usDelete = _context.Users.Find(id);
            if (usDelete != null)
            {
                _context.Users.Remove(usDelete);
                _context.SaveChanges();
            }
            return Ok(_context.Users.ToList());
        }


        [HttpDelete("/deleteraw/{id}")]
        public async Task<ActionResult> Deleteraw([FromRoute] int id)
        {
            string sqlCommand = $"DELETE FROM Users where Id={id}";
            //linq:
            _context.Database.ExecuteSqlRaw(sqlCommand);
            return Ok(_context.Users.ToList());
        }

        [HttpPut("/edit/{id}")]
        public async Task<ActionResult> UpdateUser([FromRoute] int id, [FromBody] User userEdit)
        {
            _context.Entry(userEdit).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok(_context.Users.ToList());
        }


        [HttpPut("/update/{id}")]
        public async Task<IActionResult> PutTModel([FromRoute] int id, [FromBody] User userEdit)
        {
            //Lấy ra thằng user trong csdl
            User? userUpdate = _context.Users.Find(id);
            if (userUpdate != null)
            {
                userUpdate.Name = userEdit.Name;
                userUpdate.Age = userEdit.Age;
                userUpdate.Email = userEdit.Email;
                _context.SaveChanges();
            }
            return Ok(_context.Users.ToList());

        }
    }
}