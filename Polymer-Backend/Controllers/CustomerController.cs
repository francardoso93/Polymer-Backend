using System;
using System.Collections.Generic;
using System.Web.Http;
using Polymer3D_APIs.DataAccess.Dynamo;
using Polymer3D_APIs.Models;
using System.Web.Http.Cors;
using Polymer3D_APIs.Classes.Email;

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
        }

        // POST: api/Customer
        public void Post([FromBody]CustomerModel customer)
        {
            EmailHandler email = new EmailHandler();
            Guid id = Guid.NewGuid();
            email.sendPrivateEmail(customer);
            email.sendEmailToClient(customer);
            customerContext.postCustomer(customer, id);
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

