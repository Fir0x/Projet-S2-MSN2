using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;
using Vector3 = System.Numerics.Vector3;

[Serializable]
public class Player : MonoBehaviour
{
    private int moveSpeed = 2; //pour déplacement
    private Rigidbody2D rigid;  //utile pour déplacement glace


    private bool upKey;
    private bool downKey;
    private bool leftKey;
    private bool rightKey;

    private int floor = 0;
    private int hp = 3;
    private int maxHP = 3;
    private int effectValue = 0;
    private int maxEffectValue = 10;
    public Slider effectGauge;

    private int gold = 0;
    private int diamond = 0;
    private Weapons[] weaponsList = new Weapons[2];
    private Weapons inHand;

    public void GetKeys()
    {
        upKey = Input.GetKey(KeyCode.UpArrow);
        downKey = Input.GetKey(KeyCode.DownArrow);
        leftKey = Input.GetKey(KeyCode.LeftArrow);
        rightKey = Input.GetKey(KeyCode.RightArrow);
    }

    public int GetHP()
    {
        return hp;
    }

    public void SetHP(int value)
    {
        hp = value;
    }

    public void SetMaxHP(int value)
    {
        maxHP += value;
    }

    public void SetEffectValue(int value)
    {
        effectValue = value;
        effectGauge.value = value;
    }

    public void SetMaxEffectValue(int value)
    {
        maxEffectValue = value;
        effectGauge.maxValue = value;
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
        GetKeys();
        rigid = GetComponent<Rigidbody2D>();
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