using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sens_Amethyst_Recoil
{
    class Hotkeys
    {
        private static volatile MessageWindow _wnd;
        private static volatile IntPtr _hwnd;
        private static ManualResetEvent _windowReadyEvent = new ManualResetEvent(false);
        private static event EventHandler<HotKeyEventArgs> HotKeyPressed;

        private class MessageWindow : Form
        {
            public MessageWindow()
            {
                _wnd = this;
                _hwnd = this.Handle;
                _windowReadyEvent.Set();
            }

            protected override void WndProc(ref Message m)
            {
                if (m.Msg == WM_HOTKEY)
                {
                    HotKeyEventArgs e = new HotKeyEventArgs(m.LParam);
                    Hotkey.OnHotKeyPressed(e);
                }
                base.WndProc(ref m);
            }

            protected override void SetVisibleCore(bool value)
            { base.SetVisibleCore(false); }
            private const int WM_HOTKEY = 0x312;
        }

        static Hotkeys()
        {
            Thread messageLoop = new Thread(delegate ()
            { Application.Run(new MessageWindow()); });
            messageLoop.Name = "HotKeyMessageThread";
            messageLoop.IsBackground = true;
            messageLoop.Start();
        }

        [Flags] public enum KeyModifiers { Control = 2 }

        public class HotKeyEventArgs : EventArgs
        {
            public readonly Keys Key;
            public readonly KeyModifiers Modifiers;

            public HotKeyEventArgs(IntPtr hotKeyParam)
            {
                uint param = (uint)hotKeyParam.ToInt64();
                Key = (Keys)((param & 0xffff0000) >> 16);
                Modifiers = (KeyModifiers)(param & 0x0000ffff);
            }
        }

        public class Initiate
        {
            private static int _id = 0;
            public static int RegisterHotKey(Keys key, KeyModifiers modifiers)
            {
                _windowReadyEvent.WaitOne();
                int id = Interlocked.Increment(ref _id);
                _wnd.Invoke(new RegisterHotKeyDelegate(RegisterHotKeyInternal), _hwnd, id, (uint)modifiers, (uint)key);
                return id;
            }

            delegate void RegisterHotKeyDelegate(IntPtr hwnd, int id, uint modifiers, uint key);

            private static void RegisterHotKeyInternal(IntPtr hwnd, int id, uint modifiers, uint key)
            {  DLLImports.RegisterHotKey(hwnd, id, modifiers, key); }

            public static void InitiateHotKeys()
            {
                RegisterHotKey(Keys.Tab, KeyModifiers.Control);
                RegisterHotKey(Keys.NumPad1, KeyModifiers.Control); // AK
                RegisterHotKey(Keys.NumPad2, KeyModifiers.Control); // LR300
                RegisterHotKey(Keys.NumPad3, KeyModifiers.Control); // Semi
                RegisterHotKey(Keys.NumPad4, KeyModifiers.Control); // Custom
                RegisterHotKey(Keys.NumPad5, KeyModifiers.Control); // MP5
                RegisterHotKey(Keys.NumPad6, KeyModifiers.Control); // Thompson
                RegisterHotKey(Keys.NumPad7, KeyModifiers.Control); // M39
                RegisterHotKey(Keys.NumPad8, KeyModifiers.Control); // M92
                RegisterHotKey(Keys.NumPad9, KeyModifiers.Control); // M249
                RegisterHotKey(Keys.Left, KeyModifiers.Control); // Smoothness Down
                RegisterHotKey(Keys.Right, KeyModifiers.Control); // Smoothness Up
                RegisterHotKey(Keys.Up, KeyModifiers.Control); // Randomness Up
                RegisterHotKey(Keys.Down, KeyModifiers.Control); // Randomness Down
                RegisterHotKey(Keys.Add, KeyModifiers.Control); // Scopes
                RegisterHotKey(Keys.Enter, KeyModifiers.Control); // Barrels
                HotKeyPressed += new EventHandler<HotKeyEventArgs>(Hotkey.HotKeys_HotKeyPressed);
            }
        }

        public class Hotkey
        {

            public static void OnHotKeyPressed(HotKeyEventArgs e)
            { HotKeyPressed?.Invoke(null, e); }

            public static void HotKeys_HotKeyPressed(object sender, HotKeyEventArgs e)
            {
                switch (e.Key)
                {
                    case Keys.Tab:
                        Variables.Menu.setEnabled(!Variables.Menu.isEnabled());
                        break;
                    case Keys.NumPad1:
                        Weapons.setVariables(1);
                        break;
                    case Keys.NumPad2:
                        Weapons.setVariables(2);
                        break;
                    case Keys.NumPad3:
                        Weapons.setVariables(3);
                        break;
                    case Keys.NumPad4:
                        Weapons.setVariables(4);
                        break;
                    case Keys.NumPad5:
                        Weapons.setVariables(5);
                        break;
                    case Keys.NumPad6:
                        Weapons.setVariables(6);
                        break;
                    case Keys.NumPad7:
                        Weapons.setVariables(7);
                        break;
                    case Keys.NumPad8:
                        Weapons.setVariables(8);
                        break;
                    case Keys.NumPad9:
                        Weapons.setVariables(9);
                        break;
                    case Keys.Left:
                        Variables.Weapon.setRandomness(-1);
                        break;
                    case Keys.Right:
                        Variables.Weapon.setRandomness(1);
                        break;
                    case Keys.Up:
                        Variables.Settings.setSensitivity(1);
                        break;
                    case Keys.Down:
                        Variables.Settings.setSensitivity(-1);
                        break;
                    case Keys.Add:
                        Variables.Weapon.scopeIndex++;
                        Variables.Weapon.changeScope();
                        break;
                    case Keys.Enter:
                        Variables.Weapon.barrelIndex++;
                        Variables.Weapon.changeBarrel();
                        break;
                }
                Console.Clear();
                Display.PrintHeader();
            }
        }
    }
}
