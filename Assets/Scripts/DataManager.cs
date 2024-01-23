using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
class SaveData
{
    public string nameToSave;
}

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public TMP_InputField nameField;
    public TextMeshProUGUI titleText;
    public string path;
    public int bestScoreInt;
    public string nameHighestScore;

 
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SavePointData data = JsonUtility.FromJson<SavePointData>(json);
            bestScoreInt = data.currentScoreInt; // setting bestScoreInt
            nameHighestScore = data.nameCurrent;
            Debug.Log("Loading Worked: " + bestScoreInt + nameHighestScore);
            titleText.text = "Best Score: " + nameHighestScore + ": " + bestScoreInt;
        }
    }

    public void StartGame() => SceneManager.LoadScene(1);

    public void QuitGame() => Application.Quit();

    // public void SaveName()
    //{
    //  SaveData data = new SaveData();
    //data.nameToSave = nameField.text;
    //string json = JsonUtility.ToJson(data);
    //File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    //Debug.Log("Saving worked here: " + Application.persistentDataPath);
    //}

    //public void LoadName()
    //{
    // string path = Application.persistentDataPath + "/savefile.json";
    //if (File.Exists(path))
    // {
    //   string json = File.ReadAllText(path);
    // SaveData data = JsonUtility.FromJson<SaveData>(json);
    //nameField.text = data.nameToSave;
    // Debug.Log("Loading Worked" + nameField.text);
    //}
}
