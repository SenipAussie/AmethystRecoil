using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sens_Amethyst_Recoil
{
    class Variables
    {
        public class Menu
        {
            // ENABLED //
            private static bool Enabled { get; set; } = false;
            public static bool isEnabled()
            { return Enabled; }
            public static void setEnabled(bool boolean)
            { Enabled = boolean; }
        }

        public class Weapon
        {
            // WEAPON NAME //
            private static string Name { get; set; } = string.Empty;
            public static string getName()
            {
                if (!String.IsNullOrEmpty(Name)) return Name;
                else return "None";
            }
            public static void setName(string newName)
            { Name = newName; }

            // AMMO //
            private static int Ammo { get; set; } = 0;
            public static int getAmmo()
            { return Ammo; }
            public static void setAmmo(int AmmoSize)
            { Ammo = AmmoSize; }

            // RECOIL X / Y //
            private static int[,] ActiveRecoil { get; set; } = { { 0, 0 } };
            public static int getRecoilX(int Bullet)
            { return ActiveRecoil[Bullet, 0]; }
            public static int getRecoilY(int Bullet)
            { return ActiveRecoil[Bullet, 1]; }
            public static void setRecoilPattern(int[,] Pattern)
            { ActiveRecoil = Pattern; }

            // DELAY //
            private static double[] Delay { get; set; } = { 0 };
            public static double getShotDelay(int Bullet)
            { return Delay[Bullet]; }
            public static void setShotDelay(double[] Delays)
            { Delay = Delays; }

            // SHOOTING MILLISECONDS //
            private static int ShootingMilliSec { get; set; } = 0;
            public static int getShootingMilliSec()
            { return ShootingMilliSec; }
            public static void setShootingMilliSec(int MilliSec)
            { ShootingMilliSec = MilliSec; }

            // SCOPES ATTACHMENTS //
            public static int scopeIndex { get; set; } = 0;
            private static string Scope { get; set; } = "None";
            private static double ScopeMulitplier { get; set; } = 1.0;
            public static string getActiveScope()
            {
                if (!string.IsNullOrEmpty(Scope) && Scope != "None")
                    return Scope;
                else return "None";
            }
            public static void changeScope()
            {
                switch (scopeIndex)
                {
                    case 0: // None
                        Scope = "None";
                        ScopeMulitplier = 1.0;
                        break;
                    case 1: // Simple
                        Scope = "Simple scope";
                        ScopeMulitplier = 0.8;
                        break;
                    case 2: // Holo 
                        Scope = "Holo scope";
                        ScopeMulitplier = 1.2;
                        break;
                    case 3: // 8x
                        Scope = "8x scope";
                        ScopeMulitplier = 3.83721;
                        break;
                    case 4: // 16x
                        Scope = "16x scope";
                        ScopeMulitplier = 7.65116;
                        scopeIndex = -1;
                        break;
                }
            }

            public static double getScopeMulitplier()
            { return ScopeMulitplier; }

            // BARREL ATTACHMENTS //
            public static int barrelIndex { get; set; } = 0;
            private static string Barrel { get; set; } = "None";
            private static double BarrelMultiplier { get; set; } = 1.0;
            public static string getActiveBarrel()
            {
                if (!string.IsNullOrEmpty(Barrel) && Barrel != "None")
                    return Barrel;
                else return "None";
            }
            public static void changeBarrel()
            {
                switch (barrelIndex)
                {
                    case 0: // None
                        Barrel = "None";
                        BarrelMultiplier = 1.0;
                        break;
                    case 1: // Suppressor
                        Barrel = "Suppressor";
                        BarrelMultiplier = 0.8;
                        break;
                    case 2: // Muzzle Booster
                        Barrel = "Muzzle boost";
                        BarrelMultiplier = 1.0;
                        MuzzleBoost = true;
                        break;
                    case 3: // Muzzle Break
                        Barrel = "Muzzle break";
                        BarrelMultiplier = 0.5;
                        barrelIndex = -1;
                        MuzzleBoost = false;
                        break;
                }
            }
            public static double getBarrelMultiplier()
            { return BarrelMultiplier; }

            // MUZZLE BOOST //
            private static bool MuzzleBoost { get; set; } = false;
            public static double isMuzzleBoost(double MilliSec)
            {
                if (MuzzleBoost)
                    return (MilliSec - (MilliSec * 0.1f));
                else
                    return MilliSec;
            }

            // RANDOMNESS //
            private static double Randomness { get; set; } = 5.0;
            public static void setRandomness(int RandomnessIndex)
            {
                switch (RandomnessIndex)
                {
                    case -1:
                        if (0 < Randomness)
                            Randomness--;
                        break;
                    case 1:
                        if (Randomness < 10)
                            Randomness++;
                        break;
                }
            }
            public static double getRandomness()
            { return Randomness; }
        }

        public class Settings
        {
            // SENSITIVITY //
            private static double Sensitivity { get; set; } = 1.0;
            public static void setSensitivity(int SensitivityIndex)
            {
                switch (SensitivityIndex)
                {
                    case -1:
                        if (0.2 < Sensitivity)
                            Sensitivity -= 0.1;
                        break;
                    case 1:
                        if (Sensitivity < 2.0)
                            Sensitivity += 0.1;
                        break;
                }
            }
            public static double getSensitivity()
            { return Sensitivity; }

            // FOV //
            private static int FOV { get; set; } = 90;
            public static void setFOV(int FOVIndex)
            {
                switch (FOVIndex)
                {
                    case -1:
                        if (71 < FOV)
                            FOV -= 1;
                        break;
                    case 1:
                        if (FOV < 90)
                            FOV += 1;
                        break;
                }
            }
            public static int getFOV()
            { return FOV; }
        }
    }
}
