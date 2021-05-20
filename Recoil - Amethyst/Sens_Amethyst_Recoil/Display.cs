using System;

namespace Sens_Amethyst_Recoil
{
    class Display
    {
        private static void InitializeWindow()
        {
            Console.SetWindowSize(50, 28);
            Console.SetBufferSize(50, 28);
            Console.Title = "Amethyst Recoil";
            PrintHeader();
        }

        public static void PrintHeader()
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine(@"     _                   _   _               _   ");
            Console.WriteLine(@"    / \   _ __ ___   ___| |_| |__  _   _ ___| |_ ");
            Console.WriteLine(@"   / _ \ | '_ ` _ \ / _ \ __| '_ \| | | / __| __|");
            Console.WriteLine(@"  / ___ \| | | | | |  __/ |_| | | | |_| \__ \ |_ ");
            Console.WriteLine(@" /_/   \_\_| |_| |_|\___|\__|_| |_|\__, |___/\__|");
            Console.WriteLine(@"                                   |___/         ");
            Console.WriteLine(@"  Version: 1.0 | Made by: Sen | Release: Public  ");
            Console.WriteLine(" ------------------------------------------------");
            Console.ResetColor();
            Console.WriteLine(" CTRL + NumberPad:");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;

            Console.WriteLine(" NUM_1: Ak-47    NUM_2: LR-300   NUM_3: SAR ");
            Console.WriteLine(" NUM_4: Custom   NUM_5: MP5      NUM_6: Thompson ");
            Console.WriteLine(" NUM_7: M39      NUM_8: M92      NUM_9: M249 ");
            Console.WriteLine();
            Console.ResetColor();
            Console.WriteLine(" Number Pad + / Enter Settings:");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine(" +: Cycle sights [Simple/Holo/8x/16x/None]");
            Console.WriteLine(" Enter: Cycle Barrels [Supp/Boost/Break/None]");
            Console.WriteLine();
            Console.ResetColor();
            Console.WriteLine(" Arrow Key Settings:");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine(" Randomness: [<- Less Random] [-> More Random]");
            Console.WriteLine(" In-game Sens: [^ Higher Sens] [v Lower Sens]");
            Console.WriteLine(" ------------------------------------------------");
            Console.ResetColor();
            Console.WriteLine("   Randomness     Sensitivity     Field of View");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write("      ");
            Console.Write("{0:0.0}", Variables.Weapon.getRandomness());
            Console.Write("             ");
            Console.Write("{0:0.0}", Variables.Settings.getSensitivity());
            Console.Write("               ");
            Console.Write(Variables.Settings.getFOV() + "\n");
            Console.ResetColor();
            Console.WriteLine();
            Console.Write(" Enabled: ");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write(Variables.Menu.isEnabled() + "\n");
            Console.ResetColor();

            Console.Write(" Current Weapon: ");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write(Variables.Weapon.getName() + "\n");
            Console.ResetColor();

            Console.Write(" Current Scope: ");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write(Variables.Weapon.getActiveScope() + "\n");
            Console.ResetColor();

            Console.Write(" Current Barrel: ");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write(Variables.Weapon.getActiveBarrel());
            Console.ResetColor();
        }

        static void Main(string[] args)
        {
            InitializeWindow();
            Hotkeys.Initiate.InitiateHotKeys();
            Recoil.RecoilLoop();
        }
    }
}
