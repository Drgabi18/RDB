using System.Runtime.InteropServices;
using System.Text.Json;

class Program {
	static void Main(string[] args) {
		// i gave up on making a switch to make this easier
		string DebateFormat = args[0]; // nonstop, kokoro, hanron, scrum
		string GameID = args[1]; // "DR1", "DR2", "DRV3" or "Special"
		int PrintMode = Convert.ToInt16(args[2]); // print mode
		string FileOrFolderPath = args[3]; // has to be the folder with the .dat files
		
		int SizeOfCharDebate;
		switch (GameID) {
			default:
			case "DR1"	: SizeOfCharDebate = 60; break;
			case "DR2"	: SizeOfCharDebate = 68; break;
			case "DRV3"	: SizeOfCharDebate = 105; break;	// unconfirmed, stolen from other attempts at the same thing
			case "Special": SizeOfCharDebate = 51; break;	// nonstop_90_007.dat in DR1-2 on Vita
		}

		List<string> FileNamesForAlphabet = new();

		// if we are selecting an individual file, then do only that
		if (Path.HasExtension(FileOrFolderPath)) {
			// TODO: Maybe make this better, add the checks from below too?
			FileNamesForAlphabet.Add(FileOrFolderPath);
		} else {
			foreach (string file in Directory.EnumerateFiles(FileOrFolderPath)) {
				if (file.Contains(DebateFormat) && file.EndsWith(".dat")) {
					FileNamesForAlphabet.Add(file);
				}
			}
		}
		
		// i hate computers
		FileNamesForAlphabet.Sort();
		
		foreach (string file in FileNamesForAlphabet) {

			using (FileStream fs = File.Open(file, FileMode.Open)) {
			using (BinaryReader br = new(fs) ) {
				
				// originally there was an attempt at marshalling a byte array to
				// a struct here, but that was removed, you can see however a working
				// implementation of it in EV8.Parser as of 20 august 2026

				short Duration;
				short HowManyEvents;
				Duration = br.ReadInt16();
				HowManyEvents = br.ReadInt16();

				switch (PrintMode) {
					case 0:		// equivalent to a memory viewer set to interpret integers
						Console.WriteLine(file);
						int _indx = 0;
						while (br.BaseStream.Position < br.BaseStream.Length) {
							Console.Write("{0, 6:D0}", br.ReadInt16());
							Console.Write(' ');
							++_indx;
							if (_indx % 8 == 0) Console.WriteLine(); 
						}
						Console.WriteLine();
						break;
					case 1:		// curiosity
						Console.WriteLine(file);
						short[] GodKillMe = new short[(SizeOfCharDebate/2)];
						for (int i = 0; i < HowManyEvents; i++) {
							for (int j =0; j < (SizeOfCharDebate/2); j++) {
								GodKillMe[j]=br.ReadInt16();
							}
							Console.WriteLine(JsonSerializer.Serialize(GodKillMe));
							Console.WriteLine("Index is {0:D}", GodKillMe[0]);
							Console.WriteLine("Char is {0:D}", GodKillMe[21]);
							Console.WriteLine("Expression is {0:D}", GodKillMe[22]);
						}
						break;
					case 2:		// CSV format for easy parsing
						// very ugly hack, only assumes nonstop, breaks on other styles
						List<string> CSVHeaderString = new List<string>
							{
							"FileName", "IndexInFile", "HowManyTimesShootPinkText",
							"Unk3",	// SpiralFramework and bitesized already documented
									// these unknown values, do i copy it or find myself
									// what they mean?
							"TruthBulletID", "ConsentBulletID",
							"Unk6",	// ditto
							"HasValidShootSpot",
							"Unk88", "Unk9",  "Unk10",  "Unk11",  "Unk12",	// ditto
							"TextX", "TextY", "TextMoveSpeed", "DirectionTextIsGoing",
							"Unk17",  "Unk18",  "Unk19",	// ditto
							"RotationAngle", "ValueToAddToAngle", "CharacterID",
							"Expression", "CameraID", "IconShake",
							"Unk26", "Unk27",  "Unk28",  "Unk29",	// ditto
							"StandToPointAt",
							};

							// DR2 includes 4 extra ints at the end for the sword minigame
							if (GameID == "DR2"){
								CSVHeaderString.AddRange( "Unk31", "Unk32", "Unk33", "Unk34");
							}

						Console.WriteLine(string.Join(";",CSVHeaderString));
						for (int i = 0; i < HowManyEvents; i++) {
							Console.Write(Path.GetFileName(file));
							for (int j =0; j < (SizeOfCharDebate/2); j++) {
								Console.Write(";{0}",br.ReadInt16());
							}
							Console.WriteLine();
						}
						break;
				}
			} 
			Console.WriteLine();
			}
		}
	}
}