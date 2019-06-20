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
    public class GroupController : Controller
    {
        readonly GroupsService _groups = new GroupsService();

        // GET: api/<controller>
        [HttpGet]
        public ActionResult<IEnumerable<GroupModel>> Get()
        {
            JsonResult json = new JsonResult(_groups.FindAllGroups());
            return json;
        }
        
        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult<GroupModel> Get(int id)
        {
            JsonResult json = new JsonResult(_groups.FinGroupById(id));
            return json;
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]GroupModel value)
        {
            _groups.AddGroup(value);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]GroupModel value)
        {
            _groups.UpdatGroup(value);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _groups.DeleteGroupById(id);
        }
    }
}
