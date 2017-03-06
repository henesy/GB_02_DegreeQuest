using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson; // added to the References
using MongoDB.Driver; //added to the References

namespace test_platformer
{
    class Class1
    {
        protected static IMongoClient _client;
        protected static IMongoDatabase _database;

        public void inilize()
        {
            _client = new MongoClient();
            _database = _client.GetDatabase("test");
        }

        public async void insert_document()
        {
            var document = new BsonDocument
            {
                {"address", new BsonDocument
                    {
                    { "Street", "2 Avenue"},
                    {"zipcode", "10075" },
                    {"building", "1480" },
                    {"coord", new BsonArray { 73.9557413, 40.7720266} }
                    }
                },
                {"borough", "manhattan" },
                {"cuisine", "Italian" },
                {"grades", new BsonArray
                {
                    new BsonDocument
                    {
                        { "date", new DateTime(2014,10,1,0,0,0,DateTimeKind.Utc) },
                        { "grade", "A" },
                        { "score", 11 }
                    },
                    new BsonDocument
                    {
                        {"date", new DateTime(2014, 1,6,0,0,0, DateTimeKind.Utc) },
                        {"grade", "B" },
                        {"score", 17 }
                    }
                }
                },
                {"name", "Vella" },
                {"restaurant_id", "41704620" }

            };

            var collection = _database.GetCollection<BsonDocument>("restaurants");
            await collection.InsertOneAsync(document);
        }

        public async Task<int> queryAll()
        {
            var collection = _database.GetCollection<BsonDocument>("restaurants");
            var filter = new BsonDocument();
            var count = 0;
            using (var cursor = await collection.FindAsync(filter))
            {
                while(await cursor.MoveNextAsync())
                {
                    var batch = cursor.Current;
                    foreach(var document in batch)
                    {
                        count++;
                    }
                }
            }
            return count;
        }
    }
}
