
using FluentAssertions;
using Portfoliosis.Core.Entities;
using Portfoliosis.Core.ValueObjects;

namespace Portfoliosis.Core.Tests;

public class MessageTests
{
    [Fact]
    public void CanCreateSimpleMessage()
    {
        // arrange
        string name = "John Human";
        MyEmailAddress email = MyEmailAddress.TryCreate("johnhuman@rogaikopita.org").Value;
        string text = """
                      Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Donec odio. Quisque volutpat mattis eros. Nullam malesuada erat ut turpis. Suspendisse urna nibh viverra non semper suscipit posuere a pede.
                      
                      Donec nec justo eget felis facilisis fermentum. Aliquam porttitor mauris sit amet orci. Aenean dignissim pellentesque felis.
                      """;
        DateTime utcNow = DateTime.UtcNow;

        // act
        var result = Message.TryCreate(name, email, text);

        // assert
        result.IsSuccess.Should().BeTrue();
        Message message = result.Value;

        message.Id.ToString().Should().NotBeEmpty();
        message.Name.Should().Be(name);
        message.Email.Should().Be(email);
        // possible issues if timestamp taken exactly at 00. Should be negligible.
        message.TimeStamp.Hour.Should().Be(utcNow.Hour);
    }
}
