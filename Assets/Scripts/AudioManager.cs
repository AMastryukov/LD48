using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AudioManager : MonoBehaviour
{
    public float EndCreditsSongLength { get { return musicSource.clip.length; } }

    [SerializeField] private AudioSource voiceSource;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private TextMeshProUGUI subtitleBox;

    public void PlayEndCredits()
    {
        musicSource.Play();
    }

    public IEnumerator PlayVoiceline(SubtitleAudio sub)
    {
        voiceSource.clip = sub.clip;
        voiceSource.Play();

        subtitleBox.text = sub.subtitle;

        yield return new WaitForSeconds(voiceSource.clip.length);

        subtitleBox.text = "";
    }
}
