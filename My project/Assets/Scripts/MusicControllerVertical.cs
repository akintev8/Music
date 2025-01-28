using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class MusicControllerVertical : MonoBehaviour
{
    public AudioMixer audioMixer; 
    private string normalParameter = "NormalVolume";
    private string battleParameter = "BattleVolume";
    public float timeDuration; 

    private void Start()
    {
        SetMusicState(MusicState.Normal);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetMusicState(MusicState.Battle);
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            SetMusicState(MusicState.Normal);
        }
    }

    public void SetMusicState(MusicState state)
    {
        switch (state)
        {
            case MusicState.Normal:
                StartCoroutine(EnableMusicTrack(normalParameter, timeDuration));
                StartCoroutine(DisableMusicTrack(battleParameter, timeDuration));
                break;
            case MusicState.Battle:
                StartCoroutine(EnableMusicTrack(battleParameter, timeDuration));
                break;
        }
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

public enum MusicState
{
    Normal,
    Battle
}