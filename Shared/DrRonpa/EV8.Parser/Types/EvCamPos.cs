// TODO: COPY PASTED FROM EVTMDL CAUSE I'M LAZY, FIX LATER
// TODO: Figure out how OBJTYP_CAMPOS is used in this

namespace EV8Reader.ObjectTypes {
	struct EvCamChunk {
		ObjTypeHeaderChunk Header;
		UDGPos PositionOfCameraRoot;
		// ... after some time
		ShortComment CamName2;
		// unknown 76 bytes after
	}
	
	struct EvCam {
		ObjTypeHeader Header;
		EvCamChunk[] Cams; // repeats `HowManyOfContent` times
	}
}