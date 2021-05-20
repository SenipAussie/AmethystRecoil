using System;
using System.Threading;
using System.Windows.Forms;
using static Sens_Amethyst_Recoil.Variables;

namespace Sens_Amethyst_Recoil
{
    class Recoil
    {
        private static bool IsKeyDown(Keys key)
        { return 0 != (DLLImports.GetAsyncKeyState((int)key) & 0x8000); }

        public static void RecoilLoop()
        {
            while (true)
            {
                if (Variables.Menu.isEnabled() && !string.IsNullOrEmpty(Weapon.getName()))
                {
                    while (IsKeyDown(Keys.LButton) && IsKeyDown(Keys.RButton))
                    {
                        for (int i = 0; i <= Weapon.getAmmo() - 1; i++)
                        {
                            if (!IsKeyDown(Keys.LButton)) break;
                            Smoothing(Weapon.isMuzzleBoost(Weapon.getShootingMilliSec()),
                            Weapon.isMuzzleBoost(Weapon.getShotDelay(i)),
                            (int)((((Weapon.getRecoilX(i) + GetRandomNumber(0.0, Weapon.getRandomness())) / 4) / Settings.getSensitivity()) * Weapon.getScopeMulitplier() * Weapon.getBarrelMultiplier()),
                            (int)((((Weapon.getRecoilY(i) + GetRandomNumber(0.0, Weapon.getRandomness())) / 4) / Settings.getSensitivity()) * Weapon.getScopeMulitplier() * Weapon.getBarrelMultiplier()));
                            DLLImports.mouse_event(0x0001, 0, 0, 0, 0);
                        }
                    }
                }
                Thread.Sleep(1);
            }
        }

        private static double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        static private void Smoothing(double MS, double ControlledTime, int X, int Y)
        {
            int oldX = 0, oldY = 0, oldT = 0;
            for (int i = 1; i <= (int)ControlledTime; ++i)
            {
                int newX = i * X / (int)ControlledTime;
                int newY = i * Y / (int)ControlledTime;
                int newTime = i * (int)ControlledTime / (int)ControlledTime;
                DLLImports.mouse_event(1, newX - oldX, newY - oldY, 0, 0);
                PerciseSleep(newTime - oldT);
                oldX = newX; oldY = newY; oldT = newTime;
            }
            PerciseSleep((int)MS - (int)ControlledTime);
        }

        static private void PerciseSleep(int ms)
        {
            DLLImports.QueryPerformanceFrequency(out long timerResolution);
            timerResolution /= 1000;

            DLLImports.QueryPerformanceCounter(out long currentTime);
            long wantedTime = currentTime / timerResolution + ms;
            currentTime = 0;
            while (currentTime < wantedTime)
            {
                DLLImports.QueryPerformanceCounter(out currentTime);
                currentTime /= timerResolution;
            }
        }
    }
}
