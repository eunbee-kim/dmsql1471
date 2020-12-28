
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Test1.Interface;
using Test1.Models;

namespace Test1.Controllers
{
    [Route("api/[controller]/[action]")]
    public class UserController : Controller
    {
        IUserService userService;

        public UserController(IUserService _userService)
        {
            userService = _userService;
        }

        [HttpGet]
        public IActionResult Users()
        {
            try
            {
                var users = userService.GetAllUser();
                if (users == null)
                {
                    return NotFound($"데이터가 없습니다");
                }
                else
                {
                    return Ok(users);
                }

            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
        }
        [HttpGet("{key}",Name = "GetUserById")]
        public IActionResult UserDetails(string key)
        {
            try
            {
                var model = userService.GetUserById(key);
                if (model == null)
                {
                    return NotFound($"{key} 데이터가 없습니다.");
                }
                return Ok(model);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpPost]
        //[Produces("application/json", Type = typeof(User))]
        //[Consumes("application/json")]
        public IActionResult InsertUser([FromForm] User user)
        {
            //var user = new User();
            //JsonConvert.PopulateObject(values, user);
            try
            {
                if (user.Password == null || user.Password.Length < 1)
                {
                    ModelState.AddModelError("Password", "비밀번호를 입력해야 합니다");
                }

                //모델 유효성 검사
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState); //400에러 출력
                }

                var m = userService.AddUser(user);
                //return CreatedAtRoute("GetUserById", new { id = m.Id }, m); //201
                return Ok();

            }
            catch (Exception)
            {
                return BadRequest();
            }

        }


        //[HttpPut]
        //public IActionResult UpdateUser([FromForm]string key, [FromForm]User user)
        //{
        //    //var user = new User();
        //    //JsonConvert.PopulateObject(values, user);

        //    if (user == null)
        //    {
        //        return BadRequest();
        //    }
        //    try
        //    {
        //        var oldModel = userService.GetUserById(key);
        //        if (oldModel == null)
        //        {
        //            return NotFound($"{key} 번 데이터가 없습니다");
        //        }
        //        user.Id = key;
        //        userService.UpdateUser(user);
        //        return Ok(user); //이렇게 해도되지만
        //        //return NoContent(); // 이미 던져준 정보에 모든 값을 가지고 있기에
        //    }
        //    catch (Exception)
        //    {
        //        return BadRequest("데이터가 업데이트되지 않았습니다.");
        //    }
        //}

        [HttpPut]
        public IActionResult UpdateUser([FromForm] User user)
        {
            //var user = new User();
            //JsonConvert.PopulateObject(values, user);

            if (user == null)
            {
                return BadRequest();
            }
            try
            {
                var oldModel = userService.GetUserById(user.Id);
                if (oldModel == null)
                {
                    return NotFound($"{user.Id} 번 데이터가 없습니다");
                }

                userService.UpdateUser(user);
                return Ok(user); //이렇게 해도되지만
                //return NoContent(); // 이미 던져준 정보에 모든 값을 가지고 있기에
            }
            catch (Exception)
            {
                return BadRequest("데이터가 업데이트되지 않았습니다.");
            }
        }

        [HttpDelete("{key}")]
        public IActionResult DeleteUser(string key)
        {
            try
            {
                var oldModel = userService.GetUserById(key);
                if (oldModel.Id == null && oldModel.Age == 0)
                {
                    return NotFound($"{key} 번 아이디가 없습니다");
                }
                userService.RemoveUser(key);
                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest("삭제할 수 없습니다.");
            }
        }

    }
}
