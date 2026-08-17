using DanganFurniture.Headers;

namespace DanganFurniture {
	
	// we could maybe make like an interface for these, or make them a class lol

	public static class Readers {
		// file 0002
		public static List<Furniture> ReadFurnitureFile(this string FilePath) {
			List<Furniture> Bucatarie = new List<Furniture>();
			
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
					Furniture Mobilier = new Furniture();
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
					// TODO: Remove this once we have a game toggle
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
		public static OptionsFile ReadOptionsFile(this string FilePath) {
			OptionsFile RoomInfo = new OptionsFile();
			
			using (FileStream fs = File.Open(FilePath, FileMode.Open)) {
			using (BinaryReader br = new(fs) ) {
				RoomInfo.Unk1 = br.ReadInt32();
				RoomInfo.Unk2 = br.ReadInt32();
				RoomInfo.Unk3 = br.ReadInt32();
				RoomInfo.Unk4 = br.ReadInt32();	// written by the same species
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
		public static Dictionary<string, AABBStruct> ReadAABBBonesFile(this string FilePath) {
			Dictionary<string, AABBStruct> ExtraObjectData = new();

			using (FileStream fs = File.Open(FilePath, FileMode.Open)) {
			using (BinaryReader br = new(fs) ) {
				int HowManySomething = br.ReadInt32();
				Console.WriteLine("[DanganFurniture] Found {0} somethings", HowManySomething);

				int[] SomethingOffset = new int[HowManySomething];
				for (int i = 0; i < HowManySomething; i++) {
					SomethingOffset[i] = br.ReadInt32();
				}
				
				string[] SomeNames = new string[HowManySomething - 1];
				// deal with object names first
				// i don't understand why they made these different
				br.BaseStream.Position = SomethingOffset.Last();
				SomeNames = System.Text.Encoding.ASCII.GetString(br.ReadBytes((int)fs.Length - (int)br.BaseStream.Position))
					.TrimEnd('\u0000').Split('\u0000');

				for (int i = 0; i < SomethingOffset.Count() - 1; i++) {
					AABBStruct Something = new AABBStruct();
					br.BaseStream.Position = SomethingOffset[i];
					Something.Unk1 = br.ReadInt32();
					Something.SixFloats[0] = br.ReadSingle();
					Something.SixFloats[1] = br.ReadSingle();
					Something.SixFloats[2] = br.ReadSingle();
					Something.SixFloats[3] = br.ReadSingle();
					Something.SixFloats[4] = br.ReadSingle();
					Something.SixFloats[5] = br.ReadSingle();
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
		public static CollisionFile ReadZColFile(this string FilePath) {
			CollisionFile Colissions = new();
			
			using (FileStream fs = File.Open(FilePath, FileMode.Open)) {
			using (BinaryReader br = new(fs) ) {
				Colissions.Identifier = br.ReadUInt32(); // whatev
				Colissions.FileSize = br.ReadInt32();
				Colissions.Unk2_HeaderSize = br.ReadInt32();
				Colissions.SizeBeforeTriangles = br.ReadInt32();
				Colissions.ListOfSomething = new List<int>();
				Colissions.Verticies = new List<Vertex>();

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
					Vertex vertex = new();
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
}