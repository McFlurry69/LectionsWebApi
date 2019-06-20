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
    public class SubjectController : Controller
    {
        readonly SubjectsService _subjects = new SubjectsService();

        // GET: api/<controller>
        [HttpGet]
        public ActionResult<IEnumerable<SubjectModel>> Get()
        {
            JsonResult json = new JsonResult(_subjects.FindAllSubjects());
            return json;
        }
        
        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult<SubjectModel> Get(int id)
        {
            JsonResult json = new JsonResult(_subjects.FindSubjectById(id));
            return json;
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]SubjectModel value)
        {
            _subjects.AddSubject(value);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]SubjectModel value)
        {
            _subjects.UpdateSubject(value);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _subjects.DeleteSubjectById(id);
        }
    }
}
