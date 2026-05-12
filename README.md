# RDB
This repository includes the random stuff I was able to find reverse engineering Danganronpa.

This repo contains a collection of research documents I created for the following videos that I've made:
* [[Danganronpa 1/2/V3] Unused Rooms](https://www.youtube.com/watch?v=QMtvbAJh83s)
* [[Danganronpa 1/2/V3] Development Trivia & Fun Facts](https://www.youtube.com/watch?v=ljNlJfbsQQk)
* [[Danganronpa 1/2/UDG/V3] Development Trivia & Super Fun Facts 2](./poop)

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

For the Anniversary ports where the debug symbols are from, I extraced them from the following versions:
* `jp.co.spike_chunsoft.DR1.apk` - Version 1.0.0
* `jp.co.spike_chunsoft.DR2.apk` - Version 1.0.2
* `jp.co.spike_chunsoft.DRV3.apk` - Version 1.1

Each file for each game is sorted individually into the folder for what title they are part of. To explain what each file is:
* `./DR1_Rooms.ods`, `./DR2_Rooms.ods`, `./DRV3_Rooms.txt`, `./UDG_Maps.txt` are the files containing the Room IDs and a short description of what they do. This is probably what you would want to know the most.
* `./DR1 Room File Paths.txt` and `./DR2 Room File Paths.txt` contain strings extracted from decompressed .pak files. These file paths have been recreated in `./DR1FolderStructureRemade` and `./DR2FolderStructureRemade` respectivley if you wanna explore them.
* `./DR2 Codepaths.txt` contains the file paths extracted from the Debug Symbols left in the """Unity""" versions of the game. These file paths have been recreated in `./DR2 Code Paths` if you wanna explore them.
* `./DR1ManualAddresses.csv`, `./DR2ManualAddresses.csv` and `./UDGManualAddresses.csv` contain the names I gave to memory addresses I could find. Again these only work assuming you used the same manifest versions as me and the stars aligned correctly so you have dumped the same stuff.
* `./UDG_LinFilesICaredToLookAt.txt` is just a list of the unused LIN files I found for that game.
* `./NightmarePlayerStruct.md` is some dissasembled code I am completly unable to wrap my head around to decipher how it was made.
* `./SomeStructsIFound.cpp` is what it says on the can, just some exported (slight pseudocode) structs from what I cared to look at 

Anyways, if you wanna see something fun in the first game, run `set {int}0x009f7e4c=0` (`0x00b3e4ee` in the 2nd game) in any 2D room. Have fun!