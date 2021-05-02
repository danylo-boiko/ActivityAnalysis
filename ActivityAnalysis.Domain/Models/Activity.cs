using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ActivityAnalysis.Domain.Models
{
    public class Activity
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("userId")]
        public ObjectId UserId { get; set; }
        [BsonElement("programTitle")]
        public string ProgramTitle { get; set; }
        [BsonElement("dayOfUse")]
        public string DayOfUse { get; set; }
        [BsonElement("timeSpent")]
        public TimeSpan TimeSpent { get; set; }

        public Activity(ObjectId userId, string programTitle, string dayOfUse, TimeSpan timeSpent)
        {
            Id = ObjectId.GenerateNewId();
            UserId = userId;
            ProgramTitle = programTitle;
            DayOfUse = dayOfUse;
            TimeSpent = timeSpent;
        }
    }
}