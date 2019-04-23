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


    public static void Launcher(Trajectory trajectory, Sprite sprite, Vector3 origin, Vector3 direction, float speed,
        int damage)
    {
        if (trajectory == Trajectory.Line)
            LineShot(sprite, origin, direction, speed, damage);
        if (trajectory == Trajectory.Arc)
            ArcShot(/*default*/ 6,sprite, origin, direction, speed, damage);
        if (trajectory == Trajectory.Circle)
            CircleShot(/*default*/ 10,sprite, origin, direction, speed, damage);
    }

    public static void Cqc(Sprite sprite, Vector3 origin, Vector3 direction, float speed,
        int damage, int range)
    {   
        GameObject projectile = Instantiate(Resources.Load("Projectile") as GameObject, origin, new Quaternion());
        projectile.GetComponent<Projectile>().Init(sprite, speed, damage, direction, range);
    }

    private static void LineShot(Sprite sprite, Vector3 origin, Vector3 direction, float speed, int damage)
    {
        GameObject projectile = Instantiate(Resources.Load("Projectile") as GameObject, origin, new Quaternion());
        projectile.GetComponent<Projectile>().Init(sprite, speed, damage, direction);
    }
   
    
    private static void CircleShot(int nbprojectile, Sprite sprite, Vector3 origin, Vector3 direction, float speed, int damage)
    {
        GameObject projectile = Instantiate(Resources.Load("Projectile") as GameObject, origin, new Quaternion());
        double angle = 360 / nbprojectile;
        
        for (int i = 0; i < nbprojectile; i++)
        {
            projectile.GetComponent<Projectile>().Init(sprite, speed, damage, direction);
            direction = new Vector3(direction.x, (int) (direction.y * Math.Sin(angle)), direction.z);
        } 
    }
    
    private static void ArcShot(int nbprojectile, Sprite sprite, Vector3 origin, Vector3 direction, float speed, int damage)
    {
        GameObject projectile = Instantiate(Resources.Load("Projectile") as GameObject, origin, new Quaternion());
        double angle = 120 / nbprojectile;
        projectile.GetComponent<Projectile>().Init(sprite, speed, damage, direction);
        
        for (int i = 1; i < nbprojectile/2; i++)
        {
            direction = new Vector3(direction.x, (int) (direction.y * Math.Sin(angle)), direction.z);
            projectile.GetComponent<Projectile>().Init(sprite, speed, damage, direction);
        } 
        
        for (int i = 1; i < nbprojectile/2; i++)
        {
            
            direction = new Vector3(direction.x, (int) (direction.y * Math.Sin(-angle)), direction.z);
            projectile.GetComponent<Projectile>().Init(sprite, speed, damage, direction);
        } 
    }

}
