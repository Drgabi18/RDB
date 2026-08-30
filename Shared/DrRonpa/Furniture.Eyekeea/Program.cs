#region Aknowladgement
//	This project only exists because I saw this tool by morgana
//		https://github.com/morgana-x/danganronpa-RoomObjectsToJson
//	that didn't work with DR2's furnitre format, so I made my own version and
//	I also tried to learn how to make ImHex patterns, present below

/*
==== ImHex Pattern

struct Furniture {
    u32 Type;
    u32 ID;
    s32 Unk1;
    float Position[3];
    float Size[2];
    float Rotation;
    u32 Unk2;
};

struct FurOffsets {
    u32 offset;
    Furniture entries @ offset;
};

struct FurnitureBank {
    u32 count;
    FurOffsets offsets[count];
};

FurnitureBank bank @ 0x0;

====
*/
#endregion

using DanganFurniture.Enums;
using DanganFurniture.Headers;
using DanganFurniture.PrintModesClass;

namespace DanganFurniture
{
	public class Program {
		public static List<Room> EyekeeaShowroom = new List<Room>();
		public static readonly string GodotSvgIdentifierWhatever = "1_f3sb7";
		public static readonly bool PrintJason = true;
		
		// TODO: pls
		public static bool IsDR2 = true;
		
		// TODO: arguments should be folders extracted using pak_extractor
		// or we integrate PakLibrary to read files off there
		
		public static void Main(string[] args) {
			if (args.Length == 0) throw new Exception("[DanganFurniture] No file(s) provided");
			//if (args.Contains("-d") || args.Contains("--directory")) IsFolder = true;


			string[] AllMapFolders = Directory.GetDirectories(args[0]);
			foreach (string Folder in AllMapFolders) {
				string FolderName = new DirectoryInfo(Folder).Name;
			
				Console.WriteLine(FolderName);
				// HACK: Until I make a proper command line this will have to do
				if (args[1] == "--V3") {
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