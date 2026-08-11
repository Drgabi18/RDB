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
	class Program {
		public static List<Room> EyekeeaShowroom = new List<Room>();
		public static readonly string GodotSvgIdentifierWhatever = "1_awcjp";
		
		// TODO: arguments should be folders extracted using pak_extractor
		// or we integrate PakLibrary to read files off there
		
		// TODO: Fix this for DR1
		public static void Main(string[] args) {
			if (args.Length == 0) throw new Exception("[DanganFurniture] No file(s) provided");
			//if (args.Contains("-d") || args.Contains("--directory")) IsFolder = true;

			string[] AllMapFolders = Directory.GetDirectories(args[0]);
			foreach (string Folder in AllMapFolders) {
				string FolderName = new DirectoryInfo(Folder).Name;
				
				// HACK: DR2 ONLY, skip over corrupted 054
				if (FolderName == "bg_054") continue;

				Room Showcase = new Room();
				Showcase.RoomName = FolderName;
				Showcase.Unk1 = Readers.Read0001(Path.Combine(Folder, "0001.gmo"));
				Showcase.Objects = Readers.ReadFurniture(Path.Combine(Folder, "0002.gmo"));
				// DR2 only, at the moment it currently breaks DR1 parsing
				Showcase.AABB = Readers.ReadAABBMasks(Path.Combine(Folder, "0003.gmo"));
				
				// HACK: find a way to gracefully not error out on special cases
				// like this where bg_000 has no 0000.gmo, the other files work
				// but this one doesn't, so skip at the end with others resolved
				if (FolderName != "bg_000") {
					Showcase.ModelNames = Readers.ReadModelNames(Path.Combine(Folder, "0000.gmo"));
				}

				EyekeeaShowroom.Add(Showcase);
			}

			// yes i am this lazy
			PrintModesEnum PrintModes = true ? PrintModesEnum.JsonSerialized : PrintModesEnum.LazyGodot;

			
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