using UnityEngine;
using System.Collections;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;

namespace AC
{
		public class DomeCollection : ObservedList<Dome>
		{
		
				Dome activeDome;
		
				public Dome ActiveDome {
						get {
								return activeDome;
						}
			
						set {
								activeDome = value;
								EventHandler<EventArg<Dome>> handler = ActiveDomeEvent;
								if (handler != null)
										handler (this, new EventArg<Dome> (activeDome));
						}
				}
    
				public event EventHandler<EventArg<Dome>> ActiveDomeEvent;
		
				public Dome AddDome (TerrainHex location)
				{
						foreach (Dome oldDome in this) {
								if (oldDome.Location == location) {
										Debug.Log ("Attempting to create two domes at the same location");
										return null;
								}
				
						}
				
						Dome dome = new Dome (location);
						Add (dome);
						return dome;
				}
				
				public new void Add (Dome item)
				{
						if (!item.Location)
								throw new System.Exception ("Attempt to add dome with no location");
						Debug.Log ("adding Dome at " + item.ToString ());
						base.Add (item);
				}
		}

}