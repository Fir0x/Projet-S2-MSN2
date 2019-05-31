using UnityEngine;
using UnityEngine.UI;

public class HorizontalScrollbar : MonoBehaviour
{
    public void ChangeCamPosition()
    {
        MapController.mapScript.MoveHorizontal(gameObject.GetComponent<Scrollbar>().value);
    }
}
