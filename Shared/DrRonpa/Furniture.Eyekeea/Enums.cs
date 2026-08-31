namespace DanganFurniture.Enums {
	public enum GameID : int {
		DR1 = 1,
		DR2 = 2,
		DRV3 = 3
	}

	public enum PrintModesEnum { JsonSerialized, LazyGodot }
	
	// TODO: Would these work better as objects? For example, Type 1 is people
	// in the world and uses the Positions as position in the world, but Type 40
	// which sets the overlay color casts (char)(int)(float) to a byte array to
	// transform that into a value, should we show that or just leave a comment?
	public enum FurnitureTypes : int {
		// 1 - 10
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
		Interacteable_Unk1 = 7,	// Type is indexed object in world you click,
								// used a lot in DR1, has a different
								// purpose in DR2 where they hold the string
		Interacteable_Unk2 = 8,	// used a lot in DR1, has a different
								// purpose in DR2 where they hold the string
		DR1_UNK_DR2Mask = 9,	// no clue, name taken from bg_100 in DR2
		DR1_UNK_DR2SetBloom = 10,	// only used once in DR1 in the map after celestia's trial
									// used in DR2 for some things
		
		// ============================= DR2 only =============================
		STOP = 0,	// used in DR2 to stop reading the furniture early, like ResetScript()

		// 11 - 19
		UNK_SetColors = 11,	// seems to change the world only and not the skybox
		UNK_13 = 13,	// only uses ID, Rotation and Unk2		
		UNK_Lighting = 12,	// sets some stuff for the unused lighting system
		UNK_14 = 14, 
		UNK_Background = 16,	// only present in maps with skyboxes that follow you
		
		UNK_17 = 17,
		UNK_18 = 18,
		UNK_19 = 19,	// position[x] is 0.5, ID is used for something
		 				// used in saw room and java military things
		
		// 20 - 29
		WalkInTeleport = 22,	// only used once at the end of the chapter 6 corridor
								// sees if player is behind it (on all axis), after it gets ID,
								// it searches -20 bytes in memory behind itself?????

		// 30 - 39
		Fog = 30,	// position is color, size[1] is distance
		
		// 40 - 49
		OverlayTop = 40,	// overlay top color, Unk1 is Overlay type
		OverlayBottom = 41,	// overlay bottom color

		// 50 - 59
		Sun = 50,	// object read in the load map code
		UNK_51 = 51,	// uses ID and Unk1 for something, Unk1 may be Model Index
		UNK_52 = 52,	// object read in the load map code,
						// get read next to overlay colors
						// only appears to have values in bg_266 (makoto cihiro room) where the wireframe begins
		UNK_53 = 53,	// object read in the load map code,
						// get read next to overlay colors
						// only used in bg_266 (makoto cihiro room)
		UNK_54 = 54,	// object read in the load map code,
						// supposed to change lens flare type, is broken, makes sun visible through absolutley everything

		// 60 - 69 hehe
		UNK_60 = 60,	// object read in the load map code
		WarpAroundPointIn2D = 61,
		HowMuchToMoveInTwilight2D = 62,	// Size[0] is X pos, Size[1] is how long, needs to be rotated on the Y axis by 90deg
		HowMuchToMoveInTwilight2D_2 = 63,	// roughly the same thing?
		UNUSED_UNK_64 = 64,	// only used once in bg_905, not related to the crash
		UNK_66 = 66,	// uses ID and Pos[x] for something
		UNK_67 = 67,

		// 70 - 79
		HiddenMonokuma = 70,	// object name contains monokuma, may set the id in the world for him
		UNK_71 = 71,
		UNK_72 = 72,	// ???? get model to render when climbing up floors, may set which objects are visible in multi floor maps
		UNK_73 = 73,	// related to camerea zooming to object to talk
		UNK_75 = 75,	// ???? could be related to the camera when you first visit the hotel and after the start of investigation in chapter 1
		ChangeFov = 76,	// change fov, code at 0x0056cc84, Pos[0] is FOV
		UNK_77 = 77,
		LockPerspectiveHorizontally = 78,	// lock perspective to only look left and right
		LockPerspectiveVertically = 79,	// gee i wonder which map uses this

		// 80 - 89
		PathForCameraWhenEnteringRoom = 80,	// eg bg_002, airport when you start pans front to back
		UNK_81 = 81,
		UNK_82 = 82,	// Uses Unk2 for something
		CameraModeChange = 83,	//camera mode change in strawberry and grape house when you inspect the park and lounge
		UNUSED_DissapearingBlockEffects = 84,	// only used in bg_906
		UNK_85 = 85,
		UNK_86 = 86,

		// 90 - 255
		PathWhenWalkingInRoomsWithFloors = 90,	// Unk1 represents the ID of the path, unused one in bg_025
		SANITY_CHECK = 255,	// DR2 starts reading the furniture only if this object exists
	}

	// TODO: These names are not yet corelated to the Object Names left in all files,
	// these could be all resolved next commit
	public enum FurnitureTypesV3 : int {
		SANITY_CHECK = 0,	// present in every map, only Unk4 change from time to time
		UNK_2 = 2,	// present in every map, only Unk2 and Unk4 change from time to time
		UNK_3 = 3,
		PersonInTrial = 4,	// as in chracter class object in trial, not the real life equivalent of this description
		UNK_5 = 5,	// seems related to UNK_7
		ExitPointIn3D_UNK_6 = 6,	// but not on all maps? some use 80
		UNK_7 = 7,	// seems related to UNK_5
		UNK_8 = 8,
		Person = 9,	// matches positions in ID999_dummy
		UNK_10 = 10,	// person modifier, seems to be related to person as it follows them and their id in ID154_lab_iruma
		UNK_11 = 11,	// person modifier, connected to UNK_10 in ID031_classRoom_C
		BilboardedObject_UNK_12 = 12,	// is used for the boulders you destroy too, can have colission
		ClickOrWalkableToChangeMap_UNK_13 = 13,
		UNK_14 = 14,
		UNK_15 = 15,
		UNK_16 = 16,
		Models = 17,	// match hidden monokumas in shuichi's room but also random models in the world
		UNK_18 = 18,
		UNK_19 = 19,
		UNK_20 = 20,
		UNK_21 = 21,
		UNK_22 = 22,
		UNK_23 = 23,
		UNK_24 = 24,
		UNK_25 = 25,
		UNK_26 = 26,
		UNK_27 = 27,
		UNK_28 = 28,
		UNK_29 = 29,
		UNK_30 = 30,
		UNK_31 = 31,
		UNK_32 = 32,
		UNK_33 = 33,
		UNK_34 = 34,
		Interacteable = 35,	// based on ID000_dummy
		UNK_36 = 36,
		UNK_37 = 37,
		UNK_38 = 38,
		UNK_39 = 39,
		UNK_40 = 40,
		UNK_41 = 41,
		UNK_42 = 42,
		UNK_43 = 43,
		UNK_44 = 44,
		UNK_45 = 45,
		UNK_46 = 46,
		UNK_47 = 47,
		UNK_48 = 48,
		UNK_49 = 49,
		UNK_50 = 50,
		UNK_51 = 51,
		UNK_52 = 52,
		UNK_53 = 53,
		UNK_54 = 54,
		UNK_55 = 55,
		TrialPod_UNK_56 = 56,
		UNK_57 = 57,
		UNK_58 = 58,
		UNK_59 = 59,
		UNK_60 = 60,
		UNK_61 = 61,
		UNK_62 = 62,
		UNK_63 = 63,
		UNK_64 = 64,
		UNK_65 = 65,
		UNK_66 = 66,
		UNK_67 = 67,
		UNK_68 = 68,	// only present in ID132_lab_hoshi
		UNK_69 = 69,

		// 71 - 74
		// only used in ID007_gym
		Gym_UNK_71 = 71,
		Gym_UNK_72 = 72,
		Gym_UNK_73 = 73,
		Gym_UNK_74 = 74,

		UNK_75 = 75,
		UNK_76 = 76,
		UNK_77 = 77,
		UNK_78 = 78,
		UNK_79 = 79,
		ExitPointIn3D_UNK_80 = 80,	// but not on all maps?
		UNK_81 = 81,
		UNK_82 = 82,
		UNK_83 = 83,
		UNK_84 = 84,
		UNK_85 = 85,
		UNK_86 = 86,
		UNK_87 = 87,
		UNK_88 = 88,
		UNK_89 = 89,
		UNK_90 = 90,
		UNK_91 = 91,
		UNK_92 = 92,
		UNK_93 = 93,
		UNK_94 = 94,
		UNK_95 = 95,
		UNK_96 = 96,
		UNK_97 = 97,
		UNK_98 = 98,
		UNK_99 = 99,
		UNK_100 = 100,
		UNK_101 = 101,
		UNK_102 = 102,
		UNK_103 = 103,
		UNK_104 = 104,
		UNK_105 = 105,
		UNK_106 = 106,
		UNK_107 = 107,
		UNK_108 = 108,
		UNK_109 = 109,
		UNK_110 = 110,
		UNK_111 = 111,
		UNK_112 = 112,
		UNK_113 = 113,
		UNK_114 = 114,
		UNK_115 = 115,
		UNK_116 = 116,
		UNK_117 = 117,
		UNK_118 = 118,

		// 135 - 145
		// only used in the map with the flashback light creator
		FlashbackLightSystem_UNK_135 = 135,
		FlashbackLightSystem_UNK_136 = 136,
		FlashbackLightSystem_UNK_137 = 137,
		FlashbackLightSystem_UNK_138 = 138,
		FlashbackLightSystem_UNK_139 = 139,
		FlashbackLightSystem_UNK_140 = 140,
		FlashbackLightSystem_UNK_141 = 141,
		FlashbackLightSystem_UNK_142 = 142,
		FlashbackLightSystem_UNK_143 = 143,
		FlashbackLightSystem_UNK_144 = 144,
		FlashbackLightSystem_UNK_145 = 145
	}
}