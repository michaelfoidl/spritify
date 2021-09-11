namespace Spritify.TestFramework.CurrentContext
{
    public class CurrentTest
    {
        public static string Id => NUnit.Framework.TestContext.CurrentContext.Test.ID;
    }
}
