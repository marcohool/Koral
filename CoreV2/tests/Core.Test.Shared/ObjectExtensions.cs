using FluentAssertions.Json;
using Newtonsoft.Json.Linq;

namespace Core.UnitTest.Shared;

public static class ObjectExtensions
{
    public static bool IsEquivalentJson(this object subject, object expected)
    {
        try
        {
            subject.ShouldBeEquivalentTo(expected);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static void ShouldBeEquivalentTo(this object subject, object expected)
    {
        JToken.FromObject(subject).Should().BeEquivalentTo(JToken.FromObject(expected));
    }
}
