using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Channel {
    public int index;
    public GameObject cube;
    public Transform lane;
    public float timeout = 0.5f;
    public float trashold = 0;
    public float lastSpawned = 0;
}

public class Spawner : MonoBehaviour
{
	public Channel[] channels;
    public AudioSource audioSource;
    public UnityEvent OnSpawn;
    public float delay = 0;

    private static int numberOfSamples = 512;
    private float[] samples = new float[numberOfSamples];


    // Update is called once per frame
    void Update()
    {
        if (delay <= 0)
        {
            audioSource.GetSpectrumData(samples, 0, FFTWindow.BlackmanHarris);

            float[] audioChannels = GetAudioChannels();

            // Debug.Log(string.Format("|{0}| |{1}| |{2}| |{3}|  ", audioChannels[0], audioChannels[1], audioChannels[2], audioChannels[3]));

            for (int i = 0; i < channels.Length; i++)
            {
                Channel channel = channels[i];

                if (audioChannels[channel.index] > channel.trashold && channel.lastSpawned >= channel.timeout)
                {
                    GameObject cube = Instantiate(channel.cube, channel.lane);
                    cube.transform.localPosition = Vector3.zero;
                    cube.transform.Rotate(transform.forward, 90 * Mathf.Max(0, Mathf.Min(Mathf.Abs(Mathf.Ceil(Mathf.Cos(audioChannels[channel.index] / channel.trashold * 100) * Mathf.PI)), 4)));
                    channel.lastSpawned = 0;
                    OnSpawn.Invoke();
                }

                channel.lastSpawned += Time.deltaTime;
            }
        }
        else {
            delay -= Time.deltaTime;
        }
    }

    private float[] GetAudioChannels() {
        float[] channels = new float[8];

        for (int i = 0; i < channels.Length; i++) {
            float total = 0;
            int firstSample = (int) Mathf.Pow(2, i);
            int lastSample = (int) Mathf.Pow(2, i + 1);

            for (int j = firstSample; j < lastSample; j++) {
                total += samples[j];
            }

            channels[i] = total / (lastSample - firstSample);
        }

        return channels;

    }
}
