using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LW4_Task2_MiA.Models
{
    public class Recipe
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public RecipeDifficulty Difficulty { get; set; } = RecipeDifficulty.Easy;

        [BsonRepresentation(BsonType.ObjectId)]
        public string CategoryId { get; set; } = string.Empty;

        [BsonRepresentation(BsonType.ObjectId)]
        public string AuthorUserId { get; set; } = string.Empty;
    }
}
