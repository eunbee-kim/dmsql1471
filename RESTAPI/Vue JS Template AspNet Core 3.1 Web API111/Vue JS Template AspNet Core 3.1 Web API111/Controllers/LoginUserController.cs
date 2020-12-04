using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vue_JS_Template_AspNet_Core_3._1_Web_API111.Interface;
using Vue_JS_Template_AspNet_Core_3._1_Web_API111.Models;

namespace Vue_JS_Template_AspNet_Core_3._1_Web_API111.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class LoginUserController : ControllerBase
    {
        ILoginUserService loginUserService;

        public LoginUserController(ILoginUserService _loginUserService)
        {
            loginUserService = _loginUserService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var users = loginUserService.GetAllLoginUser();
                if (users == null)
                {
                    return NotFound($"데이터가 없습니다");
                }
                else
                {
                    return Ok(users);
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }

        }
        //public IActionResult Get()
        //{
        //    LoginUser user = new LoginUser();
        //    user.Id = "dmsql1471";
        //    user.Name = "dmsql";
        //    user.Password = "cpfl1023";

        //    return Ok(user);
        //}

        [HttpGet("{id}", Name = "GetLoginUserById")]
        public IActionResult Get(string id)
        {
            try
            {
                var model = loginUserService.GetLoginUserById(id);
                if (model == null)
                {
                    return NotFound($"{id} 데이터가 없습니다.");
                }
                return Ok(model);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        //[Authorize]
        [HttpPost]
        [Produces("application/json", Type = typeof(LoginUser))]
        [Consumes("application/json")]
        public IActionResult Post([FromBody] LoginUser loginUser)
        {
            try
            {
                if (loginUser.Password == null || loginUser.Password.Length < 1)
                {
                    ModelState.AddModelError("Password", "비밀번호를 입력해야 합니다");
                }

                //모델 유효성 검사
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState); //400에러 출력
                }

                var m = loginUserService.AddLoginUser(loginUser);
                return CreatedAtRoute("GetLoginUserById", new { id = m.Id }, m); //201

            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id,[FromBody] LoginUser loginUser)
        {
            if (loginUser == null)
            {
                return BadRequest();
            }
            try
            {
                var oldModel = loginUserService.GetLoginUserById(id);
                if (oldModel == null)
                {
                    return NotFound($"{id} 번 데이터가 없습니다");
                }
                loginUser.Id = id;
                loginUserService.UpdateLoginUser(loginUser);
                return Ok(loginUser); //이렇게 해도되지만
                //return NoContent(); // 이미 던져준 정보에 모든 값을 가지고 있기에
            }
            catch (Exception)
            {
                return BadRequest("데이터가 업데이트되지 않았습니다.");
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                var oldModel = loginUserService.GetLoginUserById(id);
                if (oldModel == null)
                {
                    return NotFound($"{id} 번 아이디가 없습니다");
                }
                loginUserService.Remove(id);
                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest("삭제할 수 없습니다.");
            }
        }
    }
}
