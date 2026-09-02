using System.Text;
using System.Text.Json;
using DanganFurniture.Structs;

namespace DanganFurniture {
	
	// we could maybe make like an interface for these, or make them a class lol

	public static class Readers {
		// file 0002
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

		//public static T ParseDataFile<T>(string FileName) {
		//	return new T;
		//}
		
		public static List<HPA.Furniture> ReadFurnitureFile(this string FilePath) {
			List<HPA.Furniture> Bucatarie = new List<HPA.Furniture>();
			
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
					// couldn't think of a non romanian name sorry
					HPA.Furniture Mobilier = new HPA.Furniture();
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
					// TODO: Decide if this should remain as it harms no one, or replace it with SelectedGame = Game.DR2
					bool IsDR1 = ((NextOffsetStart - (int)br.BaseStream.Position) == 0) ? true : false;
					if (!IsDR1) {
						string AttemptedString = System.Text.Encoding.ASCII.GetString(br.ReadBytes(NextOffsetStart - (int)br.BaseStream.Position));
						//foreach (byte by in AttemptedString.ToArray()) Console.Write("{0:X2} ", by);
						// TODO: there HAS to be a better way of doing this
						if (AttemptedString.ToArray()[0] != 0x00) {
							Mobilier.ObjectName = AttemptedString.TrimEnd('\u0000');
							// Console.WriteLine("[DanganFurniture] Object {0} is {1}", i, Mobilier.ObjectName);
						} else {
							Mobilier.ObjectName = null;
						}
					} else {
						Mobilier.ObjectName = null;
					}
					Bucatarie.Add(Mobilier);
				}
				Console.WriteLine();
			}}
			return Bucatarie;
		}

		// 0000
		public static List<string> ReadModelNamesFile(this string FilePath) {
			List<string> ModelNames = new List<string>();
			
			using (FileStream fs = File.Open(FilePath, FileMode.Open)) {
			using (BinaryReader br = new(fs) ) {
				int HowManyModels = br.ReadInt32();
				Console.WriteLine("[DanganFurniture] Found {0} model names", HowManyModels);
				
				int[] ModelNameOffset = new int[HowManyModels];

				for (int i = 0; i < HowManyModels; i++) {
					ModelNameOffset[i] = br.ReadInt32();
				}

				for (int i = 0; i < ModelNameOffset.Count(); i++) {
					int NextOffsetStart =
							(!(i + 1 == ModelNameOffset.Count())) ?
							NextOffsetStart = ModelNameOffset[i+1] :
							NextOffsetStart = (int)fs.Length;
					
					br.BaseStream.Position = ModelNameOffset[i];
					string AttemptedString = System.Text.Encoding.ASCII.GetString(br.ReadBytes(NextOffsetStart - (int)br.BaseStream.Position)).TrimEnd('\u0000');
					ModelNames.Add(AttemptedString);
				}

			}}
			return ModelNames;
		}

		// 0001
		public static HPA.OptionsFile ReadOptionsFile(this string FilePath) {
			HPA.OptionsFile RoomInfo = new HPA.OptionsFile();
			
			using (FileStream fs = File.Open(FilePath, FileMode.Open)) {
			using (BinaryReader br = new(fs) ) {
				RoomInfo.Unk1 = br.ReadInt32();
				RoomInfo.Unk2 = br.ReadInt32();
				RoomInfo.Unk3 = br.ReadInt32();
				RoomInfo.CameraMode = br.ReadInt32();	// written by the same species
				RoomInfo.Unk5 = br.ReadInt32();	// that landed on the moon and
				RoomInfo.Unk6 = br.ReadInt32();	// ate food from sewage
				RoomInfo.Unk7 = br.ReadInt32();
				RoomInfo.Unk8 = br.ReadInt32();
			}}

			return RoomInfo;
		}
		
		// file 0003
		// OH MY GOD I JUST REALIZED WHY DANGANRONPA READS FROM BACK TO FRONT
		// IT'S BECAUSE IN FILES LIKE THIS IT STARTS WITH THE LAST ELEMENT
		// TO GET THE NAMES AND THEN IT SERIALZIES THE REST
		// so now the question is, in this file is everything forwards or backwards?
		// LATER EDIT: It's normal, no back to front
		public static Dictionary<string, HPA.AABBStruct> ReadAABBBonesFile(this string FilePath) {
			Dictionary<string, HPA.AABBStruct> ExtraObjectData = new();

			using (FileStream fs = File.Open(FilePath, FileMode.Open)) {
			using (BinaryReader br = new(fs) ) {
				int HowManyConnections = br.ReadInt32();
				Console.WriteLine("[DanganFurniture] Found {0} AABB connections", HowManyConnections);

				int[] SomethingOffset = new int[HowManyConnections];
				for (int i = 0; i < HowManyConnections; i++) {
					SomethingOffset[i] = br.ReadInt32();
				}
				
				string[] SomeNames = new string[HowManyConnections - 1];
				// deal with object names first
				// i don't understand why they made these different
				br.BaseStream.Position = SomethingOffset.Last();
				SomeNames = System.Text.Encoding.ASCII.GetString(br.ReadBytes((int)fs.Length - (int)br.BaseStream.Position))
					.TrimEnd('\u0000').Split('\u0000');

				for (int i = 0; i < SomethingOffset.Count() - 1; i++) {
					HPA.AABBStruct Something = new HPA.AABBStruct();
					br.BaseStream.Position = SomethingOffset[i];
					Something.Unk1 = br.ReadInt32();
					Something.SixFloats[0] = br.ReadSingle();
					Something.SixFloats[1] = br.ReadSingle();
					Something.SixFloats[2] = br.ReadSingle();
					Something.SixFloats[3] = br.ReadSingle();
					Something.SixFloats[4] = br.ReadSingle();
					Something.SixFloats[5] = br.ReadSingle();
					// TODO: GABI PLEASE
					/*
					Something.TopLeftCorner[0] = br.ReadSingle();
					Something.TopLeftCorner[1] = br.ReadSingle();
					Something.TopLeftCorner[2] = br.ReadSingle();
					Something.BottomRightCorner[0] = br.ReadSingle();
					Something.BottomRightCorner[1] = br.ReadSingle();
					Something.BottomRightCorner[2] = br.ReadSingle();
					*/
					ExtraObjectData.Add(SomeNames[i], Something);
				}
			}}
			
			return ExtraObjectData;
		}
		
		// last one before iamges
		public static HPA.CollisionFile ReadZColFile(this string FilePath) {
			HPA.CollisionFile Colissions = new();
			
			using (FileStream fs = File.Open(FilePath, FileMode.Open)) {
			using (BinaryReader br = new(fs) ) {
				Colissions.Identifier = br.ReadUInt32(); // whatev
				Colissions.FileSize = br.ReadInt32();
				Colissions.Unk2_HeaderSize = br.ReadInt32();
				Colissions.SizeBeforeTriangles = br.ReadInt32();
				Colissions.ListOfSomething = new List<int>();
				Colissions.Verticies = new List<HPA.Vertex>();

				// this is a dog shit implementation and the game is smarter here
				// at 0x0046aa00
				/*
					if ((ZColFile == (int *)0x0) || (*ZColFile != -0x112234)) {
						return 0;
					}
					ZCol_FileSize = ZColFile[1];
					DAT_00aa9f60 = '\x01';
					ZCol_???_HeaderSize = ZColFile[2];
					ZCol_ListSize = ZColFile[3];
					ZCol_StartOfList = *(uint *)((long)ZColFile + (ulong)(uint)ZCol_???_HeaderSize);
					ZCol_EndOfList = *(uint *)((long)ZColFile + (ulong)(uint)ZCol_ListSize);
					DAT_00aa9fc0 = (float *)FUN_00413ec0((ulong)ZCol_EndOfList * 0xc);
				*/
				// recreate this correctly later

				while (br.BaseStream.Position < Colissions.SizeBeforeTriangles + 4) {
					Colissions.ListOfSomething.Add(br.ReadInt32());
				}
				while (br.BaseStream.Position < Colissions.FileSize) {
					HPA.Vertex vertex = new();
					vertex.Pos[0] = br.ReadSingle();
					vertex.Pos[1] = br.ReadSingle();
					vertex.Pos[2] = br.ReadSingle();
					Colissions.Verticies.Add(vertex);
				}
			}}

			return Colissions;
		}

		public static bool IsZColFile(this string FilePath) {
			using (FileStream fs = File.Open(FilePath, FileMode.Open)) {
			using (BinaryReader br = new(fs) ) {
				return br.ReadUInt32() == 4293844428; // 0xCCDDEEFF casting to uint doesn't work??????????
			}}
		}

	}

	public static class V3Readers {
	/*
		Actually glad these were easier to understand lol
	
		================================ place.dat ================================
		
		struct FurnitureObject {
			u16 Unk1;
			u16 Unk2;
			float float1;
			float float2;
			float float3;
			float float4;
			float float5;
			float float6;
			float float7;
			float float8;
			u16 Unk3;
			u16 Unk4;
		}

		struct PlaceFile {
			int HowMuchFurniture;
			int Unk1;
			int HeaderSize;
			// 164 bytes that tell the game how to deserialize, "float1 f32 \x01 float2 f32 \x01 ..."
			// ...REFER...No..A
			// SCII...float1.f3
			// 2...float2.f32..
			// .float3.f32...fl
			// oat4.f32...float
			// 5.f32...float6.f
			// 32...float7.f32.
			// ..float8.f32...a
			// scii.ASCII...int
			// 1.s16...........
			FurnitureObject Objects[HowMuchFurniture];
			int HowMuchAscii;
			string Names[HowMuchAscii]; // UTF-8 ??????????????????????????
		}


		================================ text.stx ================================
		struct IndexNum {int Index; int Offset;}
		
		struct TextFile {
			char[8] Identifier = "STXTJPLL"
			int Unk1;
			int OffsetToStartOfIndexes;
			int Unk2;
			int HowMuchText;
			// 8 bytes of emtpy space, maybe Unk2?
			IndexNum Indexes[HowMuchText];
			// and the UTF-16LE text is here :P
		}
	*/

		// TODO: Make this return a PlaceFile instead, this should be a list just for testing
		// place.dat
		public static List<V3.Furniture> ReadFurnitureFile(this string FilePath) {
			V3.Furniture[] Bucatarie;

			using (FileStream fs = File.Open(FilePath, FileMode.Open)) {
			using (BinaryReader br = new(fs) ) {
				int HowMuchFurniture = br.ReadInt32();
				Bucatarie = new V3.Furniture[HowMuchFurniture];
				Console.WriteLine("[DanganFurniture V3] Found {0} furniture objects", HowMuchFurniture);

				br.BaseStream.Position = 0xB0;

				for (int i = 0; i < HowMuchFurniture; i++) {
					// still couldn't think of a non romanian name sorry
					V3.Furniture Mobilier = new V3.Furniture();
					Mobilier.Type = br.ReadInt16();
					Mobilier.ID = br.ReadInt16();
					Mobilier.X = br.ReadSingle();
					Mobilier.Y = br.ReadSingle();
					Mobilier.Z = br.ReadSingle();
					Mobilier.float4 = br.ReadSingle();	
					Mobilier.float5 = br.ReadSingle();	
					Mobilier.float6 = br.ReadSingle();	
					Mobilier.float7 = br.ReadSingle();
					Mobilier.float8 = br.ReadSingle();
					Mobilier.Unk3 = br.ReadInt16();
					Mobilier.Unk4 = br.ReadInt16();

					Bucatarie[i] = Mobilier;
				}
				
				int HowMuchUTF8 = br.ReadInt32();
				Console.WriteLine("[DanganFurniture V3] Found {0} object descriptions", HowMuchUTF8);

				string[] ObjectNames = new string[HowMuchUTF8];
				byte[] StringByteArray = br.ReadBytes((int)fs.Length - (int)br.BaseStream.Position);
				// BUG: \x00\x00 makes an empty element which doesn't actually exist... probably?
				ObjectNames = Encoding.UTF8.GetString(StringByteArray).Split("\x00");

				for (int i = 0; i < Bucatarie.Length; i++) {
					Console.WriteLine(ObjectNames[i]);
					Bucatarie[i].ObjectName = ObjectNames[i];
				}

				Console.WriteLine();
			}}

			// ugly lazy hack
			return Bucatarie.ToList<V3.Furniture>();
		}

		// text.stx
		// TODO: Is a string List what we want? Would the previous KeyValue
		// matter in other places?
		public static List<string> ReadTextFile(this string FilePath) {
			List<string> TextNames = new List<string>();
			
			using (FileStream fs = File.Open(FilePath, FileMode.Open)) {
			using (BinaryReader br = new(fs) ) {
				br.BaseStream.Position = 0x14;

				int HowMuchText = br.ReadInt32();
				Console.WriteLine("[DanganFurniture V3] Found {0} texts", HowMuchText);
				
				V3.IndexNum[] TextsOffsets = new V3.IndexNum[HowMuchText];

				for (int i = 0; i < HowMuchText; i++) {
					TextsOffsets[i].Index = br.ReadInt32();
					TextsOffsets[i].Offset = br.ReadInt32();
				}

				for (int i = 0; i < TextsOffsets.Count(); i++) {
					int NextOffsetStart =
						(!(i + 1 == TextsOffsets.Count())) ?
						NextOffsetStart = TextsOffsets[i+1].Offset :
						NextOffsetStart = (int)fs.Length;
					
					br.BaseStream.Position = TextsOffsets[i].Offset;
					string AttemptedString = System.Text.Encoding.Unicode.GetString(br.ReadBytes(NextOffsetStart - (int)br.BaseStream.Position)).TrimEnd('\u0000');
					TextNames.Add(AttemptedString);
				}

			}}
			Console.WriteLine(JsonSerializer.Serialize(TextNames, new JsonSerializerOptions{IncludeFields = true, WriteIndented = true}));
			return TextNames;
		}
	}
}