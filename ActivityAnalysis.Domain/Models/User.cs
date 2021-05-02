using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ActivityAnalysis.Domain.Models
{
    public class User
    {
        [BsonId]
        public ObjectId Id { get;  set; }
        [BsonElement("email")]
        public string Email { get; set; }
        [BsonElement("login")]
        public string Login { get; set; }
        [BsonElement("password")]
        public string Password { get; set; }
        
        public User(string email, string login, string password)
        {
            Id = ObjectId.GenerateNewId();
            Email = email;
            Login = login;
            Password = password;
        }
    }
}