namespace DanganFurniture.Enums {
	public enum PrintModesEnum : int { JsonSerialized, LazyGodot }
	
	public enum FurnitureTypes : int {

		NULL1 = 0,	// only appears in DR2 in maps 101, 120, 142, 143, 160
		Person = 1,	// ID is index in the world
		ObjectsThatAppearBasedOnFlag = 2,	// Type is indexed object in world you click, used a lot in DR1
		ExitPointIn3D = 3,	// unused exists for toilets in bg_252
		Marker = 4,	// used in DR1 DEMO to stop how much you walk out if you
					// were to walk in the trial room, no purpose in other versions
		Bilboarded = 5,	// ID is index in the world, Unk1 seems to be type, used
						// for lights in DR1 and palm trees in DR2
		InteracteableDoor_Unk1 = 7,	// Type is indexed object in world you click,
									// used a lot in DR1, has a different
									// purpose in DR2 where they hold the string
		InteracteableDoors_Unk2 = 8,	// used a lot in DR1, has a different
										// purpose in DR2 where they hold the string
		Mask = 9,	// no clue, name taken from bg_100 in DR2

		// DR2 only
		WalkInTeleport = 22,	// only used once at the end of the chapter 6 corridor, could just be the sun
		Sun = 50,
		WarpAroundPointIn2D = 61,
		HowMuchToMoveInTwilight2D = 62,	// Size[0] is X pos, Size[1] is how long, needs to be rotated on the Y axis by 90deg
		HowMuchToMoveInTwilight2D_2 = 63,	// roughly the same thing?
		
		HiddenMonokuma = 70,	// by name, mostly 0 exceept a few
		PathForCamera = 80,	//eg bg_002, airport when you start pans front to back
		AmbientColor = 84,	// not overlay, mixes the colors from ID 1 with 2
							// and the resulting middle is the color, maybe
							// Size is used for how much
		Path = 90,	// Unk1 represents the ID of the path, unused one in bg_025
		NULL2 = 255,	//padding?

		// Speculative

		/*
		WallsAnimator? = 4,
		Unk6 = 6,	// maybe objects with animations, in DR2 it roughly matches
					// stuff like in the SAW room and Strawberry where your camera
					// changes for those scenes
		Unk10 = 10,	// only used once in DR1 in the map after celestia's trial
					// used in DR2 for some things
		
		// DR2 only
		Color_UNK11 = 11,
		
		Unk13 = 13,	// only uses ID, Rotation and Unk2
		Unk16 = 16,	// only uses ID and Unk1
		
		Color_UNK17 = 17,	// color transition when entering room?
		Color_UNK18 = 18,
		
		Unk19 = 19,	// position[x] is 0.5, ID is used for something
					// used in saw room and java military things
		Unk30 = 30,	// may be animations and how things move, 255 is -1, Size is
					// how position[y] and [z] change

		TopOverlayColor = 40,	// matches in bg_096 but not bg_906, see if 41 is Bottom Overlay Color or Camera Pos
		BottomOverlayColor = 41,

		CameraPos = 41,	// position sits roughly where the camera is in DR2's
						// bg_169, maybe 51 is what it points at?
		
		Unk51 = 51,	// uses ID and Unk1 for something, Unk1 may be Model Index
		Unk52 = 52,	// only appears to have values in bg_266 (makoto cihiro room) where the wireframe begins
		Unk53 = 53,	// only used in bg_266 (makoto cihiro room)
		Unk62 = 62,	// only appears in the Twilight Murder Mystery rooms
		Unk64 = 64,	// only used once in bg_905, is this why it crashed?
		Unk66 = 66,	// uses ID and Pos[x] for something
		Unk71 = 71,	// roughly in the position of hidden monokumas
		
		Unk82 = 82,	// Uses Unk2 for something
		
		Unk12 = 12, Unk14 = 14,  
		Unk54 = 54, Unk60 = 60, Unk61 = 61, Unk63 = 63,
		Unk67 = 67, Unk72 = 72, Unk73 = 73, Unk75 = 75,
		Unk76 = 76, Unk77 = 77, Unk78 = 78, Unk79 = 79, Unk81 = 81,
		Unk83 = 83, Unk85 = 85, Unk86 = 86,
		*/
	}
}