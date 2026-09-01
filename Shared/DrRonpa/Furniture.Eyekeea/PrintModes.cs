using System.Text.Json;
using DanganFurniture.Enums;
using DanganFurniture.Structs;

namespace DanganFurniture.PrintModesClass {
	public class Print {
		public static string GodotSvgIdentifierWhatever = "1_awcjp";

		public static void JsonSerializedPrint(List<Room> Everything) {
			Console.WriteLine(
				JsonSerializer.Serialize(Everything,
				new JsonSerializerOptions{IncludeFields = true, WriteIndented = true})
			);
		}
		public static void JsonSerializedPrint(List<RoomV3> Everything) {
			Console.WriteLine(
				JsonSerializer.Serialize(Everything,
				new JsonSerializerOptions{IncludeFields = true, WriteIndented = true})
			);
		}

		public static void LazyGodotPrint(List<Room> Everything) {
			Random Randomy = new Random();
			int Indexer = 0;
			
			foreach (Room Map in Everything) { // lol
			Console.WriteLine("[node name=\"{0}\" type=\"Node\" parent=\".\" unique_id={1}]", Map.RoomName, Randomy.Next());
			Console.WriteLine();

			foreach (Furniture Object in Map.Places) {
				Indexer++;
				string NodeName;
				if (Object.ObjectName != null) {
					NodeName = Object.ObjectName;	// DR2 ONLY
				} else {
					NodeName = String.Concat(Enum.GetName(typeof(FurnitureTypes), Object.Type), "_Node_", Indexer);
				}
				
				Godot.Basis Test1 = new Godot.Basis().Rotated(new Godot.Vector3(Object.Position[0], Object.Position[1], Object.Position[2]),
					(float)Double.DegreesToRadians(Object.Rotation));
				Godot.Transform3D Test2 = new Godot.Transform3D(Test1, new Godot.Vector3(0, 0, 0));

				// TODO: Finish this
				Console.WriteLine(Test1);
				Console.WriteLine(Test2);
				
				Console.ReadKey();

				Console.WriteLine("[node name=\"{0}\" type=\"Marker3D\" parent=\"{1}\" unique_id={2}]",
					NodeName, Map.RoomName, Indexer * 100);
				Console.WriteLine("transform = Transform3D({0}, 0, 0, 0, {1}, 0, 0, 0, {2}, {3}, {4}, {5})",
					// some objects have the scale 0, which would make it so we can't see anything, we should
					// think a little more about what we should scare here lol
					//Object.Size[0], Object.Size[1], 1, Object.Position[0], Object.Position[1], Object.Position[2]);
					1, 1, 1, Object.Position[0], Object.Position[1], Object.Position[2]);
				Console.WriteLine("metadata/type = \"{0}\"", Object.Type);
				Console.WriteLine("metadata/id = \"{0}\"", Object.ID);
				Console.WriteLine("metadata/unk1 = \"{0}\"", Object.Unk1);
				Console.WriteLine("metadata/unk2 = \"{0}\"", Object.Unk2);
				Console.WriteLine("metadata/rotation = \"{0}\"", Object.Rotation);
				Console.WriteLine("gizmo_extents = 100.0");
				Console.WriteLine();
				// creating a bilboarded sprite
				Console.WriteLine("[node name=\"Sprite3D\" type=\"Sprite3D\" parent=\"{0}/{1}\" unique_id={2}]",
					Map.RoomName, NodeName, Randomy.Next());
				Console.WriteLine("pixel_size = 0.5");
				Console.WriteLine("billboard = 2");
				Console.WriteLine("texture = ExtResource(\"{0}\")", GodotSvgIdentifierWhatever);
				Console.WriteLine();
			}

			if (Program.IsDR2) {
			foreach (var Obiect in Map.AABB) {
				for (int i = 0; i < 4; i += 3 ) {
					string NodeName;
					Indexer++;
					NodeName = String.Concat(Obiect.Key, "_AABB_", Indexer);
					Console.WriteLine("[node name=\"{0}\" type=\"Marker3D\" parent=\"{1}\" unique_id={2}]",
						NodeName, Map.RoomName, Randomy.Next());
					Console.WriteLine("transform = Transform3D({0}, 0, 0, 0, {1}, 0, 0, 0, {2}, {3}, {4}, {5})",
						1, 1, 1, Obiect.Value.SixFloats[i], Obiect.Value.SixFloats[i+1], Obiect.Value.SixFloats[i+2]);
					Console.WriteLine("gizmo_extents = 100.0");
					Console.WriteLine();
					// creating a bilboarded sprite
					Console.WriteLine("[node name=\"Sprite3D\" type=\"Sprite3D\" parent=\"{0}/{1}\" unique_id={2}]",
						Map.RoomName, NodeName, Randomy.Next());
					Console.WriteLine("pixel_size = 0.5");
					Console.WriteLine("billboard = 2");
					Console.WriteLine("texture = ExtResource(\"{0}\")", GodotSvgIdentifierWhatever);
					Console.WriteLine();
				}
			}

			if (Map.Colissions.Verticies != null) {
				foreach (Vertex vertex in Map.Colissions.Verticies) {
					string NodeName;
					Indexer++;
					NodeName = String.Concat(Map.RoomName, "_Vertex_", Indexer);
					Console.WriteLine("[node name=\"{0}\" type=\"Marker3D\" parent=\"{1}\" unique_id={2}]",
						NodeName, Map.RoomName, Randomy.Next());
					Console.WriteLine("transform = Transform3D({0}, 0, 0, 0, {1}, 0, 0, 0, {2}, {3}, {4}, {5})",
						1, 1, 1, vertex.Pos[0], vertex.Pos[1], vertex.Pos[2]);
					Console.WriteLine("gizmo_extents = 100.0");
					Console.WriteLine();
					// creating a bilboarded sprite
					Console.WriteLine("[node name=\"Sprite3D\" type=\"Sprite3D\" parent=\"{0}/{1}\" unique_id={2}]",
						Map.RoomName, NodeName, Randomy.Next());
					Console.WriteLine("pixel_size = 0.5");
					Console.WriteLine("billboard = 2");
					Console.WriteLine("texture = ExtResource(\"{0}\")", GodotSvgIdentifierWhatever);
					Console.WriteLine();
				}
			}
			}

			}
		}

		public static void LazyGodotPrint(List<RoomV3> Everything) {
			Random Randomy = new Random();
			int Indexer = 0;
			
			foreach (RoomV3 Map in Everything) { // lol
			Console.WriteLine("[node name=\"{0}\" type=\"Node\" parent=\".\" unique_id={1}]", Map.RoomName, Randomy.Next());
			Console.WriteLine();

			foreach (FurnitureV3 Object in Map.Places) {
				Indexer++;
				string NodeName;
				if (Object.ObjectName != null) {
					NodeName = Object.ObjectName;	// DR2 ONLY
				} else {
					NodeName = String.Concat(Enum.GetName(typeof(FurnitureTypesV3), Object.Type), "_Node_", Indexer);
				}
				
				Console.WriteLine("[node name=\"{0}\" type=\"Marker3D\" parent=\"{1}\" unique_id={2}]",
					NodeName, Map.RoomName, Indexer * 100);
				Console.WriteLine("transform = Transform3D({0}, 0, 0, 0, {1}, 0, 0, 0, {2}, {3}, {4}, {5})",
					// TODO: temp
					1, 1, 1, Object.X, Object.Y, Object.Z);
				Console.WriteLine("gizmo_extents = 100.0");
				Console.WriteLine();
				// creating a bilboarded sprite
				Console.WriteLine("[node name=\"Sprite3D\" type=\"Sprite3D\" parent=\"{0}/{1}\" unique_id={2}]",
					Map.RoomName, NodeName, Randomy.Next());
				Console.WriteLine("pixel_size = 0.5");
				Console.WriteLine("billboard = 2");
				Console.WriteLine("texture = ExtResource(\"{0}\")", GodotSvgIdentifierWhatever);
				Console.WriteLine();
			}
			}
		}
	}
}