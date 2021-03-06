﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RESTAPI_201123.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebApiFileUploadController : ControllerBase
    {
        private IWebHostEnvironment _environment;

        public WebApiFileUploadController(IWebHostEnvironment environment)
        {
            _environment = environment;//현재 루트폴더를 알 수 있다
        }

        //파일업로드
        [HttpPost]
        [Consumes("application/json","multipart/form-data")]
        //files 매개변수 이름은<input type="file" name="files"/>로 매치 해줘야함
        public async Task<IActionResult> Post(List<IFormFile> files)
        {
            //파일을 업로드할 폴더
            var uploadFolder = Path.Combine(_environment.WebRootPath, "files");
            
            foreach(var file in files)
            {
                if(file.Length > 0)
                {
                    //파일명
                    var fileName = Path.GetFileName(ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"'));

                    using (var fileStream = new FileStream(Path.Combine(uploadFolder,fileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }
            }
            return Ok(new { message = "OK"});
        }
    }
}
