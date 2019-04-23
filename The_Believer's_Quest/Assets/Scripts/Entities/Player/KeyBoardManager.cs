using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyBoardManager : MonoBehaviour
{
    private Player player;
    private Weapon weapon;

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
        weapon = gameObject.GetComponent<Weapon>();

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
        moveDown.AddListener(player.MoveDown);

        if (moveLeft == null)
        {
            moveLeft = new UnityEvent();
        }
        moveLeft.AddListener(player.MoveLeft);
        if (shot == null)
        {
            shot = new UnityEvent();
        }
        shot.AddListener(weapon.Shot);
        
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveUp.Invoke();
        }
        
        if (Input.GetKey(KeyCode.LeftArrow))
            moveLeft.Invoke();
        
        if (Input.GetKey(KeyCode.DownArrow))
            moveDown.Invoke();
        
        if (Input.GetKey(KeyCode.RightArrow))
            moveRight.Invoke();
        if (Input.GetKey(KeyCode.Mouse0))
            shot.Invoke();
    }
}

