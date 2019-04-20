using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyboardManager : MonoBehaviour
{
    private Player player;

    private bool upKey;
    private bool downKey;
    private bool leftKey;
    private bool rightKey;
    private bool interactKey;
    private bool useKey;

    UnityEvent moveUp;
    UnityEvent moveRight;
    UnityEvent moveDown;
    UnityEvent moveLeft;
    UnityEvent interact;
    UnityEvent use;

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
    }
}
