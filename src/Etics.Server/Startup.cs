using Autofac;
using Etics.Server.Abstractions;
using Etics.Server.Configurations;
using Etics.Server.Service;

namespace Etics.Server;

public class Startup
{
    //--other code configuration
        
    //This method will call the after ConfigureServices Method.
    public void ConfigureContainer(ContainerBuilder builder)
    {
        builder.RegisterModule(new KeyboardInputModule());
    }

    //-- other code configuration
}