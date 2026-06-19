using System.Text;
using System.Text.Json.Serialization;

namespace EV8Reader.ObjectTypes {
	/*
	OBJTYP_NULL		- NEVER USED
	OBJTYP_MAP		- NEVER USED
	OBJTYP_BGMAP	- NEVER USED
	OBJTYP_CAMPOS	- NEVER USED
	OBJTYP_CAMTRG	- NEVER USED
	OBJTYP_CHRMOV	- NEVER USED
	OBJTYP_CHRANM	- NEVER USED
	OBJTYP_MAPOBJ	- NEVER USED
	OBJTYP_free		- NEVER USED
	OBJTYP_POINT	- NEVER USED
	OBJTYP_RECT		- NEVER USED
	OBJTYP_AREA		- NEVER USED
	OBJTYP_HAICH	- NEVER USED
	OBJTYP_EBAREA	- NEVER USED
	OBJTYP_ITMBOX	- NEVER USED
	OBJTYP_REGMAP	- NEVER USED
	OBJTYP_REGCHR	- NEVER USED
	OBJTYP_REGPEF	- NEVER USED
	OBJTYP_SETOBJ	- NEVER USED
	OBJTYP_FADE		- NEVER USED
	OBJTYP_TRIGER	- NEVER USED
	OBJTYP_DSOMAP	- The Map in which you play (seemingly never used as .LINs load the map)
	OBJTYP_EVTCOLOR	- Event - Color (over screen or enviourment?)
	OBJTYP_DSOCHR	- Used to place NPCs that could move around in cutscenes
	OBJTYP_EVTMDL	- Event - Model (e.g. Sports Boy Shrine)
	OBJTYP_EVTPTC	- Event - Particle
	OBJTYP_EVTSE	- Event - Sound Effect
	OBJTYP_EVCAMPOS	- Event - Camera Position
	OBJTYP_EVCAMTRG	- Event - Camera Trigger?
	OBJTYP_MSCRIPT	- Sometimes used in Cutscenes for text, can only hold 32 cases inside
	OBJTYP_PLIST	- Item Generator ???????
	OBJTYP_RECT00	- Removing them in the Test Map stops the teleport, can only hold 32 teleports inside
	OBJTYP_POST_PLY	- Playing field - Player Place
	OBJTYP_POST_ENM	- Playing field - Enemy Place
	OBJTYP_POST_WCM	- Playing field - Game Cabinet
	OBJTYP_POST_TBX	- Playing field - Item Box
	OBJTYP_POST_NPC	- NEVER USED - seems to have been removed/replaced late in development
	OBJTYP_POST_SAV	- NEVER USED - seems to have been removed/replaced late in development
	OBJTYP_POST_OBJ	- Playing field - Place Object (see if this is what the 2 objects above were replaced with)
	OBJTYP_POST_PTC	- Playing field - Particle
	*/

	// There's more unused object types in the code that are not in the list above,
	// for example, CBcDSOMAP, CEvtActDsoMap and CEvtIFDsoMap (latter 2 are part of CEiDsoMap)

	public struct EV8FileHeader {
		//string Header = "EvtMake ver 0.1";	// 0x00, never ever checked
		/* 0x100 */ public int Unk1;	// read at 0x14004B886 in the code
										// Always 2, is 1 only two times
		/* 0x100 */ public int FileSize;
		/* 0x100 */ public int NoOfObjects;
		// would have been an array but...
		public List<EV8ListEntry> ListOfObjects;
		//  = new List<EV8ListEntry>();
	}
	public class EV8ListEntry {
		public string ObjectName; // Example "OBJTYP_MSCRIPT"
		public int AdressOfObject;
		public int HeaderReportedSize;	// or object header size? or where object ends?
		public object DataFromSerializedObject; // only here for JsonSerializer
	}

	public class ObjectClasses{
		public enum ObjTypes {
		// list at 0x1402E39C0
		OBJTYP_NULL = 0x00,	OBJTYP_MAP,	OBJTYP_BGMAP,	OBJTYP_CAMPOS,
		OBJTYP_CAMTRG,	OBJTYP_CHRMOV,	OBJTYP_CHRANM,	OBJTYP_MAPOBJ,
		OBJTYP_free,	OBJTYP_POINT,	OBJTYP_RECT,	OBJTYP_AREA,
		OBJTYP_HAICH,	OBJTYP_EBAREA,	OBJTYP_ITMBOX,	OBJTYP_REGMAP,
		OBJTYP_REGCHR,	OBJTYP_REGPEF,	OBJTYP_SETOBJ,	OBJTYP_FADE,
		OBJTYP_TRIGER,	OBJTYP_DSOMAP,	OBJTYP_EVTCOLOR,	OBJTYP_DSOCHR,
		OBJTYP_EVTMDL,	OBJTYP_EVTPTC,	OBJTYP_EVTSE,	OBJTYP_EVCAMPOS,
		OBJTYP_EVCAMTRG,	OBJTYP_MSCRIPT,	OBJTYP_PLIST,	OBJTYP_RECT00,
		OBJTYP_POST_PLY,	OBJTYP_POST_ENM,	OBJTYP_POST_WCM,	OBJTYP_POST_TBX,
		OBJTYP_POST_NPC,	OBJTYP_POST_SAV,	OBJTYP_POST_OBJ,	OBJTYP_POST_PTC
		}
		
		public static object? CreateObject(ObjTypes ObjectName) {
			switch (ObjectName) {
				case ObjTypes.OBJTYP_NULL:		case ObjTypes.OBJTYP_MAP:		case ObjTypes.OBJTYP_BGMAP:
				case ObjTypes.OBJTYP_CAMPOS:	case ObjTypes.OBJTYP_CAMTRG:	case ObjTypes.OBJTYP_CHRMOV:
				case ObjTypes.OBJTYP_CHRANM:	case ObjTypes.OBJTYP_MAPOBJ:	case ObjTypes.OBJTYP_free:
				case ObjTypes.OBJTYP_POINT:		case ObjTypes.OBJTYP_RECT:		case ObjTypes.OBJTYP_AREA:
				case ObjTypes.OBJTYP_HAICH:		case ObjTypes.OBJTYP_EBAREA:	case ObjTypes.OBJTYP_ITMBOX:
				case ObjTypes.OBJTYP_REGMAP:	case ObjTypes.OBJTYP_REGCHR:	case ObjTypes.OBJTYP_REGPEF:
				case ObjTypes.OBJTYP_SETOBJ:	case ObjTypes.OBJTYP_FADE:		case ObjTypes.OBJTYP_TRIGER:
				// these 2 seem to have been scrapped/replaced with other types kind of late
				case ObjTypes.OBJTYP_POST_NPC:	case ObjTypes.OBJTYP_POST_SAV:
					Console.WriteLine(
						"These are Unused in the actual game, if you somehow have "+
						"a version that uses these, please open an issue and/or "+
						"document them too!/nThrowing a hard exception cause it's "+ 
						"important!!1!/nTo ignore this error, pass --ignore-unused "+
						"parameter to the app");
					// TODO: Remove this error and replace it with what we say above
					throw new Exception("Unused Objects Detected");
				case ObjTypes.OBJTYP_DSOMAP:	// DONE SANS HEADER
				case ObjTypes.OBJTYP_EVTCOLOR:	// DONE SANS HEADER
				case ObjTypes.OBJTYP_DSOCHR:	// partially done
				case ObjTypes.OBJTYP_EVTMDL:	// partially done
				case ObjTypes.OBJTYP_EVTPTC:	// partially done
				case ObjTypes.OBJTYP_EVTSE:		// partially done
				case ObjTypes.OBJTYP_EVCAMPOS:	// partially done
				case ObjTypes.OBJTYP_EVCAMTRG:	// partially done
				case ObjTypes.OBJTYP_MSCRIPT:	// DONE SANS HEADER
				case ObjTypes.OBJTYP_PLIST:		// partially done
				case ObjTypes.OBJTYP_RECT00:	// partially done
				case ObjTypes.OBJTYP_POST_PLY:	// partially done
				case ObjTypes.OBJTYP_POST_ENM:	// partially done
				case ObjTypes.OBJTYP_POST_WCM:	// partially done
				case ObjTypes.OBJTYP_POST_TBX:	// partially done
				//case ObjTypes.OBJTYP_POST_NPC:// unused, moved above, code still exists
				//case ObjTypes.OBJTYP_POST_SAV:// ditto
				case ObjTypes.OBJTYP_POST_OBJ:	// partially done
				case ObjTypes.OBJTYP_POST_PTC:	// partially done
				default:
					//break;
					throw new Exception("Not implemented yet!");
			}
		}
	}
	// Comments aren't used at all by the game to understand how stuff works
	// as such, they can be just about anything, we should have a special struct
	public struct ShortComment {
		// TODO: Force this to be ASCII 32 bytes
		string Message;
	}
	public struct LongComment {
		// TODO: Force this to be ASCII 64 bytes
		string Message;
	}

	// suspiciously matches LIN files lol
	public struct ObjTypeHeader {
		/* 0x0 */ public int SizeOfContent;	// size of Object after the header
		/* 0x4 */ public int HowManyChunks;	// how many "Chunks" of the object there are
		/* 0x8 */ public int HeaderSize;		// mostly 16 bytes
		/* 0xC */ public int Unk1; 			// always 0, unused
		public string TEMPORARY_STRING;
	}


	public struct Vector3 { float X, Y, Z; }
	public struct Vector2 { float X, Y; }

	// TODO: Check what these values really are,
	// I'm pretty sure the Vector2 is Yaw (rotation in PI) and Scale
	public struct UDGPos { Vector3 Position; Vector2 Unk2; }

	public struct ObjTypeHeaderChunk {
		short Unk99;
		short Unk98;
		int Unk97, Unk96; // is the same value as `SizeOfContent`
		int Unk95; 
		int Unk94;
		int NoOfStructs;
		// these values are offset from after `AllObjectHeaders`
		List<int> StructsSize; 
		// e.g. 0, [80], 80, [2480], 2560, [64], 2624, [4], 2628
		// For example, if we have 5 structs, the next int is 0 (X), then the next
		// int is the sizeof() the data (Y), and the int after that is the result
		// of X + Y, this goes on untill it reaches the 5th object
		// 0 (1st object) + 0x50 = 0x50 (2nd object) + 0x09B0 = 0x0A00 + 0x20 = 0x0A20 ...
		// TODO: Make this struct a class to somehow make this work
		int BytesLeftAfterBinaryToStruct; // seems to not always be the case??
										  // for some reason it's seemingly always 59 bytes

		// TODO: Determine how they are somehow associated with the LIN files.
		// For example, Rect00 on e99_000_001.ev8 has 05 00 01 00 then 05 00 02 00
		// and 05 00 03 00, see if a header class of variable size can be made
		short Unk4; // could it be type, id, group?
		short Unk5; // seems to be index in group?

		int Unk6; // could it be invisible
		// TODO: Check if this is part of objects or header, in most cases, this
		// current struct in which I am commenting in is 0x80 bytes, after which
		// there's always a 32 bytes comment, and then the 2nd sizeof() matches
		// the size after
		LongComment Comment;
	}
}