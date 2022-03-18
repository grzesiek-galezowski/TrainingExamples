using System.Net;
using FluentAssertions;
using NUnit.Framework;

namespace AntiAntiMockSpecification;

internal class DataRevealsImplementationDetails
{
    [Test]
    public void ShouldScaleUpWhenResultIsOkAndLessThan4()
    {
        //GIVEN
        var result = new OperationResult(3, HttpStatusCode.OK);
        var scaler = new NumberScaler(10);
            
        //WHEN
        var scaledResult = scaler.Scale(result);

        //THEN
        scaledResult.Should().Be(new OperationResult(13, HttpStatusCode.OK));
    }

    [Test]
    public void ShouldNotScaleUpWhenResultIsNotOk()
    {
        //GIVEN
        var result = new OperationResult(3, HttpStatusCode.InternalServerError);
        var scaler = new NumberScaler(10);
            
        //WHEN
        var scaledResult = scaler.Scale(result);

        //THEN
        scaledResult.Should().Be(result);
    }
    
    [Test]
    public void ShouldNotScaleUpWhenResultIsAtLeast4()
    {
        //GIVEN
        var result = new OperationResult(4, HttpStatusCode.OK);
        var scaler = new NumberScaler(10);
            
        //WHEN
        var scaledResult = scaler.Scale(result);

        //THEN
        scaledResult.Should().Be(result);
    }
}

internal class NumberScaler
{
    private readonly int _i;

    public NumberScaler(int i)
    {
        _i = i;
    }

    public OperationResult Scale(OperationResult result)
    {
        if (result.Status != HttpStatusCode.OK)
        {
            return result;
        }

        if (result.Number > 3)
        {
            return result;
        }
        return result with { Number = result.Number + _i };
    }
}

internal record OperationResult(int Number, HttpStatusCode Status);