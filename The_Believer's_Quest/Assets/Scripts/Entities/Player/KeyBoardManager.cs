using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

//Everyone
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
        {
            player.MoveUp();
        }

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
            player.canAttack = !player.canAttack;
        }

        if (Input.GetButtonDown("Interact") && Player.instance.GetNearShop())
        {
            player.ActiveShopUI();
            player.canAttack = !player.canAttack;
        }

        if (Input.GetAxis("Mouse ScrollWheel") != 0f) //Change weapon
        {
            player.transform.GetChild(0).GetComponent<Weapon>().SetWeapon(player.PlayerAsset.WeaponsList[1]);
        }

        if (Input.GetButtonDown("Item 1")) //Use item 1
        {
            if (player.PlayerAsset.ObjectsList[0] != null)
            {
                print("objet 1"); //DEBUG
                player.PlayerAsset.ObjectsList[0].GetComponent<Object>().ActiveChange();
                player.PlayerAsset.ObjectsList[0] = null;
            }
        }

        if (Input.GetButtonDown("Item 2")) //Use item 2
        {
            if (player.PlayerAsset.ObjectsList[1] != null)
            {
                print("objet 2"); //DEBUG
                player.PlayerAsset.ObjectsList[1].GetComponent<Object>().ActiveChange();
                player.PlayerAsset.ObjectsList[1] = null;
            }
        }
    }
}