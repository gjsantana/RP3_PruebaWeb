using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Rp3.Test.Data;

namespace Rp3.Test.WebApi.Data.Controllers
{
    public class PersonDataController : ApiController
    {
        /// <summary>
        /// Obtiene registros de Personas activas
        /// </summary>
        /// <param name="active"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Get(bool? active = null)
        {
            List<Rp3.Test.Common.Models.Person> commonModel = new List<Common.Models.Person>();

            using (DataService service = new DataService())
            {
                var query = service.Persons.GetQueryable();

                if (active.HasValue)
                    query = query.Where(p => p.Active == active.Value);

                commonModel = query.Select(p => new Common.Models.Person()
                {
                    Active = p.Active,
                    PersonId = p.PersonId,
                    Name = p.Name,
                    Identification = p.Identification
                }).ToList();
            }

            return Ok(commonModel);
        }

        /// <summary>
        /// Obtiene un registro de persona en base a su Id
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetById(int personId)
        {
            Rp3.Test.Common.Models.Person commonModel = null;
            using (DataService service = new DataService())
            {
                var model = service.Persons.GetByID(personId);

                commonModel = new Common.Models.Person()
                {
                    Active = model.Active,
                    PersonId = model.PersonId,
                    Name = model.Name ,
                    Identification = model.Identification
                };
            }
            return Ok(commonModel);
        }

        /// <summary>
        /// Inserta un registro de persona
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Insert(Rp3.Test.Common.Models.Person person)
        {
            using (DataService service = new DataService())
            {
                Rp3.Test.Data.Models.Person personModel = new Test.Data.Models.Person();
                personModel.Active = person.Active;
                personModel.Name = person.Name;
                personModel.Identification = person.Identification;

                personModel.PersonId = service.Persons.GetMaxValue<int>(p => p.PersonId, 0) + 1;

                service.Persons.Insert(personModel);
                service.SaveChanges();
            }

            return Ok(true);
        }

        /// <summary>
        /// Actualiza un registro de persona
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Update(Rp3.Test.Common.Models.Person person)
        {
            using (DataService service = new DataService())
            {
                Rp3.Test.Data.Models.Person personModel = new Test.Data.Models.Person();
                personModel.Active = person.Active;
                personModel.PersonId = person.PersonId;
                personModel.Name = person.Name;
                personModel.Identification = person.Identification;

                service.Persons.Update(personModel);
                service.SaveChanges();
            }

            return Ok(true);
        }
    }
}