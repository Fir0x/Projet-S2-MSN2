using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//Sarah et Nicolas I
public class KeyboardManager : MonoBehaviour
{
    private Player player;

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
        shot.AddListener(player.Attack);
        
    }

    private void FixedUpdate()
    {
        print("KeyBoard update");
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
            print("Attack");
        shot.Invoke();
    }
}