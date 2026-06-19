namespace EV8Reader.ObjectTypes {
	struct PostTbxChunk {
		ObjTypeHeaderChunk Header;
		UDGPos ItemLoc1;
		UDGPos ItemLoc2;
		// ... after some time
		ShortComment ItemName;
		// unknown 8 bytes
	}
	
	struct PostTbx {
		ObjTypeHeader Header;
		PostTbxChunk[] Items; // repeats `HowManyOfContent` times
	}
}