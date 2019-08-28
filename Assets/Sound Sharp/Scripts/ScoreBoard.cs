using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour {
    public string label = "Score: ";
    private Text text;
    private string score = "0%";

    private void Start()
    {
        text = gameObject.GetComponentInChildren<Text>();
        text.text = label + score;
    }

    public void SetScore(string newScore)
    {
        score = newScore;
        text.text = label + score;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
