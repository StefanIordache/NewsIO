using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsIO.Api.Utils;
using NewsIO.Data.Models.Application;
using NewsIO.Services.Intefaces;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;
using static NewsIO.Api.Utils.Models;

namespace NewsIO.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        public ICategoryService CategoryService { get; set; }

        public CategoriesController(ICategoryService categoriesService)
        {
            CategoryService = categoriesService;
        }

        // GET - /api/Categories/{pageSize?}/{pageNo?}
        [HttpGet]
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

                return Ok(categories);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET - /api/Categories/{id}
        [HttpGet("{id}")]
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

        // POST - /api/Categories/add
        [Authorize(Roles = "Administrator")]
        [HttpPost("add")]
        public async Task<IActionResult> AddNewCategory([FromBody] Category category)
        {
            try
            {
                var tokenString = Request.Headers["Authorization"].ToString();

                int categoryId = await CategoryService.AddAsync(category);

                if (!string.IsNullOrEmpty(tokenString))
                {
                    await CategoryService.PublishEntity<Category>(categoryId, JwtHelper.GetUserIdFromJwt(tokenString), JwtHelper.GetUserNameFromJwt(tokenString));
                }

                return Ok(new Response
                {
                    Status = ResponseType.Successful,
                    Value = category
                });

            }
            catch
            {
                return Ok(new Response { Status = ResponseType.Failed });
            }
        }

        // PUT - /api/Categories/edit/{id}
        [Authorize(Roles = "Administrator")]
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> EditCategory(int id, [FromBody] Category category)
        {
            try
            {
                var tokenString = Request.Headers["Authorization"].ToString();

                var updatedEntry = await CategoryService.GetByIdAsync<Category>(id);

                if (updatedEntry == null)
                {
                    return NotFound();
                }

                await CategoryService.UpdateAsync(id, category);

                if (!string.IsNullOrEmpty(tokenString))
                {
                    await CategoryService.UpdateLastEdit<Category>(id, JwtHelper.GetUserIdFromJwt(tokenString), JwtHelper.GetUserNameFromJwt(tokenString));
                }

                return Ok(new Response
                {
                    Status = ResponseType.Successful,
                    Value = category
                });
            }
            catch
            {
                return Ok(new Response { Status = ResponseType.Failed });
            }
        }

        // POST - api/Categories/Delete/{id}
        [Authorize(Roles = "Administrator")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                await CategoryService.Delete<Category>(id);

                return Ok(new Response
                {
                    Status = ResponseType.Successful
                });
            }
            catch
            {
                return Ok(new Response { Status = ResponseType.Failed });
            }
        }
    }
}
