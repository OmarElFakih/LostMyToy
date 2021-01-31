using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    public static int currentScore = 0;

    
    public static int maxScore = 0;

    public GameObject LooseUI;
    public GameObject newMaxScoreUI;

    public TextMeshProUGUI scoreText;

    private void OnEnable()
    {
        //ToyGenerator.OnWin += AddScore;
        ToyGenerator.OnLoose += LooseRoutine;
    }

    private void OnDisable()
    {
        //ToyGenerator.OnWin -= AddScore;
        ToyGenerator.OnLoose -= LooseRoutine;
    }


    // Start is called before the first frame update
    void Start()
    {
        maxScore = PlayerPrefs.GetInt("maxScore", 0);
        currentScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore()
    {
        currentScore += 5;
        scoreText.text = "Score: " + currentScore;
    }

    public void LooseRoutine()
    {
        if (currentScore > maxScore)
        {
            PlayerPrefs.SetInt("maxScore", currentScore);
            newMaxScoreUI.SetActive(true);
            newMaxScoreUI.GetComponent<ScoreGetter>().GetScore();
        }
        else
        {
            LooseUI.SetActive(true);
            LooseUI.GetComponent<ScoreGetter>().GetScore();
        }
    }
}
