using AleksandarMihaljev.Interface;
using AleksandarMihaljev.Models;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace AleksandarMihaljev.Controllers
{
    public class PassengerController : ApiController
    {
        IPassengerRepository repo { get; set; }
        public PassengerController(IPassengerRepository repository)
        {
            repo = repository;
        }

        [Route("api/brojnost")]
        [ResponseType(typeof(IEnumerable<BusSumDto>))]
        public IHttpActionResult GetAllSummed()
        {
            var res = repo.GetAllBussesAndPassengers();

            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }
        public IEnumerable<PassengerDto> GetAll()
        {
            var res = repo.GetAll().ProjectTo<PassengerDto>();
            return res;
        }

        [ResponseType(typeof(Passenger))]
        public IHttpActionResult GetId(int id)
        {
            var res = repo.GetId(id);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }
        [ResponseType(typeof(IEnumerable<Passenger>))]
        public IHttpActionResult GetAllByAdress(string adress)
        {
            var res = repo.GetAllByAdress(adress).AsEnumerable();
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);

        }
        [Authorize]
        public IHttpActionResult Post(Passenger passenger)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            repo.Add(passenger);
            return CreatedAtRoute("DefaultApi", new { id = passenger.Id }, passenger);

        }
        [Authorize]
        public IHttpActionResult Delete(int id)
        {
            var res = repo.GetId(id);
            if(res==null)
            {
                return NotFound();
            }
            repo.Delete(res);
            return Ok(res);

        }
        [Authorize]
        public IHttpActionResult Put(int id, Passenger passenger)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != passenger.Id)
            {
                return BadRequest();
            }
            try
            {
                repo.Update(passenger);
            }
            catch
            {
                return BadRequest();
            }
            return Ok(passenger);

        }
        [Authorize]
        [Route("api/pretraga")]
        [ResponseType(typeof(IEnumerable<PassengerDto>))]
        public IHttpActionResult SearchByValues(SearchDto search)
        {
            IQueryable<PassengerDto> passraw = repo.SearchByYear(search.Start, search.End).ProjectTo<PassengerDto>();
            var pass = passraw.AsEnumerable();
            if (pass == null)
            {
                return NotFound();
            }
            return Ok(pass);
        }

    }

}

