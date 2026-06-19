namespace EV8Reader.ObjectTypes {
	struct EvtPtcChunk {
		ObjTypeHeaderChunk Header;
		UDGPos PositionOfParticle;
		// ... after some time
		ShortComment ParticleName2;
		// unknown 76 bytes after
	}
	
	struct EvtPtc {
		ObjTypeHeader Header;
		EvtPtcChunk[] Particles; // repeats `HowManyOfContent` times
	}
}