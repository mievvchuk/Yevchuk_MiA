using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LW4_Task2_MiA.Models
{
    public class Category
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required, StringLength(40, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        public CategoryType Type { get; set; } = CategoryType.Unknown;
    }
}
