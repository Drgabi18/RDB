#region Yes, I know what it outputs isn't right
/*
	Yes, I'm aware that if you execute this you will see stuff like
		e77_005_004 -> e16_001_???      (Unk1: 150      0096,   Unk2: 0 0000,   Run)
		e77_005_004 -> e17_1007_???     (Unk1: 2        0002,   Unk2: 1 0001,   Run)
		e77_005_004 -> e17_1007_???     (Unk1: 2        0002,   Unk2: 1 0001,   Run)
		e77_005_004 -> e20_001_???      (Unk1: 1007     03EF,   Unk2: 4 0004,   Run)
		e77_005_004 -> e15_1007_???     (Unk1: 5        0005,   Unk2: 1 0001,   Run)
		e77_005_004 -> e00_003_???      (Unk1: 1        0001,   Unk2: 1 0001,   Run)
	or even stuff like
		e05_107_002 -> e00_-16381_???   (Unk1: 0        0000,   Unk2: -11261    D403,   Load)
	
	Truth be told, I don't know why, I have absolutely NO idea why. I am doing the
	conversion from BE to LE correctly, why would the game call for numbers
	higher than any existing LIN filenames? I also have no idea how the second
	example even has negative numbers, I verified and both opcodes are correct 
*/
#endregion

using System.Buffers.Binary;


namespace LazyOpCodeReader.Games {
	public static class UDGClasses {
		public enum HexOpCode : byte {
			START = 0x70,
			
			// THESE ARE ALL SPECULATIVE
			LOAD_SCRIPT = 0x03,
			RUN_SCRIPT = 0x30,
			SET_FLAG = 0x17,
			CHECK_FLAG1 = 0x1B,
			CHECK_FLAG2 = 0x18
		}

		public static object? CreateObject(byte OpCode, byte[] bytes) {
			switch( (HexOpCode)OpCode ) {
				case HexOpCode.LOAD_SCRIPT:	return new LoadScript(bytes);
				case HexOpCode.RUN_SCRIPT:	return new RunScript(bytes);
				case HexOpCode.SET_FLAG:	return new SetFlag(bytes);
				case HexOpCode.CHECK_FLAG1:	
				case HexOpCode.CHECK_FLAG2:	return new CheckFlag(bytes);
				default: return null;
			}
		}

		// 0x03
		// this seems to be correct and incorrect at the same time
		public class LoadScript {
			public string Type = "Load Script"; // hack to see what the objects are
			public short Chapter;
			public short Episode;
			public short Unk1;
			public short Unk2;
			public LoadScript(byte[] bytes) {
				this.Chapter	= BinaryPrimitives.ReverseEndianness(
					BitConverter.ToInt16(bytes.AsSpan(0, 2)));
				this.Episode	= BinaryPrimitives.ReverseEndianness(
					BitConverter.ToInt16(bytes.AsSpan(2, 2)));
				this.Unk1	= BinaryPrimitives.ReverseEndianness(
					BitConverter.ToInt16(bytes.AsSpan(4, 2)));
				this.Unk2	= BinaryPrimitives.ReverseEndianness(
					BitConverter.ToInt16(bytes.AsSpan(6, 2)));
			}

			public override string ToString()
			{
				// apparently you can't do this
				//if ((Chapter < 0 && Chapter > 99) || (Episode < 0 && Episode > 999)) Console.WriteLine("Load Script anomaly detected");
				return $"e{Chapter:D2}_{Episode:D3}_???\t(Unk1: {Unk1}\t{Unk1:X4},\tUnk2: {Unk2}\t{Unk2:X4},\tLoad)";
			}
		}
		// 0x30
		// this seems to be correct and incorrect at the same time
		public class RunScript {
			public string Type = "Run Script"; // hack to see what the objects are
			public short Chapter;
			public short Episode;
			public short Unk1;
			public short Unk2;
			public RunScript(byte[] bytes) {
				this.Chapter	= BinaryPrimitives.ReverseEndianness(
					BitConverter.ToInt16(bytes.AsSpan(0, 2)));
				this.Episode	= BinaryPrimitives.ReverseEndianness(
					BitConverter.ToInt16(bytes.AsSpan(2, 2)));
				this.Unk1	= BinaryPrimitives.ReverseEndianness(
					BitConverter.ToInt16(bytes.AsSpan(4, 2)));
				this.Unk2	= BinaryPrimitives.ReverseEndianness(
					BitConverter.ToInt16(bytes.AsSpan(6, 2)));
			}
			
			public override string ToString()
			{
				return $"e{Chapter:D2}_{Episode:D3}_???\t(Unk1: {Unk1}\t{Unk1:X4},\tUnk2: {Unk2}\t{Unk2:X4},\tRun)";
			}
		}

		// 0x17
		public class SetFlag {
			public string Type = "Set Flag"; // hack to see what the objects are
			public short Flag;
			public byte Value;
			public SetFlag(byte[] bytes) {
				this.Flag	= BinaryPrimitives.ReverseEndianness(BitConverter.ToInt16(bytes));
				this.Value	= bytes[2];
			}
		}

		// 0x1B and 0x18 since I'm lazy
		public class CheckFlag {
			public string Type = "Check Flag"; // hack to see what the objects are
			public short Flag;
			public CheckFlag(byte[] bytes) {
				this.Flag	= BinaryPrimitives.ReverseEndianness(BitConverter.ToInt16(bytes));
			}
		}
	}
}