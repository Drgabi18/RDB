using LazyOpCodeReader.Games;

namespace LazyOpCodeReader.OpCodes {
	public class OperationCode {
		public const byte START_HEX = 0x70;
		public System.Enum Value;
		public int NoOfArguments;

		public static List<OperationCode> DR1OpCodes = new List<OperationCode>{
			// new OperationCode(DR1Classes.HexOpCode.START, 0),
			new OperationCode(DR1Classes.HexOpCode.LOAD_MAP, 3),
			new OperationCode(DR1Classes.HexOpCode.LOAD_SCRIPT, 3),
			new OperationCode(DR1Classes.HexOpCode.RUN_SCRIPT, 3)
		};

		public static List<OperationCode> DR2OpCodes = new List<OperationCode>{
			// new OperationCode(DR2Classes.HexOpCode.START, 0),
			new OperationCode(DR2Classes.HexOpCode.LOAD_MAP, 4),	// idk why it has 1 more
			new OperationCode(DR2Classes.HexOpCode.LOAD_SCRIPT, 5),	// idk why it has 2 more
			new OperationCode(DR2Classes.HexOpCode.RUN_SCRIPT, 5)	// idk why it has 2 more
		};
		
		public static List<OperationCode> UDGOpCodes = new List<OperationCode>{
			// new OperationCode("START", 0x70, 0),
			new OperationCode(UDGClasses.HexOpCode.LOAD_SCRIPT, 4 * 2),	// 4 shorts (8 bytes)
			//new OperationCode(UDGClasses.HexOpCode.RUN_SCRIPT, 4 * 2),	// ditto
			new OperationCode(UDGClasses.HexOpCode.SET_FLAG, 3),	// 1 short, 1 byte
			new OperationCode(UDGClasses.HexOpCode.CHECK_FLAG, 2),	// 1 short
			new OperationCode(UDGClasses.HexOpCode.END_CHECK_FLAG, 2)	// 1 short
			/*
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

			0x01 = 2 bytes (text at the bottom, like ones said by genocider)
			0x0F = 0 bytes (exists after LOAD_SCRIPT in e99_004_001, maybe kill_script?)
			0x11 = 0 bytes (always present before END_CHECK_FLAG)
			0x1D = 4 bytes (seems like 2 shorts)
			0x24 = 2 bytes (associate certain object to flag?)
			0x26 = 6 bytes (seems like 3 shorts, maybe voice)
			0x27 = 3 bytes (seems like 1 byte and 1 short)
			0x30 = 8 bytes (seems like 4 shorts)
			0x3D = 10 bytes (seems like 5 shorts)
			0x41 = 8 bytes (seems like 4 shorts)
			0x4F = 0 bytes (always present before CHECK_FLAG)
			0x05 = 3 bytes
			0x06 = 4 bytes
			0x07 = 5 bytes
			0x09 = 3 bytes
			0x0C = 0 bytes
			0x12 = 5 bytes
			0x15 = 1 byte
			0x16 = 3 bytes
			0x19 = 1 byte
			0x1A = 4 bytes
			0x1C = 4 bytes
			0x21 = 3 bytes
			0x22 = 2 bytes
			0x23 = 2 bytes
			0x28 = 3 bytes
			0x32 = 4 bytes
			0x39 = 4 bytes
			*/
		};
		
		// look, i tried `OperationCode<T>(T value, ...) where T: System.Enum` but
		// it kept erroring for no fucking reason, this is the only smart way i
		// could find to do this
		public OperationCode(DR1Classes.HexOpCode value ,int arguments) {
			this.Value = value;
			this.NoOfArguments = arguments;
		}
		public OperationCode(DR2Classes.HexOpCode value ,int arguments) {
			this.Value = value;
			this.NoOfArguments = arguments;
		}
		public OperationCode(UDGClasses.HexOpCode value ,int arguments) {
			this.Value = value;
			this.NoOfArguments = arguments;
		}
	}
}