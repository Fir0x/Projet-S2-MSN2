using UnityEngine;
using UnityEngine.UI;
//Sarah
public class DisappearSlider : MonoBehaviour
{
    public void Disappear(Slider slider)
    {
        if (slider.value == 0)
        {
            slider.gameObject.transform.Find("Fill Area").gameObject.SetActive(false);
        }
        else
        {
            if (slider.gameObject.transform.Find("Fill Area").gameObject.activeInHierarchy == false)
                slider.gameObject.transform.Find("Fill Area").gameObject.SetActive(true);
        }
    }
}
