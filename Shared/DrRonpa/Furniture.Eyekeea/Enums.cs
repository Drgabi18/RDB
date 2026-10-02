namespace DanganFurniture.Enums {
	public enum GameID : int {
		DR1 = 1,
		DR2 = 2,
		DRV3 = 3
	}

	public enum PrintModes {
		JsonSerialized,
		LazyGodot,
		V3_KV_TypeNames		
	}
	
	// TODO: Would these work better as objects? For example, Type 1 is people
	// in the world and uses the Positions as position in the world, but Type 40
	// which sets the overlay color casts (char)(int)(float) to a byte array to
	// transform that into a value, should we show that or just leave a comment?
	public enum FurnitureTypes : int {
		// 1 - 10
		Person = 1,	// ID is index in the world, Size[1] is unused
		ObjectsThatAppearBasedOnFlag = 2,	// Type is indexed object in world you click, used a lot in DR1
		ExitPointIn3D = 3,	// unused exists for toilets in bg_252
		UNUSED_Marker = 4,	// used in DR1 DEMO to change those unknown -5000 to 5000 values
					// potentially limit how much you cal walk if you had the posibility
					// to walk in the trial room, no purpose in other versions
					// still present only in trial maps even in DR2
		Bilboarded = 5,	// ID is index in the world, Unk1 seems to be type, used
						// for lights in DR1 and palm trees in DR2
		DR1_UNK_DR2_CameraLimit = 6,	// limit how much you can look around,
									// Pos[0] DR_X_CameraCenter, Pos[1] DR_Y_CameraCenter, Pos[2] DR_Z_CameraCenter
									// Size[0] is DR_DistanceFromCenterPoint_RoomMode, UNK2 IS FLOAT HERE, Size[1] unused
		Interacteable_UNK1 = 7,	// Type is indexed object in world you click,
								// used a lot in DR1, has a different
								// purpose in DR2 where they hold the string
		Interacteable_UNK2 = 8,	// used a lot in DR1, has a different
								// purpose in DR2 where they hold the string
		DR1_UNK9_DR2_Mask = 9,	// no clue, name taken from bg_100 in DR2
		DR1_ScaleHack_DR2_SetBloom = 10,	// only used once in DR1 in the map after celestia's trial to fix columns
											// used in DR2 to set bloom
		
		// ============================= DR2 only =============================
		STOP = 0,	// used in DR2 to stop reading the furniture early, like ResetScript()

		// 10 - 21
		// inside the code at 0x004cd200 and 0x004ca580 all he functions are 
		// grouped together, as such, these will specially also be grouped
		UNK_SetColors = 11,	// seems to change the world only and not the skybox
		UNK_Lighting = 12,	// sets some stuff for the unused lighting system
		UNK_13 = 13,	// only uses ID, Rotation and Unk2		
		UNK_14 = 14,
		UNUSED_15 = 15,	// only present in code at 0x004ca768, has the same code as Type 14
		UNK_Background = 16,	// only present in maps with skyboxes that follow you
		
		UNK_17 = 17,
		UNK_18 = 18,
		UNUSED_UNK_19 = 19,	// present in code but the function it calls is empty
							// only present in java and saw rooms
							// code present only on the PSP version, is related to the unused light system
		UNUSED_20 = 20,	// only present in code at 0x004ca88d, calls empty functions, returns 1 elsewhere
						// code present only on the PSP version, is related to the unused light system
		UNUSED_21 = 21,	// only present in code at 0x004ca88d, calls empty functions, returns 1 elsewhere
						// code present only on the PSP version, is related to the unused light system
		
		// 22 - 29
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

	/*
		Types in this game don't matter that much, as the index is actually used
		to determine what he object does, as such, beaside the values only used once,
		it's impossible  to tell what the other did without looking at the code.
		Below is a table that asigns the names to the ints and also a table with DeepL translations.
		The enum below these lists is what I bothered to match.

		{
		"操作": [ 0 ],
		"キャラ表現": [ 2, 0 ],
		"カメラ上下角度制限": [ 3 ],
		"画角": [ 4 ],
		"開始位置": [ 5, 6, 2 ],
		"注視点座標": [ 7 ],
		"キャラ": [ 9, 24, 19, 16, 13, 18, 10, 12, 8, 11, 22, 14, 23, 28, 26, 15, 51, 36, 29, 17, 4 ],
		"オブジェクト": [ 35, 29, 41, 21, 23, 43, 17, 55, 37, 11, 32, 15, 19, 31, 18, 14, 9, 24, 30, 28, 20, 42, 16, 12, 27, 25, 22, 80, 39, 99, 34, 13, 33, 26, 76 ],
		"影": [ 46, 40, 50, 24, 27, 49, 71, 60, 96, 14, 80, 42, 89, 95, 104, 37, 93, 32, 54, 36, 38, 10, 136, 35, 31, 53, 75, 23, 26, 57, 41, 48, 47, 90, 62, 102, 52, 63, 39, 103, 85, 81, 19, 51, 45, 61, 110, 78, 98, 86, 92, 33, 59, 56, 114, 106, 116, 107, 108, 58, 16, 28, 44, 22, 91, 87, 88 ],
		"環境光": [ 48, 41, 51, 26, 28, 50, 25, 72, 62, 97, 16, 81, 43, 90, 96, 105, 38, 94, 33, 55, 37, 39, 12, 137, 36, 32, 54, 76, 24, 27, 58, 42, 49, 91, 63, 103, 53, 64, 40, 104, 86, 82, 21, 52, 46, 47, 111, 98, 79, 100, 87, 93, 34, 60, 57, 115, 107, 117, 108, 61, 109, 59, 17, 30, 45, 92, 88, 89 ],
		"平行光源": [ 49, 42, 52, 27, 29, 51, 26, 73, 63, 98, 17, 82, 44, 91, 97, 106, 39, 95, 34, 56, 38, 40, 13, 138, 37, 33, 55, 77, 25, 28, 59, 43, 50, 92, 64, 104, 54, 65, 41, 105, 87, 83, 22, 53, 47, 48, 112, 99, 80, 101, 88, 94, 35, 61, 58, 116, 108, 118, 109, 62, 110, 60, 18, 45, 66, 31, 46, 93, 89, 90 ],
		"移動速度": [ 5 ],
		"キャラカラー": [ 25, 14, 11, 9, 10, 12, 13, 15, 24, 29, 27, 19, 30 ],
		"エリア判定": [ 30, 42, 20, 24, 33, 32, 29, 17, 22, 31, 36, 19, 41, 13, 93, 43, 112, 47, 104, 52, 14, 28, 18, 89, 85, 48 ],
		"背景オブジェクト": [ 32, 47, 25, 23, 69, 95, 41, 103, 92, 50, 35, 37, 135, 34, 30, 33, 45, 61, 59, 102, 27, 84, 49, 43, 60, 76, 96, 58, 57, 56, 52, 40, 44, 46, 36, 31, 89 ],
		"オブジェクトカメラ": [ 33, 44, 18, 36, 14, 17, 98, 27, 19, 34, 31, 22, 37, 32, 21, 26, 48, 62, 29, 50, 20, 95, 77, 97, 85, 91, 57, 30, 55, 49, 51, 47, 38 ],
		"ブルーム": [ 26, 25, 23, 35, 29 ],
		"タッチフィルター": [ 74, 64, 99, 18, 83, 45, 92, 98, 107, 40, 96, 35, 57, 39, 41, 139, 34, 27, 56, 78, 93, 65, 105, 43, 55, 66, 42, 106, 88, 84, 23, 54, 48, 49, 113, 100, 81, 102, 89, 95, 36, 62, 52, 59, 51, 44 ],
		"観察眼：環境光": [ 75, 65, 100, 84, 46, 36, 35, 66, 67, 43, 107, 24, 55, 49, 50, 101, 82, 103, 90, 96, 37, 63, 53, 60, 56, 52, 45 ],
		"観察眼：点光源": [ 76, 66, 101, 47, 37, 36, 67, 68, 44, 108, 25, 56, 50, 51, 102, 83, 104, 91, 97, 38, 64, 54, 61, 57, 53, 46 ],
		"レンズフレア": [ 77, 92, 98, 117 ],
		"透過オブジェクト": [ 93, 99, 104, 145, 56, 90, 65, 94 ],
		"影設定：PS4": [ 99, 140, 79, 100, 89, 85, 26, 52, 114, 103, 105, 98, 104, 57, 58 ],
		"影設定：Vita": [ 100, 141, 80, 101, 90, 86, 27, 53, 115, 104, 106, 99, 105, 58, 59 ],
		"影マップ0：PS4": [ 101, 142, 81, 102, 91, 87, 28, 54, 116, 105, 107, 100, 106, 59, 60 ],
		"影マップ1：PS4": [ 102, 143, 82, 103, 92, 88, 29, 55, 117, 106, 108, 101, 107, 60, 61 ],
		"影マップ0：Vita": [ 103, 144, 83, 104, 93, 89, 30, 56, 118, 107, 109, 102, 108, 61, 62 ],
		"オブジェクト（未使用）": [ 53 ],
		"出現カメラアニメ": [ 9, 11, 12, 8 ],
		"キャラルート": [ 46, 44, 60, 48, 53, 111, 16 ],
		"6章砂塵": [ 61, 44, 51, 35, 50, 38 ],
		"6章砂塵：上エフェクト非表示": [ 62, 46, 52, 37 ],
		"6章砂塵：下エフェクト非表示": [ 63, 45, 53, 36, 39 ],
		"水面1：設定1": [ 93, 99, 19 ],
		"水面2：設定2": [ 94, 100, 20 ],
		"水面3：法線マップ1": [ 95, 101, 21 ],
		"水面4：法線マップ2": [ 96, 102, 22 ],
		"水面5：色": [ 97, 103, 23 ],
		"水面Windows1：設定1": [ 103, 109 ],
		"水面Windows2：設定2": [ 104, 110 ],
		"水面Windows3：法線マップ1": [ 105, 111 ],
		"水面Windows4：法線マップ2": [ 106, 112 ],
		"水面Windows5：色": [ 107, 113 ],
		"水面Windows6：水面板": [ 108, 114 ],
		"水面Vita1：設定1": [ 24 ],
		"水面Vita2：設定2": [ 25 ],
		"水面Vita3：法線マップ1": [ 26 ],
		"水面Vita4：法線マップ2": [ 27 ],
		"水面Vita5：色": [ 28 ],
		"水面6：鏡面方向Windows": [ 30 ]
		}

		操作                       “Controls”
		キャラ表現                 “Character Rendering”
		カメラ上下角度制限         “Camera Up/Down Angle Limits”
		画角                       “Field of View”
		開始位置                   “Starting Position”
		注視点座標                 “Viewpoint Coordinates”
		キャラ                     ‘Character’
		オブジェクト               “Object”
		影                         “Shadows”
		環境光                     “Ambient Light”
		平行光源                   “Parallel Light Source”
		移動速度                   “Movement Speed”
		キャラカラー               “Character Color”
		エリア判定                 “Collision Detection”
		背景オブジェクト           “Background Object”
		オブジェクトカメラ         “Object Camera”
		ブルーム                   “Bloom”
		タッチフィルター           “Touch Filter”
		観察眼：環境光             “Observation: Ambient Light”
		観察眼：点光源             “Observation: Point Light”
		レンズフレア               “Lens Flare”
		透過オブジェクト           “Transparent Objects”
		影設定：PS4                “Shadow Settings: PS4”
		影設定：Vita               “Shadow Settings: Vita”
		影マップ0：PS4             “Shadow Map 0: PS4”
		影マップ1：PS4             “Shadow Map 1: PS4”
		影マップ0：Vita            “Shadow Map 0: Vita”
		オブジェクト（未使用）     “Objects (Unused)”
		出現カメラアニメ           “Appearance Camera Animation”
		キャラルート               “Character Route”
		6章砂塵                    “Chapter 6: Sandstorm”
		6章砂塵：上エフェクト非表示  “Chapter 6: Sandstorm: Hide Top Effects”
		6章砂塵：下エフェクト非表示  “Chapter 6: Sandstorm: Hide Bottom Effects”
		水面1：設定1                “Water Surface 1: Setting 1”
		水面2：設定2                “Water Surface 2: Setting 2”
		水面3：法線マップ1          “Water Surface 3: Normal Map 1”
		水面4：法線マップ2          “Water Surface 4: Normal Map 2”
		水面5：色                   “Water Surface 5: Color”
		水面Windows1：設定1         “Water Surface Windows 1: Setting 1”
		水面Windows2：設定2         “Water Surface Windows 2: Setting 2”
		水面Windows3：法線マップ1   “Water Surface Windows 3: Normal Map 1”
		水面Windows4：法線マップ2   “Water Surface Windows 4: Normal Map 2”
		水面Windows5：色            “Water Surface Windows 5: Color”
		水面Windows6：水面板        “Water Surface Windows 6: Water Panel”
		水面Vita1：設定1            “Water Surface Vita 1: Setting 1”
		水面Vita2：設定2            "Water Surface Vita 2: Setting
		水面Vita3：法線マップ1      “Water Surface Vita3: Normal Map 1”
		水面Vita4：法線マップ2      “Water Surface Vita4: Normal Map 2”
		水面Vita5：色               “Water Surface Vita5: Color”
		水面6：鏡面方向Windows      “Water Surface 6: Specular Direction (Windows)”


	*/
	public enum FurnitureTypesV3 : int {
		// 0 - 9
		LoveSosa = 0,	// called "操作" intenrally, present in every map, Unk4 changes between first person and locked
		CharacterPortreyal = 2,	// supposedly called "キャラ表現", present in every map, only Unk2 and Unk4 change from time to time
		LookUpDownAngleLimit = 3,
		FOV_or_PersonInTrial_UNK_4 = 4,	// Type 4 with ID 1 is used for the FOV, Type 4 with any other ID is used to
										// set the camera in trial as in chracter class object in trial, not the real
										// life equivalent of this description
		MovementSpeed_UNK_5 = 5,	// seems related to UNK_7
		ExitPointIn3D_UNK_6 = 6,	// but not on all maps? some use 80
		PointOfFocusCoordinates = 7,	// seems related to UNK_5
		UNK_8 = 8,
		Person_UNK_9 = 9,	// matches positions in ID999_dummy and ID000_dummy
		
		// 10 - 19
		Bilboard_Person_UNK_10 = 10,	// matches positions in ID007_gym, person modifier in other places?
										// seems to be related to person as it follows them and their id in ID154_lab_iruma
		Person_UNK_11 = 11,	// person modifier, probably color modifier, connected to UNK_10 in ID031_classRoom_C
		BilboardedObject_UNK_12 = 12,	// is used for the boulders you destroy too, can have colission
		ClickOrWalkableToChangeMap_UNK_13 = 13,
		UNK_14 = 14,
		UNK_15 = 15,
		UNK_16 = 16,
		Models = 17,	// match hidden monokumas in shuichi's room but also random models in the world
		UNK_18 = 18,
		UNK_19 = 19,

		// 20 - 29
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

		// 30 - 39
		UNK_30 = 30,
		UNK_31 = 31,
		UNK_32 = 32,
		UNK_33 = 33,
		UNK_34 = 34,
		Object = 35,	// based on ID000_dummy
		UNK_36 = 36,
		UNK_37 = 37,
		UNK_38 = 38,
		UNK_39 = 39,

		// 40 - 49
		UNK_40 = 40,
		UNK_41 = 41,
		UNK_42 = 42,
		UNK_43 = 43,
		UNK_44 = 44,
		UNK_45 = 45,
		Shadow = 46,
		UNK_47 = 47,
		AmbientLighting = 48,
		ParallelLightSource = 49,	// name taken straight from ID000_dummy

		// 50 - 59
		UNK_50 = 50,
		UNK_51 = 51,
		UNK_52 = 52,
		UnusedObjects = 53,	// yes, objects categorized as unused by the game, but not an unused type in game per se
		UNK_54 = 54,
		UNK_55 = 55,	// objects again?
		TrialPod_UNK_56 = 56,
		UNK_57 = 57,
		UNK_58 = 58,
		UNK_59 = 59,

		// 60 - 61
		UNK_60 = 60,
		UNK_61 = 61,
		
		// 62 - 68
		// seem to be connected?
		ObjectCamera_UNK_62 = 62,
		Shadow_UNK_63 = 63,
		AmbientLight_UNK_64 = 64,
		ParallelLightSource_UNK_65 = 65,
		TouchFilter_UNK_66 = 66,
		ObservationEye_AmbientLight_UNK_67 = 67,
		Hoshi_ObservationEye_PointLightSource_UNK_68 = 68,	// only present in ID132_lab_hoshi
		
		UNK_69 = 69,	// hehe, Background Objects?

		// 71 - 74
		// only used in ID007_gym
		Gym_Shadow = 71,
		Gym_AmbientLight = 72,
		Gym_ParallelLightSource = 73,
		Gym_TouchFilter = 74,

		// 75 - 79
		ObservationEye_AmbientLight = 75,
		ObservationEye_PointLightSource = 76,
		LensFlare = 77,
		UNK_78 = 78,
		UNK_79 = 79,

		// 80 - 84
		ExitPointIn3D_UNK_80 = 80,	// but not on all maps?
		UNK_81 = 81,
		UNK_82 = 82,
		UNK_83 = 83,
		UNK_84 = 84,

		// 85 - 108
		// all seem to be connected in ID190_pool

		// ===================
		// DISCLAIMER: THESE DON'T MATCH IN ID191_pool LIKE AT ALL, FUUUUCK ME
		// ===================
		ObjectCamera_UNK_85 = 85,	// オブジェクトカメラ
		Shadow_UNK_86 = 86,	// 影
		AmbientLight_UNK_87 = 87,	// 環境光
		ParallelLightSource_UNK_88 = 88,	// 平行光源
		TouchFilter_UNK_89 = 89,	// タッチフィルター
		ObservationEye_AmbientLight_UNK_90 = 90,	// 観察眼：環境光
		ObservationEye_PointLightSource_UNK_91 = 91,	// 観察眼：点光源
		LensFlare_UNK_92 = 92,	// レンズフレア
		WaterSurface1_Setting1_UNK_93 = 93,	// 水面1：設定1
		WaterSurface2_Setting2_UNK_94 = 94,	// 水面2：設定2
		WaterSurface3_NormalMap1_UNK_95 = 95,	// 水面3：法線マップ1
		WaterSurface4_NormalMap2_UNK_96 = 96,	// 水面4：法線マップ2
		WaterSurface5_Color_UNK_97 = 97,	// 水面5：色
		ShadowSetting_PS4_UNK_98 = 98,	// 影設定：PS4
		ShadowSetting_Vita_UNK_99 = 99,	// 影設定：Vita
		ShadowMap0_PS4_UNK_100 = 100,	// 影マップ0：PS4
		ShadowMap1_PS4_UNK_101 = 101,	// 影マップ1：PS4
		ShadowMap0_Vita_UNK_102 = 102,	// 影マップ0：Vita
		WaterSurface_Windows1_Setting1_UNK_103 = 103,	// 水面Windows1：設定1
		WaterSurface_Windows2_Setting2_UNK_104 = 104,	// 水面Windows2：設定2
		WaterSurface_Windows3_NormalMap1_UNK_105 = 105,	// 水面Windows3：法線マップ1
		WaterSurface_Windows4_NormalMap2_UNK_106 = 106,	// 水面Windows4：法線マップ2
		WaterSurface_Windows5_Color_UNK_107 = 107,	// 水面Windows5：色
		WaterSurface_Windows6_WaterSurfacePlate_UNK_108 = 108,	// 水面Windows6：水面板

		UNK_109 = 109,	// "影マップ0：Vita" which is probably incorrect

		// 110 - 118
		// all connected in ID152_lab_shirogane
		// DISCLAIMER: these also probably don't match
		Shadow_UNK_110 = 110,	// 影
		AmbientLight_UNK_111 = 111,	// 環境光
		ParallelLightSource_UNK_112 = 112,	// 平行光源
		TouchFilter_UNK_113 = 113,	// タッチフィルター
		ShadowSettings_PS4_UNK_114 = 114,	// 影設定：PS4
		ShadowSettings_Vita_UNK_115 = 115,	// 影設定：Vita
		ShadowMap0_PS4_UNK_116 = 116,	// 影マップ0：PS4
		ShadowMap1_PS4_UNK_117 = 117,	// 影マップ1：PS4
		ShadowMap0_Vita_UNK_118 = 118,	// 影マップ0：Vita

		// 135 - 145
		// only used in the map with the flashback light creator, ID031_classRoom_C
		// copy pasted from google translate without even checking if the values mean anything
		FlashbackClassroom_BackgroundObject = 135,	// 背景オブジェクト
		FlashbackClassroom_Shadow = 136,	// 影
		FlashbackClassroom_AmbientLight = 137,	// 環境光
		FlashbackClassroom_ParallelLightSource = 138,	// 平行光源
		FlashbackClassroom_TouchFilter = 139,	// タッチフィルター
		FlashbackClassroom_ShadowSettings_PS4 = 140,	// 影設定：PS4
		FlashbackClassroom_ShadowSettings_Vita = 141,	// 影設定：Vita
		FlashbackClassroom_ShadowMap_0_PS4 = 142,	// 影マップ0：PS4
		FlashbackClassroom_ShadowMap_1_PS4 = 143,	// 影マップ1：PS4
		FlashbackClassroom_ShadowMap_0_Vita = 144,	// 影マップ0：Vita
		FlashbackClassroom_Transparent_Object = 145	// 透過オブジェクト
	}
}