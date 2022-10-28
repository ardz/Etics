using Autofac;
using Etics.Server.Abstractions;
using Etics.Server.Service;

namespace Etics.Server;

public class AutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<KeyboardInputService>().As<IKeyboardInputService>().SingleInstance();
    }
}