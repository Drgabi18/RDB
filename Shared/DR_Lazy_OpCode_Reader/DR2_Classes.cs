namespace LazyOpCodeReader.Games {
	public static class DR2Classes {
		public enum HexOpCode : byte {
			START = 0x70,
			
			LOAD_MAP = 0x15,
			LOAD_SCRIPT = 0x19,
			RUN_SCRIPT = 0x1B,
			
			STOP_SCRIPT = 0x1A,
		}

		public static object? CreateObject(byte OpCode, byte[] bytes) {
			switch( (HexOpCode)OpCode ) {
				case HexOpCode.LOAD_MAP: return new LoadMap(bytes);
				case HexOpCode.LOAD_SCRIPT: return new LoadScript(bytes);
				case HexOpCode.RUN_SCRIPT: return new RunScript(bytes);
				default: return null;
			}
		}

		// 0x19
		public class LoadScript {
			public string Type = "Load Script"; // hack to see what the objects are
			public byte Chapter;
			public byte Episode;
			public byte SubEpisode; // sometimes matches room id (e.g. e00_000_180)
			public byte Unk1;
			public byte Unk2;
			public LoadScript(byte[] bytes) {
				this.Chapter = bytes[0];
				this.Episode = bytes[1];
				this.SubEpisode = bytes[2];
				this.Unk1 = bytes[3];
				this.Unk2 = bytes[4];
			}
			public override string ToString()
			{
				return $"e{Chapter:D2}_{Episode:D3}_{SubEpisode:D3}";
			}
		}
		// 0x1B
		// personally I would have done `RunScript : LoadScript` but I don't know
		// how i would make the constructor for this not be LoadScript's 
		public class RunScript {
			public string Type = "Run Script"; // hack to see what the objects are
			public byte Chapter;
			public byte Episode;
			public byte Unk1;
			public byte Unk2;
			public byte SubEpisode;  // sometimes matches room id
			public RunScript(byte[] bytes) {
				this.Chapter = bytes[0];
				this.Episode = bytes[1];
				this.SubEpisode = bytes[2];
				this.Unk1 = bytes[3];
				this.Unk2 = bytes[4];
			}
			public override string ToString()
			{
				return $"e{Chapter:D2}_{Episode:D3}_{SubEpisode:D3}";
			}
		}
		// 0x15
		public class LoadMap {
			public string Type = "Load Map"; // hack to see what the objects are
			public byte Room;
			public byte Unk2;
			public byte Unk3;
			public byte Unk4;
			public LoadMap(byte[] bytes) {
				this.Room = bytes[0];
				this.Unk2 = bytes[1];
				this.Unk3 = bytes[2];
				this.Unk4 = bytes[3];
			}
			// this is where we convert the data to the room name...
			// IF I HAD ONE LIST... i mean i did make one but yeah no, not
			// converting it
			/*
			public override string ToString()
			{
				return $"{RoomName[Room]}";
			}
			*/
		}
	}
}