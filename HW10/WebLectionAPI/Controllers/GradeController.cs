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
    public class GradeController : Controller
    {
        readonly GradesService _grades = new GradesService();

        // GET: api/<controller>
        [HttpGet]
        public ActionResult<IEnumerable<GradeModel>> Get()
        {
            JsonResult json = new JsonResult(_grades.FindAllGrades());
            return json;
        }
        
        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult<GradeModel> Get(int id)
        {
            JsonResult json = new JsonResult(_grades.FindGradeById(id));
            return json;
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]GradeModel value)
        {
            _grades.AddGrade(value);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]GradeModel value)
        {
            _grades.UpdateGrade(value);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _grades.DeleteGradeById(id);
        }
    }
}
