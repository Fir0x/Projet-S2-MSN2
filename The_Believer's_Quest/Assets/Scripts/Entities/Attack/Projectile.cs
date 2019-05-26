using UnityEngine;
//Nicolas I
public class Projectile : MovingObject
{
    [SerializeField] protected PlayerAsset playerAsset;
    private float speed;
    private int damage;
    private Vector3 direction;
    private int range;
   
    
    public void Init(Sprite sprite, float speed, int damage, Vector3 direction, int range)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        this.speed = speed;
        this.damage = damage;
        this.direction = direction;
        this.range = range;
    }
    //Sarah
    public void Init(Sprite sprite, float speed, int damage, Vector3 origin, float angle) 
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        this.speed = speed;
        this.damage = damage;
        direction = transform.position - origin;
        if (angle != 0)
             transform.RotateAround(origin, Vector3.forward, angle);

    }
    private void FixedUpdate()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, 20, LayerMask.GetMask("Aerial", "Ground"));
        if (hitInfo.collider != null )
        {
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                hitInfo.collider.GetComponent<Enemy>().SetHP(damage);
                Destroy(gameObject);
            }
        }

        Vector3 position = transform.position;

        if (position.x >20 || position.y >20)
            Destroy(gameObject);
        
        gameObject.transform.Translate(new Vector3(position.x + direction.x * speed * Time.deltaTime,
            position.y + direction.y * speed * Time.deltaTime, position.z));
    }
}
