using System.Collections;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    protected readonly string weaponName;
    protected readonly int price;
    protected int damage;
    protected float cooldown;
    protected float reloadingTime;
    protected int loader;
    protected readonly int loaderCappacity;
    protected readonly int maxAmmunitions;
    protected int ammunitions;
    protected readonly bool railgun;

    public int GetDamage()
    {
        return damage;
    }

    public int GetAmmunitions()
    {
        return ammunitions;
    }

    public void SetAmmunitions(int ammunitions)
    {
        this.ammunitions = ammunitions;
    }

    public void AddAmmunitions(int location, int numAmmo, int maximum)
    {
        location += numAmmo;
        if (location > maximum)
            location -= location - maximum;
    }

    IEnumerator ReloadTimer()
    {
        yield return new WaitForSeconds(reloadingTime);
    }

    public void Reload()
    {
        AddAmmunitions(loader, ammunitions, loaderCappacity);
        print("Reload starts"); //DEBUG LOG
        StartCoroutine(ReloadTimer());
        print("Reload ends"); //DEBUG LOG
    }
}
