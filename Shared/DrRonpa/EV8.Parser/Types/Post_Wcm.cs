namespace EV8Reader.ObjectTypes {
	struct Unk_5Bytes { byte B1,B2,B3,B4,B5; }
	struct PostWcmChunk {
		ObjTypeHeaderChunk Header;
		UDGPos Wcms; // 11 floats?????
		// ... after some time
		ShortComment InteractableName;	
		// some unknowns
		Unk_5Bytes Unk0;
		int Unk1;
		short Unk2;
		float[] Unk3; // 8 floats
	}
	
	struct PostWcm {
		ObjTypeHeader Header;
		PostWcmChunk[] Particles; // repeats `HowManyOfContent` times
	}
}