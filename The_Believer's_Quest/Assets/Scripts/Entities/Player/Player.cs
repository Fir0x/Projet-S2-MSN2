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
    public static Player instance;
    [SerializeField] private GameObject camera;

    private bool goLeft;
    private bool goUp;
    private bool goRight;
    private bool goDown;

    private bool noForcedMove;
    private bool testForDash;
    private bool testForInvincibility;

    private bool nearChest;

    private Rigidbody2D rigid;  //utile pour déplacement glace

    [SerializeField] private PlayerAsset playerAsset;
    [SerializeField] private GameObject ui;
    [SerializeField] private GameObject gameover;

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

    public Vector3 GetPos()
    {
        return transform.position;
    }

    public Vector3 GetFirstPos()
    {
        return firstPos;
    }

    public void SetLife(float value)
    {
        print("aie!"); // DEBUG
        playerAsset.Invicibility = true;
        StartCoroutine(InvicibilityCoolDown());

        playerAsset.Hp = value;
        UIController.uIController.changeHp.Invoke();
        print(UIController.uIController.changeHp);
        if (playerAsset.Hp <= 0)
        {
            animator.SetTrigger(animDeathID);
            Time.timeScale = 1f;
            gameover.SetActive(false);
            print("Game Over");
        }
    }

    IEnumerator InvicibilityCoolDown()
    {
        yield return new WaitForSeconds(1);
        playerAsset.Invicibility = false;
    }


    public void SetEffect(int value)
    {
        playerAsset.EffectValue = value;
        UIController.uIController.changeEffect.Invoke();
    }
    
    public void Attack()
    {
        weapon.Attack();
    }

    public void doDash()
    {
        if((goUp || goRight || goDown || goLeft) && testForDash)
            StartCoroutine(Dash());
    }

    IEnumerator Dash()
    {
        if (testForDash == true)
        {
            if (goUp && goRight)
            {;
                Vector3 firstPos = this.transform.position;
                Vector3 lastPos = firstPos + new Vector3(0.8f, 0.8f, 0);

                StartCoroutine(Movement(lastPos, 8f));
            }
            else if (goDown && goRight)
            {
                Vector3 firstPos = this.transform.position;
                Vector3 lastPos = firstPos + new Vector3(0.8f, -0.8f, 0);

                StartCoroutine(Movement(lastPos, 8f));
            }
            else if (goUp && goLeft)
            {
                Vector3 firstPos = this.transform.position;
                Vector3 lastPos = firstPos + new Vector3(-0.8f, 0.8f, 0);

                StartCoroutine(Movement(lastPos, 8f));
            }
            else if (goDown && goLeft)
            {
                Vector3 firstPos = this.transform.position;
                Vector3 lastPos = firstPos + new Vector3(-0.8f, -0.8f, 0);

                StartCoroutine(Movement(lastPos, 8f));
            }
            else if (goUp)
            {
                Vector3 firstPos = this.transform.position;
                Vector3 lastPos = firstPos + new Vector3(0, 1.3f, 0);

                StartCoroutine(Movement(lastPos, 8f));
            }
            else if (goRight)
            {
                Vector3 firstPos = this.transform.position;
                Vector3 lastPos = firstPos + new Vector3(1.3f, 0, 0);

                StartCoroutine(Movement(lastPos, 8f));
            }
            else if (goDown)
            {
                Vector3 firstPos = this.transform.position;
                Vector3 lastPos = firstPos + new Vector3(0, -1.3f, 0);

                StartCoroutine(Movement(lastPos, 8f));
            }
            else if (goLeft)
            {
                Vector3 firstPos = this.transform.position;
                Vector3 lastPos = firstPos + new Vector3(-1.3f, 0, 0);

                StartCoroutine(Movement(lastPos, 8f));
            }
            playerAsset.Position = transform.position;

            testForDash = false;
                
            yield return new WaitForSeconds(2f);

            testForDash = true;
        }
    }

    private void Start()
    {
        instance = this;
        noForcedMove = true;
        weapon = GetComponentInChildren<Weapon>();
        weapon.Init(playerAsset.WeaponsList[0], playerAsset);
        playerAsset.Position = transform.position;
        playerAsset.Invicibility = false;
        testForDash = true;

        firstPos = transform.position;

        animator = GetComponent<Animator>();

        nearChest = false;
    }

    public void MoveUp()
    {
        if(noForcedMove)
        {
            goUp = true;

            animator.SetInteger(animDirectionHashID, 0);
            animator.SetTrigger(animMoveHashID);
            if (this.Collision(transform.position, playerAsset.Speed, 1))
            {
                this.transform.Translate(0, moveY, 0);
            }

            playerAsset.Position = transform.position;
        }
    }

    public void StopMoveUp()
    {
        goUp = false;
    }

    public void MoveRight()
    {
        if (noForcedMove)
        {
            goRight = true;

            animator.SetInteger(animDirectionHashID, 1);
            animator.SetTrigger(animMoveHashID);
            if (this.Collision(transform.position, playerAsset.Speed, 2))
            {
                this.transform.Translate(moveX, 0, 0);
            }

            playerAsset.Position = transform.position;
        }
    }

    public void StopMoveRight()
    {
        goRight = false;
    }

    public void MoveDown()
    {
        if (noForcedMove)
        {
            goDown = true;

            animator.SetInteger(animDirectionHashID, 2);
            animator.SetTrigger(animMoveHashID);
            if (this.Collision(transform.position, playerAsset.Speed, 3))
            {
                this.transform.Translate(0, -moveY, 0);
            }

            playerAsset.Position = transform.position;
        }
    }

    public void StopMoveDown()
    {
        goDown = false;
    }

    public void MoveLeft()
    {
        if (noForcedMove)
        {
            goLeft = true;

            animator.SetInteger(animDirectionHashID, 3);
            animator.SetBool(animMoveHashID, true);
            if (this.Collision(transform.position, playerAsset.Speed, 0))
            {
                this.transform.Translate(-moveX, 0, 0);
            }

            playerAsset.Position = transform.position;
        }
    }

    public void StopMoveLeft()
    {
        goLeft = false;
    }

    public void ForcedMovement(Vector3 direction)                   //makes player move without his consent
    {
        StartCoroutine(Movement(transform.position + direction * 0.5f, 3f));
    }

    IEnumerator Movement(Vector3 finalPos, float speed)
    {
        noForcedMove = false;

        bool left = false;
        bool up = false;
        bool right = false;
        bool down = false;
        Vector2 pos = transform.position;

        float step = speed * Time.deltaTime;
        Vector2 direction = (Vector2)finalPos - pos;

        if(direction.x <= 0)
        {
            left = true;
        }
        else
        {
            right = true;
        }
        if (direction.y <= 0)
        {
            down = true;
        }
        else
        {
            up = true;
        }

        bool noWall = true;

        while (noWall && (transform.position.x > finalPos.x + 0.1f || transform.position.x < finalPos.x - 0.1f) || (transform.position.y > finalPos.y + 0.1f || transform.position.y < finalPos.y - 0.1f))
        {
            if (!this.Collision(transform.position, playerAsset.Speed, 1))
            {
                break;
            }
            if (!this.Collision(transform.position, playerAsset.Speed, 2))
            {
                break;
            }
            if (!this.Collision(transform.position, playerAsset.Speed, 3))
            {
                break;
            }
            if (!this.Collision(transform.position, playerAsset.Speed, 0))
            {
                break;
            }
            transform.position = Vector3.MoveTowards(transform.position, finalPos, step);
            playerAsset.Position = transform.position;
            yield return null;
        }
        noForcedMove = true;
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

    public void IsNearChest()
    {
        nearChest = !nearChest;
    }

    public bool GetNearChest()
    {
        return nearChest;
    }

    public void ActiveChestUI()
    {
        InventoryUI.instance.EnableUI();
        ChestUI.instance.EnableUI();
    }
}



