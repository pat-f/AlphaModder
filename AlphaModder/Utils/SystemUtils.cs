﻿using AlphaModder.Constants;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlphaModder.Utils
{
    class SystemUtils
    {
        public static void openGameFolderInExplorer()
        {
            if (Directory.Exists(Properties.Settings.Default.GameFolder))
                Process.Start("explorer.exe", "/open, " + Properties.Settings.Default.GameFolder);
            else
                MessageBox.Show("The game folder could not be found. Please configure it in the Tools menu, or put the Alpha Modder executable in the game installation folder.");
        }

        public static bool verifyGameFolder(String gameFolderPath)
        {
            return (
                Directory.Exists(gameFolderPath)
                && File.Exists(gameFolderPath + "alpha.txt")
                && File.Exists(gameFolderPath + "alphax.txt")
                && File.Exists(gameFolderPath + "Alpha Centauri.Ini")
                );
        }

        // search various directories for a game installation
        // if found, set it in the program settings 
        public static bool findGameFolder()
        {
            // current folder setting was determined to be invalid, so set it to Not found
            Properties.Settings.Default.GameFolder = "Not found";
            Properties.Settings.Default.AlphaFilePath = "Not found";
            Properties.Settings.Default.AlphaXFilePath = "Not found";

            // list of directories where the game might be installed
            // repeated for a few different drive letters
            // first check the current directory
            List<String> pathsToCheck = new List<String>
            {
                ".\\",
                "C:\\GOG Games\\Sid Meier's Alpha Centauri\\",
                "C:\\Program Files (x86)\\GalaxyClient\\Games\\Alpha Centauri\\",
                "C:\\Program Files (x86)\\GalaxyClient\\Games\\Sid Meier's Alpha Centauri\\",
                "C:\\Program Files\\Firaxis Games\\Sid Meier's Alpha Centauri\\",
                "D:\\GOG Games\\Sid Meier's Alpha Centauri\\",
                "D:\\Program Files (x86)\\GalaxyClient\\Games\\Alpha Centauri\\",
                "D:\\Program Files (x86)\\GalaxyClient\\Games\\Sid Meier's Alpha Centauri\\",
                "D:\\Program Files\\Firaxis Games\\Sid Meier's Alpha Centauri\\",
                "E:\\GOG Games\\Sid Meier's Alpha Centauri\\",
                "E:\\Program Files (x86)\\GalaxyClient\\Games\\Alpha Centauri\\",
                "E:\\Program Files (x86)\\GalaxyClient\\Games\\Sid Meier's Alpha Centauri\\",
                "E:\\Program Files\\Firaxis Games\\Sid Meier's Alpha Centauri\\"
            };

            foreach (String path in pathsToCheck)
            {
                if (SystemUtils.verifyGameFolder(path))
                {
                    Properties.Settings.Default.GameFolder = path;
                    Properties.Settings.Default.AlphaFilePath = path + "alpha.txt";
                    Properties.Settings.Default.AlphaXFilePath = path + "alphax.txt";
                    Properties.Settings.Default.AlphaCentauriIniPath = path + "Alpha Centauri.Ini";
                    return true;  // installation directory was found. stop searching and return true
                }
            }

            // no game directory was found, return false
            return false;

        }

        public static void setGameFolderDialog()
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                String path = folderBrowserDialog.SelectedPath + "\\";
                if (SystemUtils.verifyGameFolder(path))
                {
                    // selected folder has been verified. set it
                    Properties.Settings.Default.GameFolder = path;
                }
                else
                {
                    MessageBox.Show("Not an Alpha Centauri installation folder." +
                        "\n\n" + path);
                }
            }

        }

        public static bool getSoundOption()
        {
            return Properties.Settings.Default.EnableSounds;
        }

        // set the sound option and save
        public static void setSoundOption(bool soundEnabled)
        {
            Properties.Settings.Default.EnableSounds = soundEnabled;
            Properties.Settings.Default.Save();
        }

        // play the specified sound
        public static void playSound(String relativePath)
        {
            if (Properties.Settings.Default.EnableSounds == true)
            {
                // if the enable sounds option is enabled,
                // get the path of the game directory and combine it with the relative path
                // of the sound file
                String absolutePath = DataUtils.getGameInstallationDirectoryStr() + relativePath;
                System.Media.SoundPlayer soundPlayer = new System.Media.SoundPlayer(absolutePath);
                soundPlayer.Play();
            }
        }

        // play a random sound from the provided list
        public static void playRandomSound(List<String> relativePathList)
        {
            // get a random sound from the provided list and call playSound()
            Random random = new Random();
            int r = random.Next(relativePathList.Count);
            playSound(relativePathList[r]);
        }
    }
}
