// reimplementation of CEiDsoMap
namespace EV8Reader.ObjectTypes {
	struct DsoMapChunk {
		ObjTypeHeaderChunk Header;
		/* 0x80 */ ShortComment MapName;
		int Unk1;
	}

	struct DsoMap {
		ObjTypeHeader Header;
		// repeats `HowManyOfContent`, although this doesn't make sense, the
		// only time multilpe map objects are "used" is in test51.ev8
		DsoMapChunk[] Maps;
	}
}