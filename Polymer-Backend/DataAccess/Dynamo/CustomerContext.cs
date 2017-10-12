using System;
using System.Collections.Generic;
using System.Linq;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Runtime;
using Polymer3D_APIs.Models;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using Amazon.DynamoDBv2.Model;

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

        public List<CustomerModel> getAllCustomers()
        {        
            ScanFilter scanFilter = new ScanFilter();
            Search getAllItems = customerTable.Scan(scanFilter);
            List<Document> allItems = getAllItems.GetRemaining();
            
            List<CustomerModel> customerModelList = new List<CustomerModel>();
            CustomerModel customerModel = null;

            foreach (Document item in allItems)
            {
                customerModel = new CustomerModel();

                foreach (string key in item.Keys)
                {
                    DynamoDBEntry dbEntry = item[key];
                    string val = dbEntry.ToString();


                    if (val == "Amazon.DynamoDBv2.DocumentModel.DynamoDBNull" || val == null || val == "null")            
                        val = "não preenchido";

                    if (key == "company")
                        customerModel.company = val;
                    if (key == "customerId")
                        customerModel.customerId = val;
                    if (key == "email")
                        customerModel.email = val;
                    if (key == "moreinfo")
                        customerModel.moreinfo = val;
                    if (key == "name")
                        customerModel.name = val;
                    if (key == "phone")
                        customerModel.phone = val;
                    if (key == "customerType")
                        customerModel.customerType = val;
                    if(key == "servicesList")
                        customerModel.servicesList = val;

                    //if (key.ToLower() == "servicesList")
                    //{
                    //    List<string> neighbours = dbEntry.AsListOfString();
                    //    StringBuilder valueBuilder = new StringBuilder();
                    //    foreach (string neighbour in neighbours)
                    //    {
                    //        valueBuilder.Append(neighbour).Append(", ");
                    //    }
                    //    val = valueBuilder.ToString();
                    //}

                    //        //Console.WriteLine(string.Format("Property: {0}, value: {1}", key, val));
                }
                customerModelList.Add(customerModel);
            }
            return customerModelList;

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