using UnityEngine;
using UnityEditor;
using System.Collections;

public class NextLevel : MonoBehaviour
{
    private bool canGo;
    private bool firstTrigger;              //just in case

    private void Start()
    {
        canGo = true;
        firstTrigger = true;
    }

    public void Wait()
    {
        StartCoroutine(CoolDown());
    }

    IEnumerator CoolDown()
    {
        canGo = false;
        yield return new WaitForSeconds(2);
        canGo = true;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(canGo && col.CompareTag("Player"))
        {
            if(firstTrigger)
            {
                Destroy(GameObject.Find("Board"));
                GameObject.FindGameObjectWithTag("BoardManager").GetComponent<Board>().Init();
                GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>().ChangeBO((col.GetComponent<Player>().PlayerAsset.Floor - 1) + 2);
                MapController.mapInstance.ResetMap();
            }
            firstTrigger = false;
        }
    }
}