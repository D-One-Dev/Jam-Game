using TMPro;
using UnityEngine;

public class RoverGame : MonoBehaviour
{
    [SerializeField] private Vector2Int roverStartPoint;
    [SerializeField] private Vector2Int itemPoint;
    [SerializeField] private MapTile[] tiles;
    [SerializeField] private Vector2Int gridSize;
    [SerializeField] private int startEnergy;
    [SerializeField] private TMP_Text energyText;
    private int currentEnergy;
    private MapTile[,] tileGrid;
    private Controls _controls;
    private Vector2Int roverPosition;
    private void Awake()
    {
        _controls = new Controls();
        _controls.Gameplay.Up.performed += ctx => MoveUp();
        _controls.Gameplay.Down.performed += ctx => MoveDown();
        _controls.Gameplay.Left.performed += ctx => MoveLeft();
        _controls.Gameplay.Right.performed += ctx => MoveRight();
    }
    private void OnEnable()
    {
        _controls.Enable();
    }
    private void OnDisable()
    {
        _controls.Disable();
    }

    private void Start()
    {
        currentEnergy = startEnergy;
        roverPosition = roverStartPoint;
        tileGrid = new MapTile[gridSize.x, gridSize.y];
        int i = 0;
        int j = 0;

        foreach (MapTile tile in tiles)
        {
            tileGrid[i,j] = tile;
            if (i < gridSize.x - 1) i++;
            else
            {
                i = 0;
                j++;
            }
        }
    }

    private void MoveUp()
    {
        if(PlayerInteraction.instance.playerStatus == 1)
        {
            if(roverPosition.y > 0 && tileGrid[roverPosition.x, roverPosition.y - 1].tileType != "Rock")
            {
                tileGrid[roverPosition.x, roverPosition.y].tileType = "Ground";
                roverPosition.y--;
                tileGrid[roverPosition.x, roverPosition.y].tileType = "Rover";
                UpdateGrid();
                LooseEnergy();
            }
        }
    }

    private void MoveDown()
    {
        if (PlayerInteraction.instance.playerStatus == 1)
        {
            if (roverPosition.y < gridSize.y - 1 && tileGrid[roverPosition.x, roverPosition.y + 1].tileType != "Rock")
            {
                tileGrid[roverPosition.x, roverPosition.y].tileType = "Ground";
                roverPosition.y++;
                tileGrid[roverPosition.x, roverPosition.y].tileType = "Rover";
                UpdateGrid();
                LooseEnergy();
            }
        }
    }

    private void MoveLeft()
    {
        if (PlayerInteraction.instance.playerStatus == 1)
        {
            if (roverPosition.x > 0 && tileGrid[roverPosition.x - 1, roverPosition.y].tileType != "Rock")
            {
                tileGrid[roverPosition.x, roverPosition.y].tileType = "Ground";
                roverPosition.x--;
                tileGrid[roverPosition.x, roverPosition.y].tileType = "Rover";
                UpdateGrid();
                LooseEnergy();
            }
        }
    }

    private void MoveRight()
    {
        if (PlayerInteraction.instance.playerStatus == 1)
        {
            if (roverPosition.x < gridSize.x - 1 && tileGrid[roverPosition.x + 1, roverPosition.y].tileType != "Rock")
            {
                tileGrid[roverPosition.x, roverPosition.y].tileType = "Ground";
                roverPosition.x++;
                tileGrid[roverPosition.x, roverPosition.y].tileType = "Rover";
                UpdateGrid();
                LooseEnergy();
            }
        }
    }

    private void UpdateGrid()
    {
        foreach(MapTile tile in tileGrid)
        {
            tile.UpdateTile();
        }
    }

    private void LooseEnergy()
    {
        if (currentEnergy > 1) currentEnergy--;
        energyText.text = currentEnergy.ToString();
    }
}
