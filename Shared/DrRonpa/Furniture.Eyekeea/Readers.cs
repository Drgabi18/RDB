using System.Text;
using System.Text.Json;
using DanganFurniture.Structs;

namespace DanganFurniture {
	// we could maybe make like an interface for these, or make them a class lol
	public static class Readers {
		
		// place.dat
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
					Mobilier.Unk1 = br.ReadInt32(); 
					Mobilier.Position[0] = br.ReadSingle(); 
					Mobilier.Position[1] = br.ReadSingle(); 
					Mobilier.Position[2] = br.ReadSingle(); 
					Mobilier.Size[0] = br.ReadSingle(); 
					Mobilier.Size[1] = br.ReadSingle(); 
					Mobilier.Rotation = br.ReadSingle(); 
					Mobilier.Unk2 = br.ReadSingle();
					
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

		// file.dat
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

		// opt.dat
		public static HPA.OptionsFile ReadOptionsFile(this string FilePath) {
			HPA.OptionsFile RoomInfo = new HPA.OptionsFile();
			
			using (FileStream fs = File.Open(FilePath, FileMode.Open)) {
			using (BinaryReader br = new(fs) ) {
				RoomInfo.HowManyOptions = br.ReadInt32();
				RoomInfo.HeaderSize = br.ReadInt32();
				RoomInfo.DR1_Unused_DR2_RoomUsesAnimation = br.ReadInt16();
				RoomInfo.UNUSED_1 = br.ReadInt16();
				RoomInfo.CameraMode = br.ReadInt16();
				RoomInfo.UNUSED_2 = br.ReadInt16();
				RoomInfo.LookUpAngle = br.ReadInt16();
				RoomInfo.LookDownAngle = br.ReadInt16();
				RoomInfo.FOV = br.ReadInt16();
			}}

			return RoomInfo;
		}
		
		// DR2 ONLY
		// bone_pos.dat
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
		
		// c.col.dat
		// z.col.dat in DR2
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

	// Actually glad these were easier to understand lol
	public static class V3Readers {

		// TODO: Decide if this should return PlaceFile instead, this should be a list just for testing
		// Place.dat
		public static List<V3.Furniture> ReadFurnitureFile(this string FilePath) {
			Console.WriteLine("[DanganFurniture V3] ==== This is untested code, run at your own risk ====");
			V3.Furniture[] Bucatarie;

			using (FileStream fs = File.Open(FilePath, FileMode.Open)) {
			using (BinaryReader br = new(fs) ) {
				int HowMuchFurniture = br.ReadInt32();
				Bucatarie = new V3.Furniture[HowMuchFurniture];
				Console.WriteLine("[DanganFurniture V3] Found {0} furniture objects", HowMuchFurniture);

				// skip refer header
				br.BaseStream.Position = 0xB0;

				for (int i = 0; i < HowMuchFurniture; i++) {
					// still couldn't think of a non romanian name sorry
					V3.Furniture Mobilier = new V3.Furniture();
					Mobilier.Type = br.ReadInt16();
					Mobilier.ID = br.ReadInt16();
					// beaside the first 2 ints, the field names here aren't actually
					// what they say they are, they are X Y and Z only on Type 11,
					// on other stuff like Type 5, X and Y are actually walk and run speed
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
				
				int HowManyUTF8Strings = br.ReadInt32();
				Console.WriteLine("[DanganFurniture V3] Found {0} object descriptions", HowManyUTF8Strings);

				string[] ObjectNames = new string[HowManyUTF8Strings];
				byte[] StringByteArray = br.ReadBytes((int)fs.Length - (int)br.BaseStream.Position);
				ObjectNames = Encoding.UTF8.GetString(StringByteArray).Split("\x00");
				// TODO: could we maybe do this better or easier, checking if it's 0x00 does not work lol
				ObjectNames = (from str in ObjectNames where str.Length > 0 select str).ToArray();
				Console.WriteLine("============== 1 ==============\n " + ReaderUtils.FuckAssSerializerForEncoding(ObjectNames));

				Dictionary<string, List<string>> LegalNameAndObjects = new Dictionary<string, List<string>>();
				List<string> TempListForAscii = null;

				for (int i = 0; i < ObjectNames.Length; i++) {
					Console.WriteLine(ObjectNames[i]);
					Console.WriteLine("Is Ascii: " + (ReaderUtils.IsEntireStringAscii(ObjectNames[i]) ? "Yes" : "No"));
					if (!ReaderUtils.IsEntireStringAscii(ObjectNames[i])) {
						TempListForAscii = new List<string>();
						LegalNameAndObjects.Add(ObjectNames[i], TempListForAscii);
					} else {
						TempListForAscii.Add(ObjectNames[i]);
					}
				}
				Console.WriteLine("============== 2 ==============\n " + ReaderUtils.FuckAssSerializerForEncoding(LegalNameAndObjects));

				// unique list of Type values so we can associate
				// type 0 withTypeNames[0], type 2 with TypeNames[1] etc.
				// so basically excel's =UNIQUE()
				List<short> UniqueListOfTypes = Bucatarie.Select(b => b.Type).Distinct().ToList();
				Console.WriteLine("============== 3 ==============\n " + ReaderUtils.FuckAssSerializerForEncoding(UniqueListOfTypes));

				Dictionary<short, string> TypeToLegalName = new Dictionary<short, string>();

				for (int i = 0; i < UniqueListOfTypes.Count; i++) {
					Console.WriteLine(i);
					Console.WriteLine("ult 1:\t\t" + UniqueListOfTypes[i]);
					Console.WriteLine("ult 2:\t\t" + LegalNameAndObjects.ElementAt(i).Key);
					Console.WriteLine();
					TypeToLegalName.Add(UniqueListOfTypes[i], LegalNameAndObjects.ElementAt(i).Key);
				}
				Console.WriteLine("============== 4 ==============\n " + ReaderUtils.FuckAssSerializerForEncoding(TypeToLegalName));

				// TODO: some linq magic to get type from Bucatarie to asign legal name, line below is complete shit
				Bucatarie.Select(f => f.LegalName = TypeToLegalName.ElementAt(f.Type).Value);

				// Console.ReadLine();

				Console.WriteLine();
			}}

			// ugly lazy hack
			return Bucatarie.ToList<V3.Furniture>();
		}

		// text.stx
		// TODO: Is a string List what we want? Would the previous KeyValue
		// matter in other places?
		public static List<string> ReadTextFile(this string FilePath) {
			Console.WriteLine("[DanganFurniture V3] ==== This is untested code, run at your own risk ====");
			List<string> TextNames = new List<string>();
			
			using (FileStream fs = File.Open(FilePath, FileMode.Open)) {
			using (BinaryReader br = new(fs) ) {
				br.BaseStream.Position = 0x14;

				int HowMuchText = br.ReadInt32();
				Console.WriteLine("[DanganFurniture V3] Found {0} texts", HowMuchText);
				
				// header size is at 0x20, it's 32 bytes, we go to that after
				br.BaseStream.Position = 0x20;

				V3.IndexNum[] TextsOffsets = new V3.IndexNum[HowMuchText];

				for (int i = 0; i < HowMuchText; i++) {
					TextsOffsets[i].Index = br.ReadInt32();
					TextsOffsets[i].Offset = br.ReadInt32();
					Console.WriteLine("{0} - {1}", TextsOffsets[i].Index, TextsOffsets[i].Offset);
				}

				for (int i = 0; i < TextsOffsets.Count(); i++) {
					// BUG: Some files have the index-offset like 12-228; 13-228; 14-320; 15-228 and it breaks this logic
					int NextOffsetStart =
						(!(i + 1 == TextsOffsets.Count())) ?
						NextOffsetStart = TextsOffsets[i+1].Offset :
						NextOffsetStart = (int)fs.Length;
					Console.WriteLine(NextOffsetStart);
					
					br.BaseStream.Position = TextsOffsets[i].Offset;
					string AttemptedString = System.Text.Encoding.Unicode.GetString(br.ReadBytes(NextOffsetStart - (int)br.BaseStream.Position)).TrimEnd('\u0000');
					TextNames.Add(AttemptedString);
				}

			}}
			Console.WriteLine(ReaderUtils.FuckAssSerializerForEncoding(TextNames));
			return TextNames;
		}
	}
	public static class ReaderUtils {
		public static bool IsZColFile(string FilePath) {
			using (FileStream fs = File.Open(FilePath, FileMode.Open)) {
			using (BinaryReader br = new(fs) ) {
				return br.ReadUInt32() == 4293844428; // 0xCCDDEEFF casting to uint doesn't work??????????
			}}
		}

		public static string FuckAssSerializerForEncoding(object obj) {
			JsonSerializerOptions CoolOptions = new JsonSerializerOptions();
			// https://stackoverflow.com/a/58003397
			CoolOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
			CoolOptions.IncludeFields = true;
			CoolOptions.WriteIndented = true;
			return JsonSerializer.Serialize(obj, CoolOptions);
		}
		
		// i'm not gonna lie but when i write stuff like this i do feel emberassed a bit
		// anyways, previously we did `if (!Char.IsAscii(ObjectNames[i][0]))` which
		// broke on lines like "6章砂塵：上エフェクト非表示"
		public static bool IsEntireStringAscii(string str) {
			foreach (char c in str) {
				if (!Char.IsAscii(c)) return false;
			}
			return true;
		}
	}
}