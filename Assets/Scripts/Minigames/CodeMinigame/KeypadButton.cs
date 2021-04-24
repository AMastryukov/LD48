using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadButton : Interactable
{

    [SerializeField] CodeMinigame codegame;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pressed(string s)
    {
        codegame.EnterKey(s);
    }



}
