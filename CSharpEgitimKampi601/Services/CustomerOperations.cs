using CSharpEgitimKampi601.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpEgitimKampi601.Services
{
    public class CustomerOperations
    {
        public void AddCustomer(Customer customer)
        {
            var connection = new MongoDbConnection();
            var customomerCollection = connection.GetCustomersCollections();

            var document = new BsonDocument
            {
                {"CustomerName",customer.CustomerName},
                {"CustomerSurname",customer.CustomerSurname },
                {"CustomerCity", customer.CustomerCity },
                {"CustomerBalance", customer.CustomerBalance },
                {"CustomerShoppingCount", customer.CustomerShoppingCount }
            };
            customomerCollection.InsertOne(document);
        }

        public List<Customer> GetAllCustomer()
        {
            var connection = new MongoDbConnection();
            var customerCollection = connection.GetCustomersCollections();

            var customers = customerCollection.Find(new BsonDocument()).ToList();
            List<Customer> customerList = new List<Customer>();
            foreach (var item in customers)
            {
                customerList.Add(new Customer
                {
                    CustomerId = item["_id"].ToString(),
                    CustomerName = item["CustomerName"].ToString(),
                    CustomerSurname = item["CustomerSurname"].ToString(),
                    CustomerCity = item["CustomerCity"].ToString(),
                    CustomerBalance = Convert.ToDecimal(item["CustomerBalance"]),
                    CustomerShoppingCount = Convert.ToInt32(item["CustomerShoppingCount"].ToString()),
                }); ;
            }
            return customerList;
        }

        public void DeleteCustomer(string id)
        {
            var connection = new MongoDbConnection();
            var collection = connection.GetCustomersCollections();
            var deleteCustomer = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id));
            collection.DeleteOne(deleteCustomer);
        }

        public void CustomerUpdate(Customer customer)
        {
            var connection = new MongoDbConnection();
            var collection = connection.GetCustomersCollections();
            var updateCustomerId = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(customer.CustomerId));
            var updateCustomer = Builders<BsonDocument>.Update
                .Set("CustomerName", customer.CustomerName)
                .Set("CustomerSurname", customer.CustomerSurname)
                .Set("CustomerCity", customer.CustomerCity)
                .Set("CustomerBalance", customer.CustomerBalance)
                .Set("CustomerShoppingCount", customer.CustomerShoppingCount);

            collection.UpdateOne(updateCustomerId, updateCustomer);
        }

        public Customer GetCustomerById(string id)
        {
            var connection = new MongoDbConnection();
            var collection = connection.GetCustomersCollections();

            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id));
            var result = collection.Find(filter).FirstOrDefault();
            return new Customer
            {
                CustomerId = id,
                CustomerName = result["CustomerName"].ToString(),
                CustomerSurname = result["CustomerSurname"].ToString(),
                CustomerCity = result["CustomerCity"].ToString(),
                CustomerBalance = Convert.ToInt32(result["CustomerBalance"].ToString()),
                CustomerShoppingCount = Convert.ToInt32(result["CustomerShoppingCount"].ToString()),
                
            };
        }
    }
}
