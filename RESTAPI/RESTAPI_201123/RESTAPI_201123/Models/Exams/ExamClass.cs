using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System;

namespace RESTAPI_201123.Models.Exams
{
    public class ExamClass
    {

    }
    
    //모델클래스
    public class Question
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
    }

    //인터페이스
    public interface IQuestionRepository
    {
        Question Add(Question model);
        List<Question> GetAll();
        Question GetById(int id);
        Question Update(Question model);
        void Remove(int id);

        List<Question> GetAllWithPaging(int pageIndex, int pageSize = 10);

        int GetRecordCount();//총 토탈 레코드 수
    }

    //리포지토리 클래스
    public class QuestionRepository : IQuestionRepository
    {
        private IConfiguration _config;
        private IDbConnection db;
        
        //DB 생성자
        public QuestionRepository(IConfiguration config)
        {
            _config = config;
            db = new SqlConnection(
                _config.GetSection("ConnectionStrings")
                .GetSection("DefaultConnection").Value);
        }
        //입력 메서드
        public Question Add(Question model)
        {
            string sql = @"
                    Insert Into Questions (Title) Values (@Title);
                    Select Cast(SCOPE_IDENTITY() As Int);";
            var id = db.Query<int>(sql, model).Single();
            model.Id = id;
            return model;
        }
        //출력 메서드
        public List<Question> GetAll()
        {
            string sql = "Select * From Questions order By Id Desc";
            return db.Query<Question>(sql).ToList();
        }
        //상세 메서드
        public Question GetById(int id)
        {
            string query = "Select * From Questions Where Id = @Id";
            return db.Query<Question>(query, new { Id = id }).Single();
        }
        //수정 메서드
        public Question Update(Question model)
        {
            var query = "Update Questions " + "Set " + " Title = @Title " + "Where Id = @Id ";//앞쪽에 공백은 넣어줘야함
            db.Execute(query, model);
            return model;
        }
        //삭제 메서드
        public void Remove(int id)
        {
            var query = "Delete From Questions Where Id = @Id";//앞쪽에 공백은 넣어줘야함
            db.Execute(query, new { Id = id });
        }
        //레코드 카운트 반환
        public int GetRecordCount()
        {
            string query = "Select Count(*) From Questions";
            return db.Query<int>(query).FirstOrDefault();
        }
        //페이징 처리된 리스트 출력 메서드
        public List<Question> GetAllWithPaging(int pageIndex, int pageSize = 10)
        {
            string sql = @"
                        Select Id, Title
                        From 
                            (
                                Select Row_Number() Over (Order By Id Desc) As RowNumbers, Id, Title
                                From Questions
                            ) As TempRowTables

                        Where 
                            RowNumbers
                                Between
                                    (@PageIndex * @PageSize + 1)
                                And
                                    (@PageIndex + 1) * @PageSize
                         ";
            return db.Query<Question>(sql, new { PageIndex = pageIndex, PageSize = pageSize }).ToList();
        }
    }

    //Dto 클래스
    public class QuestionDto
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(4000,ErrorMessage ="문제는 4000자 이하로 입력하세요.")]
        public string Title { get; set; }
    }

    //Web API 컨트롤러 클래스
    //컨벤션 기반 라우팅 대시에 어트리뷰터 라우팅 추천
    [Route("api/[controller]")]
    public class QuestionServiceController : Controller 
    {
        private IQuestionRepository _repository;

        public QuestionServiceController(IQuestionRepository repository)
        {
            _repository = repository;
        }
        //api/QuestionsService
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var model = _repository.GetAll();
                if (model == null)
                {
                    return NotFound($"아무런 데이터가 없습니다");
                }
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        //api/QuestionsService/{id}
        [HttpGet("{id}", Name = "GetQuestionById")] //이름 추가
        public IActionResult Get(int id)
        {
            try
            {
                var model = _repository.GetById(id);
                if (model == null)
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
        [Produces("application/json", Type = typeof(QuestionDto))]
        [Consumes("application/json")]
        public IActionResult Post([FromBody] QuestionDto model)
        {
            try
            {
                if (model.Title == null || model.Title.Length < 1)
                {
                    ModelState.AddModelError("Title", "문제를 입력해야 합니다");
                }

                //모델 유효성 검사
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState); //400에러 출력
                }
                // QuestionDto를 Question 모델로 변경해서 이포지터리에 전달
                var newModel = new Question { Id = model.Id, Title = model.Title };
                //넘어온 값 저장
                var m = _repository.Add(newModel);

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
        //api/QuestionService/{변경할 id}
        [HttpPut("{id:int}")] //HttpPatch == 부분 업데이트
        public IActionResult Put(int id, [FromBody] Question model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            try
            {
                var oldModel = _repository.GetById(id);
                if (oldModel == null)
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
        //Delete: api/QuestionService/{삭제할 id}
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
        public IActionResult Get(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                //페이지 번호는 1,2,3 사용, 리파지터리에서는 0,1,2 사용
                pageNumber = (pageNumber > 0) ? pageNumber - 1 : 0;
                var models = _repository.GetAllWithPaging(pageNumber, pageSize);
                if(models == null) 
                {
                    return NotFound($"아무런 데이터가 없습니다.");
                }
                //응답 헤더에 총 레코드 수를 담아서 출력
                Response.Headers.Add(
                    "X-TotalRecordCount", _repository.GetRecordCount().ToString());
                return Ok(models); //200
            }
            
            catch (Exception)
            {

                return BadRequest();
            }
        }
    }
}
