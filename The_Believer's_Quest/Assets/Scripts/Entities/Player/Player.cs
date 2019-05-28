using System;
using System.Collections.Generic;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
//Nicolas I, Nicolas L et Sarah
[Serializable]
public class Player : MovingObject
{
    [SerializeField] private GameObject camera;

    private Rigidbody2D rigid;  //utile pour déplacement glace

    [SerializeField] private PlayerAsset playerAsset;
    [SerializeField] private GameObject ui;
    private UIController uiController;

    private Weapon weapon;
    
    private List<Room> listRoom;
    private Animator animator;
    private int animMoveHashID = Animator.StringToHash("Move");
    private int animDirectionHashID = Animator.StringToHash("Direction");
    private int animDashID = Animator.StringToHash("Dash");
    private int animDeathID = Animator.StringToHash("Death");

    private float moveX;
    private float moveY;
    private Vector2 firstPos;

    public GameObject Camera { get => camera; set => camera = value; }
    public PlayerAsset PlayerAsset { get => playerAsset; set => playerAsset = value; }
    public GameObject UI { get => ui; set => ui = value; }

    public Vector2 GetPos()
    {
        return transform.position;
    }

    public Vector2 GetFirstPos()
    {
        return firstPos;
    }

    public void SetLife(int value)
    {
        playerAsset.Hp = value;
        uiController.changeHp.Invoke();
    }

    public void SetEffect(int value)
    {
        playerAsset.EffectValue = value;
        uiController.changeEffect.Invoke();
    }
    
    public void Attack()
    {
        weapon.shot = true;
    }

    public void Dash()
    {
        throw new NotImplementedException();
    }

    private void Awake()
    {
        firstPos = this.transform.position;

        animator = GetComponent<Animator>();
        uiController = ui.GetComponent<UIController>();

        weapon = GetComponentInChildren<Weapon>();
        weapon.Init(uiController, PlayerAsset.WeaponsList[0], playerAsset);
    }

    public void MoveUp()
    {
        animator.SetInteger(animDirectionHashID, 0);
        animator.SetTrigger(animMoveHashID);
        if (this.Collision(transform.position, 0, 1, playerAsset.Speed))
        {
            this.transform.Translate(0, moveY, 0);
        }
    }

    public void MoveRight()
    {
        animator.SetInteger(animDirectionHashID, 1);
        animator.SetTrigger(animMoveHashID);
        if (this.Collision(transform.position, 1, 0, playerAsset.Speed))
        {
            this.transform.Translate(moveX, 0, 0);
        }
    }

    public void MoveDown()
    {
        animator.SetInteger(animDirectionHashID, 2);
        animator.SetTrigger(animMoveHashID);
        if (this.Collision(transform.position, 0, -1, playerAsset.Speed))
        {
            this.transform.Translate(0, -moveY, 0);
        }
    }

    public void MoveLeft()
    {
        animator.SetInteger(animDirectionHashID, 3);
        animator.SetBool(animMoveHashID, true);
        if (this.Collision(transform.position, -1, 0, playerAsset.Speed))
        {
            this.transform.Translate(-moveX, 0, 0);
        }
    }

    private void FixedUpdate()
    {
        moveX = PlayerAsset.Speed * Time.deltaTime;
        moveY = PlayerAsset.Speed * Time.deltaTime;

        //déplacement et collision
        if (Input.anyKey)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                animator.SetTrigger(animDeathID);
            }
            if (Input.GetButton("Dash"))
            {
                animator.SetTrigger(animDashID);
            }
        }
        else
            animator.SetBool(animMoveHashID, false);
        
        Camera.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.transform.position.z);

        //déplacement honnete pour niveau glace

        /*transform.Translate(moveX, moveY, 0f);   
        
        if(Input.GetKey( (KeyCode.LeftArrow)))
        {
            this.rigid.AddForce(new Vector2(-1, 0) * moveSpeed * Time.deltaTime);
        }
        else if(Input.GetKey( (KeyCode.RightArrow)))
        {
            this.rigid.AddForce(new Vector2 (1, 0) * moveSpeed * Time.deltaTime);
        }
        else if(Input.GetKey( (KeyCode.UpArrow)))
        {
            this.rigid.AddForce(new Vector2 (0, 1) * moveSpeed * Time.deltaTime);
        }
        else if(Input.GetKey( (KeyCode.DownArrow)))
        {
            this.rigid.AddForce(new Vector2 (0, -1) * moveSpeed * Time.deltaTime);
        }*/

        
        
        
        //faire spawner les ennemis
        
    }
}



