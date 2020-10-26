using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PlayStudios.Model
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id;

        public string email { get; set; }
        public string password { get; set; }
        public string fullname { get; set; }
        public string token { get; set; }
        
    }
}
