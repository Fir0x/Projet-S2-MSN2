using UnityEngine;
using UnityEngine.UI;

public class HorizontalSlider : MonoBehaviour
{
    public void ChangeCamPosition()
    {
        MapController.mapScript.MoveHorizontal(gameObject.GetComponent<Slider>().value);
    }
}
