using UnityEngine;
using UnityEngine.UI;

public class VerticalScrollbar : MonoBehaviour
{
    public void ChangeCamPosition()
    {
        MapController.mapScript.MoveVertical(gameObject.GetComponent<Scrollbar>().value);
    }
}
