using System;
using UnityEngine;

public class Attack : MonoBehaviour
{
    //classe réalisé en majorité par Sarah avec l'aide de Nicolas I
    public enum Trajectory
    {
        Line,
        Arc,
        Circle,
        Cqc,
    }


    public void Launcher(Trajectory trajectory, Sprite sprite, Vector3 origin, Vector2 direction, float speed,
        int damage)
    {
        if (trajectory == Trajectory.Line)
            LineShot(sprite, origin, direction, speed, damage);
        if (trajectory == Trajectory.Arc)
            ArcShot(sprite, origin, direction, speed, damage);
        if (trajectory == Trajectory.Circle)
            LineShot(sprite, origin, direction, speed, damage);
    }

    public static void Cqc()
    {   
    }

    private void LineShot(Sprite sprite, Vector3 origin, Vector2 direction, float speed, int damage)
    {
        GameObject projectile = Instantiate(Resources.Load("Projectile") as GameObject, origin, new Quaternion());
        projectile.GetComponent<Projectile>().Init(sprite, speed, damage, direction);
    }
    
    private void ArcShot(Sprite sprite, Vector3 origin, Vector2 direction, float speed, int damage)
    {
        GameObject projectile = Instantiate(Resources.Load("Projectile") as GameObject, origin, new Quaternion());
        
        projectile.GetComponent<Projectile>().Init(sprite, speed, damage, direction);
    }
    
    private void CircleShot(int nbprojectile, Sprite sprite, Vector3 origin, Vector2 direction, float speed, int damage)
    {
        GameObject projectile = Instantiate(Resources.Load("Projectile") as GameObject, origin, new Quaternion());
        double angle = 360 / nbprojectile;
        
        for (int i = 0; i < nbprojectile; i++)
        {
            projectile.GetComponent<Projectile>().Init(sprite, speed, damage, direction);
            direction = new Vector2(direction.x, (int) (direction.y * Math.Sin(angle)));
        } 
    }
    
    private void ArcShot(int nbprojectile, Sprite sprite, Vector3 origin, Vector2 direction, float speed, int damage)
    {
        GameObject projectile = Instantiate(Resources.Load("Projectile") as GameObject, origin, new Quaternion());
        double angle = 120 / nbprojectile;
        projectile.GetComponent<Projectile>().Init(sprite, speed, damage, direction);
        
        for (int i = 1; i < nbprojectile/2; i++)
        {
            direction = new Vector2(direction.x, (int) (direction.y * Math.Sin(angle)));
            projectile.GetComponent<Projectile>().Init(sprite, speed, damage, direction);
        } 
        
        for (int i = 1; i < nbprojectile/2; i++)
        {
            
            direction = new Vector2(direction.x, (int) (direction.y * Math.Sin(-angle)));
            projectile.GetComponent<Projectile>().Init(sprite, speed, damage, direction);
        } 
    }

}
