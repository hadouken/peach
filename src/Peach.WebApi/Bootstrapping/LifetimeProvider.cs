using Autofac.Builder;
using Autofac.Integration.WebApi;
using Peach.Core.Runtime;

namespace Peach.WebApi.Bootstrapping
{
    public class LifetimeProvider : ILifetimeProvider
    {
        public void InstanceScope<TLimit, TActivatorData, TRegistrationStyle>(IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> context)
        {
            context.InstancePerDependency();
        }

        public void SingletonScope<TLimit, TActivatorData, TRegistrationStyle>(IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> context)
        {
            context.SingleInstance();
        }

        public void RequestScope<TLimit, TActivatorData, TRegistrationStyle>(IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> context)
        {
            context.InstancePerApiRequest();
        }
    }
}