using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.ObjectModel;
using System.Threading;

namespace MongoDB.Entities.Tests;

[Collection("ReviewInt64")]
public class ReviewInt64 : Review
{
    [BsonId]
    public long Id { get; set; }

    public override object GenerateNewID()
    {
        Thread.Sleep(10);
        return Convert.ToInt64(DateTime.UtcNow.Ticks);
    }

    public override bool HasDefaultID()
        => Id == 0;

    public Collection<BookInt64> Books { get; set; }
}