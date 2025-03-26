using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using webapi_blazor.Models.EbayDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

//using webapi_blazor.Models;

namespace webapi_blazor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly EbayContext _context;
        private readonly IMapper _mapper;
        public CategoryController(EbayContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("getallcategory")]
        public async Task<IActionResult> GetAllCategory()
        {
            return Ok(_context.Categories.ToList());
        }

    }
}