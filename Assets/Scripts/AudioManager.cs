using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AudioManager : MonoBehaviour
{
    public float EndCreditsSongLength { get { return musicSource.clip.length; } }

    [SerializeField] private AudioSource voiceSource;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource shipAmbienceSource;
    [SerializeField] private AudioSource crashSource;
    [SerializeField] private TextMeshProUGUI subtitleBox;

    [Header("Sound Clips")]
    [SerializeField] private AudioClip[] shipAmbience;
    [SerializeField] private SubtitleAudio[] voiceLines;
    [SerializeField] private AudioClip crashSound;

    public void PlayShipAmbience(int ambience)
    {
        if (ambience >= shipAmbience.Length) { return; }

        shipAmbienceSource.Stop();
        shipAmbienceSource.loop = true;
        shipAmbienceSource.clip = shipAmbience[ambience];
        shipAmbienceSource.Play();
    }

    public void StopShipAmbience()
    {
        shipAmbienceSource.Stop();
        shipAmbienceSource.loop = false;
    }

    public void PlayEndCredits()
    {
        musicSource.Play();
    }

    public void PlayShipCrash()
    {
        crashSource.clip = crashSound;
        crashSource.Play();
    }

    public IEnumerator WaitForVoiceline(int ID)
    {
        if (ID >= voiceLines.Length) { yield break; }

        voiceSource.clip = voiceLines[ID].clip;
        voiceSource.Play();

        if (subtitleBox != null)
        {
            subtitleBox.text = voiceLines[ID].subtitle;
        }

        yield return new WaitForSeconds(voiceSource.clip.length);

        if (subtitleBox != null)
        {
            subtitleBox.text = "";
        }
    }
}
