using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndBoard : MonoBehaviour {
    public string label = "Score: ";
    private GameObject container; 
    private Text text;
    private string score = "0%";

    private void Start()
    {
        container = gameObject.transform.Find("Container").gameObject;
        text = container.transform.Find("Canvas").Find("Score").GetComponentInChildren<Text>();
        text.text = label + score;
        container.gameObject.SetActive(false);
    }

    public void SetScore(string newScore)
    {
        score = newScore;
        text.text = label + score;
    }

    public void Show() {
        container.SetActive(true);
    }
}
