using System.Collections;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Danganronpa_Another_Tool {
	public enum ExtractionMode { DEMO, FULL };
	public static class Program {
		public static void Main(string[] args) {
			string UMDIMAGE = args[0];
			string Destination = args[1];
			string EBootFile = args[2];
			string Version = args[3];

			// start pos has been moved to position
			long StartPos; //= long.Parse(args[2]);
			ExtractionMode Extraction;

			switch ( Version ) {
				default:
				case "FULL":	// this is the translation done on top of TBE
				case "FullTranslation":
					StartPos = 0xF5A38;
					Extraction = ExtractionMode.FULL;
					break;

				// original release, DEMO parsing works for these
				case "NPJH50372-umdimage":
					StartPos = 0xF8240;
					Extraction = ExtractionMode.DEMO;
					break;
				case "NPJH50372-umdimage2":
					StartPos = 0x103C54;
					Extraction = ExtractionMode.DEMO;
					break;

				// TBE, full works for these without sorting
				case "NPJH50515-umdimage":
					StartPos = 0xF5A18;
					Extraction = ExtractionMode.FULL;
					break;
				case "NPJH50515-umdimage2":
					StartPos = 0xF5200;
					Extraction = ExtractionMode.FULL;
					break;
				
				// Translated DMEO, i don't care about original demo as i can't
				// find it anyhwere online
				case "DEMO":	
				case "TranslatedDemo":
					StartPos = 0x145c1c;
					Extraction = ExtractionMode.DEMO;
					break;
			}

			Console.WriteLine("{0}, {1}, {2}, {3}, {4}", UMDIMAGE, Destination, StartPos, EBootFile, Extraction);
			DATpsp.ExtractDAT(UMDIMAGE, Destination, StartPos, EBootFile, Extraction, Version);
		}
	}

	public static class DATpsp {
		public static void ExtractDAT(
			string UmdImageName,
			string DestDir,
			long Pos,
			string FileBoot,
			ExtractionMode GameExtraction,
			string GameVersion) {
			using ( FileStream	FileDAT = new FileStream(UmdImageName, FileMode.Open, FileAccess.Read),
								EBOOT = new FileStream(FileBoot, FileMode.Open, FileAccess.Read) ) {
				using ( BinaryReader	BinaryDAT = new BinaryReader(FileDAT, Encoding.Default),
										BinaryEBOOT = new BinaryReader(EBOOT) ) {
					// GABI: added this just so it deletes my failed attempts easier,
					// you can remove it if you want
					if (Directory.Exists(DestDir)) {
						Directory.Delete(DestDir, true);
						Directory.CreateDirectory(DestDir);
					}

					uint AmountOfFiles = 0;

					// If the user is working with the full game files.
					if ( GameExtraction == ExtractionMode.FULL ) {
						// Reads the number of files inside the UMDIMAGE.DAT.
						AmountOfFiles = BinaryDAT.ReadUInt32();
					}
					// If the user has chosen to work with the demo files.
					else if ( GameExtraction == ExtractionMode.DEMO ) {
						EBOOT.Seek(Pos, SeekOrigin.Begin);
						AmountOfFiles = BinaryEBOOT.ReadUInt32(); // Reads the number of files inside the UMDIMAGE.DAT.
						BinaryEBOOT.ReadUInt32(); // Unknown and "useless".
					}

					string[] FilesNames = new string[AmountOfFiles];
					uint[] OffsetFiles = new uint[AmountOfFiles];
					uint[] FilesSizes = new uint[AmountOfFiles];

					// If the user is working with the full game files.
					if ( GameExtraction == ExtractionMode.FULL ) {
						for ( int i = 0; i < AmountOfFiles; i++ ) {
							// Move at the beginning of the info of each file inside the DAT. 
							FileDAT.Seek( i*0x4+0x4 , SeekOrigin.Begin);
							Console.WriteLine(i*0x4+0x4);
							// Saves the file offset involved.
							OffsetFiles[i] = BinaryDAT.ReadUInt32();
							// Saves the offset of the next file in order to calculate the size of the current file.
							FilesSizes[i] = BinaryDAT.ReadUInt32();

							Console.WriteLine("{0}, 0x{1:x}, 0x{2:x}", i, OffsetFiles[i], FilesSizes[i]);

							// If the size of the next file is 0, it means that it's the very latest file.
							if ( FilesSizes[i] == 0 )
								// So the file size will be obtained taking into account the end of the DAT.
								FilesSizes[i] = (uint)FileDAT.Length-OffsetFiles[i];
							else
								// File size = starting point of the next file - starting point of the current file. 
								FilesSizes[i] -= OffsetFiles[i];

							// Moves at the beginning of the offset of the filename.
							EBOOT.Seek(Pos, SeekOrigin.Begin);
							// Saves the offset of the filename.
							
							uint Pointer;
							if (GameVersion is not "NPJH50515-umdimage" or "NPJH50515-umdimage2") {
								Pointer = BinaryEBOOT.ReadUInt32() + 0xC0;
							} else {
								Pointer = BinaryEBOOT.ReadUInt32() + 0xA0;
							}
							Pos = EBOOT.Position+8; /* pos now points to the offset of the next filename.
							The +8 is due to the fact that in the between an offset and the next there is some "unnecessary" data. */

							// Move at the beginning of the filename.
							EBOOT.Seek(Pointer, SeekOrigin.Begin);

							// START Read and save the filename.
							byte Fine = BinaryEBOOT.ReadByte();
							while ( Fine != 0 ) {
								FilesNames[i] += (char)Fine;
								Fine = BinaryEBOOT.ReadByte();
								//END Read and save the filename.
							}
							Console.WriteLine(FilesNames[i]);
							// Console.ReadLine();
						}

						// Order filenames alphanumetically.
						// GABI: latest version doesn't order them alphabetically
						if (GameVersion is not "NPJH50515-umdimage" or "NPJH50515-umdimage2") {
							Array.Sort(FilesNames, new DRAT.AlphanumComparatorFast());
						}
						
					}
					// If the user is working with the demo files.
					else if ( GameExtraction == ExtractionMode.DEMO ) {
						uint[] OffsetNamesfiles = new uint[AmountOfFiles];

						// Reads from the eboot the offsets of the file names and body. It also reads the size of each file.
						for ( int i = 0; i<AmountOfFiles; i++ ) {
							OffsetNamesfiles[i] = BinaryEBOOT.ReadUInt32()+0xC0;
							OffsetFiles[i] = BinaryEBOOT.ReadUInt32();
							FilesSizes[i] = BinaryEBOOT.ReadUInt32();
						}

						for ( int i = 0; i<AmountOfFiles; i++ ) {
							// Move at the beginning of the filename.
							EBOOT.Seek(OffsetNamesfiles[i], SeekOrigin.Begin);

							// START Read and save the filename.
							byte Fine = BinaryEBOOT.ReadByte();
							while ( Fine != 0 ) {
								FilesNames[i] += (char)Fine;
								Fine = BinaryEBOOT.ReadByte();
							} //END Read and save the filename.
						}
					}

					// Files extraction start now.
					for ( int i = 0; i<AmountOfFiles; i++ ) {
						// Move at the beginning of the body's file within the file.DAT.
						FileDAT.Seek(OffsetFiles[i], SeekOrigin.Begin);

						byte[] BodyText = new byte[FilesSizes[i]];
						// Save the body's file.
						FileDAT.ReadExactly(BodyText);

						using ( FileStream Extract = new FileStream(Path.Combine(DestDir, FilesNames[i]), FileMode.Create, FileAccess.Write) )
							// Makes and saves the new file.
							Extract.Write(BodyText, 0, BodyText.Length);
					}
				}
			}
		}

	}
	public partial class DRAT {
		// AlphanumComparatorFast taken from https://gist.github.com/ngbrown/3842065
		public class AlphanumComparatorFast : IComparer {
			public int Compare(object x, object y) {
				string s1 = x as string;
				if ( s1==null )
					return 0;

				string s2 = y as string;
				if ( s2==null )
					return 0;

				int len1 = s1.Length, len2 = s2.Length;
				int marker1 = 0, marker2 = 0;

				// Walk through two the strings with two markers.
				while ( marker1<len1&&marker2<len2 ) {
					char ch1 = s1[marker1], ch2 = s2[marker2];

					// Some buffers we can build up characters in for each chunk.
					char[] space1 = new char[len1], space2 = new char[len2];
					int loc1 = 0, loc2 = 0;

					// Walk through all following characters that are digits or
					// characters in BOTH strings starting at the appropriate marker.
					// Collect char arrays.
					do {
						space1[loc1++]=ch1;
						marker1++;

						if ( marker1<len1 )
							ch1=s1[marker1];
						else
							break;

					} while ( char.IsDigit(ch1)==char.IsDigit(space1[0]) );

					do {
						space2[loc2++]=ch2;
						marker2++;

						if ( marker2<len2 )
							ch2=s2[marker2];
						else
							break;

					} while ( char.IsDigit(ch2)==char.IsDigit(space2[0]) );

					// If we have collected numbers, compare them numerically.
					// Otherwise, if we have strings, compare them alphabetically.
					string str1 = new string(space1), str2 = new string(space2);

					int result;

					if ( char.IsDigit(space1[0])&&char.IsDigit(space2[0]) ) {
						int thisNumericChunk = int.Parse(str1);
						int thatNumericChunk = int.Parse(str2);
						result=thisNumericChunk.CompareTo(thatNumericChunk);
					} else
						result=str1.CompareTo(str2);


					if ( result!=0 )
						return result;

				}
				return len1-len2;
			}
		}
	}
}
