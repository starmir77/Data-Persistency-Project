using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SocialPlatforms.Impl;

public class SaveLoadData : MonoBehaviour
{
    public string nameBestScore;
    public int bestScoreInt;

    [System.Serializable]

    class SaveScore
    {
       
        public int bestScoreInt;
        public string nameBestScore;

    }

    // new code together 

    public void SavePlayerInfo ( int score, string name)
    {
        SaveScore data = new SaveScore();
        data.bestScoreInt = score;
        data.nameBestScore = name;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        Debug.Log("Saving player info worked here: " + score + " " + name);
    }

    public void LoadPlayerInfo()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveScore data = JsonUtility.FromJson<SaveScore>(json);
            bestScoreInt = data.bestScoreInt; // setting bestScoreInt
            nameBestScore = data.nameBestScore;
            Debug.Log("Loading player info worked: " + bestScoreInt + " " + nameBestScore);
        }
    }
    

    // old code separately ////////////////////////////////////////////////////////////////////

    public void SaveGameScore(int score) // Save score  
    {
        SaveScore data = new SaveScore();
        data.bestScoreInt = score;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savescore.json", json);
        Debug.Log("Saving game score worked here: " + score);

    }

    public int LoadGameScore() // Load score  
    {

        string path = Application.persistentDataPath + "/savescore.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveScore data = JsonUtility.FromJson<SaveScore>(json);
            bestScoreInt = data.bestScoreInt; // setting bestScoreInt
            Debug.Log("Loading game score worked: " + bestScoreInt);
        }

        return bestScoreInt;
    }


    [System.Serializable]

    class SaveNameScore

    {
        public string nameBestScore;
    }

    public void SaveName(string name) // save name
    {
        SaveNameScore data = new SaveNameScore();
        data.nameBestScore = name;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savename.json", json);
        Debug.Log("Saving name worked here: " + name);
    }

   

    public string LoadName() // load name
    {

        string path = Application.persistentDataPath + "/savename.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveNameScore data = JsonUtility.FromJson<SaveNameScore>(json);
            nameBestScore = data.nameBestScore; // setting bestScoreInt
            Debug.Log("Loading name worked: " + nameBestScore);
        }

        return nameBestScore;
            
    }

}

