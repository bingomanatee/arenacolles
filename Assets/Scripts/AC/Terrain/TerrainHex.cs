using UnityEngine.UI;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace AC
{
		public class TerrainHex : MonoBehaviour
		{
				public GameObject TerrainGraphic;
				public GameObject DomeGraphic;
				public GameObject ShoeGraphic;
				bool hasDome = false;
				Vector3 index;
				bool exposed;
				public const float HeightRatio = 0.125f;
				static Color UnExposedColor = new Color (0.2f, 0.2f, 0.33f);
				public Text Label;
				public bool Initialized = false;
				public const float RADIUS = 1.736555f;
				public static TerrainHex selectedHex;
				Dome dome = null;

				public Dome MyDome {
						get {
								return dome;
						}
						set {
								dome = value;
						}
				}
		
				public Dome.DomeTypes DomeType {
						get {
								if (MyDome == null)
										return Dome.DomeTypes.None; 
								return MyDome.DomeType;
						}
				}

				public static TerrainHex SelectedHex {
						get {
								return selectedHex;
						}
						set {
								selectedHex = value;
								EventHandler<EventArg<TerrainHex>> handler = SelectedHexEvent;
								if (handler != null)
										handler (null, new EventArg<TerrainHex> (value));
						}
				}
				
				public static event EventHandler<EventArg<TerrainHex>> SelectedHexEvent;
				
				public bool Exposed {
						get {
								return exposed;
						}
						set {
								exposed = value;
								SetColor ();
						}
				}

				public bool HasDome {
						get {
								return MyDome != null;
						}
				}

				void UpdateDomeGraphic ()
				{
						DomeGraphic.SetActive (HasDome);
				}

				void UpdateShoeGraphic ()
				{
						ShoeGraphic.SetActive (false);
				}
				
				public void AddDome ()
				{
						Debug.Log ("TerrainHex adding dome");
						if (MyDome == null) {
								MyDome = Game.Instance.AddDome (this);
						}
						UpdateDomeGraphic ();
				}

				void PlaceMe ()
				{
						if (InTerrain) {
								transform.position = InTerrain.hexGrid.GridToWorld (new Vector3 (index.x, index.y * HeightRatio, index.z));
						}
				}

				Color color {
						get { return TerrainGraphic.renderer.material.color;}
						set { TerrainGraphic.renderer.material.color = value; }
				}

				void SetColor ()
				{
						if (InTerrain)
								color = Exposed ? InTerrain.HeightColors [(int)index.y] : UnExposedColor;
								
				}
				
				public Terrain inTerrain;

				public Terrain InTerrain {
						get {
								return inTerrain;
						}
						set {
								inTerrain = value;
								PlaceMe ();
								SetColor ();
						}
				}

#region loop
				// Use this for initialization
				void Awake ()
				{
						if (!TerrainGraphic)
								TerrainGraphic = transform.GetChild (0).gameObject;
				}
				
				void Start ()
				{
						UpdateDomeGraphic ();
						UpdateShoeGraphic ();
				}
	
				// Update is called once per frame
				void Update ()
				{
						
				}
				
#endregion
		
		#region index
				public Vector3 Index {
						get {
								return index;
						}
						set {
								index = value;
								PlaceMe ();
								Label.text = string.Format ("({0},{1})", index.x, index.z);
						}
				}
    
				public int I {
						get { 
								return (int)Index.x;
						}
				}
				
				public int J {
						get {
								return (int)Index.z;
						}
				}
			#endregion
				
				public string ToString ()
				{
						return string.Format ("<Hex {0}, {1}>", I, J);
				}

				int Distance (TerrainHex terrainHex)
				{
						if (terrainHex == null)
								throw new System.ArgumentNullException ("terrainHex");
						float distance = ((terrainHex.transform.position - transform.position).magnitude);
						// Debug.Log (string.Format ("Distance between {0} and {1} is {2}", ToString (), terrainHex.ToString (), distance / RADIUS));
						return Mathf.RoundToInt (distance / RADIUS);
				}

				public TerrainHex[] Neighbors {
						get {
								List<TerrainHex> neighbors = new List<TerrainHex> ();
								for (int i = I-1; i <= I + 1; ++i) {
										for (int j = J - 1; j <= J + 1; ++j) {
												if (InTerrain.GetHex (i, j) && InTerrain.GetHex (i, j).Distance (this) <= 1) {
														neighbors.Add (InTerrain.GetHex (i, j));
												}
										}
								}
								
								return neighbors.ToArray ();
						}
				}
				
		#region mouse 
		
				void OnMouseUpAsButton ()
				{
						//	Debug.Log (string.Format ("MouseUp on {0}", ToString ()));
						if (Game.Mode == Game.STATE_BASE)
								SelectedHex = this;
				}
		
		#endregion 
		
				public static bool operator == (TerrainHex a, TerrainHex b)
				{
						// If both are null, or both are same instance, return true.
						if (System.Object.ReferenceEquals (a, b)) {
								return true;
						}
			
						// If one is null, but not both, return false.
						if (((object)a == null) || ((object)b == null)) {
								return false;
						}
			
						return a.I == b.I && a.J == b.J;
				}
		
				public static bool operator != (TerrainHex a, TerrainHex b)
				{
						return !(a == b);
				}
		
		
		}
		
}