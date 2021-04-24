using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class OxygenMinigame : BaseMinigame
{

    public Text computertexta;
    public Text computertextb;
    public Text computertextc;

    public Image computerimagea;
    public Image computerimageb;
    public Image computerimagec;

    public Sprite up;
    public Sprite down;
    public Sprite left;
    public Sprite right;

    public List<OxygenValve> valves;

    public List<Image> computerImage;
    public List<Sprite> orientationSprites;

    public int randomorientationA;
    public int randomorientationB;
    public int randomorientationC;

    public int indexof1;
    public int indexof2;
    public int indexof3;

    public GameObject valve1;
    public GameObject valve2;
    public GameObject valve3;

    public override void StartMinigame()
    {
        SetupMinigame();
        base.StartMinigame();
    }

    private void OnEnable()
    {
        OxygenValve.OnValveRotated += CheckRotations;
    }

    private void OnDisable()
    {
        OxygenValve.OnValveRotated -= CheckRotations;
        
    }

    private void CheckRotations()
    {
        OxygenValve valve1script = valve1.GetComponent<OxygenValve>();
        OxygenValve valve2script = valve2.GetComponent<OxygenValve>();
        OxygenValve valve3script = valve3.GetComponent<OxygenValve>();
        
        //garbage unity garbage string garbage
        string str1 = computerImage[indexof1].sprite.ToString();
        string str2 = computerImage[indexof2].sprite.ToString();
        string str3 = computerImage[indexof3].sprite.ToString();
        string remove = " (UnityEngine.Sprite)"; ////////////////////////

        string condition1 = str1.Replace(remove, "");
        string condition2 = str2.Replace(remove, "");
        string condition3 = str3.Replace(remove, "");

        //if all conditions are met
        if ((valve1script.currentOrientation.ToString() == condition1) && (valve2script.currentOrientation.ToString() == condition2) && (valve3script.currentOrientation.ToString() == condition3))
        {
            FinishMinigame();
        }
    }

    private void SetupMinigame()
    {
        //add sprites to list
        orientationSprites.Add(up);
        orientationSprites.Add(right);
        orientationSprites.Add(down);
        orientationSprites.Add(left);

        //add images to list
        computerImage.Add(computerimagea);
        computerImage.Add(computerimageb);
        computerImage.Add(computerimagec);

        //randomizes position of 1, 2, 3
        List<int> list = new List<int> () { 1, 2, 3 };
        list = list.OrderBy(i => Random.value).ToList();

        //finds index of each number
        indexof1 = list.IndexOf(1);
        indexof2 = list.IndexOf(2);
        indexof3 = list.IndexOf(3);

        //randomize valve numbers
        computertexta.text = list[0].ToString();
        computertextb.text = list[1].ToString();
        computertextc.text = list[2].ToString();
        
        //randomize valve rotations
        randomorientationA = Random.Range(0, 3);
        computerimagea.sprite = orientationSprites[randomorientationA];

        randomorientationB = Random.Range(0, 3);
        computerimageb.sprite = orientationSprites[randomorientationB];

        randomorientationC = Random.Range(0, 3);
        computerimagec.sprite = orientationSprites[randomorientationC];

    }


    // Start is called before the first frame update
    void Start()
    {
        SetupMinigame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
