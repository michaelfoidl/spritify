namespace Spritify.TestFramework.Attributes
{
    public class CategoryAttribute : NUnit.Framework.CategoryAttribute
    {
        public CategoryAttribute(string categoryName) : base(categoryName) { }
    }
}
