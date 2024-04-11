using UnityEngine;
using UnityEngine.UI;

public class MapTile : MonoBehaviour
{
    public string tileType;
    [SerializeField] private Image tileImage;
    [SerializeField] private Sprite[] tileSprites;

    public void UpdateTile()
    {
        switch (tileType)
        {
            case "Ground":
                tileImage.sprite = tileSprites[0];
                break;
            case "Rock":
                tileImage.sprite = tileSprites[1];
                break;
            case "Rover":
                tileImage.sprite = tileSprites[2];
                break;
            case "Item":
                tileImage.sprite = tileSprites[3];
                break;
            default:
                tileImage.sprite = tileSprites[0];
                break;
        }
    }
}
