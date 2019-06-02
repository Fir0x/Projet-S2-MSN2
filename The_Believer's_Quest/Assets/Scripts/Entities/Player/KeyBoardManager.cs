using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

//Sarah et Nicolas I
public class KeyBoardManager : MonoBehaviour
{
    private Player player;
    private Enemy enemy;

    [SerializeField] private GameObject developmentTool;

    private bool isMapActive = false;

    public GameObject DevelopmentTool { get => developmentTool; set => developmentTool = value; }

    // Start is called before the first frame update
    void Awake()
    {
        player = gameObject.GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.Backspace) && Input.GetKeyDown(KeyCode.M))
            developmentTool.SetActive(!developmentTool.activeInHierarchy); //Make development toll appear/disappear

        if (Input.GetButton("Up"))
            player.MoveUp();

        if (Input.GetButtonUp("Up"))
            player.StopMoveUp();

        if (Input.GetButton("Left"))
            player.MoveLeft();

        if (Input.GetButtonUp("Left"))
            player.StopMoveLeft();

        if (Input.GetButton("Down"))
            player.MoveDown();

        if (Input.GetButtonUp("Down"))
            player.StopMoveDown();

        if (Input.GetButton("Right"))
            player.MoveRight();

        if (Input.GetButtonUp("Right"))
            player.StopMoveRight();

        if (!isMapActive && Input.GetButton("Attack") && SceneManager.GetActiveScene()!= SceneManager.GetSceneByName("MainMenu"))
            player.Attack();

        if (Input.GetButtonDown("Dash"))
            player.doDash();

        if (Input.GetButtonDown("Map"))
        {
            isMapActive = !isMapActive;
            UIController.uIController.ShowMap();
        }

        if (Input.GetButtonDown("Interact") && Player.instance.GetNearChest())
        {
            player.ActiveChestUI();
        }

        if (Input.GetButtonDown("Interact") && Player.instance.GetNearShop())
        {
            player.ActiveShopUI();
        }
    }
}