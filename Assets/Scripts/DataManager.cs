using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public TMP_InputField nameField;
    public TextMeshProUGUI titleText;
    private string nameBestScore;
    private int bestScoreInt;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

    }


    private void Start()
    {
        LoadScore();
        LoadName();

        titleText.text = "Best Score: " + nameBestScore + " : " + bestScoreInt;
    }

    private void StartGame() => SceneManager.LoadScene(1);

    private void QuitGame() => Application.Quit();

    public class SaveScore
    {

        public int bestScoreInt;
    }

    public class SaveNameScore
    {
        public string nameBestScore;
    }

    private void LoadScore()
    {

        string path = Application.persistentDataPath + "/savescore.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveScore data = JsonUtility.FromJson<SaveScore>(json);
            bestScoreInt = data.bestScoreInt;

        }
    }

    private void LoadName()
    {
        string path1 = Application.persistentDataPath + "/savename.json";
        if (File.Exists(path1))
        {
            string json = File.ReadAllText(path1);
            SaveNameScore data = JsonUtility.FromJson<SaveNameScore>(json);
            nameBestScore = data.nameBestScore; // setting bestScoreInt

        }
    }

}
