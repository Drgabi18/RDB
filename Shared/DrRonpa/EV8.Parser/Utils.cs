using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

// Source: https://gist.github.com/13xforever/2835844
public static class CastingHelper
{
	public static T CastToStruct<T>(this byte[] data) where T : struct
	{
		var pData = GCHandle.Alloc(data, GCHandleType.Pinned);
		var result = (T)Marshal.PtrToStructure(pData.AddrOfPinnedObject(), typeof(T));
		pData.Free();
		return result;
	}

	// public static byte[] CastToArray<T>(this T data) where T : struct
	// {
	// 	var result = new byte[Marshal.SizeOf(typeof(T))];
	// 	var pResult = GCHandle.Alloc(result, GCHandleType.Pinned);
	// 	Marshal.StructureToPtr(data, pResult.AddrOfPinnedObject(), true);
	// 	pResult.Free();
	// 	return result;
	// }
}

public static class EV8Helper
{
	// ev8 files store Shift-JIS strings in them which is like japanase ASCII
	// since i couldn't find a library to convert S-JIS to anything readable
	// we'll just do the 2nd best thing, print the hex and wait for someone else
	public static string ByteArrayToHexString(byte[] Bytes)
	{
		StringBuilder sb = new StringBuilder(Bytes.Length);
		foreach (byte b in Bytes)
		{
			sb.AppendFormat("{0:x2} ", b);
		}
		return sb.ToString().ToUpper();
	}

	// since i'm on linux, i guess one soulution would be to do
	// `iconv -f SHIFT-JIS -t UTF-8` locally, but this will only work for me 
	public static string AttemptDecodeString(byte[] Bytes) {
		#if _LINUX
			// why did microslop decide string to be utf-16 whyyyyyyyy
			string AttemptedDecodedString =
				Encoding.Unicode.GetString(TrimEndBytes(Bytes));
			// https://stackoverflow.com/a/206347
			Process p = new Process();
			p.StartInfo.UseShellExecute = false;
			p.StartInfo.RedirectStandardOutput = true;
			p.StartInfo.FileName = "/bin/fish"; // platform specific... shhh
			// THIS IS BROKEN
			p.StartInfo.Arguments = $"echo \"{AttemptedDecodedString}\" | iconv -f SHIFT-JIS -t UTF-8";
			p.Start();
			string output = p.StandardOutput.ReadToEnd();
			p.WaitForExit();
			return output;
		#else
			return ByteArrayToHexString(TrimEndBytes(Bytes));
		#endif
	}

	// https://stackoverflow.com/a/27225216
	public static byte[] TrimEndBytes(byte[] array)
	{
		int lastIndex = Array.FindLastIndex(array, b => b != 0);
		Array.Resize(ref array, lastIndex + 1);
		return array;
	}
}