using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioMinigame : MonoBehaviour
{
    [SerializeField] private Renderer sinWave;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 offset = sinWave.material.GetTextureOffset("_MainTex");
        sinWave.material.SetTextureOffset("_MainTex", offset + Vector2.left * Time.deltaTime);
    }
}
