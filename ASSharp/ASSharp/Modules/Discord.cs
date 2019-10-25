using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ASSharp.Modules
{
    internal class Discord
    {
        private static string discord_modules_Code = "module.exports = require('./discord_modules.node');";
        private static string discord_desktop_core_Code = "module.exports = require('./core.asar');";
        private static string discord_modules_Path = $"{GetDiscordDirectory()}{GetDiscordVersion()}/modules/discord_modules/index.js";
        private static string discord_desktop_core_Path = $"{GetDiscordDirectory()}{GetDiscordVersion()}/modules/discord_desktop_core/index.js";

        internal static void CheckFileIntegrity()
        {
            string[] discord_modules;
            string[] discord_desktop_core;

            try
            {
                discord_modules = File.ReadAllLines(discord_modules_Path);
                discord_desktop_core = File.ReadAllLines(discord_desktop_core_Path);
            }
            catch(Exception e)
            {
                LogService.Log($"Failed To Locate Discord Directory.\n{e.ToString()}", LogLevel.Error);
                return;
            }

            if (discord_modules.Length != 1 || discord_modules[0] != discord_modules_Code || discord_desktop_core.Length != 1 || discord_desktop_core[0] != discord_desktop_core_Code)
            {
                LogService.Log("Your Discord Is Compromised.");

                if ((discord_modules.Length != 1 || discord_modules[0] != discord_modules_Code))
                {
                    RestoreOriginal(DiscordFile.discord_modules);
                }
                else
                {
                    RestoreOriginal(DiscordFile.discord_desktop_core);
                }
            }
            else
            {
                LogService.Log("Your Discord Is Not Compromised. You Are Safe To Use Discord.");
            }
        }

        internal static void RestoreOriginal(DiscordFile DFile)
        {
            switch (DFile)
            {
                case DiscordFile.discord_modules:
                    try
                    {
                        File.WriteAllText(discord_modules_Path, discord_modules_Code);
                    }
                    catch(Exception e)
                    {
                        LogService.Log($"Failed To Restore Original Discord File.\n{e.ToString()}", LogLevel.Error);
                        break;
                    }

                    LogService.Log("Successfully Restored Original Discord File. You Are Safe To Use Discord Now.");
                    break;
                case DiscordFile.discord_desktop_core:
                    try
                    {
                        File.WriteAllText(discord_desktop_core_Path, discord_desktop_core_Code);
                    }
                    catch (Exception e)
                    {
                        LogService.Log($"Failed To Restore Original Discord File\n{e.ToString()}", LogLevel.Error);
                        break;
                    }

                    Console.WriteLine("Successfully Restored Original Discord File. You Are Safe To Use Discord Now.");
                    break;
            }
        }

        internal enum DiscordFile
        {
            discord_modules = 1,
            discord_desktop_core
        }

        internal static string GetDiscordDirectory()
        {
            if (Directory.Exists($"C:/Users/{Environment.UserName}/AppData/Roaming/discord"))
            {
                return $"C:/Users/{Environment.UserName}/AppData/Roaming/discord/";
            }
            else
            {
                return string.Empty;
            }
        }

        internal static string GetDiscordVersion()
        {
            string DiscordVersion = "";
            string[] GetAllFolder;

            try
            {
                GetAllFolder = Directory.GetDirectories(GetDiscordDirectory()).Select(Path.GetFileName).ToArray();
            }
            catch(Exception e)
            {
                return string.Empty;
            }

            foreach (string FolderName in GetAllFolder)
            {
                if (FolderName.StartsWith("0."))
                {
                    DiscordVersion = FolderName;
                }
            }

            return DiscordVersion;
        }
    }
}
