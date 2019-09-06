using Newtonsoft.Json;
using Rp3.Test.Common.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;


namespace Rp3.Test.Proxies
{
    public class Proxy : BaseProxy
    {
        //Categoria
        private const string UriGetCategory = "api/categoryData/get?active={0}";
        private const string UriGetCategoryById = "api/categoryData/getById?categoryId={0}";
        private const string UriInsertCategory = "api/categoryData/insert";
        private const string UriUpdateCategory = "api/categoryData/update";

        //Persona
        private const string UriGetPerson = "api/personData/get?active={0}";
        private const string UriGetPersonById = "api/personData/getById?personId={0}";
        private const string UriInsertPerson = "api/personData/insert";
        private const string UriUpdatePerson = "api/personData/update";

        //Tipo de Transacción
        private const string UriGetTransactionType = "api/transactionTypeData/get";

        //Transacción
        private const string UriGetTransactions = "api/transactionData/get?personId={0}";
        private const string UriGetBalance = "api/transactionData/balance?personId={0}";
        private const string UriInsertTransaction = "api/transactionData/insert";
        private const string UriUpdateTransaction = "api/transactionData/update";
        private const string UriDeleteTransaction = "api/transactionData/delete";

        /// <summary>
        /// Obtiene el Listado de Tipos de Transacción
        /// </summary>
        /// <returns></returns>
        public List<TransactionType> GetTransactionTypes()
        {
            return HttpGet<List<TransactionType>>(UriGetTransactionType);
        }

        #region Category Services

        /// <summary>
        /// Obtiene el Listado de Categorías
        /// </summary>
        /// <param name="active">especifica si la consulta es sobre categorías activas o Inactivas</param>
        /// <returns></returns>
        public List<Category> GetCategories(bool? active = null)
        {
            return HttpGet<List<Category>>(string.Format(UriGetCategory, active));
        }

        /// <summary>
        /// Obtiene una Categoría por Id
        /// </summary>
        /// <param name="categoryId">Id de la Categoría</param>
        /// <returns></returns>
        public Category GetCategory(int categoryId)
        {
            return HttpGet<Category>(string.Format(UriGetCategoryById, categoryId));
        }

        /// <summary>
        /// Método para Insertar Categorías
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public bool InsertCategory(Rp3.Test.Common.Models.Category category)
        {
            return HttpPostAsJson<bool>(UriInsertCategory, category);
        }

        public bool UpdateCategory(Rp3.Test.Common.Models.Category category)
        {
            return HttpPostAsJson<bool>(UriUpdateCategory, category);
        }

        #endregion

        #region Person Services

        /// <summary>
        /// Obtiene el Listado de Personas
        /// </summary>
        /// <param name="active">especifica si la consulta es sobre personas activas o Inactivas</param>
        /// <returns></returns>
        public List<Person> GetPersons(bool? active = null)
        {
            return HttpGet<List<Person>>(string.Format(UriGetPerson, active));
        }

        /// <summary>
        /// Obtiene una Persona por Id
        /// </summary>
        /// <param name="personId">Id de la Categoría</param>
        /// <returns></returns>
        public Person GetPerson(int personId)
        {
            return HttpGet<Person>(string.Format(UriGetCategoryById, personId));
        }

        /// <summary>
        /// Método para Insertar persona
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public bool InsertPerson(Rp3.Test.Common.Models.Person person)
        {
            return HttpPostAsJson<bool>(UriInsertCategory, person);
        }

        /// <summary>
        /// Método para Actualizar persona
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public bool UpdatePerson(Rp3.Test.Common.Models.Person person)
        {
            return HttpPostAsJson<bool>(UriUpdateCategory, person);
        }

        #endregion

        #region Transacction Services
        /// <summary>
        /// Obtiene el Listado de Transacciones
        /// </summary>
        /// <returns></returns>
        public List<TransactionView> GetTransactions(int personId)
        {
            return HttpGet<List<TransactionView>>(string.Format(UriGetTransactions, personId));
        }

        /// <summary>
        /// Método para Insertar transacción
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public bool InsertTransaction(Rp3.Test.Common.Models.Transaction transaction)
        {
            return HttpPostAsJson<bool>(UriInsertTransaction, transaction);
        }

        /// <summary>
        /// Método para Actualizar transacción
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public bool UpdateTransaction(Rp3.Test.Common.Models.Transaction transaction)
        {
            return HttpPostAsJson<bool>(UriUpdateTransaction, transaction);
        }

        /// <summary>
        /// Método para Eliminar transacción
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public bool DeleteTransaction(int transactionId)
        {
            return HttpPostAsJson<bool>(UriDeleteTransaction, transactionId);
        }

        /// <summary>
        /// Obtiene datos de consulta del balance
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public List<BalanceView> GetBalance(int personId)
        {
            return HttpGet<List<BalanceView>>(string.Format(UriGetBalance, personId));
        }

        #endregion

    }
}