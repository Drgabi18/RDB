using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using EV8Reader.Headers;
using EV8Reader.ObjectTypes;

namespace EV8Reader {
	public class Program {
		public static void Main(string[] args) {
			//Console.OutputEncoding = Encoding.Unicode;
			//byte[] dataYYYY = { 0x91, 0xE5, 0x96, 0xE5, 0x91, 0xE5, 0x83, 0x43, 0x83, 0x78, 0x83, 0x93, 0x83, 0x67, 0x97, 0x70, 0x00, 0x00, 0x00 };
			//Console.WriteLine(EV8Helper.AttemptDecodeString(dataYYYY));
			//return;
			string FolderPath = args[0];
			
			List<string> LinFilesFromFolder =
				Directory.EnumerateFiles(FolderPath, "*.ev8").Order().ToList();
			
			Dictionary<string, EV8FileHeader> ListOfEventFiles = new Dictionary<string, EV8FileHeader>();

			foreach (string file in LinFilesFromFolder) {
				using (FileStream fs = File.Open(file, FileMode.Open)) {
				using (BinaryReader br = new BinaryReader(fs, Encoding.Unicode)) {
					// TODO: We should improve this code, I don't know why we're
					// manually deserializing when PtrToStruct should have done
					// this, the only bad part will be unexpected MarshallAs
					EV8FileHeader FileHeader = new EV8FileHeader();
					FileHeader = CastingHelper.CastToStruct<EV8FileHeader>(br.ReadBytes(0x295C));
					/*
					br.BaseStream.Position = 0x100;
					FileHeader.EV8Type = br.ReadInt32();
					FileHeader.FileSize = br.ReadInt32();
					FileHeader.NoOfObjects = br.ReadInt32();
					FileHeader.ListOfObjects = new List<EV8ListEntry>();

					for (int ObjType=0; ObjType < FileHeader.NoOfObjects; ObjType++) {
						EV8ListEntry TheObj = new EV8ListEntry();
						TheObj.ObjectName = Encoding.ASCII.GetString(br.ReadBytes(32)).TrimEnd('\u0000');
						TheObj.AdressOfObject = br.ReadInt32();
						TheObj.HeaderReportedSize = br.ReadInt32();

						FileHeader.ListOfObjects.Add(TheObj);
					}

					// let's have some fun trying some new stuff
					if (FileHeader.EV8Type == 2) {
						br.BaseStream.Position = 0x290C;
						FileHeader.ExtraData = CastingHelper.CastToStruct<EV8ExtraData>(br.ReadBytes(80));
					}
					*/

					foreach (EV8ListEntry obj in FileHeader.ListOfObjects) {
						Enum.TryParse(obj.ObjectName, out ObjectClasses.ObjTypes ResObj);
						ObjTypeHeader ObjHeader;
						br.BaseStream.Position = obj.AdressOfObject;
						ObjHeader.SizeOfContent=br.ReadInt32();
						ObjHeader.HowManyChunks=br.ReadInt32();
						ObjHeader.HeaderSize=br.ReadInt32();
						ObjHeader.Unk1=br.ReadInt32();
						//br.BaseStream.Position += 0x50;
						//ObjHeader.TEMPORARY_STRING = Encoding.ASCII.GetString(br.ReadBytes(64)).Replace("\u0000", "");
						// Console.WriteLine(ObjHeader.TEMPORARY_STRING);
						// obj.DataFromSerializedObject = ObjectClasses.CreateObject(ResObj);
						// obj.DataFromSerializedObject = ObjHeader;
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