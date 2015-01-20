using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AC
{
		public class Dome
		{
		
		#region types
				public enum DomeTypes
				{
						None,
						Quarters,
						Storage,
						Factory
				}
				Dome.DomeTypes domeType = DomeTypes.Quarters;

				public Dome.DomeTypes DomeType {
						get {
								return domeType;
						}
						set {
								domeType = value;
						}
				}

				public int ColonistsInDome ()
				{
						return Colonists.Count;
				}
				
				// capacities for colonists
				Dictionary<DomeTypes, int> Capacities = new Dictionary<DomeTypes, int>{
			{DomeTypes.Quarters, 4},
			{DomeTypes.Storage, 0},
			{DomeTypes.Factory, 0}
		};

				public int Capacity {
						get { return Capacities [DomeType];}
				}
		
				
#endregion

				void Init ()
				{
						DomeID = ++DomeCount;
				}

				public int DomeCount = 0;
				public int DomeID;
				public ColonistCollection Colonists = new ColonistCollection ();
		
				public Dome ()
				{
						Init ();
				}

				public Dome (TerrainHex terrainHex) : base()
				{
						Init ();
						Location = terrainHex;
				}
		
		#region location
		
				public TerrainHex m_location;

				public TerrainHex Location {
						get {
								return m_location;
						}
						set {
								m_location = value;
								if (m_location != null)
										m_location.MyDome = this;
								//	Debug.Log ("Dome location set to " + (m_location == null ? "nothing" : m_location.ToString ()));	
								Terrain.Instance.UpdateTerrain ();
						}
				}
#endregion

				public string ToString ()
				{
						return string.Format ("Dome {0} at {1}", DomeID, (Location == null ? "nothing" : Location.ToString ()));
				}

				public void AddColonist ()
				{
						if (Colonists.Count < Capacity) {
								Game.Instance.Colonists.Add (Colonists.AddColonist (this));
								// note -- creating in local collection, and inserting into game collection
						}
				}
 
		}

}