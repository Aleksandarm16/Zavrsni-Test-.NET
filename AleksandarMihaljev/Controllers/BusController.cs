using AleksandarMihaljev.Interface;
using AleksandarMihaljev.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace AleksandarMihaljev.Controllers
{
    public class BusController : ApiController
    {
        IBusRepository repo { get; set; }
        public BusController(IBusRepository repository)
        {
            repo = repository;
        }

        public IEnumerable<Bus> GetAll()
        {
            return repo.GetAll();
        }

        [ResponseType(typeof(Bus))]
        public IHttpActionResult GetId(int id)
        {
            var res = repo.GetId(id);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }
        [Route("api/tradicija")]
        [ResponseType(typeof(IEnumerable<Bus>))]
        public IHttpActionResult GetAndSortByYear()
        {
            var res = repo.GetAllSortedByYear();
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }
        [ResponseType(typeof(IEnumerable<Bus>))]
        public IHttpActionResult GetByLineType (string type)
        {
            var res = repo.GetAllSortedByYearWithParams(type);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }


    }
}
