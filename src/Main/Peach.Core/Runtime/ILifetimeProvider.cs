using Autofac.Builder;

namespace Peach.Core.Runtime
{
    public interface ILifetimeProvider
    {
        void InstanceScope<TLimit, TActivatorData, TRegistrationStyle>(IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> context);
        void SingletonScope<TLimit, TActivatorData, TRegistrationStyle>(IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> context);
        void RequestScope<TLimit, TActivatorData, TRegistrationStyle>(IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> context);
    }
}
