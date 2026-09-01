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

using System.Text;
using System.Text.Json;
using DanganFurniture.Structs;

namespace DanganFurniture.V3 {

	struct PlaceFile {
		/* 0x00 */ public int HowMuchFurniture;
		/* 0x04 */ public int Unk1;
		/* 0x08 */ public int HeaderSize;
		/* 0x0C */ public string[] FormatStuff;
		/* 0xB0 */ public FurnitureV3[] Objects;

		// seems to be separated by 0x00 (.. below)
		// 32 .. .. .. E6 93 8D E4  BD 9C .. .. E3 82 AD E3
		// 83 A3 E3 83 A9 E8 A1 A8  E7 8F BE .. 
		// 					\/
		// 50 (0x32) strings - キャラ表現 - カメラ上下角度制限
		public int HowMuchUTF8;
		public string[] Names;
		
	}

	// isn't it weird that in phienes and ferb there's a rabbit boy with a 
	// blender and nobody questions why there's an anthro rabbit?

	// text.stx
	public struct IndexNum {public int Index; public int Offset;}

	public struct TextFile {
		/* 0x0 */ // char[8] Identifier = "STXTJPLL"	// S?... Text... Japanase... LL?
		/* 0x4 */ int Unk1;
		/* 0x8 */ int OffsetToStartOfIndexes;
		/* 0x10 */ int Unk2;
		/* 0x14 */ int HowMuchText;
		/* 0x18 */ // 8 bytes of emtpy space, maybe Unk2?
		/* 0x20 */ IndexNum[] Indexes;
		// and the UTF-16LE text is here :P
	}

	public static class Readers {
		
		// TODO: Make this return a PlaceFile instead, this should be a list just for testing
		// place.dat
		public static List<FurnitureV3> ReadFurnitureFile(this string FilePath) {
			FurnitureV3[] Bucatarie;

			using (FileStream fs = File.Open(FilePath, FileMode.Open)) {
			using (BinaryReader br = new(fs) ) {
				int HowMuchFurniture = br.ReadInt32();
				Bucatarie = new FurnitureV3[HowMuchFurniture];
				Console.WriteLine("[DanganFurniture V3] Found {0} furniture objects", HowMuchFurniture);

				br.BaseStream.Position = 0xB0;

				for (int i = 0; i < HowMuchFurniture; i++) {
					// still couldn't think of a non romanian name sorry
					FurnitureV3 Mobilier = new FurnitureV3();
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
				Console.WriteLine("[DanganFurniture V3] Found {0} texts", HowMuchUTF8);

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
			return Bucatarie.ToList<FurnitureV3>();
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
				
				IndexNum[] TextsOffsets = new IndexNum[HowMuchText];

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