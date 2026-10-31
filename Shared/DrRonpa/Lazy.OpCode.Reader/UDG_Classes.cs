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
			END_CHECK_FLAG = 0x18,	// makes the most sense
			EV8_ASSOCIATE = 0x24,
			STOP_SCRIPT = 0x25 // has vargs number of arguments because of 4-byte padding :P
			
			/*
			BOTTOM_TEXT = 0x01	// 2 bytes, text at the bottom, like ones said by genocider
			S_RETURN_EARLY_1 = 0x0E,	// always present after load scripts in e99_007_002
			S_RETURN_EARLY_2 = 0x0F	// same as above,exists e99_004_001
			UNK_11 = 0x11,	// seems to always follow 0x1A
			SET_ATTRIBUTE = 0x1A,	// mostly present after EV8_ASSOCIATE, but not
									// always, i think it's something like for byte[0]
									// set values bytes[1..3]
			UNK_30 = 0x30,	// seems to be followed by 4 shorts, used by OBJTYPE_RECT that
							// make a text at the bottom
			UNK_4F = 0x4F,	// seems to be always before CHECK_FLAG

				Some observations I made that I'm not very sure about
				If you're reading this and want to be a good puppy, see
				between 0x16 and 0x21 which one is load room and .ev8
				I think 0x21 might be load .ev8, because maps64.bnd, the files
				are organized like
					1 = MP_STREET_01
					2 = MP_STREET_03 (yes, there's no mp_street_2 and 3 really uses index 2)
					3 = MP_STREET_04
					4 = MP_STREET_05
				and it makes the most sense with 0x21 where the 2nd argument seems
				to be this number 

			UNK_02 = 0x02
			UNK_05 = 0x05	// 3 bytes
			UNK_06 = 0x06	// 4 bytes
			UNK_07 = 0x07	// 5 bytes
			UNK_08 = 0x08
			UNK_09 = 0x09	// 3 bytes
			UNK_0C = 0x0C	// 0 bytes
			UNK_12 = 0x12	// 5 bytes
			UNK_15 = 0x15	// 1 byte
			UNK_16 = 0x16	// 3 bytes
			UNK_19 = 0x19	// 1 byte
			UNK_1A = 0x1A	// 4 bytes
			UNK_1C = 0x1C	// 4 bytes
			UNK_1D = 0x1D	// 4 bytes (seems like 2 shorts)
			UNK_21 = 0x21	// 3 bytes
			UNK_22 = 0x22	// 2 bytes
			UNK_23 = 0x23	// 2 bytes
			UNK_24 = 0x24	// 2 bytes (associate certain object to flag?)
			UNK_26 = 0x26	// 6 bytes (seems like 3 shorts, maybe voice)
			UNK_27 = 0x27	// 3 bytes (seems like 1 byte and 1 short)
			UNK_28 = 0x28	// 3 bytes
			UNK_2D = 0x2D
			UNK_30 = 0x30	// 8 bytes (seems like 4 shorts)
			UNK_32 = 0x32	// 4 bytes
			UNK_33 = 0x33
			UNK_39 = 0x39	// 4 bytes
			UNK_3C = 0x3C
			UNK_3D = 0x3D	// 10 bytes (seems like 5 shorts)
			UNK_41 = 0x41	// 8 bytes (seems like 4 shorts)
			UNK_45 = 0x45
			UNK_46 = 0x46
			UNK_47 = 0x47
			UNK_48 = 0x48,
			UNK_49 = 0x49, 
			*/
		}

		public static object? CreateObject(byte OpCode, byte[] bytes) {
			switch( (HexOpCode)OpCode ) {
				case HexOpCode.LOAD_SCRIPT:	return new LoadScript(bytes);
				case HexOpCode.SET_FLAG:	return new SetFlag(bytes);
				case HexOpCode.CHECK_FLAG:	return new CheckFlag(bytes);
				case HexOpCode.END_CHECK_FLAG:	return new EndFlagCheck(bytes);
				case HexOpCode.EV8_ASSOCIATE: return new Ev8Association(bytes);
				default: return null;
			}
		}

		// 0x0E
		// this seems to be correct and incorrect at the same time
		public class LoadScript {
			public string Comment = "Load Script"; // hack to see what the objects are
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
			public string Comment = "Set Flag"; // hack to see what the objects are
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
			public string Comment = "Check Flag"; // hack to see what the objects are
			public short Flag;
			public CheckFlag(byte[] bytes) {
				this.Flag	= BinaryPrimitives.ReverseEndianness(BitConverter.ToInt16(bytes));
			}
		}

		// 0x18
		// copy pasted from above since I'm lazy
		public class EndFlagCheck {
			public string Comment = "End Check Flag"; // hack to see what the objects are
			public short Flag;
			public EndFlagCheck(byte[] bytes) {
				this.Flag	= BinaryPrimitives.ReverseEndianness(BitConverter.ToInt16(bytes));
			}
		}

		// 0x24
		public class Ev8Association {
			public string Comment = "Type and Index EV8 association"; // hack to see what the objects are
			public byte Type;
			public byte Index;
			
			public Ev8Association(byte[] bytes) {
				this.Type	= bytes[0];
				this.Index	= bytes[1];
			}
		}
	}
}