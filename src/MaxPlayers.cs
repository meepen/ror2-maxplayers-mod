using Harmony;
using RoR2.Mods;
using RoR2;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace MaxPlayersMod
{
    public class MaxPlayers
    {
        [ModEntry("Max Players", "1.0.0", "Meepen")]
        public static void Init()
        {
            var harmony = HarmonyInstance.Create("dev.meepen.maxplayers");

            var _base = typeof(RoR2.Networking.GameNetworkManager).GetNestedType("SvMaxPlayersConVar", BindingFlags.NonPublic);
            var _method = _base.GetMethod("SetString", BindingFlags.Instance | BindingFlags.Public);
            harmony.Patch(_method, null, new HarmonyMethod(typeof(MaxPlayers).GetMethod("Postfix", BindingFlags.NonPublic | BindingFlags.Static)));
        }
        
        static void Postfix(string newValue)
        {
            int num;
            if (int.TryParse(newValue, out num))
                typeof(RoR2Application).GetField("maxPlayers", BindingFlags.Static | BindingFlags.Public).SetValue(null, num);
        }
    }
}