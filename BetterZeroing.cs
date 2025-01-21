namespace BetterZeroing
{
    using BepInEx;
    using HarmonyLib;
    using EFT.InventoryLogic;

    [BepInPlugin(GUID, NAME, VERSION)]
    public class BetterZeroing : BaseUnityPlugin
    {
        public const string GUID = "com.ehaugw.betterzeroing";
        public const string VERSION = "1.0.1";
        public const string NAME = "Better Zeroing";

        internal void Awake()
        {
            var harmony = new Harmony(GUID);
            harmony.PatchAll();
        }

        [HarmonyPatch(typeof(Weapon), "CreateOpticCalibrationData")]
        public class Weapon_CreateOpticCalibrationData
        {
            [HarmonyPrefix]
            public static void Prefix(Weapon __instance, ref AmmoTemplate ammoTemplate)
            {
                if (__instance?.GetCurrentMagazine() is MagazineItemClass mag && mag.FirstRealAmmo() is AmmoItemClass ammoClass1 && ammoClass1.AmmoTemplate is AmmoTemplate ammo1)
                {
                    ammoTemplate = ammo1;
                }
                else if (__instance?.Chambers is Slot[] slots && slots.Length > 0 && slots[0]?.ContainedItem is AmmoItemClass ammoClass2 && ammoClass2.AmmoTemplate is AmmoTemplate ammo2)
                {
                    ammoTemplate = ammo2;
                }
            }
            //[HarmonyPostfix]
            //public static void Postfix(Weapon __instance, AmmoTemplate ammoTemplate, Vector3[] __result)
            //{
            //    Console.WriteLine("Using Ammo: " + ammoTemplate?._name ?? "null");
            //    Console.WriteLine("Vector info: " + String.Join(", ", __result));
            //}
        }
   }
}
