using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{
    public TextMeshProUGUI bestScoreText;
    private MainManager mainManager;
    private int points;

    // Start is called before the first frame update
    void Start()
    {
        mainManager = FindObjectOfType<MainManager>().GetComponent<MainManager>(); // connect to MainManager script 
        points = mainManager.m_Points; // assing points from Main Manager
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
