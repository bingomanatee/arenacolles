using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GridFramework;

namespace AC
{
		[RequireComponent (typeof(GFHexGrid))]
		public class Terrain : MonoBehaviour
		{
				public static Terrain Instance = null;
				public const int Rows = 10;
				public const int Cols = 10;
				public const int Heights = 3;
				public TerrainHex[,] Hexes;
				public GameObject Hex;
				const string TERRAIN_HEX = "TerrainHex";
				public Color[] HeightColors = new Color[Heights];

				#region loop
				// Use this for initialization
				void Awake ()
				{
						Instance = this;
						Hexes = new TerrainHex[Rows * 2 + 1, Cols * 2 + 1];
						if (!Hex)
								Hex = (GameObject)Resources.Load (TERRAIN_HEX);
								
						for (int i = MinI (); i <= MaxI (); ++i)
								for (int j = MinJ (); j <= MaxJ (); ++j)
										AddHex (i, j);	
						//	Debug.Log ("Created Hexes");
						GetHex (0, 0).AddDome ();
						UpdateTerrain ();	
				}
				
				void Start ()
				{
						Instance = this;
				}
	
				// Update is called once per frame
				void Update ()
				{
	
				}
#endregion

				public GFHexGrid hexGrid {
						get { return GetComponent<GFHexGrid> (); }
				}

		#region extent
				int MinJ ()
				{
						return -Cols;
				}

				int MaxJ ()
				{
						return Cols;
				}

				int MinI ()
				{
						return -Rows;
				}

				int MaxI ()
				{
						return Rows;
				}
#endregion

				void AddHex (int i, int j)
				{
						int height = (int)Random.Range (0, Heights);
						//Debug.Log (string.Format ("Terrain Adding Hex ({0}, {1}); height = {2} ({3} .. {4})", i, j, height, 0, Heights));
						GameObject newHex = (GameObject)Instantiate (Hex);
						TerrainHex hex = newHex.GetComponent<TerrainHex> ();
						hex.Index = new Vector3 (i, height, j);
						hex.InTerrain = this;	
						hex.Initialized = true;
						
						newHex.transform.SetParent (transform, true);
						
						Hexes [i + Rows, j + Cols] = hex;
				}

				public TerrainHex GetHex (int i, int j)
				{
						if (i == Mathf.Clamp (i, MinI (), MaxI ()) && j == Mathf.Clamp (j, MinJ (), MaxJ ())) {
								//Debug.Log (string.Format ("getting hex {0},{1}", i, j));
								return Hexes [i - MinI (), j - MinJ ()];
						} else
								return null;
				}

				public void UpdateTerrain ()
				{
						Debug.Log (string.Format ("Updating Terrain with {0} domes", Game.Instance.Domes.Count));
						foreach (TerrainHex hex in Terrain.Instance.Hexes) {
								hex.Exposed = false;
						}					
					
						foreach (Dome dome in Game.Instance.Domes) {
								if (dome.Location != null) {
//										Debug.Log ("Updating location " + dome.Location.ToString ());
										dome.Location.Exposed = true;
										// Debug.Log (string.Format ("Updating {0} neighbors", dome.Location.Neighbors.Length));
										foreach (TerrainHex neighbor in dome.Location.Neighbors)
												neighbor.Exposed = true;
								} else {
										Debug.Log ("ignoring unplaced dome");
								}
						}
				}
		}
}