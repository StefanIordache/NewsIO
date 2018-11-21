using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsIO.Api.Utils;
using NewsIO.Data.Models.Application;
using NewsIO.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static NewsIO.Api.Utils.Models;

namespace NewsIO.Api.Controllers
{
    [Route("api/[controller]")]
    public class CategoriesController : Controller
    {
        public ICategoryService CategoryService { get; set; }

        public CategoriesController(ICategoryService _categoriesService)
        {
            CategoryService = _categoriesService;
        }

        // GET - /api/Categories/{pageSize?}/{pageNo?}
        [HttpGet]
        [Route("categories/{pageSize:int?}/{pageNo:int?}")]
        public async Task<IActionResult> GetCategoriesAsync(int pageSize = 0, int pageNo = 0)
        {
            try
            {
                IEnumerable<Category> categories;
                PagedResult<Category> pagedResult = new PagedResult<Category>();
                int numberOfRecords = 0;

                if (pageSize > 0)
                {
                    (categories, numberOfRecords) = await CategoryService.GetWithPaginationAsync<Category>(pageSize, pageNo);
                }
                else
                {
                    categories = await CategoryService.GetAllAsync<Category>();
                }

                if (categories == null || !categories.Any())
                {
                    return NotFound();
                }

                return Ok(new PagedResult<Category>(categories, pageSize, pageNo, numberOfRecords));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET - /api/Categories/{id}
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            try
            {
                Category category = await CategoryService.GetByIdAsync<Category>(id);

                if (category != null)
                {
                    return Ok(category);
                }

                return NotFound();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST - /api/Categories/addNewCategory
        [HttpPost("addNewCategory")]
        public async Task<IActionResult> AddNewCategory([FromBody] Category category)
        {
            try
            {
                await CategoryService.AddAsync(category);
                return Ok(new Response { Status = ResponseType.Successful,
                                        Value = category});

            }
            catch
            {
                return Ok(new Response { Status = ResponseType.Failed });
            }
        }

        // POST - /api/Categories/edit/{categoryId}
        [HttpPost("edit/{categoryId}")]
        public async Task<IActionResult> EditCategory(int categoryId, [FromBody] Category category)
        {
            try
            {
                await CategoryService.UpdateAsync(categoryId, category);
                return Ok(new Response { Status = ResponseType.Successful,
                                        Value = category });
            }
            catch
            {
                return Ok(new Response { Status = ResponseType.Failed});
            }
        }
    }
}
