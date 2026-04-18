//
//  As the filename implies, these are some definitions from the game's code I cared
//  to decompile, these should be as close to what the devs originally had
//

/* There are more Boolean ORs and ANDs in the code but this is the only one I
cared about */
typedef enum UDG_CanPlayerChange {
	CanChangeToAndFromGenocider=4 /* 0x0100; 1<< 3; Sets Genocider's Hud in the
									top right after changing scenes with this not
									set as 4 */
} UDG_CanPlayerChange;

typedef struct UDG_GamerStructAt0x2C UDG_GamerStructAt0x2C, *PUDG_GamerStructAt0x2C;

/* This list can probably be easier to make since I don't understand how to
combine these with boolean math in Ghidra */
typedef enum UDG_RunningStates {
	Gun_NormalRun=0 /* 0x0000; 0<<0 */,
	Gun_ConfidentRun=1 /* 0x0001; 1<<0 */,
	NoGun_NormalRun=2 /* 0x0010; 1<<1 */,
	NoGun_ConfidentRun_UNUSED=3 /* 0x0011 */,
	Gun_NormalRun_AllAmmoUnlockedAndInfinite=4 /* 0x0100; 1<<2; Used at the
												start of the game when you first
												control the gun */,
	Gun_ConfidentRun_AllAmmoUnlockedAndInfinite_UNUSED=5 /* 0x0101 */,
	StateUsedThroughoutTheGame_NormalRun=8 /* 0x1000; 1<<3 */,
	StateUsedThroughoutTheGame_ConfidentRun=9 /* 0x1001; This is ORed after the
												2nd LIN numbers in the story is
												greater than 5 and is how you get
												the confident run from after
												fighting Shirokuma to the end of
												the game */
} UDG_RunningStates;

struct UDG_GamerStructAt0x2C { /* Used in UDG_Gamer™, has Komaru's running state */
	long UNK_1; /* Set to 0xFFFFFFFF (-1) */
	enum UDG_RunningStates GetsRunningState;
	undefined field2_0x8;
	undefined field3_0x9;
	undefined field4_0xa;
	undefined field5_0xb;
	undefined field6_0xc;
	undefined field7_0xd;
	undefined field8_0xe;
	undefined field9_0xf;
};

typedef struct UDG_Gamer™ UDG_Gamer™, *PUDG_Gamer™;

/*
NOTE: This is not actually a struct in the traditional sense, I only made it like
this so it's easier for me to track the player, with that being said however, 
a part of the code from the game, more specifically the code that transfers the 
player state between changing rooms and save files, do use this region of code as
if it's a struct, please check ./NightmarePlayerStruct.md for more info.
*/
struct UDG_Gamer™ {
	enum UDG_CanPlayerChange PlayerChangeEnum;
	short CurrentPlayer; /* Changes between 0 to 5; 0-Nothing, 1-Komaru, 4-Toko, 5-Syo */
	short FirstChaser; /* Either 1 or 4 */
	short SecondChaser; /* List at 0x1400b0290 */
	undefined field4_0xa;
	undefined field5_0xb;
	byte KomaruLevel; /* 1-99 */
	byte BigBullet; /* The one that randomly appears from time to time */
	short KomaruHealth; /* 1-6 */
	short SomeFuckingBullshitForClothesRip;
	short CurrentBullet;
	short BulletBreakAmmo;
	short BulletBurnAmmo; /* Notice how the 2nd bullet in Burn */
	short BulletParalyzeAmmo;
	short BulletKnockbackAmmo;
	short BulletDanceAmmo;
	short BulletLinkAmmo;
	short BulletMoveAmmo;
	short BulletDetectAmmo;
	short MaxAttachLevel; /* In the Skills Menu */
	short AttachLevel; /* In the Skills Menu */
	undefined field21_0x28;
	undefined field22_0x29;
	undefined field23_0x2a;
	undefined field24_0x2b;
	struct UDG_GamerStructAt0x2C SomeStruct; /* This is where Komaru's running thing is */
	undefined field26_0x3c;
	undefined field27_0x3d;
	undefined field28_0x3e;
	undefined field29_0x3f;
	// ...
	// trimmed to remove unnecesary space
	// ...
	undefined field70_0x68;
	undefined field71_0x69;
	undefined field72_0x6a;
	undefined field73_0x6b;
	float CopyByValueOf_UDG_SomeKindOfFunnyScalar; /* Unused copy of a base scale float used for scaling */
	short TokoBatteries; /* 3-6 */
	undefined field76_0x72;
	undefined field77_0x73;
	float TokoBatteryFloat; /* From 100 to 0 for each cell */
	undefined field79_0x78;
	undefined field80_0x79;
	undefined field81_0x7a;
	undefined field82_0x7b;
	undefined field83_0x7c;
	undefined field84_0x7d;
	undefined field85_0x7e;
	undefined field86_0x7f;
};

typedef struct UDG_PlayerData UDG_PlayerData, *PUDG_PlayerData;

/* Unkown, used in the function when you take damage, seems to the individual
mesh, as in one structure I changed 0x20 to 2 and only Komaru's face dissapeared,
but then in another one her entire mesh is gone */
struct UDG_PlayerData {
	undefined field0_0x0;
	undefined field1_0x1;
	undefined field2_0x2;
	undefined field3_0x3;
	// ...
	// trimmed to remove unnecesary space
	// ...
	undefined field12_0xc;
	undefined field13_0xd;
	undefined field14_0xe;
	undefined field15_0xf;
	longlong *PtrToSomething_1;
	longlong *PtrToSomething_2;
	ushort SettingThisToMultipleOf2MakesTheCharacterStuckInPlaceAndInvisible;
	ushort field19_0x22;
	undefined2 SomethingWhenWeaponIsZoomed_1;
	short SomethingWhenWeaponIsZoomed_2;
	undefined field22_0x28;
	undefined field23_0x29;
	undefined field24_0x2a;
	undefined field25_0x2b;
	// ...
	// trimmed to remove unnecesary space
	// ...
	undefined field1338_0x54c;
	undefined field1339_0x54d;
	undefined field1340_0x54e;
	undefined field1341_0x54f;
	longlong PtrToSomething_3;
	short HealthToBeRemoved;
	short field1344_0x55a;
	undefined field1345_0x55c;
	undefined field1346_0x55d;
	undefined field1347_0x55e;
	undefined field1348_0x55f;
	// ...
	// trimmed to remove unnecesary space
	// ...
	undefined field1361_0x56c;
	undefined field1362_0x56d;
	undefined field1363_0x56e;
	undefined field1364_0x56f;
	longlong PtrToSomething_4;
	undefined field1366_0x578;
	undefined field1367_0x579;
	undefined field1368_0x57a;
	undefined field1369_0x57b;
	undefined field1370_0x57c;
	undefined field1371_0x57d;
	undefined field1372_0x57e;
	undefined field1373_0x57f;
	longlong *PtrToSomething_5;
};

/* There's genuinley so many Idon't even know how to categorize these, I've only
added the ones I noticed changes from */
typedef enum UDG_PlayerStateFromVariousActions {
	SetDuringLoadingLIN=64 /* 1<<6 */,
	SetScreenToBlack=3840 /* F<<8 */,
	CameraStuckInPlace_PlayerCanRotate=32768 /* 1<<15 */,
	CameraStuckInPlace_PlayerCannotRotate=34816 /* 1<<15 & 1<<11 */,
	RelatedToScreenChangingDuringTransitions=61440 /* F<<12 */,
	PausePlayerMovingButNotCamera=15728640 /* F<<20 */,
	StopModelsFromRendering=16777216 /* 1<<24 */,
	ResultsAndBreakRemote=134217728 /* 1<<27 */
} UDG_PlayerStateFromVariousActions;

typedef struct UDG_PlayerXYZ UDG_PlayerXYZ, *PUDG_PlayerXYZ;

/* This is the point in space the model follows, this value you change here is
what you use to go through walls etc. */
struct UDG_PlayerXYZ {
	undefined field0_0x0;
	undefined field1_0x1;
	undefined field2_0x2;
	undefined field3_0x3;
	undefined field4_0x4;
	undefined field5_0x5;
	undefined field6_0x6;
	undefined field7_0x7;
	undefined field8_0x8;
	undefined field9_0x9;
	undefined field10_0xa;
	undefined field11_0xb;
	float Pos_X;
	float Pos_Z;
	float Pos_Y;
};

/* Controls active skills, couldn't care less, Chiaki yawn png */
typedef enum UDG_SkillsEnum {
	UNK_1=1,
	UNK_2=2,
	UNK_3=4,
	UNK_4=64
} UDG_SkillsEnum;

typedef struct UDG_StartOfSaveFileInfo UDG_StartOfSaveFileInfo, *PUDG_StartOfSaveFileInfo;

/* Start of the save file where the game copies 0x32868 bytes */
struct UDG_StartOfSaveFileInfo {
	undefined4 MostLikelyHeader; /* Always set to 0x32860 (this is LE but in the
									save file it's BE), kinda of weird that it's
									the malloc minus 8 */
	undefined field1_0x4;
	undefined field2_0x5;
	undefined field3_0x6;
	undefined field4_0x7;
	undefined field5_0x8;
	undefined field6_0x9;
	undefined field7_0xa;
	undefined field8_0xb;
	long Time_Elapsed; /* Not a Long, couldn't actually decipher the format */
	undefined field10_0x10;
	undefined field11_0x11;
	undefined field12_0x12;
	undefined field13_0x13;
	// ...
	// trimmed to remove unnecesary space
	// ...
	undefined field99_0x69;
	undefined field100_0x6a;
	undefined field101_0x6b;
	undefined field102_0x6c;
	byte LanguageID;
	byte GameLanguage;
	undefined field105_0x6f;
	undefined field106_0x70;
	undefined field107_0x71;
	undefined field108_0x72;
	// ...
	// trimmed to remove unnecesary space
	// ...
	undefined field118_0x7c;
	undefined field119_0x7d;
	undefined field120_0x7e;
	undefined field121_0x7f;
};

typedef struct UDG_UnkCharacter_Data_1 UDG_UnkCharacter_Data_1, *PUDG_UnkCharacter_Data_1;

/* No idea, this is used to set Komaru's Rotation, Width, Visual X and Y (somehow),
paramter locations seem to be garbage, the function that moves models seems to
dereference using this struct */
struct UDG_UnkCharacter_Data_1 {
	undefined4 field0_0x0;
	undefined4 field1_0x4;
	undefined4 field2_0x8;
	undefined4 field3_0xc;
	float field4_0x10;
	float field5_0x14;
	float Pos_X;
	float field7_0x1c;
	longlong field8_0x20;
	short field9_0x28;
	char field10_0x2a;
	char field11_0x2b;
	float CharWidth1;
	float CharWidth2;
	float CharHeight;
	float field15_0x38;
	float RotationPI;
	float field17_0x40;
	float field18_0x44;
	float field19_0x48;
	float field20_0x4c;
	// ...
	// trimmed to remove unnecesary space
	// ...
	undefined field241_0x170;
	undefined field242_0x171;
	undefined field243_0x172;
	undefined field244_0x173;
	float Pos_Z;
	float Pos_Y;
	float field247_0x17c;
	float field248_0x180;
	float field249_0x184;
	float field250_0x188;
	float field251_0x18c;
	float field252_0x190;
	float field253_0x194;
	float field254_0x198;
	undefined4 field255_0x19c;
	float RotationPi_2?;
	float field257_0x1a4;
	ushort field258_0x1a8;
	byte field259_0x1aa;
	char field260_0x1ab;
	char field261_0x1ac;
	char field262_0x1ad;
	char field263_0x1ae;
};

typedef struct UDG_UnkCharacter_Data_2 UDG_UnkCharacter_Data_2, *PUDG_UnkCharacter_Data_2;

/* Unknown, still related to player character, the function that moves models
seems to dereference using this struct */
struct UDG_UnkCharacter_Data_2 {
	undefined field0_0x0;
	char field1_0x1;
	ushort field2_0x2;
	ushort field3_0x4;
	short field4_0x6;
	float Pos_X;
	float Pos_Z;
	float Pos_Y;
	float field8_0x14;
	float field9_0x18;
	float field10_0x1c;
	float field11_0x20;
	float field12_0x24;
	float field13_0x28;
	float field14_0x2c;
	float field15_0x30;
	float field16_0x34;
	float field17_0x38;
	undefined field18_0x3c;
	undefined field19_0x3d;
	undefined field20_0x3e;
	undefined field21_0x3f;
	float field22_0x40;
	uint field23_0x44;
	uint field24_0x48;
	uint field25_0x4c;
	uint field26_0x50;
	uint field27_0x54;
	uint field28_0x58;
	float field29_0x5c;
	undefined8 field30_0x60;
	longlong field31_0x68;
};

typedef struct UDG_VisualModel UDG_VisualModel, *PUDG_VisualModel;

/* The visual model for each mesh in a rig, changing these just changes the visuals,
so Monokumas still follow where the invisible point in space would be */
struct UDG_VisualModel {
	undefined field0_0x0;
	undefined field1_0x1;
	undefined field2_0x2;
	undefined field3_0x3;
	float RotYaw;
	float Offset_X;
	float Offset_Y;
	float Offset_Z;
	undefined field8_0x14;
	undefined field9_0x15;
	undefined field10_0x16;
	undefined field11_0x17;
	// ...
	// trimmed to remove unnecesary space
	// ...
	undefined field104_0x74;
	undefined field105_0x75;
	undefined field106_0x76;
	undefined field107_0x77;
	float ScaleX;
	float ScaleY;
	float RotPitch; /* Why are the other Rotation parameters so far away???? */
	float RotRoll;
	undefined field112_0x88;
	undefined field113_0x89;
	undefined field114_0x8a;
	undefined field115_0x8b;
	// ...
	// trimmed to remove unnecesary space
	// ...
	undefined field203_0xe4;
	undefined field204_0xe5;
	undefined field205_0xe6;
	undefined field206_0xe7;
	longlong *PointerToSomething;
};

//
// Other misc stuff
//

/* If we look at the function found at 0x1400f6b40, there's actually repeating
code, for some reason for clamping pi they used a function but for getting
the angle in 90 degrees intervals they made*/
#define PI 3.1415927
#define TAU 6.2831855
#define NEGATIVE_PI -3.1415927
#define GETANGLE(x) ((((225 - (int)((x * -360.0) / TAU)) / 90 + 1) * 90) % 360)
/* At least we know this define and 0x140102c30 are both stored in the same file*/