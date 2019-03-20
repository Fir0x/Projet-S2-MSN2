using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Development : MonoBehaviour
{
    public GameObject playerGO;
    private Player player;
    public Slider effectController;
    public Slider goldController;
    public Slider hpController;

    private void InterfaceControl()
    {
        player.SetEffectValue((int) effectController.value);
        player.SetMaxEffectValue((int) effectController.maxValue);
        player.SetGold((int)goldController.value);
        player.SetHP((int)hpController.value);
        player.SetMaxHP((int)hpController.maxValue);
    }

    public void Start()
    {
        player = playerGO.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        InterfaceControl();
    }
}
