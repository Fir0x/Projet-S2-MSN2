using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//Sarah et Nicolas I
public class KeyBoardManager : MonoBehaviour
{
    private Player player;
    private Enemy enemy;

    UnityEvent moveUp;
    UnityEvent moveRight;
    UnityEvent moveDown;
    UnityEvent moveLeft;
    UnityEvent interact;
    UnityEvent useLeft;
    UnityEvent useRight;
    UnityEvent shot;

    [SerializeField] private GameObject developmentTool;

    public GameObject DevelopmentTool { get => developmentTool; set => developmentTool = value; }

    // Start is called before the first frame update
    void Awake()
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

        if (useLeft == null)
        {
            useLeft = new UnityEvent();
        }

        if (useRight == null)
        {
            useRight = new UnityEvent();
        }

        if (shot == null)
        {
            shot = new UnityEvent();
        }
        shot.AddListener(player.Attack);
        
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.Backspace) && Input.GetKeyDown(KeyCode.M))
            developmentTool.SetActive(!developmentTool.activeInHierarchy); //Make development toll appear/disappear

        if (Input.GetKey(KeyCode.UpArrow))
            moveUp.Invoke();
        
        if (Input.GetKey(KeyCode.LeftArrow))
            moveLeft.Invoke();
        
        if (Input.GetKey(KeyCode.DownArrow))
            moveDown.Invoke();
        
        if (Input.GetKey(KeyCode.RightArrow))
            moveRight.Invoke();
        
        if (Input.GetKey(KeyCode.Mouse0))
        {
            print("Attack");
            shot.Invoke();
        }
    }

    public void AddToInteract(UnityAction call)
    {
        interact.AddListener(call);
    }

    public void RemoveFromInteract(UnityAction call)
    {
        interact.RemoveListener(call);
    }
}