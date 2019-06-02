using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject projectile;
    private PlayerAsset player;
    public EnemyAsset enemy;
    public BossAsset boss;

    private bool effect = false;

    private void Start()
    {
        player = Player.instance.PlayerAsset;
    }

    public enum Trajectory
    {
        Line,
        Arc,
        Railgun,
        Circle,
        Cqc,
    }

    public void BossLauncher(Trajectory traj)                               //for boss, traj changes with attack pattern
    {
        Transform playerT = GameObject.FindGameObjectWithTag("Player").transform;
        if (traj == Trajectory.Line)
            LineShot(playerT.position, enemy.Speed, boss.Damage, 0f);
        if (traj == Trajectory.Arc)
            ArcShot(enemy.NbOfProjectiles, playerT.position, enemy.ShotSpeed, boss.Damage);
        if (traj == Trajectory.Circle)
            CircleShot(enemy.NbOfProjectiles, enemy.ShotSpeed, boss.Damage);
        if (traj == Trajectory.Cqc)
            Cqc(boss.Damage);
        if (traj == Trajectory.Railgun)
            Railgun(boss.Damage);

        effect = boss.Effect;
    }

    public void Launcher()                          //for regular enemies
    {
        Transform playerT = Player.instance.transform; //GameObject.FindGameObjectWithTag("Player").transform;
        if (enemy._Trajectory == Trajectory.Line)
            LineShot(playerT.position, enemy.Speed, enemy.Damage, 0f);
        if (enemy._Trajectory == Trajectory.Arc)
            ArcShot(enemy.NbOfProjectiles, playerT.position, enemy.ShotSpeed, enemy.Damage);
        if (enemy._Trajectory == Trajectory.Circle)
            CircleShot(enemy.NbOfProjectiles, enemy.ShotSpeed, enemy.Damage);
        if (enemy._Trajectory == Trajectory.Cqc)
            Cqc(enemy.Damage);
        if (enemy._Trajectory == Trajectory.Railgun)
            Railgun(enemy.Damage);

        effect = enemy.Effect;
    }
    private void Cqc(float damage)
    {
        Collider2D[] playerTouched = Physics2D.OverlapCircleAll(transform.position, 0.7f, LayerMask.GetMask("Default"));

        if(!player.Invicibility)
        {
            for (int i = 0; i < playerTouched.Length; i++)
            {
                if (playerTouched[i].CompareTag("Player"))
                {
                    playerTouched[i].GetComponent<Player>().SetLife(player.Hp - damage);
                    
                    Vector3 forcedMov = -(gameObject.transform.position - playerTouched[i].transform.position).normalized;              //to make player move in the opposite way when touched
                    playerTouched[i].GetComponent<Player>().ForcedMovement(forcedMov);
                    Player.instance.SetEffect(player.EffectValue + damage / 2);
                }
            }
        }
    }
    private void LineShot(Vector3 direction, float speed, float damage, float angle)//tir linéaire
    {
       Instantiate(projectile, transform.position,
            transform.rotation).GetComponent<Projectile>().Init(enemy.Projectile, speed, damage, transform.position, angle, (direction-transform.position),false, false, effect); 
    }
    
    private void CircleShot(int nbprojectile, float speed, float damage)//tir en cercle
    {
        
        float angle = 360 / nbprojectile;
        
        for (int i = 0; i < nbprojectile; i++)
        {
            Instantiate(projectile, transform.position,
                transform.rotation).GetComponent<Projectile>().Init(enemy.Projectile, speed, damage, transform.position, angle, transform.up,false, true, effect); 

        } 
    }

    private void ArcShot(int nbprojectile, Vector3 direction, float speed, float damage) //tir type shotgun
    {
        if (nbprojectile % 2 != 0)
        {
            LineShot(direction, enemy.Speed, damage, 0);
        }

        for (int i = 1; i <= nbprojectile / 2; i++)
        {
            LineShot(direction, speed, damage, 10 * i);
            LineShot(direction, speed, damage, -10 * i);
        }
    }

    private void Railgun(float damage)
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position, 0.1f, LayerMask.GetMask("Default"));
   
        if (hitInfo.collider != null )
        {
            if (hitInfo.collider.CompareTag("Player"))
            {
                hitInfo.collider.GetComponent<Player>().SetLife(player.Hp - damage);
                Destroy(gameObject);
            }
            if (hitInfo.collider.CompareTag("Pattern"))
            {
                Destroy(gameObject);
            }
        }
    }

}
