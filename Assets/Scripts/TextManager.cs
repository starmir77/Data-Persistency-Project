using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{
    public TextMeshProUGUI bestScoreText;
    private SaveLoadData saveLoadData;

    // Start is called before the first frame update
    void Start()
    {
        saveLoadData = FindObjectOfType<SaveLoadData>().GetComponent<SaveLoadData>();
        bestScoreText.text = "Best Score: " + saveLoadData.nameBestScore + " : " + saveLoadData.bestScoreInt;
    }

   
}
