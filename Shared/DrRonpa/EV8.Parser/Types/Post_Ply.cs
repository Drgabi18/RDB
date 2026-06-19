namespace EV8Reader.ObjectTypes {
	struct PostPlyChunk {
		ObjTypeHeaderChunk Header;
		UDGPos PlayerLocation1;
		UDGPos PlayerLocation2;
		// ... after some time
		ShortComment PlayerName;
		// unknown 12 bytes
	}
	
	struct PostPly {
		ObjTypeHeader Header;
		PostPlyChunk[] Players; // repeats `HowManyOfContent` times
	}
}