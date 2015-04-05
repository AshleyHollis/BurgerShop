using MongoDB.Bson;
using MongoDB.Driver.Builders;
using BurgerShop.Core.Database;
using BurgerShop.MessageHandlers.Database.Documents;
using BurgerShop.Messages.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BurgerShop.MessageHandlers.Database.Queries
{
    public static class CurrentTop10
    {
        static CurrentTop10()
        {
            var db = Db.GetDatabase();
            var collection = db.GetCollection<SongPlay>("songPlays");
            collection.EnsureIndex(new IndexKeysBuilder().Descending("RecordedAtUtc"));
        }

        public static List<SongSummary> Get(out string serverEndpoint)
        {
            var db = Db.GetDatabase();
            var collection = db.GetCollection<SongPlay>("songPlays");
            var pipeline = BuildPipeline();
            var result = collection.Aggregate(pipeline);
            var results = result.ResultDocuments.Select(x => x.ToDynamic()).ToList();

            var summaries = new List<SongSummary>();
            var order = 1;
            foreach (var item in results)
            {
                summaries.Add(new SongSummary
                {
                    Title = item._id.Artist,
                    PlayCount = item.PlayCount,
                    Order = order++
                });
            }

            serverEndpoint = Db.GetServerEndpoint(db);
            return summaries;
        }

        private static BsonDocument[] BuildPipeline()
        {
            /*this is the aggregattion:
* db.songPlays.aggregate([
{$match:{RecordedAtUtc:{$gte:ISODate("2013-09-18")}}},
{$group:{_id:"$Artist",value:{$sum:1}}},
{"$sort": {"value": -1}},
{$limit: 10}
])
*/
            var utcToday = DateTime.Today.ToUniversalTime();
            var sort = new BsonDocument
            {
             { "$sort", new BsonDocument { 
                { "PlayCount",  -1  } } }
            };
            var limit = new BsonDocument
            {
             { "$limit", 10 } 
            };
            var match = new BsonDocument
            {
             { "$match", new BsonDocument { 
                { "RecordedAtUtc",  new BsonDocument { 
                { "$gte",utcToday  } } } } }
            };
            var group = new BsonDocument
                {
                    { "$group",
                        new BsonDocument
                            {
                                { "_id", new BsonDocument
                                             {
                                                 {
                                                     "Artist","$Artist"
                                                 }
                                             }
                                },
                                {
                                    "PlayCount", new BsonDocument
                                                 {
                                                     {
                                                         "$sum", 1
                                                     }
                                                 }
                                }
                            }
                  }
                };
            var pipeline = new[] { match, group, sort, limit };
            return pipeline;
        }
    }
}
