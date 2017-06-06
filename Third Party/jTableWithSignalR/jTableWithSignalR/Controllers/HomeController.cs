using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Hik.JTable.Models;
using Hik.JTable.Repositories;
using Hik.JTable.Sessions;
using SignalR.Hubs;

namespace jTableWithSignalR.Controllers
{
    public class HomeController : Controller
    {
        private static readonly Random _rnd = new Random();

        private readonly IRepositoryContainer _repository;

        public HomeController()
        {
            _repository = ApplicationRepository.GetRepository(RepositorySize.Small, "realtime");
        }

        public ActionResult Index()
        {
            ViewBag.ClientName = "user-" + _rnd.Next(10000, 99999);
            return View();
        }

        [HttpPost]
        public JsonResult StudentList()
        {
            try
            {
                List<Student> students = _repository.StudentRepository.GetAllStudents();
                return Json(new { Result = "OK", Records = students });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult CreateStudent(Student student)
        {
            try
            {
                //Validation
                if (!ModelState.IsValid)
                {
                    return Json(new { Result = "ERROR", Message = "Form is not valid! Please correct it and try again." });
                }

                Student addedStudent = _repository.StudentRepository.AddStudent(student);

                //Inform all connected clients
                var clientName = Request["clientName"];
                Task.Factory.StartNew(
                    () =>
                    {
                        var clients = Hub.GetClients<RealTimeJTableDemoHub>();
                        clients.RecordCreated(clientName, student);
                    });

                //Return result to current (caller) client
                return Json(new { Result = "OK", Record = addedStudent });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdateStudent(Student student)
        {
            try
            {
                //Validation
                if (!ModelState.IsValid)
                {
                    return Json(new { Result = "ERROR", Message = "Form is not valid! Please correct it and try again." });
                }

                //Update in the database
                _repository.StudentRepository.UpdateStudent(student);

                //Inform all connected clients
                var clientName = Request["clientName"];
                Task.Factory.StartNew(
                    () =>
                    {
                        var clients = Hub.GetClients<RealTimeJTableDemoHub>();
                        clients.RecordUpdated(clientName, student);
                    });

                //Return result to current (caller) client
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteStudent(int studentId)
        {
            try
            {
                //Delete from database
                _repository.StudentRepository.DeleteStudent(studentId);

                //Inform all connected clients
                var clientName = Request["clientName"];
                Task.Factory.StartNew(
                    () =>
                    {
                        var clients = Hub.GetClients<RealTimeJTableDemoHub>();
                        clients.RecordDeleted(clientName, studentId);
                    });

                //Return result to current (caller) client
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult GetCityOptions()
        {
            try
            {
                var cities = _repository.CityRepository.GetAllCities().Select(c => new { DisplayText = c.CityName, Value = c.CityId });
                return Json(new { Result = "OK", Options = cities });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
    }
}
