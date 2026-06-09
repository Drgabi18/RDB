# RDB
This repository includes the random stuff I was able to find reverse engineering Danganronpa.

This repo contains a collection of research documents I created for the following videos that I've made:
* [[Danganronpa 1/2/V3] Unused Rooms](https://www.youtube.com/watch?v=QMtvbAJh83s)
* [[Danganronpa 1/2/V3] Development Trivia & Fun Facts](https://www.youtube.com/watch?v=ljNlJfbsQQk)
* [[Danganronpa 1/2/V3] Unused Story (.LIN) Files](https://www.youtube.com/watch?v=nDRb3It6jOs)
* [[Danganronpa 1/2/V3] Super Development Trivia & Fun Facts 2: Goodbye Debug Menus](https://www.youtube.com/watch?v=jjzG6y8MyrU)
* [[Danganronpa UDG] Unused Rooms, Cutscenes & Development Trivia](https://www.youtube.com/eH-B8eT481U)
* [[Danganronpa UDG] Addendum: How to "play" as Toko](https://www.youtube.com/watch?v=iCMTiXoMFgI)

Please check them out since I put more effort than necessary in all of them!

I explored the following Steam depots:
* DR1 Linux: `download_depot 413410 413413 6184791363566370453`
* DR2 Linux: `download_depot 413420 413423 4176999508874191731` ~~(actually all of them and I lost my mind)~~
* DR2 MacOS: `download_depot 413420 413422 73890315654914570`
* UDG Windows: `download_depot 555950 555952 4190759877809212923`
* DRV3 Windows: `download_depot 567640 567641 5154425171419538896`

For the PSP, I explored the following Catalog Numbers:
* `NPJH90164 v1.01` - _Danganronpa 1 DEMO_ (but the English Patch)
* `NPJH50372 v1.03` - _Danganronpa 1_
* `NPJH50515 v1.00` - _Danganronpa 1 (PSP The Best Edition)_
* `NPJH50631 v1.02` - _Super Danganronpa 2_

For the Anniversary ports where the Debug Symbols are from, I extraced them from the following versions:
* `jp.co.spike_chunsoft.DR1.apk` - Version 1.0.0
* `jp.co.spike_chunsoft.DR2.apk` - Version 1.0.2
* `jp.co.spike_chunsoft.DRV3.apk` - Version 1.1

Several Markdown file with some text explanation documenting some aspects of the game:
* [The Debug Menus from the PSP and Steam releases](Shared/DR1_DR2_DebugMenus.md) _(not including Android/iOS versions which have way too many)_
* [DrRonpa documentation](Shared/DrRonpa/README.md)
* [How to add the Save with the Level Select in UDG](UDG/LevelSelectSave/README.md)
* [Confusion on how the player class in UDG is set](UDG/NightmarePlayerStruct.md)

Each file for each game is sorted individually into the folder for what title they are part of. To explain what each file is:
* `./DR1/DR1_Rooms.ods`, `./DR2/DR2_Rooms.ods`, `./DRV3/DRV3_Rooms.txt`, `./UDG/UDG_Maps.txt` are the files containing the Room IDs and a short description of what they do.
* `./DR1/DR1 Room File Paths.txt` and `./DR2/DR2 Room File Paths.txt` contain strings extracted from decompressed .pak files. These file paths have been recreated in `./DR1/DR1FolderStructureRemade` and `./DR2/DR2FolderStructureRemade` respectivley if you wanna explore them.
* `./Shared/Codepaths_from_DR2.txt` contains the file paths extracted from the Debug Symbols left in the """Unity""" (Anniversary) versions of the game. These file paths have been recreated in `./Shared/Codepaths_from_DR2/` if you wanna explore them.
* `./DR1/DR1_UserDefinedSymbols.csv`, `./DR2/DR2_UserDefinedSymbols.csv` and `./UDG/UDG_UserDefinedSymbols.csv` contain the names I gave to memory addresses I could find. Again these only work assuming you used the same manifest versions as me and the stars aligned correctly so you have dumped the same stuff (in DRV3's case, you can do the same by running x64dbg in Wine to dump the game, since ASLR is disabled in Wine, thank fuck).
* `./DR1/DR1_DebugSymbols.csv`, `./DR2/DR2_DebugSymbols.csv`, `./UDG/UDG_DebugSymbols.csv` and `./DRV3/DRV3_DebugSymbols.csv` contain the names of the symbols extracted from the Anniversary versions of the game (except for UDG which is just the strings that remained for Steam Achivements and uhh... random vtables???????), these are provided as plaintext files instead of a Ghidra Project
* `./Shared/DR12V3_Unused_LINs.txt` contains a list of the unused LIN files I found in the mainline series games.
* `./Shared/DR1_DR2_DebugMenus.md` contains explanations for what the Debug Menus included in the games actually do.
* `./DR1/DR1_UnusedLINStrings.txt` and `./DR2/DR2_UnusedLINStrings.txt` conain the strings from those unused LINs, so it's easier to see where they send you.
* `./UDG/UDG_LinFilesICaredToLookAt.txt` is just a list of the unused LIN files I found for that game.
* `./UDG/UDG_UnusedCutscenesEV8.txt` is a list of the Unused Cutscenes I showed in my video, these are specifically those not showcased by a video on TCRF.
* `./UDG/NightmarePlayerStruct.md` is some dissasembled code I am completly unable to wrap my head around to decipher how it was made.
* `./UDG/Some_ReverseEngineered_Code.cpp` is what it says on the can, ok I lied a bit, it's just a few structs for what control the player character, commented with too much detail, contains addresses which are `base + 0xNNNNNNNN` which should be helpul to use in Cheat Enine.
* `./UDG/LevelSelectSave/` is a Save that brings you onto one of the Unused Lins, which has a Level Select, Woohoo!
* `./Shared/DrRonpa/` is a suite of C# apps I've made in order to read the binary data from some (easily reverse engineer-able) file formats. The apps are explained in the [README file](Shared/DrRonpa/README.md)
   * `./Lazy.NonStopDebate.Reader` contains the code and output of my Lazy DAT Reader™, used to parse Nonstop/Hanron/Kokonronpa Debate Files. Results are in the .ods files inside the `././output/` folder.
   * `./Lazy.OpCode.Reader` contains the code and output of my Lazy OpCode Reader™™, used primarily for my curiosity in analysis of discrete and the weird ass structure the games have. Results are in the .json files inside the `././output/` folder.
   * `./TrialCamera.Exercise` contains the code of my Trial Camera Reimplementation™™™, that I absolutley didn't write during lunch break at work. Contains a bleak reimplementation of the structs used for Cameras in Trials. Has no other practical use than just to brag that I wrote it.

Note that what you're seeing here is a passion project, the accuracy of some information may be low or straight up incorrect. I'm discovering stuff at the same time as of the making of the videos.

I've put a restriction on myself to not look at information that would help me, information like [Spiral Framework's Spiral](https://github.com/SpiralFramework/Spiral) or [BitesizeBird's Danganronpa Modding Information](https://github.com/BitesizeBird/Danganronpa-Modding) who have basically documented 5 years before me what I've discovered for the first time in the period of when I upload my videos. This is also the reason I don't look or use the Debug Symbols that much either. The joy of making these videos is discovery and learning to use the tools available to me for this, having everything in front of you isn't so fun anymore, wouldn't you agree?

## Acknowledgements
-# (for projects that are here on GitHub which I used, not auxiliary things like TCRF or TSR; and in the order that I used them)
* Liquid-S - For creating [DRAT](https://github.com/Liquid-S/Danganronpa-Another-Tool) which I used for repacking game files for the Sayaka Fun House joke
* vn-tools - For creating [danganronpa-tools](https://github.com/vn-tools/danganronpa-tools/) which I used `pak_archiver` and `wad_archiver` from in the initial first 2 episodes.
* morgana-x - For creating [Danganronpa-Script-Dumps](https://github.com/morgana-x/Danganronpa-Script-Dumps) which is what lead to me making the Unused Story Files video; For creating [PakLib](https://github.com/morgana-x/PakLib) which was easier to use for unpacking.
* shadow.nero - Helping me understand how the text renderer and debug text array work in-game... well in-code... as well as other interesting tidbits; also being cool and friendly.
* CaptainSwag101 - For creating [DRV3-Sharp](https://github.com/CaptainSwag101/DRV3-Sharp) which I used to extract files for the Super Dev Facts Pt 2 and DRV3 Chapter test in Unused Story Files video
* BitesizeBird - For their [Danganronpa Modding Information](https://github.com/BitesizeBird/Danganronpa-Modding)
* Spiral Framework - For their efforts documenting the [OpCodes](https://github.com/SpiralFramework/Spiral)
* You - yes, you! if you watched or commented <3

Anyways, if you wanna see something fun in the first game, run `set {int}0x009f7e4c=0` (`0x00b3e4ee` in the 2nd game) in any 2D room. Have fun!

## For TCRF or other research projects
I hereby grant my permission for info from this repo to be used on TCRF pages for the respecive games and other projects documenting these games. I am unfamiliar with how to edit pages on WikiMedia forks and English isn't my first language if it wasn't already obvious.