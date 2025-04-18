//Xây dựng 2 api (get all product và get Product by id)
//getallproduct => 2 param : page index (số trang) - pageSize(số phần tử trên 1 trang) => ví dụ người dùng truyền pageindex = 0 -> pageSize= 10 => dòng 0 -> 10 .Skip(0).Take(10);
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using webapi_blazor.Models.EbayDB;

namespace webapi_blazor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly EbayContext _context;
        private readonly IMapper _mapper;
        public ProductController(EbayContext db, IMapper mapper)
        {
            _context = db;
            _mapper = mapper;
        }

        [Authorize(Roles = "Buyer")]
        [HttpGet("/product/getall")]
        public async Task<IActionResult> getAll(int pageIndex = 0, int pageSize = 10)
        {
            //linq
            var productList = _context.Products.Skip(pageIndex * pageSize).Take(pageSize);
            return Ok(productList);
        }

        [HttpGet("/product/getallsqlraw")]
        public async Task<IActionResult> getAllSQLRaw(int pageIndex = 0, int pageSize = 10)
        {
            int pageNumber = 0;
            bool isNumber = int.TryParse(pageIndex.ToString(), out pageNumber);
            if (isNumber)
            {
                string sqlCommand = $"select * from products order by id offset  {pageNumber * pageSize} rows fetch next {pageSize} rows only;";
                //linq
                var productList = _context.Products.FromSqlRaw(sqlCommand).ToList();
                return Ok(productList);
            }
            return BadRequest("Tham số không hợp lệ !");
        }

        [HttpGet("/product/getProductListCategory")]
        public async Task<IActionResult> GetProductListCategory(int pageIndex = 0, int pageSize = 10, string sortOption = "", string category = "")
        {
            var res = _context.ProductListCategories.AsQueryable();
            if (!string.IsNullOrEmpty(category))
            {
                res = res.Where(p => p.Category == category);
            }
            if (sortOption == "low")
            {
                res = res.OrderBy(p => p.Price);
            }
            else if (sortOption == "high")
            {
                res = res.OrderByDescending(p => p.Price);
            }
            else
            {
                res = res.OrderBy(p => p.Id);
            }
            var data = await res.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();
            return Ok(data);
        }

        [HttpGet("/product/getProductEbayDB")]
        public async Task<IActionResult> GetProductEbayDB(int pageIndex = 0, int pageSize = 10, string sortOption = "", string category = "")
        {
            var res = _context.EbayProducts.AsQueryable();
            if (!string.IsNullOrEmpty(category))
            {
                res = res.Where(p => p.Category == category);
            }
            if (sortOption == "low")
            {
                res = res.OrderBy(p => p.Price);
            }
            else if (sortOption == "high")
            {
                res = res.OrderByDescending(p => p.Price);
            }
            else
            {
                res = res.OrderBy(p => p.Id);
            }
            var data = await res.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();
            return Ok(data);
        }

        [HttpGet("/product/getbyid/{id}")]
        [OutputCache(Duration =60 , VaryByRouteValueNames = new [] {"id"})]
        public async Task<IActionResult> getById([FromRoute] int id)
        {
            var productDetail = _context.Products.SingleOrDefault(prod => prod.Id == id);
            if (productDetail != null)
            {
                return Ok(productDetail);
            }
            return BadRequest("mã sản phẩm không tồn tại");
        }
        [HttpGet("/GetDetailProductById/{id}")]
        public async Task<IActionResult> GetDetailProductById([FromRoute] int id)
        {
            //Cách 1: Tạo DBSet Class cho phần view của câu select
            // var result = await _context.Set<ProductDetailVM>().FromSqlRaw($@"
            // SELECT P.Id, P.Name, C.Name as 'Category', p.[Description], P.Price, P.CreatedAt, PI.ImageUrl as 'ListImage'
            // FROM Products P, Categories C, ProductImages PI
            // WHERE P.CategoryId = C.Id and PI.ProductId = P.Id AND P.Id = {id}
            // ").ToListAsync();
            //Cách 2: truy vấn thẳng db mà không qua dbset
            var result = await _context.Database.SqlQueryRaw<ProductDetailVM>($@"EXEC GetProductDetailImageListById {id}").ToListAsync();
            if (result.Count() == 0)
            {
                return BadRequest("mã sản phẩm không tồn tại");
            }
            var res = _mapper.Map<ProductDetailResultVM>(result.FirstOrDefault());
            return Ok(res);
        }
        [HttpGet("/GetAllUserRole")]
        public async Task<IActionResult> GetAllUserRole()
        {
            var lstUserRole = _context.UserRoles.Include(p => p.Role).Select(ul => new
            {
                id = ul.UserId,
                Role = ul.Role
            });
            return Ok(lstUserRole);
        }
    }
}

