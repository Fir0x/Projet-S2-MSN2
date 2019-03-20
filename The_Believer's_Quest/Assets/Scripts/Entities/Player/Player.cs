using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
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

    [SerializeField] private PlayerAsset playerAsset;

    public Slider effectGauge;
    public Slider hpGauge;

    public Text nbGold;
    public Text nbDiamond;
    public Text nbAmmo;
    public Image ImgWeapon;

    protected PlayerAsset PlayerAsset { get => playerAsset; set => playerAsset = value; }

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
        rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {  //déplacement et collision
        float moveX = moveSpeed * Time.deltaTime;
        float moveY = moveSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (this.Collision(transform.position, -1, 0))
            {
                this.transform.Translate(-moveX, 0, 0);
            }
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (this.Collision(transform.position, 1, 0))
            {
                this.transform.Translate(moveX, 0, 0);
            }
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (this.Collision(transform.position, 0, 1))
            {
                this.transform.Translate(0, moveY, 0);
            }
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (this.Collision(transform.position, 0, -1))
            {
                this.transform.Translate(0, -moveY, 0);
            }
        }

        camera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, camera.transform.position.z);

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



