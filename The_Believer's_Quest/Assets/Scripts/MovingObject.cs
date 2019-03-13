using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class MovingObject : MonoBehaviour
{
    private LayerMask mask;

    // Start is called before the first frame update
    void Start()
    {
        mask = LayerMask.GetMask("Blocks");
    }



    // Update is called once per frame
    void Update()
    {

    }

    public bool Collision(Vector2 pos, float x, float y)
    {
        bool noCollision = true;
        RaycastHit2D hit1 = Physics2D.Raycast(pos + new Vector2(0.2f, 0.01f), new Vector2(x, y), 0.03f);
        RaycastHit2D hit2 = Physics2D.Raycast(pos + new Vector2(0.2f, -0.4f), new Vector2(x, y), 0.03f);
        RaycastHit2D hit3 = Physics2D.Raycast(pos + new Vector2(-0.2f, 0.01f), new Vector2(x, y), 0.03f);
        RaycastHit2D hit4 = Physics2D.Raycast(pos + new Vector2(-0.2f, -0.4f), new Vector2(x, y), 0.03f);

        Debug.DrawRay(pos + new Vector2(0.2f, 0.01f), new Vector2(x, y) * 0.03f, Color.red);
        Debug.DrawRay(pos + new Vector2(0.2f, -0.4f), new Vector2(x, y) * 0.03f, Color.red);
        Debug.DrawRay(pos + new Vector2(-0.2f, 0.01f), new Vector2(x, y) * 0.03f, Color.red);
        Debug.DrawRay(pos + new Vector2(-0.2f, -0.4f), new Vector2(x, y) * 0.03f, Color.red);

        if (hit1 || hit2 || hit3 || hit4) //origine, direction, taille)
        {
            print("lol");
            noCollision = false;
        }
        print(noCollision);
        return noCollision;


    }
}