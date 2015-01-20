using UnityEngine;
using System.Collections;
using System;

namespace AC
{
		public class StateListItem
		{

				public string name;
				StateList list;
		
				public StateListItem (string n, StateList l)
				{
						name = n;
						list = l;
				}
		
// this constructor only exists for testing; in production, items should always be list aware
				public StateListItem (string n)
				{
						name = n;
				}

				public bool Equals (string otherName)
				{
						try {
								return otherName.ToLower () == name.ToLower ();
						} catch (NullReferenceException nex) {
								Debug.Log ("Cannot do Equals with " + name + " and " + otherName);
								return false;
						}
				}

				public override string ToString ()
				{
						return name;
				} 

		}
}