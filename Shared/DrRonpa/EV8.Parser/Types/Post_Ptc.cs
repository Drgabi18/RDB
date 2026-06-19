namespace EV8Reader.ObjectTypes {
	struct PostPtcChunk {
		ObjTypeHeaderChunk Header;
		UDGPos PositionOfParticle1;
		UDGPos PositionOfParticle2;
		// ... after some time
		ShortComment ParticleName2;
		// unknown 76 bytes after
	}
	
	struct PostPtc {
		ObjTypeHeader Header;
		PostPtcChunk[] Particles; // repeats `HowManyOfContent` times
	}
}