using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace spotikiller
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var processName = "spotify";

            KillProcesses(GetProcesses(processName));

            // run a second time to deal with any spawned from killing the originals (e.g. spotifyCrashHelper)
            KillProcesses(GetProcesses(processName));
        }

        static IEnumerable<Process> GetProcesses(string processName)
        {
            return Process.GetProcesses().Where(p => p.ProcessName.IndexOf(processName, StringComparison.OrdinalIgnoreCase) != -1);
        }

        static void KillProcesses(IEnumerable<Process> processes)
        {
            foreach (var spotifyProcess in processes)
            {
                try
                {
                    Console.Write($"Killing {spotifyProcess.ProcessName} ... ");
                    spotifyProcess.Kill();
                    spotifyProcess.WaitForExit();
                    Console.Write("killed.");
                    Console.WriteLine();
                }
                catch (Exception)
                {
                    /* ignore */
                }
            }
        }
    }
}
