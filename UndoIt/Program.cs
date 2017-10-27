using RecoveryThisFile.RangeTree;
using KickassUndelete;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Reflection;
using System.Threading;

namespace RecoveryThisFile
{

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static Mutex mutex = new Mutex(true, "{870A625B-030C-4E85-8F03-3EF1E558A577}");
        [STAThread]
        static void Main(string[] args)
        {
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                try
                {
                    if (IsWindows())
                        AttachConsole(1);
                    ParseArgs(args);

                    EnsureUserIsAdmin();

                    if (!IsWindows())
                    {
                        PrintUsage();
                        Environment.Exit(0);
                    }
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new StartForm());
                }
                finally
                {
                    mutex.ReleaseMutex();
                }
            }
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool AttachConsole(int dwProcessId);
        static void PrintUsage()
        {
            Console.Error.WriteLine("Usage: RecoveryThisFile {\n" +
                                                            "         -listdisks: Show all attached disks, and their filesystems.\n" +
                                                            "         -listfiles <FS>: List deleted files on the disk with name <FS>.\n" +
                                                            "         -dumpfile <FS> <File>: Write the contents of file <File> on disk <FS> to stdout.\n" +
                                                            "}");
        }

        static void ParseArgs(string[] args)
        {
            for (var i = 0; i < args.Count(); i++)
            {
                if (string.Equals(args[i], "-listdisks", StringComparison.OrdinalIgnoreCase))
                {
                    ConsoleCommands.ListDisks();
                    Environment.Exit(0);
                }
                else if (string.Equals(args[i], "-listfiles", StringComparison.OrdinalIgnoreCase))
                {
                    if (i + 1 >= args.Count())
                    {
                        PrintUsage();
                        Console.Error.WriteLine("Expected: Disk name");
                        Environment.Exit(1);
                    }
                    var disk = args[i + 1];
                    ConsoleCommands.ListFiles(disk);
                    Environment.Exit(0);
                }
                else if (string.Equals(args[i], "-dumpfile", StringComparison.OrdinalIgnoreCase))
                {
                    if (i + 2 >= args.Count())
                    {
                        PrintUsage();
                        Console.Error.WriteLine("Expected: Disk name and file name.");
                        Environment.Exit(1);
                    }
                    var disk = args[i + 1];
                    var file = args[i + 2];
                    ConsoleCommands.DumpFile(disk, file);
                    Environment.Exit(0);
                }
                else
                {
                    PrintUsage();
                    Console.Error.WriteLine("Unknown parameter: " + args[i]);
                    Environment.Exit(1);
                }
            }
            GC.Collect();
        }

        static bool IsWindows()
        {
            int p = (int)Environment.OSVersion.Platform;
            return ((p != 4) && (p != 6) && (p != 128));
        }

        static void EnsureUserIsAdmin()
        {
            if (IsWindows())
            {
                WindowsPrincipal principal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
                if (!principal.IsInRole(WindowsBuiltInRole.Administrator))
                {
                    RelaunchAsAdmin();
                }
            }
        }

        static void RelaunchAsAdmin()
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo(Application.ExecutablePath);
                psi.Verb = "runas";
                Process.Start(psi);
            }
            catch { }

            Environment.Exit(0);
        }

    }
}
