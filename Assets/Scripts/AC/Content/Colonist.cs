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

				public Dome MyDome {
						get;
						set;
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
		
				public string ToString ()
				{
						return "Colonist " + ColonistName;
				}
				
		}

}