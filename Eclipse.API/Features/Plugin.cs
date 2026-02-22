namespace Eclipse.API.Features
{
    using Eclipse.API.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    public abstract class Plugin : IPlugin
    {
        public virtual string Name { get; }
        public virtual string Author { get; }
        public virtual Version Version { get; }

        public virtual void OnEnabled()
        {
            var attribute = GetType().GetCustomAttribute<AssemblyInformationalVersionAttribute>();
            Console.WriteLine($"{Name} v{(Version is not null ? $"{Version.Major}.{Version.Minor}" : attribute is not null ? attribute.InformationalVersion : string.Empty)} by {Author} has been enabled!");
        }
        public virtual void OnDisable() => Log.Info($"{Name} has been disabled!");
    }
}
