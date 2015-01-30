using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AC
{
		public class Colonist
		{
		
#region constructor

				public string ColonistName;
				
				void Init ()
				{
						if (ColonistNameBank.Count < 1)
								ReadAstronauts ();
						ColonistName = ColonistNameBank [0];
						ColonistNameBank.RemoveAt (0);
				}
		
				public Colonist ()
				{
						Init ();
				}

				public Colonist (Dome dome)
				{
						Init ();
						MyDome = dome;
				}

		#endregion		

				Dome myDome;

				public Dome MyDome {
						get {
								return myDome;
						}
						set {
								myDome = value;
								Location = myDome.Location;
						}
				}
				
		#region names
		
				string astronautFile { get { return Application.dataPath + "/Resources/astronauts.txt"; } }
		
				static List<string> ColonistNameBank = new List<string> ();
		
				void ReadAstronauts ()
				{
			
						string line;
						System.IO.StreamReader file = new System.IO.StreamReader (astronautFile);
						while ((line = file.ReadLine()) != null) {
								if (line.Length > 2)
										ColonistNameBank.Insert (Random.Range (0, ColonistNameBank.Count), line);
						}
			
						file.Close ();
			
				}
		
		#endregion		

		#region location
				TerrainHex location;

				public TerrainHex Location {
						get {
								return location;
						}
						set {
								location = value;
								ColonistGraphic.transform.position = value.transform.position;
								if (location != null) {
										location.SetExplored (true, true);
								}
								Terrain.Instance.UpdateFeet ();
						}
				}
		
				public string ToString ()
				{
						return "Colonist " + ColonistName;
				}
				
				public bool IsAdjcentTo (TerrainHex targetHex)
				{
						if (targetHex == null)
								return false;
						if (Location == null)
								return false;
			
						foreach (TerrainHex hex in Location.Neighbors) {
								if (hex == targetHex)
										return true;
						}
			
						return false;
				}
		
#endregion

				GameObject colonistGraphic;

				public GameObject ColonistGraphic {
						get {
								if (colonistGraphic == null)
										colonistGraphic = (GameObject)GameObject.Instantiate ((GameObject)Resources.Load ("Colonist"));
								return colonistGraphic;
						}
				}
		}

}