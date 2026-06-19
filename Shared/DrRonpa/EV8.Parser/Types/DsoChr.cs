namespace EV8Reader.ObjectTypes {
	struct DsoCharChunk {
		ObjTypeHeaderChunk Header;
		UDGPos PositionOfCharacter;
		// ...
		/* 0xA3C */ ShortComment CharacterName; // only appears on the first chunk
		/* 0xA3C */ ShortComment CharacterType; // or animation?
		// ... 5 other uknown ints?
	}
	struct DsoChar {
		ObjTypeHeader Header;
		DsoCharChunk[] Characters; // repeats `HowManyOfContent` times, nonsensical
	}
}