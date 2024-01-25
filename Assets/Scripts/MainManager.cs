using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Reflection;
using UnityEngine.UIElements;


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
    private SaveLoadData saveLoadData;
    public GameObject resetButton;
    public string nameMenu;


    

    // Start is called before the first frame update
     void Start()
    {
       
        saveLoadData = FindObjectOfType<SaveLoadData>().GetComponent<SaveLoadData>();

        // load previous best score
        //saveLoadData.LoadPlayerInfo();
        saveLoadData.LoadGameScore();
        saveLoadData.LoadName();

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
        resetButton.SetActive(true);
        nameMenu = DataManager.Instance.nameField.text;

        if (m_Points > saveLoadData.bestScoreInt)
        {
            saveLoadData.SaveGameScore(m_Points);
            saveLoadData.SaveName(nameMenu);
        //  saveLoadData.SavePlayerInfo
        }



    }


    public void ResetGameScore() // Reset game score 
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
           File.Delete(path);
           Debug.Log("Reset score worked here");
        }
    }


}




