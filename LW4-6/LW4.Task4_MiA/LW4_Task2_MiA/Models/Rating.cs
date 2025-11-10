using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LW4_Task2_MiA.Models
{
    public class Rating
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string RecipeId { get; set; } = string.Empty;

        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; } = string.Empty;

        public int Value { get; set; }
        public string? Comment { get; set; }
    }
}
