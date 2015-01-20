using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

namespace AC
{
		public class NavigationBar : MonoBehaviour
		{
				public Text ColonistsValueText;
				public Text DomesValueText;
				
				// Use this for initialization
				void Awake ()
				{
						Game.Instance.Domes.ListChangedEvent += HandleListChangedEvent;
						Game.Instance.Colonists.ListChangedEvent += HandleColonistsChangedEvent;
				}

				void HandleColonistsChangedEvent (object sender, EventArg<ObservedList<Colonist>> e)
				{
						UpdateCapLabel ();
				}

				void HandleListChangedEvent (object sender, EventArg<ObservedList<Dome>> e)
				{
						UpdateCapLabel ();
				}
				
				void Start ()
				{
						UpdateCapLabel ();
				}

				void UpdateCapLabel ()
				{
						int capacity = 0;
						int colonists = 0;
						foreach (Dome dome in Game.Instance.Domes) {
			
								capacity += dome.Capacity;
								colonists += dome.ColonistsInDome ();
			
						}
			
						ColonistsValueText.text = string.Format ("({0}/{1})", colonists, capacity);
						DomesValueText.text = string.Format ("{0} Domes", Game.Instance.Domes.Count);
				}
				
				// Update is called once per frame
				void Update ()
				{
	
				}
		}
		
}