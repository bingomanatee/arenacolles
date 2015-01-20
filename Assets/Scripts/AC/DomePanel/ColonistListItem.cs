using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace AC
{
		public class ColonistListItem : MonoBehaviour
		{

				public Text NameLabel;
				Colonist colonist;
	
				public Colonist Colonist {
						get {
								return colonist;
						}
						set {
								colonist = value;
								NameLabel.text = (colonist != null) ? colonist.ColonistName : "(none)";
						}
				}
		}

}