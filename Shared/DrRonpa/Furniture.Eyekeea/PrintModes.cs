using System.Text.Json;
using DanganFurniture.Enums;
using DanganFurniture.Headers;

namespace DanganFurniture.PrintModesClass {
	public class Print {
		public static void JsonSerializedPrint(List<Room> Everything) {
			Console.WriteLine(
				JsonSerializer.Serialize(Everything,
				new JsonSerializerOptions{IncludeFields = true, WriteIndented = true})
			);
		}

		public static void LazyGodotPrint(List<Room> Everything) {
			Random Randomy = new Random();
			string GodotSvgIdentifierWhatever = "1_awcjp";
			int Indexer = 0;
			
			foreach (Room Map in Everything) { // lol
			Console.WriteLine("[node name=\"{0}\" type=\"Node\" parent=\".\" unique_id={1}]", Map.RoomName, Randomy.Next());
			Console.WriteLine();

			foreach (Furniture Object in Map.Objects) {
				Indexer++;
				string NodeName;
				if (Object.ObjectName != null) {
					NodeName = Object.ObjectName;	// DR2 ONLY
				} else {
					NodeName = String.Concat(Enum.GetName(typeof(FurnitureTypes), Object.Type), "Node", Indexer);
				}
				// TODO: Godot.Transform3D WHY DID THEY HAVE TO MAKE A CLASS NOT SUPPORTED HERE
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
				// ALTERNATIVE creating a box because it's impossible to see these on a 4K monitor
				//Console.WriteLine("[node name=\"CSGBox3D\" type=\"CSGBox3D\" parent=\"{0}\" unique_id={1}]",
				// 	NodeName, Randomy.Next());
				//Console.WriteLine("size = Vector3(100, 100, 100)");
				Console.WriteLine();
			}

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
			}
		}
	}
}