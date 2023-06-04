using System.Reflection;

namespace SaoViet.Portal.Infrastructure;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
    public static readonly Assembly[] AppDomainAssembly = AppDomain.CurrentDomain.GetAssemblies();
    public static readonly Assembly ExecuteAssembly = Assembly.GetExecutingAssembly();
    public static readonly Assembly CallingAssembly = Assembly.GetCallingAssembly();
}

public static class AssemblyReference<T> where T : notnull
{
    public static readonly Assembly TypeAssembly = typeof(T).Assembly;
}