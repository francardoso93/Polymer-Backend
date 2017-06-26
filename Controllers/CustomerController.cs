using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Polymer3D_APIs.DataAccess.Dynamo;
using Polymer3D_APIs.Models;
using System.Web.Http.Cors;

namespace Polymer3D_APIs.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CustomerController : ApiController
    {
        CustomerContext customerContext = new CustomerContext();  

        // GET: api/Customer
        public List<CustomerModel> Get()
        {
            return customerContext.getAllCustomers();
        }

        // GET: api/Customer/5
        public string Get(string id)
        {
            return customerContext.getCustomer(id);
            //return "value";
        }

        // POST: api/Customer
        public void Post([FromBody]CustomerModel customer)
        {
            Guid id = Guid.NewGuid();
            customerContext.postCustomer(customer, id);
            //TODO: return 200 instead of 204
        }

        // PUT: api/Customer/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Customer/5
        //public void Delete(int id)
        //{
        //}
    }
}
