using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public ScoreBoard scoreBoard;
    public EndBoard endBoard;
    public Laser pointer;
    public int maxMissStrike = 10;

    private AudioSource audioSource;
    private float totalCubes = 0;
    private float totalHits = 0;
    private int missStrike = 0;
    private float timePlaying = 0;
    private bool gameOver = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    private void Update()
    {
        if (audioSource.clip.length + 3f <= timePlaying ) EndGame();

        timePlaying += Time.deltaTime;

        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger)) {

            GameObject target = pointer.getTarget();
            if (target && target.name == "RestartBtn") StartCoroutine(LoadSceneWithDelay(SceneManager.GetActiveScene().name, 1.0f));
            if (target && target.name == "MenuBtn") StartCoroutine(LoadSceneWithDelay("Main", 1.0f));
        }
    }

    public void OnSpawn() {
        // totalCubes++;
    }

    public void OnHit() {
        if (gameOver) return;

        totalCubes++;
        totalHits++;
        missStrike = 0;
        UpdateScore();
    }

    private void UpdateScore() {
        string score = (Mathf.Round(totalHits / totalCubes * 1000f) / 10f).ToString() + "%";
        scoreBoard.SetScore(score);
        endBoard.SetScore(score);
    }

    public void OnMiss() {
        if (gameOver) return;

        totalCubes++;
        missStrike++;
        UpdateScore();

        if (missStrike >= maxMissStrike) EndGame();
    }

    private void EndGame() {
        gameOver = true;
        audioSource.Stop();
        scoreBoard.Hide();
        endBoard.Show();
        pointer.Show();
    }

    private IEnumerator LoadSceneWithDelay(string scene, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(scene);
    }
}
