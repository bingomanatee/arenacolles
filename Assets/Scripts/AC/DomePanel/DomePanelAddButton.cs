using UnityEngine;
using System.Collections;

namespace AC
{
		public class DomePanelAddButton : MonoBehaviour
		{

				public DomePanel Panel;
		
				void Start ()
				{
						if (Panel == null)
								Panel = GetComponentInParent<DomePanel> ();
				}
		
				void OnMouseUpAsButton ()
				{
			
						Panel.AddColonist ();
			
				}
		}

}