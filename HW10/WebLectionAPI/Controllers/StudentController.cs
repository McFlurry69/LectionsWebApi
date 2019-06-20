using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using HelperLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using ConnectLibrary.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebLectionAPI.Controllers
{
    [Route("api/[controller]")]
    public class StudentController : Controller
    {
        readonly StudentsService _student = new StudentsService();

        // GET: api/<controller>
        [HttpGet]
        public ActionResult<IEnumerable<StudentModel>> Get()
        {
            JsonResult json = new JsonResult(_student.FindAllStudents());
            return json;
        }
        
        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult<StudentModel> Get(int id)
        {
            JsonResult json = new JsonResult(_student.FindStudentById(id));
            return json;
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]StudentModel value)
        {
            _student.AddStudent(value);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]StudentModel value)
        {
            _student.UpdateStudentInfo(value);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _student.ExcludeStudent(id);
        }
    }
}
