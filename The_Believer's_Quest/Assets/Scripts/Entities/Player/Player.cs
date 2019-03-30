using System;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

[Serializable]
public class Player : MovingObject
{
    public GameObject camera;

    private int moveSpeed = 2; //pour déplacement
    private Rigidbody2D rigid;  //utile pour déplacement glace


    private bool upKey;
    private bool downKey;
    private bool leftKey;
    private bool rightKey;

    [SerializeField] protected PlayerAsset playerAsset;
    //[SerializeField] protected Animator animator;
    private Animator animator;
    private int animMoveHashID = Animator.StringToHash("Move");
    private int animDirectionHashID = Animator.StringToHash("Direction");
    private int animDashID = Animator.StringToHash("Dash");
    private int animDeathID = Animator.StringToHash("Death");

    public void GetKeys()
    {
        upKey = Input.GetKey(KeyCode.UpArrow);
        downKey = Input.GetKey(KeyCode.DownArrow);
        leftKey = Input.GetKey(KeyCode.LeftArrow);
        rightKey = Input.GetKey(KeyCode.RightArrow);
    }

    public Vector2 GetPos()
    {
        return this.transform.position;
    }

    public void SetLife(int value)
    {
        playerAsset.Hp = value;
    }

    public void Dash()
    {
        throw new NotImplementedException();
    }

    private void Start()
    {
        GetKeys();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {  //déplacement et collision
        float moveX = moveSpeed * Time.deltaTime;
        float moveY = moveSpeed * Time.deltaTime;

        if (Input.anyKey)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                animator.SetInteger(animDirectionHashID, 3);
                animator.SetBool(animMoveHashID, true);
                if (this.Collision(transform.position, -1, 0))
                {
                    this.transform.Translate(-moveX, 0, 0);
                }
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                animator.SetInteger(animDirectionHashID, 1);
                animator.SetTrigger(animMoveHashID);
                if (this.Collision(transform.position, 1, 0))
                {
                    this.transform.Translate(moveX, 0, 0);
                }
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                animator.SetInteger(animDirectionHashID, 0);
                animator.SetTrigger(animMoveHashID);
                if (this.Collision(transform.position, 0, 1))
                {
                    this.transform.Translate(0, moveY, 0);
                }
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                animator.SetInteger(animDirectionHashID, 2);
                animator.SetTrigger(animMoveHashID);
                if (this.Collision(transform.position, 0, -1))
                {
                    this.transform.Translate(0, -moveY, 0);
                }
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                animator.SetInteger(animDirectionHashID, 2);
                animator.SetTrigger(animMoveHashID);
                if (this.Collision(transform.position, 0, -1))
                {
                    this.transform.Translate(0, -moveY, 0);
                }
            }
            if (Input.GetKey(KeyCode.Space))
            {
                animator.SetTrigger(animDeathID);
            }
            if (Input.GetKey(KeyCode.D))
            {
                animator.SetTrigger(animDashID);
            }
        }
        else
            animator.SetBool(animMoveHashID, false);
        
        camera.transform.position = new Vector3(transform.position.x, transform.position.y, camera.transform.position.z);

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



