using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using Polymer3D_APIs.Models;
using System.Net;

namespace Polymer3D_APIs.Classes.Email
{
    public class EmailHandler
    {
        public void sendEmailToClient(CustomerModel customer)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("contato@polymer3d.com.br");
            mail.To.Add(customer.email);
            //TODO: Transformar isso em uma factory. Uma interface para tipos de email, um método factory com o switch/case, e um metodo unico para escrever email 
            switch (customer.customerType)
            {
                case "fornecedor":
                    mail = writeMailToSupplier(mail, customer);
                    break;
                case "comprador":
                    mail = writeMailToBuyer(mail, customer);
                    break;
                case "contato":
                    mail = writeMailToContact(mail, customer);
                    break;
            }


            mail.IsBodyHtml = true;

            using (SmtpClient smtp = new SmtpClient("smtp.uhserver.com", 587))
            {
                smtp.Credentials = new NetworkCredential("contato@polymer3d.com.br", "@totvs2017!");
                smtp.EnableSsl = false;
                smtp.Send(mail);
            }
        }

        //Manda o email interno para nossa equipe
        public void sendPrivateEmail(CustomerModel customer)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("contato@polymer3d.com.br");
                //TODO: Mailist em arquivo externo
                mail.To.Add("francardoso@outlook.com");
                mail.To.Add("giovanniferreira_90@hotmail.com.com.br");
                mail.To.Add("gioferreira.90@gmail.com");
                ////
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

        private MailMessage writeMailToSupplier(MailMessage mail, CustomerModel customer)
        {
            mail.Subject = "Seja bem vindo " + customer.name + "!";

            mail = beginMailBody(mail, customer);
            mail.Body += "<p>Recebemos seu pré-cadastro com sucesso. Verificamos que você tem interesse em fornecer os seguintes serviços: </p>"+ "<ul>";
            List<string> servicesList = customer.servicesList.Split(',').ToList();
            foreach (string service in servicesList)
            {
                mail.Body += "<li>" + service + "</li>";
            }
            mail.Body += "</ul>"
            + "<p>Para concluir seu cadastro e conhecermos melhor seus serviços, por favor, preencha os formulários a seguir: </p>";
            //TODO: Listagem dos formularios exigidos
            mail.Body += "<p><a href=\"https://goo.gl/forms/XNJyQA0WnygkaJ933\" target=\"_blank\">Dados Básicos da Loja </a> </p>";
            if(servicesList.Contains("Impressão 3D") || servicesList.Contains("Impressão Industrial 3D"))
                mail.Body += " <p> <a href=\"https://goo.gl/forms/L0etdqZBWLKcV2AI2\" target=\"_blank\"> Detalhes do Serviço de Impressão </a> </p>";
            if(servicesList.Contains("Designer/Projetista"))
                mail.Body += "<p> <a href=\"https://goo.gl/forms/Fi5CLxD8GxGYzxqg1\" target=\"_blank\">Detalhes do Serviço de Modelagem </a>  </p>";

            mail.Body += "<p>Seja muito bem vindo à família Polymer!</p>";
            mail = endMailBody(mail);
            return mail;
        }

        private MailMessage writeMailToBuyer(MailMessage mail, CustomerModel customer)
        {
            mail.Subject = "Vamos encontrar os melhores profissionais para te ajudar";
            mail = beginMailBody(mail, customer);
            mail.Body += "<p>Nesse exato momento estamos buscando as melhores pessoas para te ajudar <br />Em breve entraremos em contato para entender melhor a sua necessidade e bater um papo!</p>";
            mail = endMailBody(mail);
            return mail;
        }

        private MailMessage writeMailToContact(MailMessage mail, CustomerModel customer)
        {
            mail.Subject = "Sobre o seu contato com a Polymer3D";
            mail = beginMailBody(mail, customer);
            mail.Body += "<p>Obrigado por nos procurar!</p>"
               + "<p>Iremos responder a sua solicitação em até 12 horas.<br />Se preferir, também pode falar com a gente no whatsapp: <strong>(11) 96418-4303</strong> </p>";
            mail = endMailBody(mail);
            return mail;
        }

        private MailMessage beginMailBody(MailMessage mail, CustomerModel customer)
        {
            mail.Body = mail.Body
                        + "<body style=\"color: gray\">"
                        + "<p> Olá " + customer.name + ", tudo bem?</p>";
            //TODO: Listagem dos formularios exigidos
            return mail;
        }

        private MailMessage endMailBody(MailMessage mail)
        {
            mail.Body = mail.Body
            + "<p> Atenciosamente,"
                + "<br />"
                + "<br /> <strong> Francisco Cardoso </strong>"
                + "<br />"
                + "<br/>"
                + "<img src = \"https://s3-us-west-2.amazonaws.com/www.polymer3d.com.br/img/logo.PNG\" />"
            + "</p>"
            + "</body>"
            ;
            return mail;
        }
    }
}