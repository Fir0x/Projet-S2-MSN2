using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : MonoBehaviour
{
    protected int life = 3;
    protected int effectGauge = 0;

    public int GetLife()
    {
        return life;
    }

    public void SetLife(int life)
    {
        this.life = life;
    }

    protected void Attack(Player player)
    {
        player.SetLife(-1);
    }

    protected void Move()
    { }
}
