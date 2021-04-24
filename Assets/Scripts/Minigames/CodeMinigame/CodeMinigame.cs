using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CodeMinigame : BaseMinigame
{
    
    [SerializeField] private TMPro.TextMeshProUGUI keypad_display;
    [SerializeField] private TMPro.TextMeshProUGUI code_display;
    [SerializeField] private TMPro.TextMeshProUGUI mapping_display;
    [SerializeField] private GameObject keypad;

    Dictionary<string, string> key_mapping;
    int mapping_size = 9;
    string sequence = "";
    string encoding = "";
    string current_sequence = "";

    public override void StartMinigame()
    {
        base.StartMinigame();
        GenerateSequence(5);
        code_display.text = encoding;
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateKeyMapping();
        GenerateSequence(5);
        code_display.text = encoding;
        keypad_display.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateKeyMapping()
    {
        key_mapping = new Dictionary<string, string>();
        List<char> characters = new List<char>("ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray());

        for (int i = 0; i < mapping_size; i++)
        {
            int c = Random.Range(0, characters.Count);
            key_mapping.Add((i + 1).ToString(), characters[c].ToString());
            characters.RemoveAt(c);
        }
        UpdateKeyMapping();
    }

    public void UpdateKeyMapping()
    {
        string update_text = "";
        for (int i = 1; i < mapping_size + 1; i++)
        {
            update_text += i.ToString() + " -> " + key_mapping[i.ToString()] + "\n";
        }
        mapping_display.text = update_text;
    }

    public void GenerateSequence(int len)
    {
        sequence = "";
        encoding = "";
        for (int i = 0; i < len; i++)
        {
            int num = Random.Range(1, mapping_size+1);
            sequence += num.ToString();
            encoding += key_mapping[num.ToString()];
        }

    }

    public void EnterKey(string s)
    {
        
        current_sequence += s;
        keypad_display.text = current_sequence;
        if (current_sequence.Length >= sequence.Length)
        {
            print(current_sequence);
            print(sequence);
            if (current_sequence == sequence)
            {
                keypad_display.text = "Correct";
                current_sequence = "";
            }
            else
            {
                keypad_display.text = "Incorrect";
                current_sequence = "";
            } 
        }
    }
}
