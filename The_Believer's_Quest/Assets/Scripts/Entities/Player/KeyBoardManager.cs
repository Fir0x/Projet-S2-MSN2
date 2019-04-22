using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyboardManager : MonoBehaviour
{
    private Player player;
    private Weapon Weapon;

    private bool upKey;
    private bool downKey;
    private bool leftKey;
    private bool rightKey;
    private bool interactKey;
    private bool useKey;
    private bool shotKey;

    UnityEvent moveUp;
    UnityEvent moveRight;
    UnityEvent moveDown;
    UnityEvent moveLeft;
    UnityEvent interact;
    UnityEvent use;
    UnityEvent shot;

    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponent<Player>();

        upKey = Input.GetKey(KeyCode.UpArrow);
        downKey = Input.GetKey(KeyCode.DownArrow);
        leftKey = Input.GetKey(KeyCode.LeftArrow);
        rightKey = Input.GetKey(KeyCode.RightArrow);
        interactKey = Input.GetKey(KeyCode.E);
        useKey = Input.GetKey(KeyCode.Space);
        shotKey = Input.GetKey(KeyCode.Mouse0);

        if (moveUp == null)
        {
            moveUp = new UnityEvent();
        }
        moveUp.AddListener(player.MoveUp);

        if (moveRight == null)
        {
            moveRight = new UnityEvent();
        }
        moveRight.AddListener(player.MoveRight);

        if (moveDown == null)
        {
            moveDown = new UnityEvent();
        }
        moveDown.AddListener(player.MoveRight);

        if (moveLeft == null)
        {
            moveLeft = new UnityEvent();
        }
        moveLeft.AddListener(player.MoveLeft);
        if (shot == null)
        {
            shot = new UnityEvent();
        }
        shot.AddListener(Weapon.Shot);
        
    }
}

