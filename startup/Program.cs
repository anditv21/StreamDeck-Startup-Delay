using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Threading;

namespace startup
{
    internal class Program
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        static void Main(string[] args)
        {
            //hide console
            var handle = GetConsoleWindow();
            ShowWindow(handle, SW_HIDE);
            check();
        }

        static void check()
        {
            Process[] pname = Process.GetProcessesByName("ShareX");
            if (pname.Length == 0)
            {
                Thread.Sleep(1000);
                check();
            }
            else
            {
                var process = new Process
                {
                    StartInfo = {FileName = "C:\\Program Files\\Elgato\\StreamDeck\\StreamDeck.exe", Arguments = "--runinbk"}
                };
                process.Start();
                RegisterInStartup();
                Environment.Exit(0);
            }

        }

        static void RegisterInStartup()
        {
            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            registryKey.SetValue("streamdeckstartup", Application.ExecutablePath);
        }
    }
}