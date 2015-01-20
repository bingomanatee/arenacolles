using UnityEngine;
using System.Collections;

namespace AC
{
  public class DomePanelCloseButton : MonoBehaviour
  {
    public DomePanel Panel;

    void OnMouseUpAsButton ()
    {
				
      Panel.Close ();
				
    }
  }

}