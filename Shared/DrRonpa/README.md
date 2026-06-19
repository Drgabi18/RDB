> From the same company that brought you _Dr Hopper_, presenting - **DrRonpa** - an all new AI¹ solution which uses carefully trained and configured agentic² prompts in order to discover the hidden meanings behind some archeological binary files. The agen- I can't be fucking bothered to finish the joke. 

A suite of C# apps I've made in order to read the binary data from some (easily reverse engineer-able) file formats in the Danganronpa series. This has already been done better by other people before me, but I wanted to learn how to do the same thing as well individual of others' research and code.

**You are expected to pipe the output out to a file when running any of these.**

Run `dotnet build ./DrRonpa.slnx -c Release -o ./Binaries/` to build all the applications at once.

# Requierments
* .NET (tested only on .NET 10)

# Projects
## Lazy.NonStopDebate.Reader
Used to parse NonStop Debate files into a readable format.

Parameters are **sequential** and all **requiered**:
1. Debate Format
	* Accepted options: `"nonstop"`; `"kokoro"`; `"hanron"`
2. Game Name 
	* Accepted options: `"DR1"`; `"DR2"`
3. Print Mode
	* Accepted options:
		* `0` - same format as if you did `xxd` on a file, but shows shorts instead of hex;
		* `1` - Prints the Text Index, Character and Expression;
		* `2` - prints the format which can be easily used in a .csv file
4. Folder Path (place where all the nonstop_\*_\*.dat files are)

**Example usage**: `./DebateReader "nonstop" "DR2" 2 "./Path/To/Files/"`

This was already done better before me by [BitesizeBird](https://github.com/BitesizeBird/Danganronpa-Modding/tree/master/Nonstop%20Debate%20DAT%20converters), which also documented what all the Unknown values in my code are.

## Lazy.OpCode.Reader
Used to parse LIN files into a readable format.

Parameters **aren't sequential** but are all **requiered**:
* `-g`/`--game`
	* Accepted options: `"DR1"`; `"DR2"`; `"UDG"`
* `-d`/`--directory`
	* Path where the LIN files are. DR2 Novels are skipped.
* `-m`/`--mode`
	* `"JsonSerialized"` - prints the OpCodes into a .json file which can be easily readable. [Example](Lazy.OpCode.Reader/output/UDG_Serialized.json).
	* `"GraphViz"` - prints the OpCodes into a text file which can be easily used to print GraphViz files. [Example](Lazy.OpCode.Reader/output/udg_manual_graphviz.txt).
	* `"Specialized"` - whatever bullshit you wanna make

**Example usage**: `./OpCodeReader --game "DR2" -d "./Path/To/Files/" --mode "JsonSerialized"`

This was already done better than me [countless](https://github.com/vn-tools/danganronpa-tools) [times](https://github.com/SpiralFramework/Spiral) [before](https://github.com/morgana-x/danganronpa-lin-compiler-v2).

## EV8.Parser
**WIP** Tool used to parse EV8 files from Ultra Despair Girls.

Only parameter is the Folder with the `.ev8` files.

**Example usage**: `./EV8Parser "/Path/To/Files/"`

## TrialCamera.Exercise
Just random code I absolutley didn't write because I forgot to bring my lunch to work. Currently useless.

# Footnotes

¹ - I haven't used AI to code anything in this entire project, everything here is writen with pure stupidity, researched by pure autism and fueled by pure lesbianism. [I am training to become a lesbian the difficulty level is pretty high I am 99% gay and 1% synesthesia](https://bsky.app/profile/blockedforthispost.bsky.social/post/3mleqlvgmoh2x)

² - How the hell do I block this word off GitHub