using System;

namespace Spritify.TestFramework.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class KnownIssueAttribute : IgnoreAttribute
    {
        private const string ReasonTemplate = "Known issue: #{0}";

        public KnownIssueAttribute(int issue) : base(string.Format(KnownIssueAttribute.ReasonTemplate, issue)) { }
    }
}
