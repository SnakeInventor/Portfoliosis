using System.Reflection.Metadata;
using System.Reflection;

namespace Portfoliosis.Infrastructure;

public static class AssemblyReferencer
{
    public static readonly Assembly Assembly = typeof(AssemblyReferencer).Assembly;
}
