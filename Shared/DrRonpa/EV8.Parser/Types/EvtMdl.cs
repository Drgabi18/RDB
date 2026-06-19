namespace EV8Reader.ObjectTypes {
	struct EvtMdlChunk {
		ObjTypeHeaderChunk Header;
		UDGPos PositionOfCharacter;
		// ... after some time
		ShortComment ModelName2;
		// unknown 76 bytes after
	}
	
	struct EvtMdl {
		ObjTypeHeader Header;
		EvtMdlChunk[] Models; // repeats `HowManyOfContent` times
	}
}