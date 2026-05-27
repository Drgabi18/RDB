// reworked again cause i fucked up the first rework attempt

// at the moment the system is a bit flawed since i never associate the objects
// to a specific file

// also we could multithread it

using System.Text.Json;
using LazyOpCodeReader.Games;
using LazyOpCodeReader.LIN;
using LazyOpCodeReader.OpCodes;

namespace LazyOpCodeReader {
	public enum PrintModes {
		None = 0,
		JsonSerialized,
		GraphViz,
	}
	public class Program {
		public static Game OurGame;
		public static void Main(string[] args) {
			// at least this code is easy enough to copy paste from the old code
			Game.GameID GameID = Game.GameID.NONE;	// "DR1", "DR2" or "UDG"
			string FolderPath = null;	// folder with .lin files
			PrintModes PrintMode = PrintModes.None;	// just to not deal with stupid errors

			foreach (string command in args) {
				switch(command) {
					case "-g":
					case "--game":
						// "DR1" to Game.GameID.DR1
						var _temp1 = 
							Enum.TryParse(args[Array.IndexOf(args, command)+1],
							out Game.GameID _result1);
						GameID = _result1;
						break;
					case "-d":
					case "--directory":
						FolderPath = args[Array.IndexOf(args, command)+1]; break;
					case "-m":
					case "--mode":
						// same thing as the previous enum parse
						var _temp2 =
							Enum.TryParse(args[Array.IndexOf(args, command)+1],
							out PrintModes _result2);
						PrintMode = _result2;
						break;
					case "-?":
					case "--help":
						// stupid line break
						Console.WriteLine("Tool used to extract some specialized data from Danganronpa's LIN files\n\n" +
										"Example Usage:\n\t./ThisApp.dll " +
										"[-g/--game \"DR1\"] " +
										"[-d/--directory \"./Path/To/LinFiles/\"] " +
										"[-m/--mode \"JsonSerialized\"] " +
										"[-?/--help]" + "\n\n" +
										"Valid options are:\n" +
										"\t-g \"DR1\"/\"DR2\"/\"UDG\" (note that UDG is experimental)\n" +
										"\t-m \"JsonSerialized\"/\"GraphViz\"\n"

										); return;
					default: break;
				}
			}

			// draft so it's easier for me to understand

			// Game
			//	∟ List of LinFiles
			
			// LinFile
			//	∟ Name (preferably eXX_XXX_XXX)
			//	∟ List of OpCodes
			//	∟ List of Resulting Classes?

			// OpCode
			//	∟ Value of OpCode
			//	∟ How many bytes to read
			//	∟ out Resulting Class?

			OurGame = new Game(GameID);

			// BUG?: Because we're doing it in parallel, the alphabetic order no longer matters
			List<string> LinFilesFromFolder =
				Directory.EnumerateFiles(FolderPath, "*.lin").Order().ToList();
			List<Task> x = new List<Task>();
			
			//foreach (string file in LinFilesFromFolder) {
			Parallel.ForEach(LinFilesFromFolder, file => {
				// skip DR2 novels cause i'm lazy
				if (OurGame.GameName == Game.GameID.DR2 && file.Contains("novel")) return;
				LinFile OurLinFile = new LinFile(file);
				ReadFile(OurLinFile);
				OurGame.LinFiles.Add(OurLinFile);
				// Thread.Sleep(16);
			}
			);

			Console.Clear();

			switch(PrintMode) {
				default:
				case PrintModes.None: // default none to something
				case PrintModes.JsonSerialized:
					Console.WriteLine(JsonSerializer.Serialize(OurGame,
						new JsonSerializerOptions{
							IncludeFields = true, WriteIndented = true
						}));
					return;
				case PrintModes.GraphViz:
					// throw new Exception("I didn't get to - or if you're still reading this - want to rewrite this part (yet)");
					foreach (LinFile lin in OurGame.LinFiles) {
						foreach (object objy in lin.ResultingOpCodeObjects) {;
							Console.WriteLine($"{lin} -> {objy}");
						}
					}
					return;
			}
		}

		// whenever i write deep nested loops like this, god holds a gun between
		// my temple, but in the back instead of the front, and he's ready to pull
		public static void ReadFile(LinFile leFile) {
			// Console.WriteLine(leFile.LongFileName);
			// although originally i just got the entire file, found every 0x70
			// and then did the job of that, i will most likely need binary
			// reader in the future, so to learn it, i'm using it in this project
			using (FileStream fs = File.Open(leFile.LongFileName, FileMode.Open)) {
				using (BinaryReader br = new BinaryReader(fs)) {
					while (br.BaseStream.Position < br.BaseStream.Length) {
						byte ByteRead = br.ReadByte();
						if (ByteRead == OperationCode.START_HEX) { // hacky
							//Console.WriteLine("Found potential opcode");
							// who in their right fucking mind decided that
							// chars in C# are actually an int and not a byte
							// guess we'll do it like this then
							byte NextByte = br.ReadByte();
							if (NextByte != 0x00) { // hacky
								//Console.WriteLine("Found {0:X2}",NextByte);
								OperationCode ResultingOpCode =
									OurGame.GetValidOpCodes().FirstOrDefault(op => Convert.ToByte(op.Value) == NextByte);
								if (ResultingOpCode is not null) {
									int ResultingArguments = ResultingOpCode.NoOfArguments;
									byte[] TheBytesPassed = br.ReadBytes(ResultingArguments);
									// a bit buggy buuuuuut it looks cool
									Console.Write("{0} - {1} -", leFile.PrettyFileName, ResultingOpCode.Value);
									foreach (byte bytes in TheBytesPassed) Console.Write("{0:X2} ", bytes);
									Console.Write("- Offset: {0}", br.BaseStream.Position);
									Console.WriteLine();
									leFile.ResultingOpCodeObjects.Add(
										OurGame.MakeGameLinObjects(NextByte, TheBytesPassed)
									);
								}
							} 
						}
					}
				}
			}
		}
	}
}