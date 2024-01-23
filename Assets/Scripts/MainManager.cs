using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Reflection;
using UnityEngine.UIElements;

[System.Serializable]
class SavePointData
{
    public int currentScoreInt;
    public int bestScoreInt;
    public string nameHighestScore;
    public string nameCurrent;
}

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;

    private bool m_Started = false;
    public int m_Points;

    private bool m_GameOver = false;
    private TextManager textManager;
    private TextMeshProUGUI bestScoreText;
    public int bestScoreInt;
    public int currentScoreInt;
    public string nameHighestScore;
    public string nameCurrent;


    // Start is called before the first frame update
    void Start()
    {
        // connect to Text Manager script 
        textManager = FindObjectOfType<TextManager>().GetComponent<TextManager>();
        bestScoreText = textManager.bestScoreText;


        // start of game instructions
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

       OnLoad();

    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {

        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {

        m_GameOver = true;
        GameOverText.SetActive(true);

        if (m_Points > bestScoreInt)
        {
            SaveGameScore();
        }

    }


    public void SaveGameScore () // Save game score & player name
    {

        SavePointData data = new SavePointData();
        data.currentScoreInt = m_Points;
        data.nameCurrent = DataManager.Instance.nameField.text;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        Debug.Log("Saving score worked here: " + m_Points + data.nameHighestScore);
        

    }

    public void LoadGameScore() // Load game score & player name
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SavePointData data = JsonUtility.FromJson<SavePointData>(json);
            bestScoreInt = data.currentScoreInt; // setting bestScoreInt
            nameHighestScore = data.nameCurrent;
            Debug.Log("Loading Worked: " + bestScoreInt + nameHighestScore);

        }

    }

    public void ResetGameScore() // Reset game score 
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
           File.Delete(path);
        }
    }

    public void OnLoad() // load highest score & name on started game
    {
        LoadGameScore();
        bestScoreText.text = "Best Score: " + nameHighestScore + " " + bestScoreInt;
    }


}




