using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicControllerHorizontal : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioMixerGroup normalGroup;
    public AudioMixerGroup battleGroup;
    public float timeDuration;
    private string normalGroupVolume = "NormalVolume";
    private string battleGroupVolume = "BattleVolume";

    private void Start()
    {
        normalGroup.audioMixer.SetFloat(normalGroupVolume, 0);
        battleGroup.audioMixer.SetFloat(battleGroupVolume, -80);
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space)) {
            StartTransition(normalGroupVolume, battleGroupVolume, timeDuration);
        } else if (Input.GetKeyDown(KeyCode.Escape)) {
            StartTransition(battleGroupVolume, normalGroupVolume, timeDuration);
        }
    }
    public void StartTransition(string fromGroupVolume,string toGroupVolume, float transitionDuration)
    {
        StartCoroutine(DisableMusicTrack(fromGroupVolume, transitionDuration)); 
        StartCoroutine(EnableMusicTrack(toGroupVolume, transitionDuration)); 
    }

    private IEnumerator DisableMusicTrack(string parameter, float duration)
    {
        float currentVolume;
        audioMixer.GetFloat(parameter, out currentVolume); 
        float startVolume = currentVolume;
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            float newVolume = Mathf.Lerp(startVolume, -80f, time / duration);
            audioMixer.SetFloat(parameter, newVolume);
            yield return null;
        }
    }

    private IEnumerator EnableMusicTrack(string parameter, float duration)
    {
        float currentVolume;
        audioMixer.GetFloat(parameter, out currentVolume);
        float startVolume = currentVolume;
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            float newVolume = Mathf.Lerp(startVolume, 0f, time / duration); 
            audioMixer.SetFloat(parameter, newVolume);
            yield return null;
        }
    }
}
