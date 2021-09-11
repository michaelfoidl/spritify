namespace Spritify.TestFramework.Interfaces.Lifecycle
{
    public interface ILifecycleContext<TTestContext>
    {
        string TestId { get; set; }
        TTestContext Context { get; set; }
        object CloneTestRunProperties();
        object CloneTestClassProperties();
    }
}