using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class MovingObject : MonoBehaviour
{
    private LayerMask mask;
    
    // Start is called before the first frame update
    void Start()
    {
        mask = LayerMask.GetMask("Default");
    }
    
    

    // Update is called once per frame
    void Update()
    {    
        
    }
    
    public bool Collision(Vector2 pos, float x, float y)
    {
        bool noCollision = true;
        
        RaycastHit hit;

        Debug.DrawRay(pos, new Vector2(x, y) * 0.2f, Color.red);
        if (Physics2D.Raycast(pos, new Vector2(x, y), 0.2f)) //origine, direction, taille)
        {
            print("lol");
            noCollision = false;
        }
        
        return noCollision;


    }
}
