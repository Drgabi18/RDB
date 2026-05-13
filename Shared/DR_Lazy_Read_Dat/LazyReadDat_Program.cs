using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.Json;

class Program {

	// unused and here just for reference
	[StructLayout(LayoutKind.Sequential)]
	struct CharDebateInfo {
		short Index; // 0-indexed
		short Unk1, Unk2, Unk3, Unk4, Unk5, Unk6, Unk7, Unk8, Unk9, Unk10, Unk11;
		short Unk12, Unk13, Unk14, Unk15, Unk16, Unk17, Unk18, Unk19;
		short CharacterID; // 0-Hajime, 1-Nagito etc.
		short ExpressionID;
		short Unk22, Unk23, Unk24, Unk25, Unk26, Unk27, Unk28, Unk29;
		short Unk30, Unk31, Unk32, Unk33; // for some reason DR2 includes these 4 extra shorts
	} // 60-bytes in DR1, 68-bytes in DR2

	/*
	[StructLayout(LayoutKind.Sequential)]
	struct DebateInfo {
		short Duration;
		short HowManyEvents;
		CharDebateInfo[] Debates;
	} // this should have been a class, i could easily do `CharDebateInfo[HowManyEvents] Debates` but alas
	*/

	static void Main(string[] args) {

		// i gave up on making a switch to make this easier
		string DebateFormat = args[0]; // nonstop, kokoro, hanron
		string GameID = args[1]; // "DR1" or "DR2"
		int PrintMode = Convert.ToInt16(args[2]); // print mode
		string FolderPath = args[3]; // has to be the folder with the .dat files
		
		int SizeOfCharDebate = GameID == "DR1" ? 60 : 68;
		List<string> FileNamesForAlphabet = new();

		foreach (string file in Directory.EnumerateFiles(FolderPath)) {
			if (file.Contains(DebateFormat) && file.EndsWith(".dat")) {
				FileNamesForAlphabet.Add(file);
			}
		}
		
		// i hate computers
		FileNamesForAlphabet.Sort();
		
		foreach (string file in FileNamesForAlphabet) {
			short Duration;
			short HowManyEvents;

			using (FileStream fs = File.Open(file, FileMode.Open)) {
			using (BinaryReader br = new(fs) ) {
				// and we're commenting ot this code cause i don't know 
				// how to cast a byte array to a struct
				/*
				Duration = br.ReadInt16();
				HowManyEvents = br.ReadInt16();
				for (int i = 0; i < HowManyEvents; i++) {
					br.Read(Marshal.SizeOf(CharDebateInfo));
				}
				*/
				switch (PrintMode) {
					case 0:
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
					case 1:
						Console.WriteLine(file);
						Duration = br.ReadInt16();
						HowManyEvents = br.ReadInt16();
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
					case 2:
						Duration = br.ReadInt16();
						HowManyEvents = br.ReadInt16();
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




	