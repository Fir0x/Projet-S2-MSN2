using UnityEngine;
using UnityEngine.UI;

public class VerticalSlider : MonoBehaviour
{
    public void ChangeCamPosition()
    {
        MapController.mapScript.MoveVertical(gameObject.GetComponent<Slider>().value);
    }
}
