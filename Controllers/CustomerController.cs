using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Polymer3D_APIs.DataAccess.Dynamo;
using Polymer3D_APIs.Models;

namespace Polymer3D_APIs.Controllers
{
    public class CustomerController : ApiController
    {
        CustomerContext customerContext = new CustomerContext();  

        // GET: api/Customer
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
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
        }

        // PUT: api/Customer/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Customer/5
        public void Delete(int id)
        {
        }
    }
}
