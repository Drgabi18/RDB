using System.Text.Json;
using DanganFurniture.Enums;
using DanganFurniture.Structs;

namespace DanganFurniture {
	public class Print {
		// easier to parse the game id here 
		public static void JsonSerializedPrint<T>(List<T> Everything) where T : struct {
			// although not ever necesary, check if it's the right type
			// TODO: Can we check for just T instead?
			if (Everything.GetType() == typeof(List<HPA.Room>) ||
				Everything.GetType() == typeof(List<V3.Room>)) {
				Console.WriteLine(
					JsonSerializer.Serialize(Everything,
					new JsonSerializerOptions{IncludeFields = true, WriteIndented = true})
				);
				}
		}

		public static void LazyGodotPrint(List<HPA.Room> Everything) {
			Random Randomy = new Random();
			int Indexer = 0;
			
			// godot 4.x
			Console.WriteLine("[gd_scene format=3 uid=\"uid://{0}\"]", PrintUtils.RandomUIDGenerator());

			Console.WriteLine("[node name=\"Node3D\" type=\"Node3D\" unique_id={0}]", Randomy.Next());
			Console.WriteLine();

			foreach (HPA.Room Map in Everything) { // lol
			Console.WriteLine("[node name=\"{0}\" type=\"Node\" parent=\".\" unique_id={1} groups=[{2}]]",
				Map.RoomName, Randomy.Next(), PrintUtils.ReturnListForGroups([Map.RoomName]));
			Console.WriteLine();

			foreach (HPA.Furniture Object in Map.Places) {
				Indexer++;
				string NodeName;
				if (Object.ObjectName != null) {
					NodeName = Object.ObjectName;	// DR2 ONLY
				} else {
					NodeName = String.Concat(Enum.GetName(typeof(FurnitureTypes), Object.Type), "_Node_", Indexer);
				}

				// fun fact, this stupidly doesn't work but making a transform3d from 2 objects does
				/*
				var DOES_NOT_WORK = new Godot.Transform3D()
				.Translated(new Godot.Vector3(Object.Position[0], Object.Position[1], Object.Position[2]))
				.RotatedLocal(Godot.Vector3.Up, (float)Double.DegreesToRadians(Object.Rotation));
				*/

				// TODO: Figure out scale later :^)
				Godot.Basis BasisRotation = new Godot.Basis(Godot.Vector3.Up, (float)Double.DegreesToRadians(Object.Rotation));
				Godot.Transform3D TransformTranslaed = new Godot.Transform3D(BasisRotation, new Godot.Vector3(Object.Position[0], Object.Position[1], Object.Position[2]));

				Console.WriteLine("[node name=\"{0}\" type=\"Marker3D\" parent=\"{1}\" unique_id={2} groups=[{3}]]",
					NodeName, Map.RoomName, Indexer * 100, PrintUtils.ReturnListForGroups([Map.RoomName, "Type" + Object.Type.ToString()]));
				Console.WriteLine("transform = Transform3D({0}, {1}, {2}, {3})",
					// some objects have the scale 0, which would make it so we can't see anything, we should
					// think a little more about what we should scare here lol
					//Object.Size[0], Object.Size[1], 1, Object.Position[0], Object.Position[1], Object.Position[2]);
					TransformTranslaed.Basis.X.ToString()[1..^1], TransformTranslaed.Basis.Y.ToString()[1..^1],
					TransformTranslaed.Basis.Z.ToString()[1..^1], TransformTranslaed.Origin.ToString()[1..^1]);
				Console.WriteLine("metadata/type = \"{0}\"", Object.Type);
				Console.WriteLine("metadata/id = \"{0}\"", Object.ID);
				Console.WriteLine("metadata/unk1 = \"{0}\"", Object.Unk1);
				Console.WriteLine("metadata/unk2 = \"{0}\"", Object.Unk2);
				Console.WriteLine("gizmo_extents = 100.0");
				Console.WriteLine();
				// creating a bilboarded sprite
				Console.WriteLine("[node name=\"Label3D\" type=\"Label3D\" parent=\"{0}/{1}\" unique_id={2}]",
					Map.RoomName, NodeName, Randomy.Next());
				Console.WriteLine("pixel_size = 1.0");
				Console.WriteLine("billboard = 1");
				Console.WriteLine("no_depth_test = true");
				Console.WriteLine("text = \"{0}\"", Object.Type);
				Console.WriteLine();
			}

			// can we not add Program. without making a method to get the value?
			if (Program.SelectedGame == GameID.DR2) {
			foreach (var Obiect in Map.AABB) {
				for (int i = 0; i < 4; i += 3 ) {
					string NodeName;
					Indexer++;
					NodeName = String.Concat(Obiect.Key, "_AABB_", Indexer);
					Console.WriteLine("[node name=\"{0}\" type=\"Marker3D\" parent=\"{1}\" unique_id={2} groups=[{3}]]",
						NodeName, Map.RoomName, Randomy.Next(), PrintUtils.ReturnListForGroups([Map.RoomName, "AABB"]));
					Console.WriteLine("transform = Transform3D({0}, 0, 0, 0, {1}, 0, 0, 0, {2}, {3}, {4}, {5})",
						1, 1, 1, Obiect.Value.SixFloats[i], Obiect.Value.SixFloats[i+1], Obiect.Value.SixFloats[i+2]);
					Console.WriteLine("gizmo_extents = 100.0");
					Console.WriteLine();
					// creating a bilboarded sprite
					Console.WriteLine("[node name=\"Label3D\" type=\"Label3D\" parent=\"{0}/{1}\" unique_id={2}]",
						Map.RoomName, NodeName, Randomy.Next());
					Console.WriteLine("pixel_size = 0.5");
					Console.WriteLine("billboard = 1");
					Console.WriteLine("no_depth_test = true");
					Console.WriteLine("text = \"AABB\"");
					Console.WriteLine();
				}
			}
			}

			if (Map.Colissions.Verticies != null) {
				foreach (HPA.Vertex vertex in Map.Colissions.Verticies) {
					string NodeName;
					Indexer++;
					NodeName = String.Concat("C_", Map.RoomName, "_Vertex_", Indexer);
					Console.WriteLine("[node name=\"{0}\" type=\"Marker3D\" parent=\"{1}\" unique_id={2} groups=[{3}]]",
						NodeName, Map.RoomName, Randomy.Next(), PrintUtils.ReturnListForGroups([Map.RoomName, "Collision"]));
					Console.WriteLine("transform = Transform3D({0}, 0, 0, 0, {1}, 0, 0, 0, {2}, {3}, {4}, {5})",
						1, 1, 1, vertex.Pos[0], vertex.Pos[1], vertex.Pos[2]);
					Console.WriteLine("gizmo_extents = 100.0");
					Console.WriteLine();
					// creating a bilboarded sprite
					Console.WriteLine("[node name=\"Label3D\" type=\"Label3D\" parent=\"{0}/{1}\" unique_id={2}]",
						Map.RoomName, NodeName, Randomy.Next());
					Console.WriteLine("pixel_size = 1");
					Console.WriteLine("billboard = 1");
					Console.WriteLine("no_depth_test = true");
					Console.WriteLine("text = \"C\"");
					Console.WriteLine();
				}
			}

			}
		}

		public static void LazyGodotPrint(List<V3.Room> Everything) {
			Random Randomy = new Random();
			int Indexer = 0;

			// godot 4.x
			Console.WriteLine("[gd_scene format=3 uid=\"uid://{0}\"]", PrintUtils.RandomUIDGenerator());

			Console.WriteLine("[node name=\"Node3D\" type=\"Node3D\" unique_id={0}]", Randomy.Next());
			Console.WriteLine();

			foreach (V3.Room Map in Everything) { // lol
			Console.WriteLine("[node name=\"{0}\" type=\"Node\" parent=\".\" unique_id={1} groups=[{2}]]",
				Map.RoomName, Randomy.Next(), PrintUtils.ReturnListForGroups([Map.RoomName]));
			Console.WriteLine();

			foreach (V3.Furniture Object in Map.Places) {
				Indexer++;
				string NodeName;
				// useless for now
				if (Object.ObjectName != null) {
					NodeName = Object.ObjectName;
				} else {
					NodeName = String.Concat(Enum.GetName(typeof(FurnitureTypesV3), Object.Type), "_Node_", Indexer);
				}
				
				Console.WriteLine("[node name=\"{0}\" type=\"Marker3D\" parent=\"{1}\" unique_id={2} groups=[{3}]]",
					NodeName, Map.RoomName, Indexer * 100, PrintUtils.ReturnListForGroups([Map.RoomName, "Type" + Object.Type.ToString()]));
				Console.WriteLine("transform = Transform3D({0}, 0, 0, 0, {1}, 0, 0, 0, {2}, {3}, {4}, {5})",
					// TODO: temp
					1, 1, 1, Object.X, Object.Y, Object.Z);
				Console.WriteLine("metadata/float4 = \"{0}\"", Object.float4);
				Console.WriteLine("metadata/float5 = \"{0}\"", Object.float5);
				Console.WriteLine("metadata/float6 = \"{0}\"", Object.float6);
				Console.WriteLine("metadata/float7 = \"{0}\"", Object.float7);
				Console.WriteLine("metadata/float8 = \"{0}\"", Object.float8);
				Console.WriteLine("metadata/unk3 = \"{0}\"", Object.Unk3);
				Console.WriteLine("metadata/unk4 = \"{0}\"", Object.Unk4);
				Console.WriteLine("gizmo_extents = 100.0");
				Console.WriteLine();
				// creating a bilboarded sprite
				Console.WriteLine("[node name=\"Label3D\" type=\"Label3D\" parent=\"{0}/{1}\" unique_id={2}]",
					Map.RoomName, NodeName, Randomy.Next());
				Console.WriteLine("pixel_size = 1");
				Console.WriteLine("billboard = 1");
				Console.WriteLine("no_depth_test = true");
				Console.WriteLine("text = \"{0}\")", Object.Type);
				Console.WriteLine();
			}
			}
		}
	}

	static public class PrintUtils {
		static public string AddQuotes(string str) {
			return String.Concat('"', str, '"');
		}

		static public string ReturnListForGroups(string[] strs) {
			return string.Join(", ", strs.Select(s => AddQuotes(s)).ToArray());
		}
		
		// i didn't check how godot does it
		static public string RandomUIDGenerator(int Size = 13) {
			// why do i have to do this instead of it just working on a string
			char[] ResultString = new char[Size];
			string AllowedCharacters = "abcdefghijklmnopqrstuvwxyz0123456789";
			Random RNG = new Random();

			for (int i = 0; i < Size; i++)
				ResultString[i] = AllowedCharacters[RNG.Next(AllowedCharacters.Length)];

			// char[].ToString() doesn't work?????????????/
			return new string(ResultString);
		}
	}
}