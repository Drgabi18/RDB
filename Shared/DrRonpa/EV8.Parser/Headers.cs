using System.Runtime.InteropServices;

namespace EV8Reader.Headers {
	[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Ansi, Size = 0x295C, Pack = 4)]
	public struct EV8FileHeader {
		// never ever checked, 100 bytes long, could be used for comments, ascii/shift-jis
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
		[FieldOffset(0)]
		public string Header;
		// read at 0x14004B886 in the code
		// if 2, there's extra data that is 80 bytes in size
		[FieldOffset(0x100)]
		public int Type;
		[FieldOffset(0x104)]
		public int FileSize;
		[FieldOffset(0x108)]
		public int NoOfObjects;
		
		[FieldOffset(0x10C)]
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
		// BUG: This doesn't work because it uses pointers and on x64 a pointer
		// size is 8 bytes, so this section gets marshalled to double the size
		public EV8ListEntry[] ListOfObjects;
		
		[FieldOffset(0x290C)]
		[MarshalAs(UnmanagedType.LPStruct)]
		public EV8ExtraData ExtraData;
	}

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Size = 0x28)]
	public struct EV8ListEntry {
		//[FieldOffset(0x0), MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1)]
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		public string ObjectName; // Example "OBJTYP_MSCRIPT"
		// [FieldOffset(0x20)]
		public int AdressOfObject;
		// [FieldOffset(0x24)]
		public int HeaderReportedSize;	// or object header size? or where object ends?
		// public object DataFromSerializedObject; // only here for JsonSerializer
	}

	[StructLayout(LayoutKind.Sequential, Size = 0x50)]
	public struct EV8ExtraData {
		public int Unk1; // seems to be 1
		public int Unk2;
		public int Unk3;
		public int Unk4; // seems to be 1
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		public float[] Unknown16Floats;
		public EV8ExtraData() {
			//Unk1, Unk2, Unk3, Unk4 = 0;
			Unk1 = 0;
			Unk2 = 0;
			Unk3 = 0;
			Unk4 = 0;
			Unknown16Floats = new float[16];
		}
	}

	// suspiciously matches LIN files lol
	[StructLayout(LayoutKind.Sequential)]
	public struct ObjTypeHeader {
		/* 0x0 */ public int SizeOfContent;	// size of Object after the header
		/* 0x4 */ public int HowManyChunks;	// how many "Chunks" of the object there are
		/* 0x8 */ public int HeaderSize;		// mostly 16 bytes
		/* 0xC */ public int Unk1; 			// always 0, unused
		//public string TEMPORARY_STRING;
	}

	// Comments aren't used at all by the game to understand how stuff works
	// as such, they can be just about anything, they are seemingly not unions
	public struct ShortComment {
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		string Message;
	}
	public struct LongComment {
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
		string Message;
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