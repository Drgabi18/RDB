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
*/

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

	public static readonly Dictionary<byte, string> OpCodeName = new Dictionary<byte, string> {
		[0x70] = "Start", 
		[0x15] = "Load Map", 
		[0x19] = "Load Script", 
		[0x1A] = "Stop Script", 
		[0x1B] = "Run Script", 
	};

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
		int IsDr1orDr2 = 0; // 1 or 2
		string FolderPath = null;

		foreach (string command in args) {
			switch(command) {
				case "-g":
				case "--game":
					IsDr1orDr2 = Convert.ToInt16(args[Array.IndexOf(args, command)+1]); break;
				case "-d":
				case "--directory":
					FolderPath = args[Array.IndexOf(args, command)+1]; break;
				case "-?":
				case "--help":
					Console.WriteLine("./ThisApp.exe -g/--game 1 -d/--directory ./Path/To/LinFiles/"); return;
				default: break;
			}
		}
		
		switch(IsDr1orDr2) {
			case 1: Globals.ListOfDrOpCodes = GangnamClass.DR1_DrOpCodes; break;
			case 2: Globals.ListOfDrOpCodes = GangnamClass.DR2_DrOpCodes; break;
			default: throw new Exception("No game ID specified");
		}

		List<string> LinFiles = Directory.EnumerateFiles(FolderPath, "*.lin").Order().ToList();

		// everything below could be multithread but naaaah
		foreach (string file in LinFiles) {
			if (file.Contains("novel")) continue; // skip novels cause i'm lazy
			ReadFile(file);
			// break; // early debugging exit
		}
		CreateNodes();


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

		string FileNameForCodes = IsDr1orDr2 == 1 ? "./dr1_code_results.txt" : "./dr2_code_results.txt";
		string FileNameForNodes = IsDr1orDr2 == 1 ? "./dr1_node_results.txt" : "./dr2_node_results.txt";
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
							fileInfo.Name, GangnamClass.OpCodeName[(byte)opy.ID], opy.ID.ToString("x").ToUpper(), offset); // nice to see stuff :P
					}
				}
			}
		offset++;
		}
		Globals.DictionaryWithFilesAndCodes.Add(fileInfo.Name, ListOfFoundCodes);
		// Thread.Sleep(16); // just for show
	}

	public static void CreateNodes() {
		foreach (var Codes in Globals.DictionaryWithFilesAndCodes) {
			NodeClass.Node newNode = new NodeClass.Node(Codes.Key);
			NodeClass.AddsToListOfNodes(newNode);
			foreach (var Instruction in Codes.Value) {
				if (Instruction.Key == GangnamClass.OpCodeHex.LOAD_SCRIPT) {
					newNode.AddNodeConnection(new NodeClass.Node(Instruction.Value.ToArray()));
				}
			}
		}
	}
}
