# AlphaModder

A tool for generating custom alpha.txt settings files for the game Sid Meier's Alpha Centauri and alphax.txt files for the Alien Crossfire expansion pack.

## Current features:
* Adjust difficulty of Drone Riots, Mindworms, and Xenofungus.
* Adjust terraforming speed, tech discovery rate, and population limits.
* Increase the size of "Huge" planet by up to 8x.
* Options to unlock, make free cost, or disable units, abilities, facilities, and reactors.
* Adjust various other game rules.
* Automatically finds your game installation folder.
* Adjust the game's resolution.
* Hurry cost calculator that tells you how many energy credits to pay to complete production next turn.
* Save and load presets.

Please review the [Issues](https://github.com/pat-f/AlphaModder/issues) and [Milestones](https://github.com/pat-f/AlphaModder/milestones) to see what features are planned for future versions.  Issuses that are assigned are being worked on.  

## Download and run the application:
* Go to the [Releases](https://github.com/pat-f/AlphaModder/releases) page and download the .exe file for the latest version.  On first run, Windows might warn you that the executable is from an unknown publisher.  Click proceed to allow the application to run.
* The app will automatically find your game installation folder if it's installed in a common directory.  It can be set manually, but the first place that will be checked is the current directory, so put the app in the game's installation folder if it's not found automatically.
* The app will overwrite your alpha.txt and alphax.txt files when you click Apply.  Your new file will have all the options you selected in the app.

## Editing the application source:
* Clone the project, or download as .zip and extract.  Open the AlphaModder.sln file in Visual Studio.  

## Project technology:
* Windows Forms
* C# 7.3
* .NET Framework 4.7.2
