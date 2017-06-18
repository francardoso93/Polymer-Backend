using System;
using System.Collections.Generic;
using System.Linq;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Runtime;
using Polymer3D_APIs.Models;
using Newtonsoft.Json;

namespace Polymer3D_APIs.DataAccess.Dynamo
{
    public class CustomerContext
    {
        private AmazonDynamoDBClient client = new AmazonDynamoDBClient();
        private string tableName = "Customer";
        Table customerTable;

        public CustomerContext()
        {
            customerTable = Table.LoadTable(client, tableName);
        }

        public void postCustomer(CustomerModel customer, Guid id)
        {
            customer.customerId = id.ToString();
            var customerJson = JsonConvert.SerializeObject(customer, Formatting.Indented);
            var customerDynamoDocument = Document.FromJson(customerJson);
            customerTable.PutItem(customerDynamoDocument);
        }

        public string getCustomer(string id)
        {

            //Console.WriteLine("\n*** Executing RetrieveBook() ***");
            //// Optional configuration.
            //GetItemOperationConfig config = new GetItemOperationConfig
            //{
            //    AttributesToGet = new List<string> { "Id", "ISBN", "Title", "Authors", "Price" },
            //    ConsistentRead = true
            //};
            Document document = customerTable.GetItem(id);
            return document.ToJson();
        }
    }
}