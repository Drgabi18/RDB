// example from e02_019_003.ev8
namespace EV8Reader.ObjectTypes {
	struct UDGColor { byte R, G, B, A; } // TODO: CHECK IF THIS IS REALLY RGBA

	// ???? ChunkHeader has a name for this, why the fuck would you name a color
	struct EvtColorChunk {
		ObjTypeHeaderChunk Header;
		int HowManyColors;
		UDGColor[] Colors; // is `HowManyColors` bytes long, is 12 UDGColor
	}

	// 188 bytes or 1564 bytes in one instance
	struct EvtColor{
		ObjTypeHeader Header;
		EvtColorChunk[] ColorChunks; // // repeats `HowManyOfContent` times
	}
}