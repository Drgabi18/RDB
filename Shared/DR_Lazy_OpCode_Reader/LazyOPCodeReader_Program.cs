/*
	Now, if you're reading this file after watching the part 2 of my dev videos
	you might be questioning, why did I write a LIN reader myself from zero instead
	of just repurposing the code used to make that serialized file?
	
	I'm gonna be honest, only after I wrote this code I got curious to see the
	tool that was used to make  those files, imagine my face when I saw that it
	was also in C#, half of this code could have just been a simple LINQ query
	per .lin file from that script.

	Oh well, I really like exercising sport (programming included), no reaeson to
	not write some code yourself either to understand the methods better.

	Meaning of Op Codes used from here
	https://github.com/SpiralFramework/Spiral/blob/master/Formats.md#op-codes 

	If you're thinking aobut submitting a PR to fix my horrible code, don't, I
	wouldn't understand why your commits would fix it, I'd rather learn myself
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

public class NodeClass {
	static List<Node> Nodes = new List<Node>();

	// e.g. e00_000_000 or e05_196_001
	public class Node {
		// ¯\_(ツ)_/¯
		public Node(int arg1, int arg2, int arg3) {
			this.Arg1 = arg1;
			this.Arg2 = arg2;
			this.Arg3 = arg3;
		}

		// e.g. 0x70 0x19 0x00 0x64 0x00 (e00_100_000)
		// must cast to int when reading originally
		// OBSERVATION: in DR2, when you click cameras you go to 999, those are not
		//				converted properly here
		public Node(byte[] readBytes) {
			// inb4 Convert.ToInt16
			this.Arg1 = (int)readBytes[0];
			this.Arg2 = (int)readBytes[1];
			this.Arg3 = (int)readBytes[2];
		}

		// e.g. e03_296_000.lin
		public Node(string fileName) {
			// I know I can use .Skip(1) to remove the "e" but idk how to remove ".lin"
			string Numbers = fileName[1..^4];
			string[] Arguments = Numbers.Split('_');
			this.Arg1 = Convert.ToInt16(Arguments[0]);
			this.Arg2 = Convert.ToInt16(Arguments[1]);
			this.Arg3 = Convert.ToInt16(Arguments[2]);
		}

		// adding required gives me error about object initializer which idk how to fix
		public /* required */ int Arg1, Arg2, Arg3;

		// only forward connections assuming backwards ones are filled automatically
		// CATASTROPHICALLY WASTEFUL, SINCE EVEN CONNECTED NODES HAVE A NODE CONNECTION FOR NO REASON
		public List<Node>? NodeConnections = new List<Node>();

		public void AddNodeConnection(Node nodey) {
			AddsToListOfNodes(nodey); // gets rejected by the function if it exists already
			if (!GetNodeExists(NodeConnections, nodey)) this.NodeConnections.Add(nodey);
		}

		public override string ToString()
		{
			return $"e{Arg1:D2}_{Arg2:D3}_{Arg3:D3}";
		}
	}

	public static List<Node> GetNodes() {
		return Nodes;
	}

	// TODO: This creates a new object insead of making a reference to an exising one
	public static void AddsToListOfNodes(Node nodey) {
		if (!GetNodeExists(Nodes, nodey)) Nodes.Add(nodey);
	}

	static bool GetNodeExists(List<Node> collection, Node nodey) {
		return collection.Any<Node>(
			n => (n.Arg1 == nodey.Arg1) &&
				 (n.Arg2 == nodey.Arg2) &&
				 (n.Arg3 == nodey.Arg3));

	}
}

public class GangnamClass {
	public enum OpCodeHex : byte {
		START = 0x70,
		LOAD_MAP = 0x15,
		LOAD_SCRIPT = 0x19,
		STOP_SCRIPT = 0x1A,
		RUN_SCRIPT = 0x1B
	}

	public enum UDGOpCodeHex : byte {
		START = 0x70,
		LOAD_SCRIPT = 0x0E
	}

	public static readonly List<DrOpCode> DR1_DrOpCodes = new List<DrOpCode>{
		new(OpCodeHex.LOAD_MAP, 3),
		new(OpCodeHex.LOAD_SCRIPT, 3),
		new(OpCodeHex.RUN_SCRIPT, 3),
	};

	public static readonly List<DrOpCode> DR2_DrOpCodes = new List<DrOpCode>{
		new(OpCodeHex.LOAD_MAP, 4), // idk why it has ONE more
		new(OpCodeHex.LOAD_SCRIPT, 5), // idk why it has TWO more
		new(OpCodeHex.RUN_SCRIPT, 5), // idk why it has TWO more
	};

	// named DrOpCode because C# already has an OpCode class :)
	public class DrOpCode {
		public DrOpCode(/* string name, */ OpCodeHex id, int arguments) {
			//this.Name=name;
			this.ID=id;
			this.Arguments=arguments;
		}
		// cosmetic, no one understands what 0x15 would mean without text :P
		// also horribly wasteful, since it makes no sense to store this
		//public readonly string Name;
		// [JsonConverter(typeof(OpCodeHex))]
		public readonly OpCodeHex ID;
		public readonly int Arguments;
	}

	// this is fucking disastorous coding
	public class UDGOpCode {
		public UDGOpCode(UDGOpCodeHex id, int arguments) {
			this.ID=id;
			this.Arguments=arguments;
		}
		public readonly UDGOpCodeHex ID;
		public readonly int Arguments;
	}
}

public class Globals {
	public static List<GangnamClass.DrOpCode> ListOfDrOpCodes;
	public static Dictionary<string, List<KeyValuePair<GangnamClass.OpCodeHex, List<byte>>>>
		DictionaryWithFilesAndCodes = new();
}

class Program {
	// accept directory only
	// TODO: directiroies*
	public static void Main(string[] args) {
		string GameID = null; // rant time, why the fuck is dotnet giving me an error when i compile these without null,
		string FolderPath = null; // but when i add null, it then tells me it's not null and can be simplified?
		bool CoolGraphThing = false;

		foreach (string command in args) {
			switch(command) {
				case "-g":
				case "--game":
					GameID = args[Array.IndexOf(args, command)+1]; break;
				case "-d":
				case "--directory":
					FolderPath = args[Array.IndexOf(args, command)+1]; break;
				case "-v":
				case "--graphviz":
					CoolGraphThing = true; break;
				case "-?":
				case "--help":
					Console.WriteLine("./ThisApp.exe -g/--game 1 -d/--directory ./Path/To/LinFiles/"); return;
				default: break;
			}
		}
		
		switch(GameID) {
			case "DR1": Globals.ListOfDrOpCodes = GangnamClass.DR1_DrOpCodes; break;
			case "DR2": Globals.ListOfDrOpCodes = GangnamClass.DR2_DrOpCodes; break;
			case "UDG": break; // we are handling this inside the udg code sadly
			default: throw new Exception("No game ID specified");
		}

		List<string> LinFiles = Directory.EnumerateFiles(FolderPath, "*.lin").Order().ToList();

		// everything below could be multithread but naaaah
		foreach (string file in LinFiles) {
			if (file.Contains("novel")) continue; // skip novels cause i'm lazy
			if (GameID != "UDG") { // i hate how this reads
				ReadFile(file);
			} else {
				ReadFileUDG(file);
			}
			// break; // early debugging exit
		}
		
		if (GameID != "UDG") {
			CreateNodes();
		} else {
			TheOnlyWayToFixThisIs_With_A_Gun();
			return; // exit super early because this whole thing is a mess
		}

		if (CoolGraphThing) {
			string FileNameForViz = null;
			switch(GameID) {
				case "DR1": FileNameForViz = "./dr1_lazy_graphviz.txt";break;
				case "DR2": FileNameForViz = "./dr2_lazy_graphviz.txt";break;
				case "UDG": FileNameForViz = "./udg_lazy_graphviz.txt";break; // unused
			}
			
			using (StreamWriter WhyDoINeedThis = new StreamWriter(FileNameForViz)) {
				foreach (NodeClass.Node nodey in NodeClass.GetNodes()) {
					WhyDoINeedThis.WriteLine("\"{0}\"", nodey);
					if (nodey.NodeConnections is not null) {
						foreach (NodeClass.Node inception in nodey.NodeConnections) {
							WhyDoINeedThis.WriteLine("\"{0}\" -> \"{1}\"", nodey, inception);
						}
					}
					WhyDoINeedThis.Flush();
				}
			}

			return; // exit early from Main here cause i didn't my commit to look ugly
		}

		// write to file
		var SeriOptons = new JsonSerializerOptions{
			WriteIndented = true,
			IncludeFields = true,
			DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, // not working?????
		};

		// all the instructions per file
		var SerializedOutput = JsonSerializer.Serialize(
			Globals.DictionaryWithFilesAndCodes, SeriOptons);
		Console.WriteLine(SerializedOutput);

		// all the nodes we can use for a map
		var SerializedOutput2 = JsonSerializer.Serialize<List<NodeClass.Node>>(
			NodeClass.GetNodes(), SeriOptons);
		Console.WriteLine(SerializedOutput2);

		string FileNameForCodes = null;
		string FileNameForNodes = null;
		switch(GameID) {
			case "DR1":
				FileNameForCodes = "./dr1_code_results.txt";
				FileNameForNodes = "./dr1_node_results.txt";
				break;
			case "DR2":
				FileNameForCodes = "./dr2_code_results.txt";
				FileNameForNodes = "./dr2_node_results.txt";
				break;
			case "UDG": // unused
				FileNameForCodes = "./udg_code_results.txt";
				FileNameForNodes = "./udg_node_results.txt";
				break;
		}
		
		File.WriteAllText(FileNameForCodes, SerializedOutput);
		File.WriteAllText(FileNameForNodes, SerializedOutput2);
	}


	// now, the following code could range from bad to absolutley horrible
	// if you're a programmer, you are urged to look away to avert the horrors
	// if you are a beginner, you should not follow my steps at all
	// as it's written right now, the connections aren't even references but new objects
	public static void ReadFile(string theFile) {
		var fileInfo = new FileInfo(theFile);

		List<KeyValuePair<GangnamClass.OpCodeHex, List<byte>>> ListOfFoundCodes = new();
		byte[] FileBytes = File.ReadAllBytes(theFile);
		// easiest way i could think of making this is just getting all instances
		// of 0x70 and then searching for DrOpCodes if they match
		int offset = 0;
		foreach (byte bi in FileBytes) {
			if (bi == (byte)GangnamClass.OpCodeHex.START) { // if current byte is 0x70
				foreach (GangnamClass.DrOpCode opy in Globals.ListOfDrOpCodes) { // for every known opcode we have
					if (FileBytes[offset + 1] == (byte)opy.ID) { // if it matches one of our known codes
						// we have a match! store arguments in a dictionary
						List<byte> tempBuffer = new();
						for(int i = 1; i <= opy.Arguments; i++) {
							tempBuffer.Add(FileBytes[offset + 1 + i]);
						}
						ListOfFoundCodes.Add(new KeyValuePair<GangnamClass.OpCodeHex, List<byte>>(opy.ID, tempBuffer)); // https://www.youtube.com/watch?v=dT4oWwM266k
						Console.WriteLine("[{0}] Found\t{1}\t(0x{2})\tat offset {3}",
							fileInfo.Name, Enum.GetName<GangnamClass.OpCodeHex>(opy.ID), opy.ID.ToString("x").ToUpper(), offset); // nice to see stuff :P
					}
				}
			}
		offset++;
		}
		Globals.DictionaryWithFilesAndCodes.Add(fileInfo.Name, ListOfFoundCodes);
		//Thread.Sleep(16); // just for show for the video :P
	}

	public static void CreateNodes() {
		foreach (var Codes in Globals.DictionaryWithFilesAndCodes) {
			NodeClass.Node newNode = new NodeClass.Node(Codes.Key);
			NodeClass.AddsToListOfNodes(newNode);
			foreach (var Instruction in Codes.Value) {
				if (Instruction.Key == GangnamClass.OpCodeHex.LOAD_SCRIPT ||
					Instruction.Key == GangnamClass.OpCodeHex.RUN_SCRIPT) {
					newNode.AddNodeConnection(new NodeClass.Node(Instruction.Value.ToArray()));
				}
			}
		}
	}

	// hacky code made for UDG only since i didn't think beforehand when making
	// this code also because i was butthurt and didn't want to extend it to
	// multiple files, guess what now...

	public static Dictionary<string, List<KeyValuePair<GangnamClass.UDGOpCodeHex, byte[]>>>
		DictionaryWithFilesAndCodes2 = new();

	public static void ReadFileUDG(string theFile) {
		var fileInfo = new FileInfo(theFile);

		List<KeyValuePair<GangnamClass.UDGOpCodeHex, byte[]>> ListOfFoundCodes = new();
		byte[] FileBytes = File.ReadAllBytes(theFile);
		int offset = 0;
		foreach (byte bi in FileBytes) {
			if (bi == (byte)GangnamClass.UDGOpCodeHex.START) {
				if (FileBytes[offset + 1] == (byte)GangnamClass.UDGOpCodeHex.LOAD_SCRIPT) {
					byte[] tempBuffer = new byte[6];
					for(int i = 0; i <= 5; i++) { // hardcoded for loadscript's argument size for now
						tempBuffer[i]=FileBytes[offset + 2 + i];
					}
					ListOfFoundCodes.Add(new KeyValuePair<GangnamClass.UDGOpCodeHex, byte[]>(GangnamClass.UDGOpCodeHex.LOAD_SCRIPT, tempBuffer));
					Console.WriteLine("[{0}] Found\t{1}\t({2})\tat offset {3}",
						fileInfo.Name, "LOAD_SCRIPT", "0x0E", offset); // nice to see stuff :P
				}
			}
			offset++;
		}
		DictionaryWithFilesAndCodes2.Add(fileInfo.Name, ListOfFoundCodes);
		//Thread.Sleep(16); // just for show for the video :P
	}

	public static void TheOnlyWayToFixThisIs_With_A_Gun() {
		byte[] ThisIsStupid1 = new byte[2];
		byte[] ThisIsStupid2 = new byte[2];
		byte[] ThisIsStupid3 = new byte[2];
		foreach (var Codes in DictionaryWithFilesAndCodes2) {
			foreach (var Instruction in Codes.Value) {
				// 00 37 00 66 00 4A -> 37 00 66 00 4A 00
				// idk how to big endian to little endian easier
				ThisIsStupid1[0] = Instruction.Value[1];
				ThisIsStupid1[1] = Instruction.Value[0];
				ThisIsStupid2[0] = Instruction.Value[3];
				ThisIsStupid2[1] = Instruction.Value[2];
				ThisIsStupid3[0] = Instruction.Value[5];
				ThisIsStupid3[1] = Instruction.Value[4];
				Console.WriteLine("\"{0}\" -> \"e{1:D2}_{2:D3}_{3:D3}.lin\"", // doing it manually instead
					Codes.Key,
					BitConverter.ToInt16(ThisIsStupid1), // this code is aleady bad so we're not even gonna bother
					BitConverter.ToInt16(ThisIsStupid2), // with platform specific stuff like checking if we're LE first
					BitConverter.ToInt16(ThisIsStupid3)
				);
			}
		}
	}
}