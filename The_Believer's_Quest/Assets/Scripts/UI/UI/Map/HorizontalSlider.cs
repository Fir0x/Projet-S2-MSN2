using UnityEngine;
using UnityEngine.UI;

public class HorizontalSlider : MonoBehaviour
{
    public void ChangeCamPosition()
    {
        MapController.mapInstance.MoveHorizontal(gameObject.GetComponent<Slider>().value);
    }
}
