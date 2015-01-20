using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class PanelHover : MonoBehaviour
{
		public Color BaseColor;
		public Color OverColor;
		Image MyImage;
	
		// Use this for initialization
		void Start ()
		{
				MyImage = GetComponent<Image> ();
				BaseColor = MyImage.color;
		}
	
		public void Hover ()
		{
				MyImage.color = OverColor;
		}
		
		public void StopHover ()
		{
				MyImage.color = BaseColor;
		}
		
}
