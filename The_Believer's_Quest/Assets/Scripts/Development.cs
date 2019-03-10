using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Development : MonoBehaviour
{
    public GameObject playerGO;
    private Player player;
    public Slider effectController;

    private void EffectGaugeControl()
    {
        player.SetEffectValue((int) effectController.value);
        player.SetMaxEffectValue((int) effectController.maxValue);
    }

    public void Start()
    {
        player = playerGO.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        EffectGaugeControl();
    }
}
