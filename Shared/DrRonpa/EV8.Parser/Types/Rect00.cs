namespace EV8Reader.ObjectTypes {
	// tested using e99_000_001.ev8, test06.ev8 seems to have this broken
	struct Rect00Chunk {
		ObjTypeHeaderChunk Header;
		UDGPos BottomLeftCorner; // or not? check again
		UDGPos TopRightCorner;
		// nothing for a few bytes, then
		int Unk1;
		int Unk2;
		// and then nothing after
	}

	struct Rect00Obj {
		ObjTypeHeader Header;
		Rect00Chunk[] Triggers; // repeats `HowManyOfContent` times
	}
}