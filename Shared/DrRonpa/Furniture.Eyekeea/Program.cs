#region Aknowladgement
//	This project only exists because I saw this tool by morgana
//		https://github.com/morgana-x/danganronpa-RoomObjectsToJson
//	that didn't work with DR2's furnitre format, so I made my own version 
#endregion

#region Sketching
/*
	Game (1, 2, 3)
	 ∟ Rooms (Room class)
	   ∟ List of Furniture Items (all 3 games have it) 
*/
#endregion


using DanganFurniture.Enums;
using DanganFurniture.Headers;
using DanganFurniture.PrintModesClass;

namespace DanganFurniture {
	public class Program {
		public static List<Room> EyekeeaShowroom = new List<Room>();
		GameID DefaultGame = GameID.DR1;
		PrintModesEnum DefaultPrintMode = PrintModesEnum.JsonSerialized;
		public static readonly bool PrintJason = true;
		
		// TODO: pls
		public static bool IsDR2 = true;
		
		// TODO: arguments should be folders extracted using pak_extractor
		// or we integrate PakLibrary to read files off there
		
		public static void Main(string[] args) {
			if (args.Length == 0) throw new Exception("[DanganFurniture] No file(s) provided");

			string[] AllMapFolders = Directory.GetDirectories(args[0]);
			foreach (string Folder in AllMapFolders) {
				string FolderName = new DirectoryInfo(Folder).Name;
			
				// HACK: Until I make a proper command line this will have to do
				if (args[1] == "--V3") {
					RoomV3 ShowcaseV3 = new RoomV3();
					
					DanganFurniture.V3.Readers.ReadFurnitureFile(Path.Combine(Folder, "place.dat"));
					//DanganFurniture.V3.Readers.ReadTextFile(Path.Combine(Folder, "text.stx"));
					continue;
				}
				
				// HACK: DR2 PC ONLY, skip over corrupted 054
				if (IsDR2 == true && FolderName == "bg_054") continue;

				Room Showcase = new Room();
				Showcase.RoomName = FolderName;
				Showcase.ModelNameFile = Readers.ReadModelNamesFile(Path.Combine(Folder, "0000"));
				Showcase.Options = Readers.ReadOptionsFile(Path.Combine(Folder, "0001"));
				Showcase.Places = Readers.ReadFurnitureFile(Path.Combine(Folder, "0002"));
				// DR2 only, at the moment it currently breaks DR1 parsing
				if (IsDR2) {
					Showcase.AABB = Readers.ReadAABBBonesFile(Path.Combine(Folder, "0003"));
					
					// TODO: is there a better way than just seraching through every file
					// 		and not using the file list inside the game? 
					foreach (string File in Directory.GetFiles(Folder)) {
						if (Readers.IsZColFile(File)) {
							Showcase.Colissions = Readers.ReadZColFile(File);
						}
					}
				}

				EyekeeaShowroom.Add(Showcase);
			}

			// HACK
			if (args[1] == "--V3") return;

			// yes i am this lazy
			PrintModesEnum PrintModes = PrintJason ? PrintModesEnum.JsonSerialized : PrintModesEnum.LazyGodot;
			
			switch (PrintModes) {
				default:
				case PrintModesEnum.JsonSerialized:
					Console.Clear();
					Print.JsonSerializedPrint(EyekeeaShowroom);
					return;
				case PrintModesEnum.LazyGodot:
					Console.Clear();
					Print.LazyGodotPrint(EyekeeaShowroom);
					return;
			}
		}
	}
}