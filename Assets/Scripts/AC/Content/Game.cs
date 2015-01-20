using UnityEngine;
using System.Collections;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;

namespace AC
{
		public class Game
		{
				public static Game Instance = new Game ();
				public DomeCollection Domes = new DomeCollection ();
				public ColonistCollection Colonists = new ColonistCollection ();
				
				
		#region GameState;
				public State GameState;
				public const string STATE_BASE = "base";
				public const string STATE_DOME_PANEL = "DomePanel";
#endregion

				public static string Mode {
						get{ return Instance.GameState.state; }
				}

				int DomeCount {
						get { return Domes.Count; }
				}

				public Dome AddDome (TerrainHex loc)
				{
						if (loc == null)
								throw new System.Exception ("Attempt to add dome with no location");
						Debug.Log ("Game addDome");
						return Domes.AddDome (loc);
				}

				public object Capacity {
						get {
								int i = 0;
								foreach (Dome dome in Domes)
										i += dome.Capacity;
								return i;
						}
				}

				public Colonist NewColonist ()
				{
						Colonist c = new Colonist ();
			
			
						return c;
				}
				
				public Game ()
				{
						GameState = StateList.Init ("gameState", STATE_BASE, STATE_DOME_PANEL);
						GameState.Change (STATE_BASE);
						
						TerrainHex.SelectedHexEvent += HandleSelectedHexEvent;
				}

				void HandleSelectedHexEvent (object sender, EventArg<TerrainHex> e)
				{
						switch (GameState.state) {
			
						case Game.STATE_BASE: 
								if (e.CurrentValue == null) {
										Debug.Log ("No hex is current");
								} else if ((e.CurrentValue.HasDome)) {
										Debug.Log (string.Format ("state changed based on dome at {0}", e.CurrentValue.ToString ()));
										Domes.ActiveDome = e.CurrentValue.MyDome;
										GameState.Change (STATE_DOME_PANEL);
								} else {
										Debug.Log (string.Format ("clicked on non done {0}", e.CurrentValue.ToString ()));
								}
								break;
			
						}
				}		
    
		}
}