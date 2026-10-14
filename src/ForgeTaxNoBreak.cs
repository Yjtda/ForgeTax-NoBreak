using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;

[BepInPlugin(PluginGuid, PluginName, PluginVersion)]
public sealed class ForgeTaxNoBreak : BaseUnityPlugin
{
    internal const string PluginGuid = "jacky.forgetax.nobreak";
    internal const string PluginName = "ForgeTax No Break";
    internal const string PluginVersion = "1.1.0";

    internal static ManualLogSource Log;
    internal static ConfigEntry<bool> Enabled;

    private void Awake()
    {
        Log = Logger;
        Enabled = Config.Bind(
            "General",
            "EnableNoBreak",
            true,
            "When enabled, every destruction ticket/result is converted to damage.");

        try
        {
            new Harmony(PluginGuid).PatchAll(typeof(ForgeTaxNoBreak).Assembly);
            Log.LogInfo("ForgeTax No Break loaded: destruction is converted to damage.");
        }
        catch (Exception ex)
        {
            Log.LogError("ForgeTax No Break could not install its patches: " + ex);
        }
    }

    internal static bool IsEnabled()
    {
        return Enabled != null && Enabled.Value;
    }

    internal static ForgeChances ConvertBreakTickets(ForgeChances chances)
    {
        if (!IsEnabled())
        {
            return chances;
        }

        int destruction = chances[(int)ForgeOutcome.Destruction];
        if (destruction <= 0)
        {
            return chances;
        }

        int[] tickets = new int[5];
        for (int i = 0; i < tickets.Length; i++)
        {
            tickets[i] = chances[i];
        }

        tickets[(int)ForgeOutcome.Destruction] = 0;
        tickets[(int)ForgeOutcome.Damage] += destruction;
        return new ForgeChances(tickets);
    }

    [HarmonyPatch]
    private static class ForgeEconomyChancesPatch
    {
        private static IEnumerable<MethodBase> TargetMethods()
        {
            return typeof(ForgeEconomy)
                .GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(method => method.Name == "Chances" && method.ReturnType == typeof(ForgeChances));
        }

        private static void Postfix(ref ForgeChances __result)
        {
            __result = ConvertBreakTickets(__result);
        }
    }

    [HarmonyPatch(typeof(GPT6ForgeRoom), "ChancesFor")]
    private static class ForgeRoomChancesPatch
    {
        private static void Postfix(ref ForgeChances __result)
        {
            __result = ConvertBreakTickets(__result);
        }
    }

    [HarmonyPatch(typeof(GPT6ForgeRoom), "get_StartScreenChances")]
    private static class ForgeRoomStartScreenChancesPatch
    {
        private static void Postfix(ref ForgeChances __result)
        {
            __result = ConvertBreakTickets(__result);
        }
    }

    [HarmonyPatch]
    private static class ForgeEconomyApplyPatch
    {
        private static MethodBase TargetMethod()
        {
            MethodInfo[] methods = typeof(ForgeEconomy)
                .GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (MethodInfo method in methods)
            {
                if (method.Name == "Apply" && method.GetParameters().Length == 6)
                {
                    return method;
                }
            }

            throw new MissingMethodException("ForgeEconomy.Apply with six parameters was not found.");
        }

        private static void Prefix(ref ForgeOutcome raw)
        {
            if (IsEnabled() && raw == ForgeOutcome.Destruction)
            {
                raw = ForgeOutcome.Damage;
                Log.LogInfo("Converted a destruction outcome to damage.");
            }
        }
    }
}

