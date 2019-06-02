using UnityEngine;
using UnityEditor;
using System.Collections;

public class NextLevel : MonoBehaviour
{
    [SerializeField] private PlayerAsset playerAsset;
    private bool canGo;
    private bool firstTrigger;              //just in case

    public PlayerAsset PlayerAsset { get => playerAsset; set => playerAsset = value; }
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
                GameObject.FindGameObjectWithTag("BoardManager").GetComponent<Board>().Init();
                if (playerAsset.Floor != 0)
                {
                    GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>().ChangeBO(playerAsset.Floor + 3);
                    Destroy(GameObject.Find("Board"));
                    MapController.mapInstance.ResetMap();
                }
            }
            firstTrigger = false;
        }
    }
}