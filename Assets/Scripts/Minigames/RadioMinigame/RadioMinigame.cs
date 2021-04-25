using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ToggleOption
{
    AMPLITUDE,
    FREQUENCY,
    X_SPEED,
    Y_SPEED
}

public class RadioMinigame : BaseMinigame
{
    [SerializeField] private Renderer sinWave;
    [SerializeField] private Renderer sinWaveTarget;
    [SerializeField] private Renderer statusLight;

    private int y_speed_target = 0;
    private int x_speed_target = 0;
    private int amp_target = 0;
    private int freq_target = 2;

    private float[] y_speed_presets = { 0, 0.5f, 1, 1.5f };
    private float[] x_speed_presets = { 0.5f, 1, 1.5f, 0};
    private float[] amp_presets = { 1, 1.5f, 2, 0.5f };
    private float[] freq_presets = { 0.5f, 0.75f, 1, 0.25f };

    private int current_y_speed = 0;
    private int current_x_speed = 0;
    private int current_amp = 0;
    private int current_freq = 0;

    public override void StartMinigame()
    {
        base.StartMinigame();
        current_amp += Random.Range(1, amp_presets.Length);
        current_freq += Random.Range(1, freq_presets.Length);
        current_x_speed += Random.Range(1, x_speed_presets.Length);
        current_y_speed += Random.Range(1, y_speed_presets.Length);
        statusLight.material.color = Color.red;
    }

    public override void FinishMinigame()
    {
        base.FinishMinigame();
        statusLight.material.color = Color.green;
    }


    // Start is called before the first frame update
    void Start()
    {
        statusLight.material.color = Color.green;
        StartMinigame();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateWave(sinWave, current_amp, current_freq, current_x_speed, current_y_speed);
        UpdateWave(sinWaveTarget, amp_target, freq_target, x_speed_target, y_speed_target);
    }

    public void UpdateWave(Renderer wave, int _amp, int _freq, int _x_speed, int _y_speed)
    {
        float amp = amp_presets[_amp];
        float freq = freq_presets[_freq];
        float x_speed = x_speed_presets[_x_speed];
        float y_speed = y_speed_presets[_y_speed];

        Vector2 x_offset = Vector2.left * Time.time * x_speed * amp;
        Vector2 y_offset = Vector2.down * Time.time * y_speed * freq;
        Vector2 tiling = new Vector2(freq, amp);

        wave.material.SetTextureOffset("_MainTex", x_offset + y_offset);
        wave.material.SetTextureScale("_MainTex", tiling);
    }

    public void toggle(ToggleOption option)
    {
        switch (option)
        {
            case ToggleOption.AMPLITUDE:
                current_amp = (current_amp + 1) % amp_presets.Length;
                break;
            case ToggleOption.FREQUENCY:
                current_freq = (current_freq + 1) % freq_presets.Length;
                break;
            case ToggleOption.X_SPEED:
                current_x_speed = (current_x_speed + 1) % x_speed_presets.Length;
                break;
            case ToggleOption.Y_SPEED:
                current_y_speed = (current_y_speed + 1) % y_speed_presets.Length;
                break;
            default:
                break;
        }

        if (current_amp == amp_target &&
            current_freq == freq_target &&
            current_x_speed == x_speed_target &&
            current_y_speed == y_speed_target)
        {
            FinishMinigame();
        }
        
    }
}
