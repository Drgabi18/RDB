namespace EV8Reader.ObjectTypes {
	struct MScriptChunk {
		ObjTypeHeaderChunk Header;
		// mostly just "CAS:" and a number, laughing at the ones that have "# copy"
		//string CommentAboutLIN; // present in header
		
		short LinChapter;
		short LinEpisode;
		short LinUnk1; // or SubEpisode
		// according to the header, this is 6 bytes, so what is this?
		short LinUnk2; // text in LIN? checking ChunkHeader, this seems to be 1 byte sized?
		
	}
	
	// function at 0x140074CC0
	// CAN ONLY HOLD 32 CASES BEFORE A NEW MSCRIPT IS NEEDED (e.g. test01.ev8)
	struct MScript {
		ObjTypeHeader Header;
		MScriptChunk[] Cases;
	}
}