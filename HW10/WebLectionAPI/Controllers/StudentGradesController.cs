using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HelperLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using ConnectLibrary.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebLectionAPI.Controllers
{
    [Route("api/[controller]")]
    public class StudentGradesController : Controller
    {
        CommonService studentGrades = new CommonService();

        // GET: api/<controller>
        [HttpGet]
        public ActionResult<IEnumerable<IGrouping<string, CommonModel>>> Get()
        {
            JsonResult json = new JsonResult(studentGrades.FindStudentGrades());
            return json;
        }


        // GET api/<controller>/5
        [HttpGet("{studentName}")]
        public ActionResult<string> Get(string studentName)
        {
            JsonResult json = new JsonResult(studentGrades.GetAmountOfVisitedLections(studentName, false));
            return json;
        }
        
        // GET api/<controller>/5
        [HttpGet("{subject},{lecturer}")]
        public ActionResult<string> Get(string subject, string lecturer)
        {
            JsonResult json = new JsonResult(studentGrades.GetAmountOfVisitedLections(subject, lecturer, false));
            return json;
        }
    }
}
