using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public Laser pointer;
    public Text[] menuItems;
    public AudioSource[] tracks;

    private string currentHighlighted;
    private bool loading = false;

    private void Update()
    {
        GameObject target = pointer.getTarget();

        if (target != null && target.name != currentHighlighted) {
            currentHighlighted = target.name;
            HighlightMenuItem(currentHighlighted);
            PlayTrack(currentHighlighted);
        }

        if (!loading && OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger)) {
            switch (currentHighlighted)
            {
                case "Alan Walker - Fade":
                    StartCoroutine(LoadSceneWithDelay("Level_1", 1.0f));
                    loading = true;
                    break;
                case "DEAF KEV - Invincible":
                    StartCoroutine(LoadSceneWithDelay("Level_2", 1.0f));
                    loading = true;
                    break;
                case "Jim Yosef - Firefly":
                    StartCoroutine(LoadSceneWithDelay("Level_3", 1.0f));
                    loading = true;
                    break;
                case "Syn Cole - Feel Good":
                    StartCoroutine(LoadSceneWithDelay("Level_4", 1.0f));
                    loading = true;
                    break;
                case "Tobu - Infectious":
                    StartCoroutine(LoadSceneWithDelay("Level_5", 1.0f));
                    loading = true;
                    break;
                default:

                    break;
            }
        }
    }

    private void HighlightMenuItem(string item) {
        for (int i = 0; i < menuItems.Length; i++) {
            menuItems[i].color = item == menuItems[i].name ? Color.cyan : Color.white;
        }
    }

    private void PlayTrack(string track)
    {
        for (int i = 0; i < tracks.Length; i++)
        {
            if (track == tracks[i].name)
            {
                tracks[i].PlayDelayed(1f);
            }
            else { tracks[i].Stop(); };
        }
    }

    private IEnumerator LoadSceneWithDelay(string scene, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(scene);
    }
}
