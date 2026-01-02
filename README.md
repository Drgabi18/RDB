# RBS
This repository includes the random stuff I was able to find reverse engineering Danganronpa.

This repo is related to these 2 videos I made:
* [[Danganronpa 1/2/V3] Unused Rooms](https://www.youtube.com/watch?v=QMtvbAJh83s)
* [[Danganronpa 1/2/V3] Development Trivia & Fun Facts](https://www.youtube.com/watch?v=ljNlJfbsQQk)

It also includes the Ghidra Project Files (but compressed as the .gzf files) for some of the memory addresses I found. I do kinda wish I could export only the addresses and the lables though, since I didn't take the time to remake any struct.

There are 2 excel files with all the room labled internally for the first 2 games and a simple text file for V3.

I remade the file structure as files you can navigate (but no content in them) too, I fucking hope they don't dissapear when you download this repo as a zip.

Assuming you use the same thing as me, latest Linux executable, if you wanna see something fun in the first game, run `set {int}0x009f7e4c=0` (`0x00b3e4ee` in the 2nd game) in Makoto's Room. Have fun!