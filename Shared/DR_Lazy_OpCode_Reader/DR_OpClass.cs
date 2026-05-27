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
			new OperationCode(UDGClasses.HexOpCode.RUN_SCRIPT, 4 * 2),	// ditto
			new OperationCode(UDGClasses.HexOpCode.SET_FLAG, 3),	// 1 short, 1 byte
			new OperationCode(UDGClasses.HexOpCode.CHECK_FLAG1, 2),	// 1 short
			new OperationCode(UDGClasses.HexOpCode.CHECK_FLAG2, 2)	// 1 short
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