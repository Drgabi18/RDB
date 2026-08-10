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

using System.Numerics;
using System.Text.Json;

namespace DanganFurniture
{
	struct Room {
		public string RoomName;
		public List<Furniture> Objects;
	}

	struct Furniture {
		public int Type;
		public int ID;	// for characters, they are placed by how they load the LIN
						// for world, ID maps to the respective model file you click
		public uint Unk1;
		public float[] Position;
		
		// i have a nagging suspicion that this is not size in the way most 
		// people expect, the mesh gets scaled by 10% afterall
		public float[] Size;
		public float Rotation;
		public int Unk2;
		public string? ObjectName;

		public Furniture() {
			Type = 0;
			ID = 0;
			Unk1 = 0;
			Position = new float[3];
			Size = new float[2];
			Rotation = 0;
			Unk2 = 0;
			ObjectName = null;
		}
	};

	class Program {
		// TODO: A propper Folder support... later
		//public static bool IsFolder = false;

		public enum PrintModesEnum : int { JsonSerialized, LazyGodot }
		public enum ObjectTypes : int {

			NULL1 = 0,	// only appears in DR2 in maps 101, 120, 142, 143, 160
			Person = 1,	// ID is index in the world
			Unk1Interacteable = 2,	// Type is indexed object in world you click, used a lot in DR1
			Marker = 4,	// used in DR1 DEMO to stop how much you walk out if you
						// were to walk in the trial room, no purpose in other versions
			BilboardLights = 5,	// ID is index in the world, Unk1 is type
			Unk2Interacteable = 7,	// Type is indexed object in world you click, used a lot in DR1
			InteracteableDoors = 8,	// used a lot in DR1
			Mask = 9,	// no clue, name taken from bg_100 in DR2
			Sun = 50,	// DR2 only
			HiddenMonokuma = 70,	// DR2 only
			Path = 90,	// DR2 Only, Unk1 represents the ID of the path
			NULL2 = 255	// DR2 only, padding?

			// Speculative
			/*
			Floor = 3,	// or maybe light floor?
			Walls = 4,
			ObjectWithAnimationWhenEnterirng = 6,
			Color_UNK1 = 11,
			Color_UNK2 = 17,
			Color_UNK3 = 30,	// uses Size for something
			Color_UNK4 = 84,
			WorldBorder = 61,	// DR2 only
			WorldMesh = 80
			Unk82 = 82,	// Uses Unk2 for something
			
			Unk8 = 8, Unk10 = 10, Unk12 = 12, Unk13 = 13,
			Unk14 = 14, Unk16 = 16, Unk18 = 18, Unk19 = 19, Unk22 = 22,
			Unk40 = 40, Unk41 = 41, Unk51 = 51, Unk52 = 52, Unk53 = 53,
			Unk54 = 54, Unk60 = 60, Unk61 = 61, Unk62 = 62, Unk63 = 63, Unk64 = 64,
			Unk66 = 66, Unk67 = 67, Unk71 = 71, Unk72 = 72, Unk73 = 73, Unk75 = 75,
			Unk76 = 76, Unk77 = 77, Unk78 = 78, Unk79 = 79, Unk81 = 81,
			Unk83 = 83, Unk85 = 85, Unk86 = 86,
			*/
		}
		public static List<Room> EyekeeaShowroom = new List<Room>();
		
		public static void Main(string[] args) {
			if (args.Length == 0) throw new Exception("[DanganFurniture] No file(s) provided");
			//if (args.Contains("-d") || args.Contains("--directory")) IsFolder = true;
			//string FileToOpen = args[0];
			foreach (string file in args.ToArray()) {
				//ReadRoom(FileToOpen);
				ReadRoom(file);
			}

			// yes i am this lazy
			PrintModesEnum PrintModes = false ? PrintModesEnum.JsonSerialized : PrintModesEnum.LazyGodot;

			switch (PrintModes) {
				default:
				case PrintModesEnum.JsonSerialized:
					Console.WriteLine(
						JsonSerializer.Serialize(EyekeeaShowroom,
						new JsonSerializerOptions{IncludeFields = true, WriteIndented = true})
					);
					return;
				case PrintModesEnum.LazyGodot:
					Console.Clear();
					Random Randomy = new Random();
					int Indexer = 0;
					foreach (Furniture Object in EyekeeaShowroom.First<Room>().Objects) {
						Indexer++;
						string NodeName;
						if (Object.ObjectName != null) {
							NodeName = Object.ObjectName;	// DR2 ONLY
						} else {
							NodeName = String.Concat(Enum.GetName(typeof(ObjectTypes), Object.Type), "Node", Indexer);
						}
						// TODO: Use Quaternion
						// TODO: Godot.Transform3D WHY DID THEY HAVE TO MAKE A CLASS NOT SUPPORTED HERE
						// Matrix3x2 Rotation = new Matrix3x2();
						//Console.WriteLine(Matrix3x2.CreateRotation(Single.DegreesToRadians(Object.Rotation)));
						//Console.ReadKey();
						Console.WriteLine("[node name=\"{0}\" type=\"Marker3D\" parent=\".\" unique_id={1}]",
							NodeName, Indexer * 100);
						Console.WriteLine("transform = Transform3D({0}, 0, 0, 0, {1}, 0, 0, 0, {2}, {3}, {4}, {5})",
							// some objects have the scale 0, which would make it so we can't see anything, we should
							// think a little more about what we should scare here lol
							Object.Size[0], Object.Size[1], 1, Object.Position[0], Object.Position[1], Object.Position[2]);
							//1, 1, 1, Object.Position[0], Object.Position[1], Object.Position[2]);
						Console.WriteLine("metadata/type = \"{0}\"", Object.Type);
						Console.WriteLine("metadata/id = \"{0}\"", Object.ID);
						Console.WriteLine("metadata/unk1 = \"{0}\"", Object.Unk1);
						Console.WriteLine("metadata/unk2 = \"{0}\"", Object.Unk2);
						Console.WriteLine("metadata/rotation = \"{0}\"", Object.Rotation);
						Console.WriteLine("gizmo_extents = 100.0");
						Console.WriteLine();
						// creating a bilboarded sprite
						Console.WriteLine("[node name=\"Sprite3D\" type=\"Sprite3D\" parent=\"{0}\" unique_id={1}]", NodeName, Randomy.Next());
						Console.WriteLine("pixel_size = 0.5");
						Console.WriteLine("billboard = 2");
						Console.WriteLine("texture = ExtResource(\"1_f3sb7\")");
						// creating a box because it's impossible to see these on a 4K monitor
						//Console.WriteLine("[node name=\"CSGBox3D\" type=\"CSGBox3D\" parent=\"{0}\" unique_id={1}]", NodeName, Randomy.Next());
						//Console.WriteLine("size = Vector3(100, 100, 100)");
						Console.WriteLine();

					}
					return;
			}
		}

		// TODO: Eventually make this return something else, we would have a room
		// object with furniture array blah blah
		public static void ReadRoom(string FilePath) {
			Room Showcase = new Room();
			List<Furniture> Bucatarie = new List<Furniture>();
			Showcase.RoomName = Path.GetFileName(Path.GetDirectoryName(FilePath));

			using (FileStream fs = File.Open(FilePath, FileMode.Open)) {
			using (BinaryReader br = new(fs) ) {
				int HowMuchFurniture = br.ReadInt32();
				Console.WriteLine("[DanganFurniture] Found {0} furniture objects", HowMuchFurniture);
				int[] FurnitureOffset = new int[HowMuchFurniture];

				for (int i = 0; i < HowMuchFurniture; i++) {
					FurnitureOffset[i] = br.ReadInt32();
				}

				for (int i = 0; i < FurnitureOffset.Count(); i++) {
					
					int NextOffsetStart =
						(!(i + 1 == FurnitureOffset.Count())) ?
						NextOffsetStart = FurnitureOffset[i+1] :
						NextOffsetStart = (int)fs.Length;

					br.BaseStream.Position = FurnitureOffset[i];
					Furniture Mobilier = new Furniture(); // couldn't think of a non romanian name sorry
					Mobilier.Type = br.ReadInt32();
					Mobilier.ID = br.ReadInt32(); 
					Mobilier.Unk1 = br.ReadUInt32(); 
					Mobilier.Position[0] = br.ReadSingle(); 
					Mobilier.Position[1] = br.ReadSingle(); 
					Mobilier.Position[2] = br.ReadSingle(); 
					Mobilier.Size[0] = br.ReadSingle(); 
					Mobilier.Size[1] = br.ReadSingle(); 
					Mobilier.Rotation = br.ReadSingle(); 
					Mobilier.Unk2 = br.ReadInt32();
					
					//Console.WriteLine("iter {0} - pos {1} - next {2} - size {3}",
					//i,
					//(int)br.BaseStream.Position,
					//NextOffsetStart,
					//NextOffsetStart - (int)br.BaseStream.Position);
					
					// DR1 does not store object names, thus the result for these will always be 0,
					// we should probably do this check earlier than here and only once
					bool IsDR1 = ((NextOffsetStart - (int)br.BaseStream.Position) == 0) ? true : false;
					if (!IsDR1) {
						string AttemptedString = System.Text.Encoding.ASCII.GetString(br.ReadBytes(NextOffsetStart - (int)br.BaseStream.Position));
						//foreach (byte by in AttemptedString.ToArray()) Console.Write("{0:X2} ", by);
						// TODO: there HAS to be a better way of doing this
						if (AttemptedString.ToArray()[0] != 0x00) {
							Mobilier.ObjectName = AttemptedString.TrimEnd('\u0000');
							Console.WriteLine("[DanganFurniture] Object {0} is {1}", i, Mobilier.ObjectName);
						} else {
							Mobilier.ObjectName = null;
						}
					} else {
						Mobilier.ObjectName = null;
					}
					Bucatarie.Add(Mobilier);
				}
				Showcase.Objects = Bucatarie;
				Console.WriteLine();
				EyekeeaShowroom.Add(Showcase);
			}}
		}
	}
}