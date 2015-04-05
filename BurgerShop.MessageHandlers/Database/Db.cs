using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using Sixeyed.Top10.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//TODO - move to core
namespace Sixeyed.Top10.Core.Database
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

        public static string ServerEndpoint
        {
            get
            {
                var db = GetDatabase();
                var instance = db.Server.Primary;
                if (instance == null)
                {
                    instance = db.Server.Instance;
                }
                return instance.Address.Host;
            }
        }
    }
}
