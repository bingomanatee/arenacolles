using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace AC
{
		public class DomePanel : MonoBehaviour
		{
		
				public GameObject Panel;
				public Text Location;
				public Text Type;
				public Text CapLabel;
				public ColonistListItem[] Labels = new ColonistListItem[4];
		
				void Start ()
				{
						Panel.SetActive (false);
				}
		

				// Update is called once per frame
				void Awake ()
				{
//						Debug.Log ("Initializing DomePanel");
						Game.Instance.GameState.StateChangedEvent += OnGameStateChangedHandler;
						Game.Instance.Domes.ActiveDomeEvent += HandleActiveDomeEvent;
				}

				void UpdateCapLabel ()
				{
						CapLabel.text = CapacityString ();
				}

				void UpdateLocationText (Dome dome)
				{
						if ((dome != null) && dome.Location)
								Location.text = string.Format ("({0}, {1})", dome.Location.I, dome.Location.J);
						else
								Location.text = "(unknown)";
				}

				void UpdateTypeText (Dome dome)
				{
						if (dome != null) 
								Type.text = dome.DomeType.ToString ();
				}

				void HandleActiveDomeEvent (object sender, EventArg<Dome> data)
				{
						UpdateLocationText (data.CurrentValue);
						UpdateTypeText (data.CurrentValue);
						if (data.CurrentValue != null) {
								UpdateCapLabel ();
						} else {
								CapLabel.text = "";
						}
				}

				void OnGameStateChangedHandler (StateChange change)
				{
						Debug.Log ("DOME PANEL Changing panel state based on " + change.state);
						Panel.SetActive (change.state == Game.STATE_DOME_PANEL);
				}

				public void Close ()
				{
						Game.Instance.GameState.Change (Game.STATE_BASE);
				}

				public void AddColonist ()
				{
						if (Game.Instance.Domes.ActiveDome != null) {
								Game.Instance.Domes.ActiveDome.AddColonist ();
								RenderColonists ();
						} else {
								Debug.Log ("No Dome to add to");
						}
						UpdateCapLabel ();
				}

				void RenderColonists ()
				{
						foreach (ColonistListItem cli in Labels) {
								cli.gameObject.SetActive (false);
						}
						if (Game.Instance.Domes.ActiveDome != null) {
								int i = 0;
								foreach (Colonist c in Game.Instance.Domes.ActiveDome.Colonists) {
										if (i >= Labels.Length)
												break;
//										Debug.Log (string.Format ("Showing colonist {0}", c.ToString ()));
										Labels [i].Colonist = c;
										Labels [i].gameObject.SetActive (true);
										++i;
								}
						}
				}

				string CapacityString ()
				{
						string s = "";
						if (Game.Instance.Domes.ActiveDome != null) {
								s = string.Format ("({0}/{1})",
								 Game.Instance.Domes.ActiveDome.Colonists.Count, Game.Instance.Domes.ActiveDome.Capacity);

						}
//						Debug.Log ("capacity is " + s);
						return s;
				}
		}
  
}