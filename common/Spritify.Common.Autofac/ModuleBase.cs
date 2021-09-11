using Autofac;

namespace Spritify.Common.Autofac
{
    public abstract class ModuleBase
    {
        public abstract void Load(ContainerBuilder builder);
    }
}
