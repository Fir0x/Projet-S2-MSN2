using UnityEngine;
using UnityEngine.UI;
//Nicolas I
public class VerticalSlider : MonoBehaviour
{
    public void ChangeCamPosition()
    {
        MapController.mapInstance.MoveVertical(gameObject.GetComponent<Slider>().value);
    }
}
