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
using DanganFurniture.Structs;
using DanganFurniture.PrintModesClass;

namespace DanganFurniture {
	
	public static class Program {
		public static GameID SelectedGame = GameID.DR1;	// default to DR1
		static PrintModes PrintMode = PrintModes.JsonSerialized;	// default to Json
		static string FolderPath = null;

		public static List<HPA.Room> EyekeeaShowroom = new List<HPA.Room>();
		public static List<V3.Room> EyekeeaShowroomV3 = new List<V3.Room>();
		
		
		// TODO: don't forget to add instructions for what files are needed
		public static void Main(string[] args) {
			if (args.Length == 0) throw new Exception("[DanganFurniture] No commands were provided");
			
			HandleCommandLineArguments(args);

			string[] AllMapFolders = Directory.GetDirectories(FolderPath);
			foreach (string Folder in AllMapFolders) {
				string FolderName = new DirectoryInfo(Folder).Name;

				// TOOD: make this sexier
				Console.WriteLine(FolderName);
				
				if (SelectedGame == GameID.DR1 || SelectedGame == GameID.DR2) {
					if (SelectedGame == GameID.DR2 && FolderName == "bg_054") continue;

					HPA.Room Showcase = new HPA.Room();
					Showcase.RoomName = FolderName;
					Showcase.ModelNameFile = Readers.ReadModelNamesFile(Path.Combine(Folder, "0000"));
					Showcase.Options = Readers.ReadOptionsFile(Path.Combine(Folder, "0001"));
					Showcase.Places = Readers.ReadFurnitureFile(Path.Combine(Folder, "0002"));
					// DR2 only, at the moment it currently breaks DR1 parsing
					if (SelectedGame == GameID.DR2) {
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
				} else if (SelectedGame ==GameID.DRV3) {
					V3.Room ShowcaseV3 = new V3.Room();
					ShowcaseV3.RoomName = FolderName;
					ShowcaseV3.Places = V3Readers.Readers.ReadFurnitureFile(Path.Combine(Folder, "place.dat"));
					//DanganFurniture.V3.Readers.ReadTextFile(Path.Combine(Folder, "text.stx"));

					EyekeeaShowroomV3.Add(ShowcaseV3);
				}
			}
			
			switch (PrintMode) {
				default:
				case PrintModes.JsonSerialized:
					Console.Clear();
					Print.JsonSerializedPrint(SelectedGame, EyekeeaShowroom);
					return;
				case PrintModes.LazyGodot:
					Console.Clear();
					Print.LazyGodotPrint(SelectedGame, EyekeeaShowroom);
					return;
			}
		}

		public static void HandleCommandLineArguments(string[] args) {
			foreach (string command in args) {
				switch(command) {
					case "-g":
					case "--game":
						// "DR1" to GameID.DR1
						var _temp1 = 
							Enum.TryParse(args[Array.IndexOf(args, command)+1],
							out GameID _result1);
						SelectedGame = _result1;
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
					default: break;
				}
			}

			if (FolderPath == null) throw new Exception("[DanganFurniture] No folder path was provided");
		}
	}
}