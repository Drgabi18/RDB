namespace EV8Reader.ObjectTypes {
	struct PostObjChunk {
		ObjTypeHeaderChunk Header;
		UDGPos ObjectPos1;
		UDGPos ObjectPos2; // ????
		// ... after some time
		ShortComment ObjectName;
		// unknown empty space after
	}
	
	struct PostObj {
		ObjTypeHeader Header;
		PostObjChunk[] Objects; // repeats `HowManyOfContent` times
	}
}