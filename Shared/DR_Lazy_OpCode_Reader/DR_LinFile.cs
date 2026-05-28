using System.Text.Json.Serialization;

namespace LazyOpCodeReader.LIN {
	public class LinFile {
		[JsonIgnore] public string LongFileName;
		// should we create a class for eXX_XXX_XXX instead?
		public string PrettyFileName; // eXX_XXX_XXX
		public int LinType;
		public int HeaderSize;
		// i'd wish to make this nullable but C# throws a hissy fit
		public int TextOffset;
		public int FileSize;
		public int HowManyStrings;
		public List<string> Strings = new();
		// preferably, when we are to print the resulting op code objects, we
		// would also want to know what the name is, as we have it right now,
		// it does not do that, at all
		// ^ one solution would be to just inclde what it does as a string
		public List<object>? ResultingOpCodeObjects = new();

		public LinFile(string file) {
			this.LongFileName = file;
			this.PrettyFileName = new FileInfo(file).Name[..^4];
		}

		public override string ToString()
		{
			return PrettyFileName;
		}
	}
}