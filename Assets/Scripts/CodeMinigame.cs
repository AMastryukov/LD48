using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CodeMinigame : MonoBehaviour
{
    Dictionary<string, string> key_mapping;
    int mapping_size = 9;
    List<int> sequence;
    
    [SerializeField] private TMPro.TextMeshProUGUI keypad_display;
    [SerializeField] private TMPro.TextMeshPro mapping;
    [SerializeField] private GameObject keypad;


    // Start is called before the first frame update
    void Start()
    {
        key_mapping = new Dictionary<string, string>();
        List<char> characters = new List<char>("ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray());
        
        for (int i = 0; i < mapping_size; i++)
        {
            int c = Random.Range(0, characters.Count);
            key_mapping.Add((i+1).ToString(), characters[c].ToString());
            characters.RemoveAt(c);
        }
        GenerateSequence(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateSequence(int len)
    {
        sequence = new List<int>();
        for (int i = 0; i < len; i++)
        {
            int num = Random.Range(1, mapping_size+1);
            sequence.Add(num);
        }
    }

    public void EnterKey()
    {
        string s = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text;
        if (keypad_display.text.Length >= 5)
        {
            keypad_display.text = "";
        }
        keypad_display.text = keypad_display.text + s;
    }
}
