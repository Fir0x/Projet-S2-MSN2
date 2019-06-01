using UnityEngine;
using UnityEngine.UI;

public class VerticalSlider : MonoBehaviour
{
    public void ChangeCamPosition()
    {
        MapController.mapInstance.MoveVertical(gameObject.GetComponent<Slider>().value);
    }
}
