using Rp3.Test.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Rp3.Test.WebApi.Data.Controllers
{
    public class TransactionDataController : ApiController
    {
        /// <summary>
        /// Obtiene la lista de transacciones por persona.
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Get(int? personId)
        {            
            List<Rp3.Test.Common.Models.TransactionView> commonModel = new List<Common.Models.TransactionView>();

            using (DataService service = new DataService())
            {
                IEnumerable<Rp3.Test.Data.Models.Transaction> 
                    dataModel = service.Transactions.Get(p=> p.PersonId == personId,
                    includeProperties: "Category,TransactionType", 
                    orderBy: p=> p.OrderByDescending(o=>o.RegisterDate) );

                //Para incluir una condición, puede usar el primer parametro de Get
                /*
                 * Ejemplo
                 IEnumerable<Rp3.Test.Data.Models.Transaction>
                    dataModel = service.Transactions.Get(p=> p.TransactionId > 0
                    includeProperties: "Category,TransactionType",
                    orderBy: p => p.OrderByDescending(o => o.RegisterDate));

                 */

                commonModel = dataModel.Select(p => new Common.Models.TransactionView()
                {
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category.Name,
                    Notes = p.Notes,
                    Amount = p.Amount,
                    RegisterDate = p.RegisterDate,
                    ShortDescription = p.ShortDescription,
                    TransactionId = p.TransactionId,
                    TransactionType = p.TransactionType.Name,
                    TransactionTypeId = p.TransactionTypeId
                }).ToList();
            }

            return Ok(commonModel);
        }

        [HttpGet]
        public IHttpActionResult Balance(int? personId)
        {
            List<Rp3.Test.Common.Models.BalanceView> commonModel = new List<Common.Models.BalanceView>();

            if (personId.HasValue)
            {

                using (DataService service = new DataService())
                {
                    IEnumerable<Rp3.Test.Data.Models.Balance>
                         dataModel = service.Balances.GetWithRawSql(string.Format(@"EXEC dbo.spGetBalance @idPersona = {0}", personId));         

                    commonModel = dataModel.Select(p => new Common.Models.BalanceView()
                    {
                        Category = p.Category,
                        Amount = p.Amount
                    }).ToList();
                }
            }

            return Ok(commonModel);
        }

        [HttpPost]
        /// <summary>
        /// Inserta un regsitro de transacción
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public IHttpActionResult Insert(Rp3.Test.Common.Models.Transaction transaction)
        {
            //Complete the code
            using (DataService service = new DataService())
            {
                Rp3.Test.Data.Models.Transaction model = new Test.Data.Models.Transaction();
                model.TransactionId = service.Transactions.GetMaxValue<int>(p => p.TransactionId, 0) + 1;
                model.TransactionTypeId = transaction.TransactionTypeId;
                model.CategoryId = transaction.CategoryId;
                model.RegisterDate = transaction.RegisterDate;
                model.ShortDescription = transaction.ShortDescription;
                model.Amount = transaction.Amount;
                model.Notes = transaction.Notes;
                model.PersonId = transaction.PersonId;

                service.Transactions.Insert(model);
                service.SaveChanges();
            }

            return Ok();
        }

        [HttpPost]
        public IHttpActionResult Update(Rp3.Test.Common.Models.Transaction transaction)
        {
            //Complete the code
            using (DataService service = new DataService())
            {
                Rp3.Test.Data.Models.Transaction model = service.Transactions.GetByID(transaction.TransactionId);
                model.TransactionTypeId = transaction.TransactionTypeId;
                model.CategoryId = transaction.CategoryId;
                model.RegisterDate = transaction.RegisterDate;
                model.DateUpdate = transaction.DateUpdate;
                model.ShortDescription = transaction.ShortDescription;
                model.Amount = transaction.Amount;
                model.Notes = transaction.Notes;
                model.PersonId = transaction.PersonId;

                service.Transactions.Update(model);
                service.SaveChanges();
            }

            return Ok();
        }


    }
}
