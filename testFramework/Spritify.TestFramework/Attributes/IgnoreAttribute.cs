using System;

namespace Spritify.TestFramework.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class IgnoreAttribute: NUnit.Framework.IgnoreAttribute
    {
        public IgnoreAttribute(string reason) : base(reason)
        {
        }
    }
}
