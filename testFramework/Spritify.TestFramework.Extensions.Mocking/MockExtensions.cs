using System;
using Moq;

namespace Spritify.TestFramework.Extensions.Mocking
{
    public static class MockExtensions
    {
        public static void SetupDispose<TMocked>(this Mock<TMocked> mock) where TMocked : class, IDisposable
        {
            mock.Setup(m => m.Dispose());
        }
    }
}
