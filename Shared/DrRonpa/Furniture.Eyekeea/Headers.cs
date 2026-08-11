namespace DanganFurniture.Headers {
	/*
		TODO: As I was making these, I discovered there's more binary file fomrat
		files in a map, in DR2 there's 0000-0003 and then the last one before .tga

		==== 0000
		first number is how many names, after that it's just offsets and then the
		names of the .gmo models... except it can't be that because there's N
		numbers of files and the last one always has binary data and not model data 

		==== 0001
		4 ints, unknown, 1 int, unknown

		==== 0002
		the code you see below, Furniture struct

		==== 0003
		first number is how many names, after that it's just offsets and the last
		offset is a string array ,seems to be a K=V where K is the name and
		V is a struct, so Dictionary<string, struct>

		==== last file before .tgas
		first number is how many names, after that it's just offsets and the last
		offset is a string array ,seems to be a K=V where K is the name and
		V is a struct, so Dictionary<string, struct>

	*/

	public struct Room {
		public string RoomName;	// taken from folder name
		public List<string> ModelNames;	// file 0000
		public UnkStruct1 Unk1;	// file 0001
		public List<Furniture> Objects;	// file 0002
		public Dictionary<string, AABBStruct> AABB;	// file 0003
		//public Dictionary<string, UnkStruct3> Masks; // last file before iamges
	}

	// 0001
	public struct UnkStruct1 {
		public int Unk1;
		public int Unk2;
		public int Unk3;
		public int Unk4;
		public int Unk5;
		public int Unk6;
		public int Unk7;
		public int Unk8;
	}

	// 0002
	public struct Furniture {
		public int Type;	// see FurnitureTypes enum, we should convert this to
							// that type at a latter point
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
	}

	// 0003
	// TODO: oh my fucking god just fix the print mode this is embarasing
	public struct AABBStruct {
		public int Unk1;
		public float[] SixFloats;
		//public float[] TopLeftCorner;
		//public float[] BottomRightCorner;

		public AABBStruct() {
			Unk1= 0;
			SixFloats = new float[6];
			//TopLeftCorner = new float[3];
			//BottomRightCorner = new float[3];
		}
	}
}