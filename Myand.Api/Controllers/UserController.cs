using Microsoft.AspNetCore.Mvc;
using Myand.Api.ApiModels;
using Myand.Commons.Securities;
using Myand.Domain;
using Myand.Service;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Myand.Api.Controllers
{
	[Route("api/[controller]")]
	public class UserController : Controller
	{
		private readonly IMstUserService _userService;

		public UserController(IMstUserService userService)
		{
			_userService = userService;
		}

		[HttpGet]
		public IActionResult Get()
		{
			try
			{
				var data = _userService.GetAllActive();
				return Ok(new ApiOkResponse(data, data.Count()));
			}
			catch (Exception ex)
			{
				return BadRequest(new ApiResponse(400, ex.Message));
			}
		}

		// POST api/values
		[HttpPost]
		public async Task<IActionResult> Post()
		{
			try
			{
				var tmpData = new MstUser
				{
					Id = Guid.NewGuid(),
					Username = "Dummy with Asnyc",
					Password = Encryptor.Encrypt("dummy"),
					Name = "i'm dummy Asnyc",
					Status = true
				};

				// Create with non Async
				//_userService.Save(tmpData);

				// Create with Async
				await _userService.SaveAsync(tmpData);

				return Ok(new ApiResponse(200));
			}
			catch (Exception ex)
			{
				return BadRequest(new ApiResponse(400, ex.Message));
			}
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Put(Guid id)
		{
			try
			{
				var tmpData = _userService.GetAsync(x => x.Id == id).Result.SingleOrDefault();

				if (tmpData != null)
				{
					tmpData.Name = "Dummy with Asnyc updated";
					tmpData.Description = "updated";
					tmpData.Password = Encryptor.Encrypt(tmpData.Password);

					// Update with non Async
					//_userService.Update(tmpData);

					// Update with Async
					await _userService.UpdateAsync(tmpData);
					return Ok(new ApiResponse(200));
				}
				return Ok(new ApiResponse(404));
			}
			catch (Exception ex)
			{
				return BadRequest(new ApiResponse(400, ex.Message));
			}
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			try
			{
				var tmpData = _userService.GetAsync(x => x.Id == id).Result.SingleOrDefault();

				if (tmpData != null)
				{
					// Delete with non Async
					//_userService.Delete(tmpData.SingleOrDefault());

					// Delete with Async
					await _userService.DeleteAsync(tmpData);
					return Ok(new ApiResponse(200));
				}
				return Ok(new ApiResponse(404));

			}
			catch (Exception ex)
			{
				return BadRequest(new ApiResponse(400, ex.Message));
			}
		}

	}
}
