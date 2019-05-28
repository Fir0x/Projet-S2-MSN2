using UnityEngine;
using UnityEditor;

public class NextLevel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("BoardManager").GetComponent<Board>().Init();
            Destroy(this.transform.parent.gameObject);
        }
    }
}