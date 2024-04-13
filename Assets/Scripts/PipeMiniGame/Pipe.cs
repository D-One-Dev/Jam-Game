using UnityEngine;
using UnityEngine.UI;

public class Pipe : MonoBehaviour
{
    public bool[] ways;
    public int pipeType;
    [SerializeField] private RectTransform imageTransform;
    [SerializeField] private Image _image;
    [SerializeField] private Sprite[] unfilledSprites;
    [SerializeField] private Sprite[] filledSprites;
    public bool movable;
    public bool updated;
    public bool filled;
    public void RotatePipe(bool dir)
    {
        if (dir)
        {
            bool[] newWays = new bool[4];
            newWays[1] = ways[0];
            newWays[2] = ways[1];
            newWays[3] = ways[2];
            newWays[0] = ways[3];
            ways = newWays;
            imageTransform.localEulerAngles += new Vector3(0f, 0f, -90f);
        }
        else
        {
            bool[] newWays = new bool[4];
            newWays[3] = ways[0];
            newWays[2] = ways[3];
            newWays[1] = ways[2];
            newWays[0] = ways[1];
            ways = newWays;
            imageTransform.localEulerAngles += new Vector3(0f, 0f, 90f);
        }
    }

    public void UpdateImage()
    {
        //0 - 4way
        //1 - 3way
        //2 - 2wayAngle
        //3 - 2wayStraight
        switch (pipeType)
        {
            case 0:
                if (filled)
                {
                    _image.sprite = filledSprites[0];
                }
                else
                {
                    _image.sprite = unfilledSprites[0];
                }
                break;
            case 1:
                if (filled)
                {
                    _image.sprite = filledSprites[1];
                }
                else
                {
                    _image.sprite = unfilledSprites[1];
                }
                break;
            case 2:
                if (filled)
                {
                    _image.sprite = filledSprites[2];
                }
                else
                {
                    _image.sprite = unfilledSprites[2];
                }
                break;
            case 3:
                if (filled)
                {
                    _image.sprite = filledSprites[3];
                }
                else
                {
                    _image.sprite = unfilledSprites[3];
                }
                break;
            default:
                if (filled)
                {
                    _image.sprite = filledSprites[0];
                }
                else
                {
                    _image.sprite = unfilledSprites[0];
                }
                break;
        }

    }
}
