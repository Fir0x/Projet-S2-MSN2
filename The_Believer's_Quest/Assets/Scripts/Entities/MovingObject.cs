using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class MovingObject : MonoBehaviour
{
    public bool Collision(Vector2 pos, float x, float y)
    {
        bool noCollision = true;
        RaycastHit2D hit2 = Physics2D.Raycast(pos + new Vector2(0.2f, -0.4f), new Vector2(x, y), 0.06f);
        RaycastHit2D hit4 = Physics2D.Raycast(pos + new Vector2(-0.2f, -0.4f), new Vector2(x, y), 0.06f);
        Debug.DrawRay(pos + new Vector2(0.2f, -0.4f), new Vector2(x, y) * 0.06f, Color.red);
        Debug.DrawRay(pos + new Vector2(-0.2f, -0.4f), new Vector2(x, y) * 0.06f, Color.red);

        if (hit2 || hit4)
        {
            noCollision = false;
        }
        return noCollision;
    }
}