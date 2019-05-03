using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
//Nicolas I
public class Saver : MonoBehaviour
{
    [SerializeField] private PlayerAsset playerData;

    public PlayerAsset PlayerData { get => playerData; set => playerData = value; }

    [Serializable]
    public class PlayerSave
    {
        string diamond;
        List<string> unlockedWeapons = new List<string>();

        public PlayerSave(string diamond, List<string> unlockedWeapons)
        {
            this.diamond = diamond;
            this.unlockedWeapons = unlockedWeapons;
        }
    }

    private void Saving(PlayerSave save, string filename)
    {
        if (!filename.Contains(".json"))
            filename += ".json";

        string path = Path.Combine(Path.GetDirectoryName(Application.dataPath), filename);
        print("File path: " + path); //DEBUG
        File.WriteAllText(path, JsonUtility.ToJson(save));
    }

    public void SavePlayerData()
    {
        Saving(new PlayerSave(Cipher(PlayerData.Diamond), new List<string>()), "playerData");
    }

    private string Cipher(int value)
    {
        List<int> digits = new List<int>();

        while (value > 10)
        {
            digits.Add(value % 10);
            value /= 10;
        }
        digits.Add(value);

        string result = "";
        for (int i = 0; i < digits.Count; i++)
            result += (char) ((digits[i] * 10 + 10 - digits[i]) * 2 + 17);

        return result;
    }
}
