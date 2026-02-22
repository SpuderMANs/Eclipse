namespace Eclipse.Events
{
    using Eclipse.API.Features;
    using Eclipse.API.Interfaces;
    using Eclipse.Events.EventArgs.Player;
    using Eclipse.Events.EventArgs.Round;
    using Eclipse.Events.Features;
    using Eclipse.Events.Handlers;
    using HarmonyLib;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using UnityEngine;
    using Coroutine = Eclipse.API.Features.Coroutine;
    using Player = Eclipse.API.Features.Player;

    public class Plugin : IPlugin
    {
        public string Name => "Eclipse.Events";
        public string Author { get; set; } = "SpuderMANs";
        public Version Version { get; set; } = new Version(1, 0, 0);

        public void OnEnabled()
        {
            Log.Info("Eclipse.Events has been enabled!");
            var harmony = new Harmony("com.spudermans.eclipseevents");
            harmony.PatchAll();
        }
        public void OnDisable()
        {
            var harmony = new Harmony("com.spudermans.eclipse");
            harmony.UnpatchAll();

        }
    }
    
}
