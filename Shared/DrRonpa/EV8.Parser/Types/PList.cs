namespace EV8Reader.ObjectTypes {
	// they could never get me to get rid of you PeePeePos
	struct PeePeePos { UDGPos PosRot; } 

	struct PListChunk {
		ObjTypeHeaderChunk Header;
		PeePeePos[] LocationForGenerators;
		// random number and then the same empty space like mdl, dsochr, particle etc.
	}

	struct PList {
		ObjTypeHeader Header;
		PListChunk[] Cases;
	}
}