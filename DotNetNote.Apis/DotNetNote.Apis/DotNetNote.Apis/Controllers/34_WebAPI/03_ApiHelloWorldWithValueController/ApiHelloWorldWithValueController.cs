using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DotNetNote.Apis.Controllers._34_WebAPI._03_ApiHelloWorldWithValueController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiHelloWorldWithValueController : ControllerBase
    {
        // GET: api/<ApiHelloWorldWithValueController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "안녕하세요", "반갑습니다" };
        }

        // GET api/<ApiHelloWorldWithValueController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ApiHelloWorldWithValueController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ApiHelloWorldWithValueController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ApiHelloWorldWithValueController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    public class Value
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }

    public class ApiHelloWorldDemoController : ControllerBase
    {
        public IActionResult Index()
        {

            string html = @"
<html>
    <head>
        <title>JQuery로 JSON 사용하기</title>
    </head>

    <body>
        <h1>Wed API 호출</h1>
        <div id='print></div>

        <script src='https://code.jquery.com/jquery-1.12.4.min.js'></script>
        < script>
            var API_URI='api/ApiHelloWorldWithValue';
$(function() {
$.getJSON(API_URI,function(data){
var str = '<dl>';
str += '</dl>';});
});
        </script>
    </body>
</html>";


            return new ContentResult() { Content = html, ContentType = "text/html; charset=utf-8" };
        }
    }
}
