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
    public class LecturerController : Controller
    {
        readonly LecturersService _lecturer = new LecturersService();

        // GET: api/<controller>
        [HttpGet]
        public ActionResult<IEnumerable<LecturerModel>> Get()
        {
            JsonResult json = new JsonResult(_lecturer.FindAllLecturers());
            return json;
        }
        
        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult<LecturerModel> Get(int id)
        {
            JsonResult json = new JsonResult(_lecturer.FindLecturerById(id));
            return json;
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]LecturerModel value)
        {
            _lecturer.AddLecturer(value);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]LecturerModel value)
        {
            _lecturer.UpdateLecturerInfo(value);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _lecturer.FireLecturer(id);
        }
    }
}
