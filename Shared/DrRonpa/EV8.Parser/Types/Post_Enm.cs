namespace EV8Reader.ObjectTypes {
	
	struct PostEnmChunk {
		ObjTypeHeaderChunk Header;
		UDGPos EnemyPos1;
		UDGPos EnemyPos2; // ?????
		// ... after some time
		ShortComment EnemyName2;
		// 13 8-byte bitmasks but only on the first enemy????
	}
	
	struct PostEnm {
		ObjTypeHeader Header;
		PostEnmChunk[] Enemys; // repeats `HowManyOfContent` times
	}
}