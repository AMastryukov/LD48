using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadButton : Interactable
{

    [SerializeField] CodeMinigame codegame;
    private AudioSource src;
    private bool isPressed;

    // Start is called before the first frame update
    void Start()
    {
        isPressed = false;
        src = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pressed(string s)
    {
        if (isPressed) return;
        src.pitch = Random.Range(0.8f, 1.2f);
        src.Play();
        codegame.EnterKey(s);
        StartCoroutine(AnimatePress());
    }

    private IEnumerator AnimatePress()
    {
        isPressed = true;
        Vector3 original = transform.localPosition;
        transform.localPosition = original + Vector3.right*0.03f;
        yield return new WaitForSeconds(0.2f);
        transform.localPosition = original;
        isPressed = false;
    }



}
