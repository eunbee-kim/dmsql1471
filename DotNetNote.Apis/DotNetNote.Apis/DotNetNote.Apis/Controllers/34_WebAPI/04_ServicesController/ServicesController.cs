using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;


namespace DotNetNote.Apis.Controllers._34_WebAPI._04_ServicesController
{
    [Route("api/[Controller]")]//라우트 토큰
    [ApiController]
    public class ServicesController : ControllerBase //controller보다 가볍다 api에 최적화된
    {

        [HttpGet]
        [Produces("application/json")]
        public IEnumerable<string> Get()
        {
            return new string[] { "안녕", "반갑습니다" };
        }


        [HttpGet("{id?}")]//생략가능
        [HttpGet("{id=1000}")]//기본값세팅 값이 잇으면 그걸로 없으면 세팅된걸로
        [HttpGet("{id:int}")]//제약조건
        //public string Get([FromRoute] int id, [FromQuery] string query) //Get by id
        public IActionResult Get([FromRoute] int id, [FromQuery] string query)
        {
            //return $"넘어온값: {id}, {query}";
            return Ok(new Dto { Id = id, Text = $"값: {id}"});
        }


        [HttpPost]
        public IActionResult Post([FromBody] Dto value)
        {
            if (!ModelState.IsValid)
            {
                //throw new InvalidOperationException("잘못되었다");
                return BadRequest(ModelState); //400 bad request
            }

            //데이터 저장 후 identity값 반환
            value.Id++;

            return CreatedAtAction("Get", new { id = value.Id }, value);//201 created
        }

        
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Dto value)
        {
        }

        
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    public class Dto
    {
        public int Id { get; set; }

        [MinLength(5)]//최소 5자 이상이어
        public string Text { get; set; }
    }
}
