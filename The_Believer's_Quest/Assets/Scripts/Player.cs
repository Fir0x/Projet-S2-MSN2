using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Player : MonoBehaviour
{
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
}
