using FluentAssertions;
using Portfoliosis.Core.ValueObjects;

namespace Portfoliosis.Core.Tests;

public class MyEmailAddressTests
{
    [Fact]
    public void CanCreateInstanceFromSimpleEmail()
    {
        // arrange
        string sampleEmail = "user.name@sub.domain.tld";

        // act
        var result = MyEmailAddress.TryCreate(sampleEmail);

        // assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be(sampleEmail);
    }
}
