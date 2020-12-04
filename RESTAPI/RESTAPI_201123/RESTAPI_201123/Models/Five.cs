using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace RESTAPI_201123.Models
{
    //테이블과 1:1 매칭 시켜줌
    //모델클래스
    public class Five
    {
        public int Id { get; set; }

        [Required]
        public string Note { get; set; }
    }

    //인터페이스
    public interface IFiveRepository
    {
        Five Add(Five model);
        List<Five> GetAll();
        Five GetById(int id);
        Five Update(Five model);
        void Remove(int id);

        List<Five> GetAllWithPaging(int pageIndex, int pageSize =10);

        int GetRecordCount();//총 토탈 레코드 수
    }

    //인터페이스 상속 : 리파지터리 클래스
    public class FiveRepository : IFiveRepository
    {
        private IConfiguration _config;
        private IDbConnection db;

        //생성자
        public FiveRepository(IConfiguration config)
        {
            _config = config;//이 config를 통해 데이터베이스 문자열을 알 수 있다
            db = new SqlConnection(_config.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
        }
        //출력 메서드
        public List<Five> GetAll()
        {
            string sql = "Select * From Fives order By Id Desc";
            return db.Query<Five>(sql).ToList();
        }
        //입력 메서드
        public Five Add(Five model)
        {
            string sql = @"Insert Into Fives (Note) Values (@Note); Select Cast(SCOPE_IDENTITY() As Int);";
            var id = db.Query<int>(sql, model).Single();
            model.Id = id;
            return model;
        }
        //상세보기
        public Five GetById(int id)
        {
            string query = "Select * From Fives Where Id = @Id";
            return db.Query<Five>(query, new { Id = id}).Single();
        }
        //수정
        public Five Update(Five model)
        {
            var query = "Update Fives " + "Set " + " Note = @Note " + "Where Id = @Id ";//앞쪽에 공백은 넣어줘야함
            db.Execute(query, model);
            return model;
        }
        //삭제
        public void Remove(int id)
        {
            var query = "Delete From Fives Where Id = @Id";//앞쪽에 공백은 넣어줘야함
            db.Execute(query, new { Id = id});
        }

        public List<Five> GetAllWithPaging(int pageIndex, int pageSize = 10)
        {
            string sql = @"
                        Select Id, Note
                        From 
                            (
                                Select Row_Number() Over (Order By Id Desc) As RowNumbers, Id, Note
                                From Fives
                            ) As TempRowTables

                        Where 
                            RowNumbers
                                Between
                                    (@PageIndex * @PageSize + 1)
                                And
                                    (@PageIndex + 1) * @PageSize
                         ";
            return db.Query<Five>(sql, new { PageIndex = pageIndex, PageSize = pageSize }).ToList();
        }
        //레코드 카운트
        public int GetRecordCount()
        {
            string query = "Select Count(*) From Fives";
            return db.Query<int>(query).FirstOrDefault();
        }
    }

    //컨벤션 기반 라우팅 대신에 어트리뷰트 라우팅 추천
    [Route("api/[controller]")]
    public class FiveServiceController : ControllerBase
    {
        private IFiveRepository _repository;

        public FiveServiceController(IFiveRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var fives = _repository.GetAll();
                if(fives == null)
                {
                    return NotFound($"아무런 데이터가 없습니다");
                }
                else
                {
                    return Ok(fives);
                }
            }
            catch (Exception)
            {

                return BadRequest(); 
            }
        }

        [HttpGet("{id}", Name = "GetById")] //이름 추가
        public IActionResult Get(int id)
        {
            try
            {
                var model = _repository.GetById(id);
                if(model == null)
                {
                    return NotFound($"{id}번 데이터가 없습니다.");
                }
                return Ok(model);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpPost]
        [Produces("application/json",Type = typeof(Five))]
        [Consumes("application/json")]
        public IActionResult Post([FromBody]Five model)
        {
            try
            {
                if (model.Note == null || model.Note.Length < 1)
                {
                    ModelState.AddModelError("Note", "노트를 입력해야 합니다");
                }

                //모델 유효성 검사
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState); //400에러 출력
                }

                var m = _repository.Add(model);

                if (DateTime.Now.Second % 2 == 0) //둘 중 원하는 방식 사용 2가지 방식이 있어서
                {
                    //return CreatedAtAction("GetById", new { id = m.Id }, m);
                    return CreatedAtRoute("GetById", new { id = m.Id }, m); //201
                }
                else
                {
                    var uri = Url.Link("GetById", new { id = m.Id });
                    return Created(uri, m); //201
                }

                //return Ok(m);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id:int}")] //HttpPatch == 부분 업데이트
        public IActionResult Put(int id, [FromBody] Five model)
        {
            if(model == null)
            {
                return BadRequest();
            }
            try
            {
                var oldModel = _repository.GetById(id);
                if(oldModel == null)
                {
                    return NotFound($"{id} 번 데이터가 없습니다");
                }
                model.Id = id;
                _repository.Update(model);
                //return Ok(model); //이렇게 해도되지만
                return NoContent(); // 이미 던져준 정보에 모든 값을 가지고 있기에
            }
            catch (Exception)
            {
                return BadRequest("데이터가 업데이트되지 않았습니다.");
            }
        }

        [HttpDelete("{id:int}")] //데코레이터 특성
        public IActionResult Delete(int id)
        {
            try
            {
                var oldModel = _repository.GetById(id);
                if (oldModel == null)
                {
                    return NotFound($"{id} 번 아이디가 없습니다");
                }
                _repository.Remove(id);
                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest("삭제할 수 없습니다.");
            }
        }

        [HttpGet("{pageNumber:int}/{pageSize:int}")] //이름 추가
        public IActionResult Get(int pageNumber = 1,int pageSize = 10)
        {
            try
            {
                var fives = _repository.GetAllWithPaging(pageNumber -1, pageSize);
                if (fives == null)
                {
                    return NotFound($"아무런 데이터가 없습니다");
                }
                else
                {
                    //헤더에 총 레코드 수를 담아서 출력
                    Response.Headers.Add("X-TotalRecoedCount", _repository.GetRecordCount().ToString());
                    return Ok(fives);
                }
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
    }

}
