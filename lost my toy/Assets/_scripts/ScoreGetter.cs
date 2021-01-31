using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreGetter : MonoBehaviour
{
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI maxScoreText;

    private void OnEnable()
    {
        ToyGenerator.OnLoose += GetScore;
    }

    private void OnDisable()
    {
        ToyGenerator.OnLoose -= GetScore;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetScore()
    {
        if (currentScoreText != null)
        {
            currentScoreText.text = "" + ScoreManager.currentScore;
        }

        if (maxScoreText != null)
        {
            maxScoreText.text = "" + ScoreManager.maxScore;
        }
        Debug.Log("score got");
    }

}
