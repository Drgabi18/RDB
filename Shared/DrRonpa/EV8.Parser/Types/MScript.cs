using EV8Reader.Headers;

/*
Code used for reading this found at 0x140074CC0

UDG_LoadLinFiles((uint)*(ushort *)(MScriptChunk + 0x50),(uint)*(ushort *)(MScriptChunk + 0x52),
				(uint)*(ushort *)(MScriptChunk + 0x54));
FUN_140074f20(*(undefined2 *)(MScriptChunk + 8),*(undefined2 *)(MScriptChunk + 10),
				*(undefined2 *)(MScriptChunk + 0x50),*(undefined2 *)(MScriptChunk + 0x52),
				*(undefined2 *)(MScriptChunk + 0x54));

Hex View  00 01 02 03 04 05 06 07  08 09 0A 0B 0C 0D 0E 0F
 
00002950                                       88 00 00 00              ....
00002960  01 00 00 00 10 00 00 00  00 00 00 00 00 00 01 C0  ................
00002970  88 00 00 00 88 00 00 00  14 03 03 C0 00 00 00 00  ................
00002980  03 00 00 00 00 00 00 00  50 00 00 00 50 00 00 00  ........P...P...
00002990  06 00 00 00 56 00 00 00  01 00 00 00 00 00 00 00  ....V...........
000029A0  3B 00 00 00 01 00 01 00  00 00 00 00 00 00 00 00  ;...............
000029B0  00 00 00 00 00 00 00 00  00 00 00 00 00 00 00 00  ................
000029C0  00 00 00 00 00 00 00 00  00 00 00 00 00 00 00 00  ................
000029D0  00 00 00 00 00 00 00 00  00 00 00 00 00 00 00 00  ................
000029E0  00 00 00 00 00 00 00 00  00 00 00 00 00 00 00 00  ................
000029F0  C8 00 01 00                                      ....

==== Header
0x0000295C - 0x88, how big the entire chunk is
0x00002960 - 0x01, how many object chunks there are
0x00002964 - 0x10, header size
==== Header End
==== Unknown
==== Unknown End
==== Struct Sizer 
0x00002980 - 0x03,	how many structs in header there are, followed by offset then
					size (0, 0x50 = 0x50 + 0x06 = 0x56)
0x00002998 - 0x01, end of header struct size?
==== Struct Sizer End
==== MScriptChunk
0x0000299C - Start of `MScriptChunk`
0x000029A0 - 0x3B,	sometimes matches the chunk header size left? is present at
					exactly number value after the header size, checked at 0x140074D6D
					on ENV_COLOR, it represents comment - 50
0x000029A4 - 0x01, Unknown, is `MScriptChunk + 8` from above
0x000029A6 - 0x01, Unknown, is `MScriptChunk + 10` from above
0x000029AC - 64 bytes ASCII/Shift-JIS string, not accounted by sturct above
0x000029EC - 6 shorts,	is LIN_Chapter (`MScriptChunk + 0x50`),
						LIN_Episode (`MScriptChunk + 0x52`) and
						LIN_SubEpisode (`MScriptChunk + 0x54`)
0x000029F2 - 0x0001, Unknown, is chcked at 0x140074D5E
==== MScriptChunk End
*/

namespace EV8Reader.ObjectTypes {
	struct MScriptChunk {
		ObjTypeHeaderChunk Header;
		// mostly just "CAS:" and a number, laughing at the ones that have "# copy"
		//string CommentAboutLIN; // present in header
		
		short LinChapter;
		short LinEpisode;
		short LinUnk1; // or SubEpisode
		// according to the header, this is 6 bytes, so what is this?
		short Unk2; // text in LIN? checking ChunkHeader, this seems to be 1 byte sized?
		
	}
	
	// function at 0x140074CC0
	// CAN ONLY HOLD 32 CASES BEFORE A NEW MSCRIPT IS NEEDED (e.g. test01.ev8)
	struct MScript {
		ObjTypeHeader Header;
		MScriptChunk[] Cases;
	}
}