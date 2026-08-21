namespace DanganFurniture.Enums {
	public enum PrintModesEnum : int { JsonSerialized, LazyGodot }
	
	// TODO: Would these work better as objects? For example, Type 1 is people
	// in the world and uses the Positions as position in the world, but Type 40
	// which sets the overlay color casts (char)(int)(float) to a byte array to
	// transform that into a value, should we show that or just leave a comment?
	public enum FurnitureTypes : int {
		Person = 1,	// ID is index in the world, Size[1] is unused
		ObjectsThatAppearBasedOnFlag = 2,	// Type is indexed object in world you click, used a lot in DR1
		ExitPointIn3D = 3,	// unused exists for toilets in bg_252
		Marker = 4,	// used in DR1 DEMO to change those unknown -5000 to 5000 values
					// potentially limit how much you cal walk if you had the posibility
					// to walk in the trial room, no purpose in other versions
					// still present only in trial maps even in DR2
		Bilboarded = 5,	// ID is index in the world, Unk1 seems to be type, used
						// for lights in DR1 and palm trees in DR2
		DR1_UNK_DR2CameraLimit = 6,	// limit how much you can look around,
									// Pos[0] DR_X_CameraCenter, Pos[1] DR_Y_CameraCenter, Pos[2] DR_Z_CameraCenter
									// Size[0] is DR_DistanceFromCenterPoint_RoomMode, UNK2 IS FLOAT HERE, Size[1] unused
		Interacteable_UNK1 = 7,	// Type is indexed object in world you click,
								// used a lot in DR1, has a different
								// purpose in DR2 where they hold the string
		Interacteable_Unk2 = 8,	// used a lot in DR1, has a different
								// purpose in DR2 where they hold the string
		DR1_UNK_DR2Mask = 9,	// no clue, name taken from bg_100 in DR2
		DR1_UNK_DR2SetBloom = 10,	// only used once in DR1 in the map after celestia's trial
					// used in DR2 for some things
		
		// ============================= DR2 only =============================
		STOP = 0,	// used in DR2 to stop reading the furniture early
		UNK_SetColors = 11,	// seems to change the world only and not the skybox
		UNK_Lighting = 12,	// sets some stuff for the unused lighting system
		UNK_Background = 16,	// only present in maps with skyboxes that follow you
		WalkInTeleport = 22,	// only used once at the end of the chapter 6 corridor
								// sees if player is behind it (on all axis), after it gets ID,
								// it searches -20 bytes in memory behind itself?????
		Fog = 30,	// position is color, size[1] is distance
		OverlayTop = 40,	// overlay top color, Unk1 is Overlay type
		OverlayBottom = 41,	// overlay bottom color
		Sun = 50,	// object read in the load map code
		UNK_52 = 52,	// object read in the load map code,
						// get read next to overlay colors
						// only appears to have values in bg_266 (makoto cihiro room) where the wireframe begins
		UNK_53 = 53,	// object read in the load map code,
						// get read next to overlay colors
						// only used in bg_266 (makoto cihiro room)
		UNK_54 = 54,	// object read in the load map code,
						// supposed to change lens flare type, is broken, makes sun visible through absolutley everything
		UNK_60 = 60,	// object read in the load map code
		WarpAroundPointIn2D = 61,
		HowMuchToMoveInTwilight2D = 62,	// Size[0] is X pos, Size[1] is how long, needs to be rotated on the Y axis by 90deg
		HowMuchToMoveInTwilight2D_2 = 63,	// roughly the same thing?
		UNUSED_UNK_64 = 64,	// only used once in bg_905, not related to the crash
		HiddenMonokuma = 70,	// object name contains monokuma, may set the id in the world for him
		UNK_72 = 72,	// ???? get model to render when climbing up floors, may set which objects are visible in multi floor maps
		UNK_73 = 73,	// related to camerea zooming to object to talk
		UNK_75 = 75,	// ???? could be related to the camera when you first visit the hotel and after the start of investigation in chapter 1
		ChangeFov = 76,	// change fov, code at 0x0056cc84, Pos[0] is FOV
		LockPerspectiveHorizontally = 78,	// lock perspective to only look left and right
		LockPerspectiveVertically = 79,	// gee i wonder which map uses this
		PathForCameraWhenEnteringRoom = 80,	// eg bg_002, airport when you start pans front to back
		CameraModeChange = 83,	//camera mode change in strawberry and grape house when you inspect the park and lounge
		UNUSED_DissapearingBlockEffects = 84,	// only used in bg_906
		PathWhenWalkingInRoomsWithFloors = 90,	// Unk1 represents the ID of the path, unused one in bg_025
		SANITY_CHECK = 255,	// DR2 starts reading the furniture only if this object exists

		/*

		// ========== Speculative ==========
		// TODO: I deleted some of the UNK from here without documenting, re-add them back later
		UNK_11 = 11,
		UNK_13 = 13,	// only uses ID, Rotation and Unk2		
		UNK_14 = 14, 
		UNK_19 = 19,	// position[x] is 0.5, ID is used for something
						// used in saw room and java military things
		UNK_51 = 51,	// uses ID and Unk1 for something, Unk1 may be Model Index
		UNK_66 = 66,	// uses ID and Pos[x] for something
		UNK_67 = 67,
		UNK_77 = 77,
		UNK_81 = 81,
		UNK_82 = 82,	// Uses Unk2 for something
		UNK_85 = 85,
		UNK_86 = 86,

		*/
	}
}