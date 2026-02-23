namespace Eclipse.Loader
{
    using HarmonyLib;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using YamlDotNet;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Eclipse.API.Interfaces;
    using System.Reflection;
    using Eclipse.API.Features;
    using System.Security.Policy;
    using System.Xml.Linq;
    using YamlDotNet.Core.Tokens;
    using YamlDotNet.Serialization.NamingConventions;
    using YamlDotNet.Serialization;
    using System.Runtime.InteropServices;

    public class Loader
    {
        private static string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private static string eclipsePath = Path.Combine(appData, "Eclipse");
        private static string pluginsPath = Path.Combine(eclipsePath, "Plugins");
        private static string dependencyPath = Path.Combine(eclipsePath, "Dependencies");
        private static string configsPath = Path.Combine(eclipsePath, "Configs");

        public static List<IPlugin> Plugins { get; } = new List<IPlugin>();
        public static void Load()
        {
            try
            {
                Server.Start();
                Console.WriteLine("Initializing Mod Loader");             

                if (!Directory.Exists(eclipsePath))
                {
                    Directory.CreateDirectory(eclipsePath);
                    Console.WriteLine($"Created directory at {eclipsePath}");
                }

                if (!Directory.Exists(pluginsPath))
                {
                    Directory.CreateDirectory(pluginsPath);
                    Console.WriteLine($"Created plugins directory at {pluginsPath}");
                }
                if (!Directory.Exists(configsPath))
                {
                    Directory.CreateDirectory(configsPath);
                    Console.WriteLine($"Created config directory at {configsPath}");
                }
                if (!Directory.Exists(dependencyPath))
                {
                    Directory.CreateDirectory(dependencyPath);
                    Console.WriteLine($"Created dependency directory at {dependencyPath}");
                }

                LoadDependencies();
                LoadPlugins();

                var typedPlugins = Plugins.Where(p => p.GetType().GetInterface("IPlugin`1") != null) .Select(p => (IPlugin)p).ToList();

                var loadedConfigs = ConfigManager.Load(typedPlugins);

                foreach (var plugin in typedPlugins)
                {
                    plugin.Config = loadedConfigs[plugin.Name];
                }
                OnEnabled();
                var harmony = new Harmony("com.spudermans.eclipse");
                harmony.PatchAll();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load mod loader error: {ex}");
            }
        }
        public static void LoadPlugins()
        {
            foreach(var pluginPath in Directory.GetFiles(pluginsPath).Where(p => p.EndsWith(".dll")))
            {
                Assembly assembly = LoadAssembly(pluginPath, false);
                if (assembly == null) continue;

                foreach(Type type in assembly.GetTypes())
                {
                    if (typeof(IPlugin).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                    {
                        try
                        {
                            IPlugin pluginInstance = (IPlugin)Activator.CreateInstance(type);
                            Plugins.Add(pluginInstance);
                            
                            Console.WriteLine($"Loaded Plugin: {pluginInstance.Name} by @{pluginInstance.Author} , Version {pluginInstance.Version}");
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine($"Failed to load plugin from type {type.FullName} error: {ex}");
                        }
                    }
                }

            }
        }
        public static void LoadDependencies()
        {
            foreach(var dependencyFile in Directory.GetFiles(dependencyPath).Where(p => p.EndsWith(".dll")))
            {
                LoadAssembly(dependencyFile, true);           
            }
        }

        public static Assembly LoadAssembly(string path, bool isDependency)
        {
            try
            {
                if (isDependency)
                {
                    var assemblyName = AssemblyName.GetAssemblyName(path);
                    var assemblyVersion = assemblyName.Version;
                    Console.WriteLine($"Loaded dependency: {assemblyName}, {assemblyVersion}");
                    return Assembly.LoadFrom(path);
                }
                else
                {
                    return Assembly.LoadFrom(path);
                }            
            }
            catch(Exception ex)
            {
                if (isDependency) Console.WriteLine($"Failed to load dependency at {path} error: {ex}");
                else Console.WriteLine($"Failed to load assembly at {path} error: {ex}");
                return null;
            }
        }

        public static void OnEnabled()
        {
            foreach (IPlugin plugin in Plugins)
            {
                try
                {
                    plugin.OnEnabled();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Catch error in plugin {plugin.Name} error: {ex}");
                }
            }
        }

    }
}
