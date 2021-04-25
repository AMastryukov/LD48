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

    Dictionary<string, string> key_mapping;
    int mapping_size = 9;
    string sequence = "";
    string encoding = "";
    string current_sequence = "";

    public override void StartMinigame()
    {
        base.StartMinigame();
        GenerateSequence(6);

        code_display.text = encoding;
        keypad_display.text = "";
    }

    public override void FinishMinigame()
    {
        base.FinishMinigame();
        code_display.text = "";
    }

    void Start()
    {
        GenerateKeyMapping();

        keypad_display.text = "";
        code_display.text = "";
    }

    /*
     * This method generates a key mapping. A good idea to only call this 
     * once per game, unless you really want to confuse the player.
     * Also updates the UI mapping in the game
     */
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
        UpdateKeyMappingDisplay();
    }

    /*
     * Just updating the UI
     */
    public void UpdateKeyMappingDisplay()
    {
        string update_text = "";
        for (int i = 1; i < mapping_size + 1; i++)
        {
            update_text += i.ToString() + "=" + key_mapping[i.ToString()] + " ";

            if (i % 3 == 0) { update_text += "\n"; }
        }
        mapping_display.text = update_text;
    }

    /*
     * Generate a sequence on len digits. Encoding is also generated using the key mapping.
     * Encoding is the sequence that is shown to the player, which he/she has to decode using
     * the mapping UI.
     */
    public void GenerateSequence(int len)
    {
        sequence = "";
        encoding = "";
        for (int i = 0; i < len; i++)
        {
            int num = Random.Range(1, mapping_size + 1);
            sequence += num.ToString();
            encoding += key_mapping[num.ToString()];
        }

    }

    /*
     * Refister Key press by the player. Update current_sequence and compare to the
     * target sequence if needed.
     * Also end game if sequence is correct.
     */
    public void EnterKey(string s)
    {
        if (!isActive) return;

        current_sequence += s;
        keypad_display.text = current_sequence;
        if (current_sequence.Length >= sequence.Length)
        {
            if (current_sequence == sequence)
            {
                keypad_display.text = "Correct";
                current_sequence = "";

                FinishMinigame();
            }
            else
            {
                keypad_display.text = "Incorrect";
                current_sequence = "";
            }
        }
    }
}
