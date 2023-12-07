using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using PartyLib.Config;

namespace PartyLib.Mega
{
    public class MegaDownloader
    {
        [StructLayout(LayoutKind.Sequential)]
        private struct STARTUPINFO
        {
            public Int32 cb;
            public string lpReserved;
            public string lpDesktop;
            public string lpTitle;
            public Int32 dwX;
            public Int32 dwY;
            public Int32 dwXSize;
            public Int32 dwYSize;
            public Int32 dwXCountChars;
            public Int32 dwYCountChars;
            public Int32 dwFillAttribute;
            public Int32 dwFlags;
            public Int16 wShowWindow;
            public Int16 cbReserved2;
            public IntPtr lpReserved2;
            public IntPtr hStdInput;
            public IntPtr hStdOutput;
            public IntPtr hStdError;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct PROCESS_INFORMATION
        {
            public IntPtr hProcess;
            public IntPtr hThread;
            public int dwProcessId;
            public int dwThreadId;
        }

        [DllImport("kernel32.dll")]
        private static extern bool CreateProcess(
            string lpApplicationName,
            string lpCommandLine,
            IntPtr lpProcessAttributes,
            IntPtr lpThreadAttributes,
            bool bInheritHandles,
            uint dwCreationFlags,
            IntPtr lpEnvironment,
            string lpCurrentDirectory,
            [In] ref STARTUPINFO lpStartupInfo,
            out PROCESS_INFORMATION lpProcessInformation
        );

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CloseHandle(IntPtr hObject);

        private const int STARTF_USESHOWWINDOW = 1;
        private const int SW_SHOWNOACTIVATE = 4;
        private const int SW_SHOWMINNOACTIVE = 7;
        private const int CREATE_NEW_CONSOLE = 0x00000010;

        public static void StartProcessNoActivate(string cmdLine)
        {
            STARTUPINFO si = new STARTUPINFO();
            si.cb = Marshal.SizeOf(si);
            si.dwFlags = STARTF_USESHOWWINDOW;
            si.wShowWindow = SW_SHOWMINNOACTIVE;

            PROCESS_INFORMATION pi = new PROCESS_INFORMATION();

            CreateProcess(null, cmdLine, IntPtr.Zero, IntPtr.Zero, true,
                CREATE_NEW_CONSOLE, IntPtr.Zero, null, ref si, out pi);

            CloseHandle(pi.hProcess);
            CloseHandle(pi.hThread);
        }

        /// <summary>
        /// Downloads a MEGA file from a public link. Requires proxies.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="parentPath"></param>
        /// <param name="password">Optional file decryption key, if not included in URL</param>
        public void ExecuteMegaGet(string url, string parentPath, string password = "")
        {
            /*
            var process = new Process();
            var processInfo = new ProcessStartInfo();
            processInfo.FileName = $"{PartyConfig.MegaOptions.MegaCMDPath + "\\MEGAclient.exe"}"; // Executed via cmd so console window shows regardless
            if (password == "")
            {
                // Passwordless download
                processInfo.Arguments = $"get --ignore-quota-warn {url} \"{parentPath}\"";
            }
            else
            {
                // Passworded download
                processInfo.Arguments = $"get  --password={password} --ignore-quota-warn {url} \"{parentPath}\"";
            }
            processInfo.WorkingDirectory = PartyConfig.MegaOptions.MegaCMDPath;
            processInfo.WindowStyle = ProcessWindowStyle.Minimized; // Try not to annoy the users
            processInfo.UseShellExecute = true;
            process.StartInfo = processInfo;

            process.Start();

            process.WaitForExit();
            */
            if (password == "")
            {
                // Passwordless download
                StartProcessNoActivate($"{PartyConfig.MegaOptions.MegaCMDPath + "\\MEGAclient.exe"} get --ignore-quota-warn {url} \"{parentPath}\"");
            }
            else
            {
                // Passworded download
                StartProcessNoActivate($"{PartyConfig.MegaOptions.MegaCMDPath + "\\MEGAclient.exe"} get  --password={password} --ignore-quota-warn {url} \"{parentPath}\"");
            }
            // Internal create process provides no handle, so we just search the process list for it
            Process megaClient = Process.GetProcessesByName("MEGAclient").FirstOrDefault();
            megaClient.WaitForExit();
        }

        /// <summary>
        /// Class constructor
        /// </summary>
        public MegaDownloader()
        {
            if (PartyConfig.MegaOptions.MegaCMDPath == String.Empty)
            {
                throw new Exception("Mega downloader initialized, but MegaCMD not found! Did you set the install directory?");
            }
        }
    }
}