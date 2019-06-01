using UnityEngine;
using UnityEngine.UI;

public class BossLifebar : MonoBehaviour
{
    private Slider lifeBar;

    public void Start()
    {

    }

    public void SetMaxValue(float maxHP)
    {
        lifeBar = UIController.uIController.transform.FindChild("Data UI").FindChild("Boss Health").GetComponent<Slider>();
        lifeBar.maxValue = maxHP;
        lifeBar.value = maxHP;
        lifeBar.gameObject.SetActive(true);
    }

    public void SliderAppear()
    {
        print(lifeBar);
         lifeBar.gameObject.SetActive(false);
    }

    public void SetValue(float HP)
    {
        lifeBar.value = HP;
    }
}