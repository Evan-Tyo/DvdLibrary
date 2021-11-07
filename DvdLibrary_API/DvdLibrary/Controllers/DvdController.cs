using DvdLibrary.Factories;
using DvdLibrary.Interfaces;
using DvdLibrary.Models;
using DvdLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DvdLibrary.Controllers
{
    // EnableCors on DvdController class
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DvdController : ApiController
    {
        // Create repository for use with http methods
        IDvdRepository repository = RepositoryFactory.GetRepository();

        // Get all dvds from database
        [Route("dvds")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetAll()
        {
            return Ok(repository.LoadAllDvds());
        }

        // Get, based on id, get single dvd from database
        [Route("dvd/{id}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult Get(int id)
        {
            Dvd found = repository.LoadSingleDvd(id);

            if (found == null)
            {
                return NotFound();
            }

            return Ok(found);
        }

        // Add (or create) a new dvd into database
        [Route("dvd")]
        [AcceptVerbs("POST")]
        public void Add(AddDvd addDvd)
        {
            repository.AddDvd(addDvd);
        }

        // Update a current dvd in database
        [Route("dvd/{id}")]
        [AcceptVerbs("PUT")]
        public void Update(int id, UpdateDvd updateDvd)
        {
            repository.UpdateDvd(id, updateDvd);
        }

        // Delete a dvd in database
        [Route("dvd/{id}")]
        [AcceptVerbs("DELETE")]
        public void Delete(int id)
        {
            repository.DeleteDvd(id);
        }

        // Get, based on title, dvds from database
        [Route("dvds/title/{title}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetAllTitle(string title)
        {
            return Ok(repository.SearchTitle(title));
        }


        // Get, based on release year, dvds from database
        [Route("dvds/year/{releaseYear}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetAllReleaseYear(int releaseYear)
        {
            return Ok(repository.SearchReleaseYear(releaseYear));
        }

        // Get, based on director, dvds from database
        [Route("dvds/director/{director}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetAllDirector(string director)
        {
            return Ok(repository.SearchDirector(director));
        }

        // Get, based on rating, dvds from database
        [Route("dvds/rating/{rating}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetAllRating(string rating)
        {
            return Ok(repository.SearchRating(rating));
        }
    }
}
