
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using BurgerShop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerShop.Core.Database
{
    public static class Db
    {

        public static MongoDatabase GetDatabase()
        {
            var connectionString = Config.GetSetting("mongodb");
            var client = new MongoClient(connectionString);
            var server = client.GetServer();
            return server.GetDatabase("sc-psod-reactive");
        }

        public static dynamic ToDynamic(this BsonDocument doc)
        {
            var json = doc.ToJson();
            dynamic obj = JToken.Parse(json);
            return obj;
        }

        public static string GetServerEndpoint(MongoDatabase db)
        {
            var currentInstance = db.Server.Primary;
            //look through the servers to see which instance we're connected to:
            foreach (var instance in db.Server.Instances)
            {
                if (instance.ConnectionPool != null && 
                    instance.ConnectionPool.CurrentPoolSize > instance.ConnectionPool.AvailableConnectionsCount)
                {
                    currentInstance = instance;
                    break;
                }
            }
            return currentInstance.Address.Host;
        }
    }
}
