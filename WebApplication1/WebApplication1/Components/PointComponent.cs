using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Components
{
    public class PointComponent
    {
    }
    public class Point
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public int TotalPoint { get; set; }
    }
    //모델 클래스
    public class PointLog
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public int NewPoint { get; set; }
        public DateTimeOffset Created { get; set; }
    }

    //포인트 상태 정보를 금은동으로 반환
    public class PointStatus
    {
        public int Gold { get; set; }
        public int Silver { get; set; }
        public int Bronze { get; set; }
    }

    //레퍼지토리 클래스 
    public interface IPointRepository
    {
        int GetTotalPointByUserId(int userId=1234);
        PointStatus GetPointStatusByUser();
    }

    public class PointRepository : IPointRepository
    {
        public PointStatus GetPointStatusByUser()
        {
            
            throw new NotImplementedException();
        }

        public int GetTotalPointByUserId(int userId=1234)
        {
            //TODO: 실제 데이터베이스 연동하는 코드
            return 1234;
        }
    }

    public class PointRepositoryInMemory : IPointRepository
    {
        public PointStatus GetPointStatusByUser()
        {
            return new PointStatus() { Gold = 10, Silver = 123, Bronze = 65 };
        }

        public int GetTotalPointByUserId(int userId = 1234)
        {
            return 1234;
        }
    }

    public interface IPointLogRepository
    {

    }
    public class PointLogRepository : IPointLogRepository
    {

    }

    public class PointController : Controller
    {
        private IPointRepository _repository;

        public PointController(IPointRepository repository)
        {
            _repository = repository;
        }
        public IActionResult Index()
        {
            var myPoint = _repository.GetTotalPointByUserId();
            ViewBag.MyPoint = myPoint;
            return View();
        }
    }
    [Route("api/[controller]")]
    public class PointServiceController : Controller
    {
        private IPointRepository _repository;

        public PointServiceController(IPointRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Get()
        {
            var myPoint = _repository.GetTotalPointByUserId();
            var json = new {point = myPoint };
            return Ok(json);
        }

        [HttpGet]
        [Route("{userId:int}")]
        public IActionResult Get(int userId)
        {
            var myPoint = _repository.GetTotalPointByUserId(userId);
            var json = new { point = myPoint };
            return Ok(json);
        }
    }

    public class PointLogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }

    [Route("api/[controller]")]
    public class PointLogServiceController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Get()
        {
            var json = new { point = 5678 };
            return Ok(json);
        }
    }
    //포인트 상태 정보를 반환하는 Web API
    [Route("api/[controller]")]
    public class PointStatusController : Controller
    {
        private IPointRepository _repository;

        public PointStatusController(IPointRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        [Route("")]
        public IActionResult Get()
        {
            var point = _repository.GetPointStatusByUser();
            return Ok(point);
        }
    }


}
