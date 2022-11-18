using System.Collections.Immutable;
using FluentAssertions;
using LanguageExt;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace LanguageExtExamples;

public class _01_SerializationDeserialization
{
    [Test]
    public void ShouldBeSerializedWithBothSerializersTheSameAsList()
    {
        var stjSerializedSeq = JsonSerializer.Serialize(Seq<int>.Empty.Add(1).Add(2));
        var nsSerializedSeq = JsonConvert.SerializeObject(Seq<int>.Empty.Add(1).Add(2));
        var stjSerializedArr = JsonSerializer.Serialize(Arr<int>.Empty.Add(1).Add(2), SystemTextJsonOptions.WithLanguageExtExtensions());
        var nsSerializedArr = JsonConvert.SerializeObject(Arr<int>.Empty.Add(1).Add(2));
        var serializedList = JsonConvert.SerializeObject(new List<int> { 1, 2 });

        stjSerializedSeq.Should().Be(serializedList);
        nsSerializedSeq.Should().Be(serializedList);
        stjSerializedArr.Should().Be(serializedList);
        nsSerializedArr.Should().Be(serializedList);
    }
    
    [Test]
    public void ShouldBeDeserializedWithBothSerializersFromSerializedList()
    {
        var originalList = new List<int> { 1, 2 };
        var serializedList = JsonConvert.SerializeObject(originalList);
        var nsDeserializedSeq = JsonConvert.DeserializeObject<Seq<int>>(serializedList);
        var stjDeserializedSeq = JsonSerializer.Deserialize<Seq<int>>(serializedList, SystemTextJsonOptions.WithLanguageExtExtensions());
        var nsDeserializedArr = JsonConvert.DeserializeObject<Arr<int>>(serializedList);
        var stjDeserializedArr = JsonSerializer.Deserialize<Arr<int>>(serializedList, SystemTextJsonOptions.WithLanguageExtExtensions());

        stjDeserializedSeq.Should().Equal(originalList);
        nsDeserializedSeq.Should().Equal(originalList);
        
        stjDeserializedArr.ToArray().Should().Equal(originalList); //conversion needed
        nsDeserializedArr.ToArray().Should().Equal(originalList); //conversion needed
    }
    
    [Test]
    public void ShouldBeSerializedWithinDataStructureWithBothSerializersTheSameAsList()
    {
        var stjSerializedSeqRecord = JsonSerializer.Serialize(new DataWithSeq(Seq<int>.Empty.Add(1).Add(2)));
        var nsSerializedSeqRecord = JsonConvert.SerializeObject(new DataWithSeq(Seq<int>.Empty.Add(1).Add(2)));
        var stjSerializedArrRecord = JsonSerializer.Serialize(new DataWithArr(Arr<int>.Empty.Add(1).Add(2)), SystemTextJsonOptions.WithLanguageExtExtensions());
        var nsSerializedArrRecord = JsonConvert.SerializeObject(new DataWithArr(Arr<int>.Empty.Add(1).Add(2)));
        var nsSerializedListRecord = JsonConvert.SerializeObject(new DataWithImmutableArray(ImmutableArray<int>.Empty.AddRange(1,2)));
        var stjSerializedListRecord = JsonSerializer.Serialize(new DataWithImmutableArray(ImmutableArray<int>.Empty.AddRange(1,2)), SystemTextJsonOptions.WithLanguageExtExtensions());

        stjSerializedSeqRecord.Should().Be(nsSerializedListRecord);
        nsSerializedSeqRecord.Should().Be(nsSerializedListRecord);
        stjSerializedArrRecord.Should().Be(stjSerializedListRecord);
        nsSerializedArrRecord.Should().Be(nsSerializedListRecord);
    }
    
    [Test]
    public void ShouldBeDeserializedWithinDataStructureWithBothSerializersFromSerializedList()
    {
        var originalList = new DataWithImmutableArray(ImmutableArray<int>.Empty.AddRange(1,2));
        var serializedList = JsonConvert.SerializeObject(originalList);
        var nsDeserializedSeq = JsonConvert.DeserializeObject<DataWithSeq>(serializedList);
        var stjDeserializedSeq = JsonSerializer.Deserialize<DataWithSeq>(serializedList, SystemTextJsonOptions.WithLanguageExtExtensions());
        var nsDeserializedArr = JsonConvert.DeserializeObject<DataWithArr>(serializedList);
        var stjDeserializedArr = JsonSerializer.Deserialize<DataWithArr>(serializedList, SystemTextJsonOptions.WithLanguageExtExtensions());

        stjDeserializedSeq.Ints.Should().Equal(originalList.Ints);
        nsDeserializedSeq.Ints.Should().Equal(originalList.Ints);
        
        stjDeserializedArr.Ints.ToArray().Should().Equal(originalList.Ints);
        nsDeserializedArr.Ints.ToArray().Should().Equal(originalList.Ints);
    }

    public record DataWithImmutableArray(ImmutableArray<int> Ints);
    public record DataWithSeq(Seq<int> Ints);
    public record DataWithArr(Arr<int> Ints);
}