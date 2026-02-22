namespace Eclipse.API.Interfaces
{
    using System;
    using System.Reflection;

    public interface IPlugin
    {
        string Name { get; }
        string Author { get; }
        Version Version { get; }

        void OnEnabled();
        void OnDisable();
    }
}
