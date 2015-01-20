using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace AC
{
		public class ColonistMenuItem : MonoBehaviour
		{
		
				public GameObject Panel;
				public Text Value;
		
				// Use this for initialization
				void Start ()
				{
						Game.Instance.ActiveColonistEvent += HandleActiveColonistEvent;
				}

				void HandleActiveColonistEvent (object sender, EventArg<Colonist> e)
				{
						Debug.Log ("Active Colonist set to " + e.CurrentValue);
						Panel.SetActive (e.CurrentValue != null);
						
						Value.text = (e.CurrentValue == null) ? "" : e.CurrentValue.ColonistName;
				}
	
				// Update is called once per frame
				void Update ()
				{
	
				}
		}
}