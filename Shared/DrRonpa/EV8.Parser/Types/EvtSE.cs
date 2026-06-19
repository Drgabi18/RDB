namespace EV8Reader.ObjectTypes {
	struct EvtSeChunk {
		ObjTypeHeaderChunk Header;
		UDGPos PositionOfSound;
		// ... after some time
		ShortComment SoundName;
		// unknown 16 bytes
		ShortComment SoundNameInASCII;
		// unknown 96 bytes after
	}
	
	struct EvtSe {
		ObjTypeHeader Header;
		EvtSeChunk[] Sounds; // repeats `HowManyOfContent` times
	}
}