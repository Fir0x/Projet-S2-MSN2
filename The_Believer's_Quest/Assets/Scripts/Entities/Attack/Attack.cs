using UnityEngine;

public class Attack : MonoBehaviour
{
    public enum Trajectory
    {
        Line,
        Arc,
        Circle
    };

    public void Launcher(Trajectory trajectory, Sprite sprite, Vector3 origin, Vector2 direction, float speed, int damage)
    {
        if (trajectory == Trajectory.Line)
        {
            LineShot(sprite, origin, direction, speed, damage);
        }
    }

    public void Cqc()
    {
        throw new System.NotImplementedException();
    }

    private void LineShot(Sprite sprite, Vector3 origin, Vector2 direction, float speed, int damage)
    {
        GameObject projectile = Instantiate(Resources.Load("Projectile") as GameObject, origin, new Quaternion());
        projectile.GetComponent<Projectile>().Init(sprite, speed, damage, direction);
    }

    private void ArcShot()
    {
        throw new System.NotImplementedException();
    }

    private void circleShot()
    {
        throw new System.NotImplementedException();
    }
}
