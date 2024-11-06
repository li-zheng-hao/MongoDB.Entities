using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Threading;

namespace MongoDB.Entities.Tests;

[Collection("PlaceInt64")]
public class PlaceInt64 : Place
{
    [BsonId]
    public Int64 Id { get; set; }

    public override object GenerateNewID()
    {
        Thread.Sleep(10);
        return Convert.ToInt64(DateTime.UtcNow.Ticks);
    }

    public override bool HasDefaultID()
          => Id == 0;
}