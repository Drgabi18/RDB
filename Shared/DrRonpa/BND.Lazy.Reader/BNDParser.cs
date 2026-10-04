using System.Text;

namespace BNDParser {
	class Program {
		//public union HeaderSize(int, long); // AAAAAAAAAAAAAAAAAAAAAAA
		// TODO: 64 header too
		public struct BndHeader {
			public int NoOfGroups;
			public Group[] TablesOfLocations;
		};
		public struct Group {
			//public int Index; // not in the actual file, created so it's easier for me
			public int Address;
			public int Unk1;
		}

		//static void ReturnHeaderFromFile<T>(string fileName, T Header) where T : struct {}

		static void Main(string[] args) {
			string FolderPath = args[0];

			// TODO: Bnc seems to have the header defines for strings
			List<string> LinFilesFromFolder =
				Directory.EnumerateFiles(FolderPath, "*.bnd").Order().ToList();

			Dictionary<string, BndHeader> DictionaryOfHeaders = new Dictionary<string, BndHeader>();

			foreach (string file in LinFilesFromFolder) {
				// TODO: I don't need to explain
				if (file.Contains("64")) continue;
				using (FileStream fs = File.Open(file, FileMode.Open)) {
				using (BinaryReader br = new BinaryReader(fs, Encoding.ASCII)) {
					BndHeader HeaderWeHave = new BndHeader{};
					int Groups = br.ReadInt32();
					HeaderWeHave.NoOfGroups = Groups;
					HeaderWeHave.TablesOfLocations = new Group[Groups];
					for (int i = 0; i < Groups; i++) {
						HeaderWeHave.TablesOfLocations[i] = new Group { /*Index = i,*/ Address = br.ReadInt32(), Unk1 = br.ReadInt32() };
					}

					foreach (Group group in HeaderWeHave.TablesOfLocations) {
						br.BaseStream.Position = group.Address;
						int _nextLoc = br.ReadInt32();
						if (_nextLoc < 0) { Console.WriteLine("Less than 0!"); continue; }
						br.BaseStream.Position = _nextLoc;
						// int _nextNextLoc = br.ReadInt32();
						// br.BaseStream.Position = _nextNextLoc;
						Console.WriteLine("{0}\t{1:X4}\t{2:X4}", file, group.Address, _nextLoc);
						Console.WriteLine(Encoding.Unicode.GetString(br.ReadBytes(16)));
					}
					DictionaryOfHeaders.Add(new FileInfo(file).Name, HeaderWeHave);
				}}
			}

			// Console.WriteLine(JsonSerializer.Serialize(DictionaryOfHeaders, new JsonSerializerOptions{
			// 		IncludeFields = true,
			// 		WriteIndented = true
			// 		}));
		}
	}
}