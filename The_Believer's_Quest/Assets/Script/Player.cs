using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = System.Numerics.Vector3;

[Serializable]
public class Player : MonoBehaviour
{
    private int moveSpeed = 2; //pour déplacement
    private Rigidbody2D rigid;  //utile pour déplacement glace


    private bool upKey = Input.GetKey(KeyCode.UpArrow);       //pourra ê modifié par
    private bool downKey = Input.GetKey(KeyCode.DownArrow);    //le joueur
    private bool leftKey = Input.GetKey(KeyCode.LeftArrow);
    private bool rightKey = Input.GetKey(KeyCode.RightArrow);

    private int floor = 0;
    private int life = 3;
    private int effectGauge = 0;
    private int gold = 0;
    private int diamond = 0;
    private Weapons[] weaponsList = new Weapons[2];
    private Weapons inHand;

    public int GetLife()
    {
        return life;
    }

    public void SetLife(int life)
    {
        this.life = life;
    }

    public int GetGold()
    {
        return gold;
    }

    public void SetGold(int gold)
    {
        this.gold = gold;
    }

    public int GetDiamond()
    {
        return diamond;
    }

    public void SetDiamond(int diamond)
    {
        this.diamond = diamond;
    }

    private void Attack(Ennemy ennemy)
    {
        ennemy.SetLife(inHand.GetDamage());
    }

    public void Dash()
    { }

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        Update();
    }

    private void Update()
    {   //déplacement et collision

        float moveX = moveSpeed * Time.deltaTime;
        float moveY = moveSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Translate(-moveX, 0, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Translate(moveX, 0, 0);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.Translate(0, moveY, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.Translate(0, -moveY, 0);
        }


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
