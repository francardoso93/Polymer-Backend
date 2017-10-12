using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Polymer3D_APIs.DataAccess.Dynamo;
using Polymer3D_APIs.Models;
using System.Web.Http.Cors;
using System.Net.Mail;

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
            sendPrivateEmail(customer);
            sendEmailToClient(customer);
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


        //TODO: Migrar para uma classe própria de emails
        //Personalizar respostas para vendedores/compradores
        //Manda email para o clientess
        private void sendEmailToClient(CustomerModel customer)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("contato@polymer3d.com.br");
                mail.To.Add(customer.email);
                mail.Subject = "Seja bem vindo " + customer.name + "!";
                mail.Body = "<p>Olá " + customer.name + ", tudo bem?</p><p>Nesse exato momento estamos buscando as melhores pessoas para te ajudar. <br>Em breve entraremos em contato para entender melhor a sua necessidade e bater um papo! </p>" +
                    "<p> Obrigado por utilizar a nossa plataforma. </p> <p> Atenciosamente, <br> Equipe Polymer3D </p>";

                mail.IsBodyHtml = true;
                //mail.Attachments.Add(new Attachment("~/Assets/Como_Montar_Seu_Ecommerce-Ebook.pdf"));

                using (SmtpClient smtp = new SmtpClient("smtp.uhserver.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("contato@polymer3d.com.br", "@totvs2017!");
                    smtp.EnableSsl = false; 
                    smtp.Send(mail);
                }

            }
        }

        //Manda o email interno para nossa equipe
        private void sendPrivateEmail(CustomerModel customer)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("contato@polymer3d.com.br");
                mail.To.Add("francardoso@outlook.com");
                mail.To.Add("giovanni.ferreira@integradora.com.br");
                mail.Subject = "Novo Cliente: " + customer.name;
                mail.Body = "<h2>Novo Cliente ! </p> <br/> <p> Nome Completo: " + customer.name +
                    " <br/>E-mail: " + customer.email +
                    " <br/>Nome da Empresa: " + customer.company +
                    " <br/>Telefone: " + customer.phone +
                    " <br/>Tipo de Cliente: " + customer.customerType +
                    " <br/>Lista de Serviços: " + customer.servicesList +
                    " <br/>Detalhes: " + customer.moreinfo +
                    " </p>";

                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("smtp.uhserver.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("contato@polymer3d.com.br", "@totvs2017!");
                    smtp.EnableSsl = false;
                    smtp.Send(mail);
                }

            }
        }
    }
}

