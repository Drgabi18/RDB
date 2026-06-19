using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using EV8Reader.ObjectTypes;

namespace EV8Reader {
	public class Program {
		public static void Main(string[] args) {
			// Console.OutputEncoding = Encoding.Unicode;
			string FolderPath = args[0];
			
			List<string> LinFilesFromFolder =
				Directory.EnumerateFiles(FolderPath, "*.ev8").Order().ToList();
			
			Dictionary<string, EV8FileHeader> ListOfEventFiles = new Dictionary<string, EV8FileHeader>();

			foreach (string file in LinFilesFromFolder) {
				using (FileStream fs = File.Open(file, FileMode.Open)) {
				using (BinaryReader br = new BinaryReader(fs, Encoding.Unicode)) {
					br.BaseStream.Position = 0x100;
					EV8FileHeader FileHeader;
					//  = new EV8FileHeader();
					FileHeader.Unk1 = br.ReadInt32();
					FileHeader.FileSize = br.ReadInt32();
					FileHeader.NoOfObjects = br.ReadInt32();
					FileHeader.ListOfObjects = new List<EV8ListEntry>();

					for (int ObjType=0; ObjType < FileHeader.NoOfObjects; ObjType++) {
						EV8ListEntry TheObj = new EV8ListEntry();
						TheObj.ObjectName = Encoding.ASCII.GetString(br.ReadBytes(32)).Replace("\u0000", "");
						TheObj.AdressOfObject = br.ReadInt32();
						TheObj.HeaderReportedSize = br.ReadInt32();

						FileHeader.ListOfObjects.Add(TheObj);
					}

					foreach (EV8ListEntry obj in FileHeader.ListOfObjects) {
						Enum.TryParse(obj.ObjectName, out ObjectClasses.ObjTypes ResObj);
						ObjTypeHeader ObjHeader;
						br.BaseStream.Position = obj.AdressOfObject;
						ObjHeader.SizeOfContent=br.ReadInt32();
						ObjHeader.HowManyChunks=br.ReadInt32();
						ObjHeader.HeaderSize=br.ReadInt32();
						ObjHeader.Unk1=br.ReadInt32();
						br.BaseStream.Position += 0x50;
						ObjHeader.TEMPORARY_STRING = Encoding.Unicode.GetString(br.ReadBytes(64)).Replace("\u0000", "");
						// Console.WriteLine(ObjHeader.TEMPORARY_STRING);
						// obj.DataFromSerializedObject = ObjectClasses.CreateObject(ResObj);
						obj.DataFromSerializedObject = ObjHeader;
					}

					string ShortFileName = new FileInfo(file).Name;
					ListOfEventFiles[ShortFileName] = FileHeader;
					}
				}
			}

			// return;
			Console.WriteLine(JsonSerializer.Serialize(ListOfEventFiles,
				new JsonSerializerOptions{
					IncludeFields = true,
					WriteIndented = true,
					Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
				}));
		}
	}
}