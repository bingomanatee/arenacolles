using UnityEngine;
using System.Collections;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;

namespace AC
{
		public class ColonistCollection : ObservedList<Colonist>
		{
		
				public Colonist AddColonist (Dome dome = null)
				{
						Colonist colonist = new Colonist (dome);
						Add (colonist);
						return colonist;
				}
		
				public new void Add (Colonist item)
				{
						base.Add (item);
				}
		
		
		}

}