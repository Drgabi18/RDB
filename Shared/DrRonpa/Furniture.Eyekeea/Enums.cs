namespace DanganFurniture.Enums {
	public enum PrintModesEnum : int { JsonSerialized, LazyGodot }
	
	public enum FurnitureTypes : int {

		NULL1 = 0,	// only appears in DR2 in maps 101, 120, 142, 143, 160
		Person = 1,	// ID is index in the world
		ObjectsThatAppearBasedOnFlag = 2,	// Type is indexed object in world you click, used a lot in DR1
		ExitPointIn3D = 3,
		Marker = 4,	// used in DR1 DEMO to stop how much you walk out if you
					// were to walk in the trial room, no purpose in other versions
		Bilboarded = 5,	// ID is index in the world, Unk1 seems to be type, used
						// for lights in DR1 and palm trees in DR2
		InteracteableDoor_Unk1 = 7,	// Type is indexed object in world you click,
									// used a lot in DR1
		InteracteableDoors_Unk2 = 8,	// used a lot in DR1
		Mask = 9,	// no clue, name taken from bg_100 in DR2

		// DR2 only
		Sun = 50,
		HiddenMonokuma = 70,
		Unk71 = 71,
		AmbientColor = 84,	// not overlay, mixes the colors from ID 1 with 2
							// and the resulting middle is the color, maybe
							// Size is used for how much
		Path = 90,	// Unk1 represents the ID of the path, unused one in bg_025
		NULL2 = 255,	//padding?

		// Speculative

		/*
		WallsAnimator? = 4,
		ObjectWithAnimationWhenEnterirng = 6,
		Unk10 = 10,	// only used once in DR1 in the map after celestia's trialm
					// used in DR2 for some things
		Color_UNK1 = 11,
		Color_UNK2 = 17,
		WalkInTeleport = 22,
		Color_UNK3 = 30,	// uses Size for something
		CameraPos = 41,	// position sits roughly where the camera is in DR2's
						// bg_169, maybe 51 is what it points at?
		WorldBorder = 61,	// DR2 only
		WorldMesh = 80
		Unk82 = 82,	// Uses Unk2 for something
		
		Unk8 = 8, Unk12 = 12, Unk13 = 13,
		Unk14 = 14, Unk16 = 16, Unk18 = 18, Unk19 = 19,
		Unk40 = 40, Unk51 = 51, Unk52 = 52, Unk53 = 53,
		Unk54 = 54, Unk60 = 60, Unk61 = 61, Unk62 = 62, Unk63 = 63, Unk64 = 64,
		Unk66 = 66, Unk67 = 67, Unk72 = 72, Unk73 = 73, Unk75 = 75,
		Unk76 = 76, Unk77 = 77, Unk78 = 78, Unk79 = 79, Unk81 = 81,
		Unk83 = 83, Unk85 = 85, Unk86 = 86,
		*/
	}
}