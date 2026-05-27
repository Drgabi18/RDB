using LazyOpCodeReader.OpCodes;
using LazyOpCodeReader.LIN;

namespace LazyOpCodeReader.Games {
	public class Game {
		public enum GameID {
			NONE = 0,	// this was genuinley the easiest fix i could think of
						// for "use of unasgined variable" error 
			DR1 = 1,
			DR2 = 2,
			UDG = 122,	// get it, 1 to 2, cause it's in the middle...
			// V3 format is completly different, thus not in-scope here
		}

		public GameID GameName;
		public List<LinFile> LinFiles = new List<LinFile>();

		public List<OperationCode> GetValidOpCodes() {
			switch(GameName){
				case GameID.DR1: return OperationCode.DR1OpCodes;
				case GameID.DR2: return OperationCode.DR2OpCodes;
				case GameID.UDG: return OperationCode.UDGOpCodes;
				default: throw new Exception("No Games PS5");
			}
		}

		public object? MakeGameLinObjects(byte opCode, byte[] bytes) {
			switch(GameName){
				case GameID.DR1: return DR1Classes.CreateObject(opCode, bytes);
				case GameID.DR2: return DR2Classes.CreateObject(opCode, bytes);
				case GameID.UDG: return UDGClasses.CreateObject(opCode, bytes);
				default: throw new Exception("No Games PS5");
			}
		}

		// simple constructor
		public Game(GameID game) {
			this.GameName = game;
		}
	}	
}