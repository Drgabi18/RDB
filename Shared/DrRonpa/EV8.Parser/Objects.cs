namespace EV8Reader.ObjectTypes {
	/*

	From the Debug Files left inside, these are probably
	* `CBcEVTPTC`, Classs Binary Chunk Event Particles
	* `CEiDsoRect00`, Class Entity DSO Rectangle Trigger Area
	* `CEvtActRect00`, Class Event Act(?) Rectangle Trigger Area
	* `CEvtIFRect00`, Class Event Interface(?) Rectangle Trigger Area

	* `Evt` - Event
	* `POST` - Playing Area
	* `If` - Interface(?)

	===== "Chunk" Object Types ====
		These are most likely the results of the CBc* classes
	OBJTYP_NULL		- THESE ARE USED AS SUBTYPES TO THE BOTTOM ONES
	OBJTYP_MAP		- ditto
	OBJTYP_BGMAP	- ditto
	OBJTYP_CAMPOS	- ditto
	OBJTYP_CAMTRG	- ditto
	OBJTYP_CHRMOV	- ditto
	OBJTYP_CHRANM	- ditto
	OBJTYP_MAPOBJ	- ditto
	OBJTYP_free		- ditto
	OBJTYP_POINT	- ditto
	OBJTYP_RECT		- ditto
	OBJTYP_AREA		- ditto
	OBJTYP_HAICH	- ditto
	OBJTYP_EBAREA	- ditto
	OBJTYP_ITMBOX	- ditto
	OBJTYP_REGMAP	- ditto
	OBJTYP_REGCHR	- ditto
	OBJTYP_REGPEF	- ditto
	OBJTYP_SETOBJ	- ditto
	OBJTYP_FADE		- ditto
	OBJTYP_TRIGER	- ditto
	===== "Group" Object Types ==== // no official name
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

	public class ObjectClasses {
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
				// subtypes
				case ObjTypes.OBJTYP_POST_NPC:	case ObjTypes.OBJTYP_POST_SAV:
					Console.WriteLine(
						"These aren't \"unused\" per se, but are \"chunks\""+
						"of other \"groups\". If you somehow have a version that"+
						"uses these, please open an issue and/or document them it!/n"+
						"Throwing a hard exception cause it's important!!1!/n"+
						"To ignore this error, pass --block-subs parameter to the app");
					// TODO: Remove this error and replace it with what we say above
					throw new Exception("Unused Objects Detected");
				case ObjTypes.OBJTYP_DSOMAP:	// DONE SANS HEADER
				case ObjTypes.OBJTYP_EVTCOLOR:	// DONE SANS HEADER
				case ObjTypes.OBJTYP_DSOCHR:	// needs subtypes implemented
				case ObjTypes.OBJTYP_EVTMDL:	// ditto
				case ObjTypes.OBJTYP_EVTPTC:	// ditto
				case ObjTypes.OBJTYP_EVTSE:		// ditto
				case ObjTypes.OBJTYP_EVCAMPOS:	// ditto
				case ObjTypes.OBJTYP_EVCAMTRG:	// ditto
				case ObjTypes.OBJTYP_MSCRIPT:	// DONE SANS HEADER
				case ObjTypes.OBJTYP_PLIST:		// needs subtypes implemented
				case ObjTypes.OBJTYP_RECT00:	// ditto
				case ObjTypes.OBJTYP_POST_PLY:	// ditto
				case ObjTypes.OBJTYP_POST_ENM:	// ditto
				case ObjTypes.OBJTYP_POST_WCM:	// ditto
				case ObjTypes.OBJTYP_POST_TBX:	// ditto
				//case ObjTypes.OBJTYP_POST_NPC:// moved above
				//case ObjTypes.OBJTYP_POST_SAV:// ditto
				case ObjTypes.OBJTYP_POST_OBJ:	// needs subtypes implemented
				case ObjTypes.OBJTYP_POST_PTC:	// needs subtypes implemented
				default:
					//break;
					throw new Exception("Not implemented yet!");
			}
		}
	}
}