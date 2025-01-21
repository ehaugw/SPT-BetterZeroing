namespace BetterZeroing
{
    using System.Collections.Generic;
    using UnityEngine;
    using BepInEx;
    using HarmonyLib;
    using System;
    using System.Diagnostics;
    using EFT.InventoryLogic;
    using CommonAssets.Scripts.ArtilleryShelling.Client.Audio;
    using System.Linq;
    using EFT.UI;

    [BepInPlugin(GUID, NAME, VERSION)]
    public class BetterZeroing : BaseUnityPlugin
    {
        public const string GUID = "com.ehaugw.betterzeroing";
        public const string VERSION = "1.0.0";
        public const string NAME = "Better Zeroing";

        internal void Awake()
        {
            var harmony = new Harmony(GUID);
            harmony.PatchAll();
            //Print("Loaded BetterZeroing by ehaugw");
        }

        [HarmonyPatch(typeof(Weapon), "CreateOpticCalibrationData")]
        public class Weapon_CreateOpticCalibrationData
        {
            [HarmonyPrefix]
            public static void Prefix(Weapon __instance, ref AmmoTemplate ammoTemplate)
            {
                if (__instance?.Chambers is Slot[] slots && slots.Length > 0 && slots[0]?.ContainedItem is AmmoItemClass ammoClass && ammoClass.AmmoTemplate is AmmoTemplate ammo)
                {
                    ammoTemplate = ammo;
                }
            }
            //[HarmonyPostfix]
            //public static void Postfix(Weapon __instance, AmmoTemplate ammoTemplate, Vector3[] __result)
            //{
            //    Print("Using Ammo: " + ammoTemplate?._name ?? "null");
            //    Print("Vector info: " + String.Join(", ", __result));
            //}
        }

        //private static void Print(string message)
        //{
        //    //ConsoleScreen.Log(message);
        //    UnityEngine.Debug.Log(message);
        //    Console.WriteLine(message);
        //}
    }
}

//[HarmonyPatch(typeof(Weapon), "CreateOpticCalibrationPoints")]
//public class Weapon_CreateOpticCalibrationPoints
//{
//    [HarmonyPrefix]
//    public static void Prefix(Weapon __instance, ref AmmoTemplate __state)
//    {
//        //Print("Start of patch");
//        __state = __instance?.Template?.DefAmmoTemplate;
//        if (__instance?.Chambers is Slot[] slots && slots.Length > 0 && slots[0]?.ContainedItem is AmmoItemClass ammoClass && ammoClass.AmmoTemplate is AmmoTemplate ammo)
//        {
//            Print("Swapping Ammo to " + ammo._name);
//            SetAmmoTemplate(__instance, ammo);
//            Print("Swap done");
//        }
//    }

//    [HarmonyPostfix]
//    public static void Postfix(Weapon __instance, ref AmmoTemplate __state)
//    {
//        if (__state != null && __state != __instance?.Template?.DefAmmoTemplate)
//        { 
//            SetAmmoTemplate(__instance, __state);
//        }
//    }
//}

//private static void SetAmmoTemplate(Weapon weapon, AmmoTemplate template)
//{
//    At.SetField<WeaponTemplate>(weapon.Template, "_defAmmoTemplate", template);
//}