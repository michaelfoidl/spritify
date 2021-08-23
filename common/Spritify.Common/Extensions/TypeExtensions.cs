using System;

namespace Spritify.Common.Extensions
{
    public static class TypeExtensions
    {
        public static string GetUniqueKey(this Type type)
        {
            var key = type.AssemblyQualifiedName;

            if (key == null)
            {
                throw new ArgumentException($"Invalid type: Cannot read assembly-qualified name of type '{type.FullName}'.");
            }

            return key;
        }
    }
}
