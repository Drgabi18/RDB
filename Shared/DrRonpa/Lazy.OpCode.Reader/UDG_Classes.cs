#region Yes, I know what it outputs isn't right
/*
	Yes, I'm aware that if you execute this you will see stuff like
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
		// THESE ARE ALL SPECULATIVE
		// as far as I'm aware, I'm the first to try to make something out of
		// these, smarter people than me are welcome to show off
		public enum HexOpCode : byte {
			START = 0x70,
			TEXT_ENTRIES = 0x00,
			LOAD_SCRIPT = 0x0E,
			SET_FLAG = 0x17,
			CHECK_FLAG = 0x1B,
			END_CHECK_FLAG = 0x18, // HOLY FUCK I JUST REALIZED THIS SHOULD BE AN ENDIF
			STOP_SCRIPT = 0x25 // has vargs number of arguments because of 4-byte padding
			/*
			UNK_0x01 = 01, UNK_0x02 = 02, UNK_0x05 = 05, UNK_0x06 = 06, UNK_0x07 = 07,
			UNK_0x08 = 08, UNK_0x09 = 09, UNK_0x0C = 0C, UNK_0x11 = 11, UNK_0x12 = 12,
			UNK_0x15 = 15, UNK_0x16 = 16, UNK_0x19 = 19, UNK_0x1A = 1A, UNK_0x1C = 1C,
			UNK_0x1D = 1D, UNK_0x21 = 21, UNK_0x22 = 22, UNK_0x23 = 23, UNK_0x24 = 24,
			UNK_0x26 = 26, UNK_0x27 = 27, UNK_0x28 = 28, UNK_0x2D = 2D, UNK_0x30 = 30,
			UNK_0x32 = 32, UNK_0x33 = 33, UNK_0x39 = 39, UNK_0x3C = 3C, UNK_0x3D = 3D,
			UNK_0x41 = 41, UNK_0x45 = 45, UNK_0x46 = 46,  UNK_0x47 = 47, UNK_0x48 = 48,
			UNK_0x49 = 49, UNK_0x4F = 4F
			*/
		}

		public static object? CreateObject(byte OpCode, byte[] bytes) {
			switch( (HexOpCode)OpCode ) {
				case HexOpCode.LOAD_SCRIPT:	return new LoadScript(bytes);
				//case HexOpCode.RUN_SCRIPT:	return new RunScript(bytes);
				case HexOpCode.SET_FLAG:	return new SetFlag(bytes);
				case HexOpCode.CHECK_FLAG:	return new CheckFlag(bytes);
				case HexOpCode.END_CHECK_FLAG:	return new EndFlagCheck(bytes);
				default: return null;
			}
		}

		// 0x0E
		// this seems to be correct and incorrect at the same time
		public class LoadScript {
			public string Type = "Load Script"; // hack to see what the objects are
			public short Chapter;
			public short Episode;
			public short SubEpisode;
			public short Unk1;	// this is most likely a bitmask, since, for example
								// if we look at e69_007_003 it has 28687 for this
								// value, which is 0x700F (LE), which is
								// 0b0111_0000_0000_1111	
			public LoadScript(byte[] bytes) {
				this.Chapter	= BinaryPrimitives.ReverseEndianness(
					BitConverter.ToInt16(bytes.AsSpan(0, 2)));
				this.Episode	= BinaryPrimitives.ReverseEndianness(
					BitConverter.ToInt16(bytes.AsSpan(2, 2)));
				this.SubEpisode	= BinaryPrimitives.ReverseEndianness(
					BitConverter.ToInt16(bytes.AsSpan(4, 2)));
				this.Unk1	= BinaryPrimitives.ReverseEndianness(
					BitConverter.ToInt16(bytes.AsSpan(6, 2)));
			}

			public override string ToString()
			{
				// apparently you can't do this
				//if ((Chapter < 0 && Chapter > 99) || (Episode < 0 && Episode > 999)) Console.WriteLine("Load Script anomaly detected");
				return $"e{Chapter:D2}_{Episode:D3}_{SubEpisode:D3}\t[comment=\"Unk1: {Unk1}\t{Unk1:b16}\"]";
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
			public override string ToString()
			{
				// easiest hack i could think
				return $"Set flag 0x{Flag:X4}\t({Flag})\tto {Value}";
			}
		}

		// 0x1B
		public class CheckFlag {
			public string Type = "Check Flag"; // hack to see what the objects are
			public short Flag;
			public CheckFlag(byte[] bytes) {
				this.Flag	= BinaryPrimitives.ReverseEndianness(BitConverter.ToInt16(bytes));
			}
		}

		// 0x18 since I'm lazy
		public class EndFlagCheck {
			public string Type = "End Check Flag"; // hack to see what the objects are
			public short Flag;
			public EndFlagCheck(byte[] bytes) {
				this.Flag	= BinaryPrimitives.ReverseEndianness(BitConverter.ToInt16(bytes));
			}
		}
	}
}