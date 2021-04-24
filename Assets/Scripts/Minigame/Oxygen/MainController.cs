using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class MainController : MonoBehaviour
{

    public Text computertexta;
    public Text computertextb;
    public Text computertextc;

    public Image computerimagea;
    public Image computerimageb;
    public Image computerimagec;

    public Sprite orientation_up;
    public Sprite orientation_down;
    public Sprite orientation_left;
    public Sprite orientation_right;

    public List<Sprite> orientationSprites;

    //public Texture2D clockwise;
    //public Texture2D counterclockwise;

    // Start is called before the first frame update
    void Start()
    {

        List<int> list = new List<int> { 1, 2, 3};
        list = list.OrderBy(i => Random.value).ToList();

        //randomize valve numbers
        
        computertexta.text = list[0].ToString();
        
        computertextb.text = list[1].ToString();
        
        computertextc.text = list[2].ToString();

        orientationSprites.Add(orientation_up);
        orientationSprites.Add(orientation_down);
        orientationSprites.Add(orientation_left);
        orientationSprites.Add(orientation_right);

        //randomize valve rotations
        int randomorientationA = Random.Range(0, 3);
        computerimagea.sprite = orientationSprites[randomorientationA];

        int randomorientationB = Random.Range(0, 3);
        computerimageb.sprite = orientationSprites[randomorientationB];

        int randomorientationC = Random.Range(0, 3);
        computerimagec.sprite = orientationSprites[randomorientationC];

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
