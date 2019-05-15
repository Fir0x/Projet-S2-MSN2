using System.IO;
using UnityEngine;
using UnityEngine.UI;
//Nicolas I
public class ContinueController : MonoBehaviour
{
    [SerializeField] private Button continueButton;

    public Button ContinueButton { get => continueButton; set => continueButton = value; }

    private void Awake()
    {
        if (!File.Exists("playerData"))
        {
            continueButton.interactable = false;
            continueButton.gameObject.GetComponentInChildren<Text>().color = Color.gray;
        }
    }
}
