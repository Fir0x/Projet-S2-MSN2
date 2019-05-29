using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
//Nicolas I, Nicolas L et Sarah
[Serializable]
public class Player : MovingObject
{
    [SerializeField] private GameObject camera;

    private bool goLeft;
    private bool goUp;
    private bool goRight;
    private bool goDown;
    private bool testForDash;

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
        if (playerAsset.Hp <=0)
            print("Game Over");
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

    public void doDash()
    {
        if(goUp || goRight || goDown || goLeft)
            StartCoroutine(Dash());
    }

    IEnumerator Dash()
    {
        if (testForDash == true)
        {
            if (goUp && goRight)
            {
                Vector3 firstPos = this.transform.position;
                Vector3 lastPos = firstPos + new Vector3(0.8f, 0.8f, 0);
                Vector3 newPos = new Vector3(0, 0, 0);
                while (newPos.magnitude < (lastPos - firstPos).magnitude && Collision(transform.position, playerAsset.Speed, 1) && Collision(transform.position, playerAsset.Speed, 2))
                {                 
                    this.transform.Translate(0.1f, 0.1f, 0);
                    newPos = transform.position - firstPos;
                }
            }
            else if (goDown && goRight)
            {
                Vector3 firstPos = this.transform.position;
                Vector3 lastPos = firstPos + new Vector3(0.8f, -0.8f, 0);
                Vector3 newPos = new Vector3(0, 0, 0);
                while (newPos.magnitude < (lastPos - firstPos).magnitude && Collision(transform.position, playerAsset.Speed, 2) && Collision(transform.position, playerAsset.Speed, 3))
                {
                    this.transform.Translate(0.1f, -0.1f, 0);
                    newPos = transform.position - firstPos;
                }
            }
            else if (goUp && goLeft)
            {
                Vector3 firstPos = this.transform.position;
                Vector3 lastPos = firstPos + new Vector3(-0.8f, 0.8f, 0);
                Vector3 newPos = new Vector3(0, 0, 0);
                while (newPos.magnitude < (lastPos - firstPos).magnitude && Collision(transform.position, playerAsset.Speed, 0) && Collision(transform.position, playerAsset.Speed, 1))
                {
                    this.transform.Translate(-0.1f, 0.1f, 0);
                    newPos = transform.position - firstPos;
                }
            }
            else if (goDown && goLeft)
            {
                Vector3 firstPos = this.transform.position;
                Vector3 lastPos = firstPos + new Vector3(-0.8f, -0.8f, 0);
                Vector3 newPos = new Vector3(0, 0, 0);
                while (newPos.magnitude < (lastPos - firstPos).magnitude && Collision(transform.position, playerAsset.Speed, 1) && Collision(transform.position, playerAsset.Speed, 2))
                {
                    this.transform.Translate(-0.1f, -0.1f, 0);
                    newPos = transform.position - firstPos;
                }
            }
            else if (goUp)
            {
                Vector3 firstPos = this.transform.position;
                Vector3 lastPos = firstPos + new Vector3(0, 1.3f, 0);
                Vector3 newPos = new Vector3(0, 0, 0);
                while (newPos.magnitude < (lastPos - firstPos).magnitude && Collision(transform.position, playerAsset.Speed, 1))
                {
                    this.transform.Translate(0, 0.1f, 0);
                    newPos = transform.position - firstPos;
                }
            }
            else if (goRight)
            {
                Vector3 firstPos = this.transform.position;
                Vector3 lastPos = firstPos + new Vector3(1.3f, 0, 0);
                Vector3 newPos = new Vector3(0, 0, 0);
                while (newPos.magnitude < (lastPos - firstPos).magnitude && Collision(transform.position, playerAsset.Speed, 2))
                {
                    this.transform.Translate(0.1f, 0, 0);
                    newPos = transform.position - firstPos;
                }
            }
            else if (goDown)
            {
                Vector3 firstPos = this.transform.position;
                Vector3 lastPos = firstPos + new Vector3(0, -1.3f, 0);
                Vector3 newPos = new Vector3(0, 0, 0);
                while (newPos.magnitude < (lastPos - firstPos).magnitude && Collision(transform.position, playerAsset.Speed, 3))
                {
                    this.transform.Translate(0, -0.1f, 0);
                    newPos = transform.position - firstPos;
                }
            }
            else if (goLeft)
            {
                Vector3 firstPos = this.transform.position;
                Vector3 lastPos = firstPos + new Vector3(-1.3f, 0, 0);
                Vector3 newPos = new Vector3(0, 0, 0);
                while (newPos.magnitude < (lastPos - firstPos).magnitude && Collision(transform.position, playerAsset.Speed, 1))
                {
                    this.transform.Translate(-0.1f, 0, 0);
                    newPos = transform.position - firstPos;
                }
            }

            testForDash = false;
                
            yield return new WaitForSeconds(1.5f);
            testForDash = true;;
        }
    }

    private void Awake()
    {
        testForDash = true;

        firstPos = this.transform.position;

        animator = GetComponent<Animator>();
        uiController = ui.GetComponent<UIController>();
        
        weapon = GetComponentInChildren<Weapon>();
        weapon.Init(uiController, PlayerAsset.WeaponsList[0], playerAsset);
    }

    public void MoveUp()
    {
        goUp = true;

        animator.SetInteger(animDirectionHashID, 0);
        animator.SetTrigger(animMoveHashID);
        if (this.Collision(transform.position, playerAsset.Speed, 1))
        {
            this.transform.Translate(0, moveY, 0);
        }
    }

    public void StopMoveUp()
    {
        goUp = false;
    }

    public void MoveRight()
    {
        goRight = true;

        animator.SetInteger(animDirectionHashID, 1);
        animator.SetTrigger(animMoveHashID);
        if (this.Collision(transform.position, playerAsset.Speed, 2))
        {
            this.transform.Translate(moveX, 0, 0);
        }
    }

    public void StopMoveRight()
    {
        goRight = false;
    }

    public void MoveDown()
    {
        goDown = true;

        animator.SetInteger(animDirectionHashID, 2);
        animator.SetTrigger(animMoveHashID);
        if (this.Collision(transform.position, playerAsset.Speed, 3))
        {
            this.transform.Translate(0, -moveY, 0);
        }
    }

    public void StopMoveDown()
    {
        goDown = false;
    }

    public void MoveLeft()
    {
        goLeft = true;

        animator.SetInteger(animDirectionHashID, 3);
        animator.SetBool(animMoveHashID, true);
        if (this.Collision(transform.position, playerAsset.Speed, 0))
        {
            this.transform.Translate(-moveX, 0, 0);
        }
    }

    public void StopMoveLeft()
    {
        goLeft = false;
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
        }
        else
        {
            animator.SetBool(animMoveHashID, false);
        }
        
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

        
    }
}



