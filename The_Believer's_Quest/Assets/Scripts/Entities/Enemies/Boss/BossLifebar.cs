using UnityEngine;
using UnityEngine.UI;
//Nicolas I
public class BossLifebar : MonoBehaviour
{
    private Slider lifeBar;


    public void SetMaxValue(float maxHP)
    {
        lifeBar = UIController.uIController.transform.Find("Data UI").Find("Boss Health").GetComponent<Slider>();
        lifeBar.maxValue = maxHP;
        lifeBar.value = maxHP;
        lifeBar.gameObject.SetActive(true);
    }

    public void SliderDisappear()
    {
        lifeBar.gameObject.SetActive(false);
    }

    public void SetValue(float HP)
    {
        lifeBar.value = HP;
    }
}