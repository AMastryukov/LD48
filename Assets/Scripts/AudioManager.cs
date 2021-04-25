using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    private AudioSource src;
    [SerializeField] TMPro.TextMeshProUGUI subtitleBox;

    private void Start()
    {
        
    }

    public IEnumerator play(SubtitleAudio sub)
    {
        src.clip = sub.clip;
        src.Play();
        subtitleBox.text = sub.subtitle;
        yield return new WaitForSeconds(src.clip.length);
        subtitleBox.text = "";
    }
}
