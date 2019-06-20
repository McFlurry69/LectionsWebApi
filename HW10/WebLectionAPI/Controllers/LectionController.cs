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
    public class LectionController : Controller
    {
        readonly LectionsService _lection = new LectionsService();

        // GET: api/<controller>
        [HttpGet]
        public ActionResult<IEnumerable<LectionModel>> Get()
        {
            JsonResult json = new JsonResult(_lection.FindAllLections());
            return json;
        }
        
        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult<LectionModel> Get(int id)
        {
            JsonResult json = new JsonResult(_lection.FindLectionById(id));
            return json;
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]LectionModel value)
        {
            _lection.AddLection(value);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]LectionModel value)
        {
            _lection.UpdateLection(value);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _lection.DeleteLectionById(id);
        }
    }
}
