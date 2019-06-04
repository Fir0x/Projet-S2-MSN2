using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class MasseEnemy : MonoBehaviour
{
    [SerializeField] GameObject iceSlime;
    List<GameObject> allEnemies;
    bool ok;
    RoomManager thisManager;

    private void Start()
    {
        ok = false;
        allEnemies = new List<GameObject>();
        StartCoroutine(Spawn());
        thisManager = gameObject.GetComponentInParent<RoomManager>();
    }

    IEnumerator Spawn()
    {
        int x;
        int y;
        for (int i = 30; i > 0; i--)
        {
            x = Random.Range(-4, 5);
            y = Random.Range(-3, 4);
            GameObject slime1 = Instantiate(iceSlime, new Vector3(this.gameObject.transform.position.x + x, this.gameObject.transform.position.y + y, 0), Quaternion.identity);
            slime1.transform.SetParent(gameObject.transform.parent.transform);
            gameObject.transform.parent.GetComponent<RoomManager>().SpawnEnemiesForThirdBoss(slime1);

            x = Random.Range(-4, 5);
            y = Random.Range(-3, 4);
            GameObject slime2 = Instantiate(iceSlime, new Vector3(this.gameObject.transform.position.x + x, this.gameObject.transform.position.y + y, 0), Quaternion.identity);
            slime2.transform.SetParent(this.transform);

            gameObject.transform.parent.GetComponent<RoomManager>().SpawnEnemiesForThirdBoss(slime1);

            yield return new WaitForSeconds(5);
        }
        gameObject.transform.parent.GetComponent<RoomManager>().SpawnEnemiesForThirdBoss(null);
    }

    private void Update()
    {
        if (ok)
        {
            gameObject.GetComponentInParent<RoomManager>().DestroyEnemy(gameObject);
        }
    }
}
