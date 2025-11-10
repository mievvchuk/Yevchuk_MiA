using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LW4_Task2_MiA.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string DisplayName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public UserRole Role { get; set; } = UserRole.Regular;
    }
}
