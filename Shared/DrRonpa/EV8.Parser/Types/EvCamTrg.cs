// TODO: COPY PASTED FROM EVTMDL CAUSE I'M LAZY, FIX LATER

namespace EV8Reader.ObjectTypes {
	struct EvCamTrgChunk {
		ObjTypeHeaderChunk Header;
		UDGPos PositionOfCharacter;
		// ... after some time
		ShortComment CamTrgName2;
		// unknown 76 bytes after
	}
	
	struct EvCamTrg {
		ObjTypeHeader Header;
		EvCamTrgChunk[] CamTrgs; // repeats `HowManyOfContent` times
	}
}