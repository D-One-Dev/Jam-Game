using UnityEngine;
using UnityEngine.UI;

public class RoverGame : MonoBehaviour, IInteractable
{
    [SerializeField] private Vector2Int roverStartPoint;
    [SerializeField] private Vector2Int itemPoint;
    [SerializeField] private MapTile[] tiles;
    [SerializeField] private Vector2Int gridSize;
    [SerializeField] private int startEnergy;
    [SerializeField] private Image energySprite;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private Animator _animator;
    [SerializeField] private bool canBreakRocks;

    [SerializeField] private AudioClip roverMove, rockBreak, gameWin, gameLoose, itemGet;

    private int currentEnergy;
    private MapTile[,] tileGrid;
    private Controls _controls;
    private Vector2Int roverPosition;
    private bool active;
    private bool gameWon;
    private bool hasItem;
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
        if(PlayerInteraction.instance.playerStatus == 1 && active && !gameWon)
        {
            if(roverPosition.y > 0 && tileGrid[roverPosition.x, roverPosition.y - 1].tileType != "Rock")
            {
                CheckItem(new Vector2Int(0, -1));
                if (CheckWin(new Vector2Int(0, -1)))
                {
                    Debug.Log("Win");
                }
                else
                {
                    tileGrid[roverPosition.x, roverPosition.y].tileType = "Ground";
                    roverPosition.y--;
                    tileGrid[roverPosition.x, roverPosition.y].tileType = "Rover";
                    UpdateGrid();
                    LooseEnergy();
                    SoundController.instance.PlaySoundRandomPitch(roverMove);
                }
            }
            else if (roverPosition.y > 0 && tileGrid[roverPosition.x, roverPosition.y - 1].tileType == "Rock")
            {
                if (canBreakRocks)
                {
                    tileGrid[roverPosition.x, roverPosition.y].tileType = "Ground";
                    roverPosition.y--;
                    tileGrid[roverPosition.x, roverPosition.y].tileType = "Rover";
                    UpdateGrid();
                    LooseEnergy();
                    LooseEnergy();
                    SoundController.instance.PlaySoundRandomPitch(rockBreak);
                }
            }
        }
    }

    private void MoveDown()
    {
        if (PlayerInteraction.instance.playerStatus == 1 && active && !gameWon)
        {
            if (roverPosition.y < gridSize.y - 1 && tileGrid[roverPosition.x, roverPosition.y + 1].tileType != "Rock")
            {
                CheckItem(new Vector2Int(0, 1));
                if (CheckWin(new Vector2Int(0, 1)))
                {
                    Debug.Log("Win");
                }
                else
                {
                    tileGrid[roverPosition.x, roverPosition.y].tileType = "Ground";
                    roverPosition.y++;
                    tileGrid[roverPosition.x, roverPosition.y].tileType = "Rover";
                    UpdateGrid();
                    LooseEnergy();
                    SoundController.instance.PlaySoundRandomPitch(roverMove);
                }
            }
            else if (roverPosition.y < gridSize.y - 1 && tileGrid[roverPosition.x, roverPosition.y + 1].tileType == "Rock")
            {
                if (canBreakRocks)
                {
                    tileGrid[roverPosition.x, roverPosition.y].tileType = "Ground";
                    roverPosition.y++;
                    tileGrid[roverPosition.x, roverPosition.y].tileType = "Rover";
                    UpdateGrid();
                    LooseEnergy();
                    LooseEnergy();
                    SoundController.instance.PlaySoundRandomPitch(rockBreak);
                }
            }

        }
    }

    private void MoveLeft()
    {
        if (PlayerInteraction.instance.playerStatus == 1 && active && !gameWon)
        {
             if (roverPosition.x > 0 && tileGrid[roverPosition.x - 1, roverPosition.y].tileType != "Rock")
             {
                CheckItem(new Vector2Int(-1, 0));
                if (CheckWin(new Vector2Int(-1, 0)))
                 {
                     Debug.Log("Win");
                 }
                 else
                 {
                     tileGrid[roverPosition.x, roverPosition.y].tileType = "Ground";
                     roverPosition.x--;
                     tileGrid[roverPosition.x, roverPosition.y].tileType = "Rover";
                     UpdateGrid();
                     LooseEnergy();
                    SoundController.instance.PlaySoundRandomPitch(roverMove);
                }
             }
            else if (roverPosition.x > 0 && tileGrid[roverPosition.x - 1, roverPosition.y].tileType == "Rock")
            {
                if (canBreakRocks)
                {
                    tileGrid[roverPosition.x, roverPosition.y].tileType = "Ground";
                    roverPosition.x--;
                    tileGrid[roverPosition.x, roverPosition.y].tileType = "Rover";
                    UpdateGrid();
                    LooseEnergy();
                    LooseEnergy();
                    SoundController.instance.PlaySoundRandomPitch(rockBreak);
                }
            }
        }
    }

    private void MoveRight()
    {
        if (PlayerInteraction.instance.playerStatus == 1 && active && !gameWon)
        {
             if (roverPosition.x < gridSize.x - 1 && tileGrid[roverPosition.x + 1, roverPosition.y].tileType != "Rock")
             {
                 CheckItem(new Vector2Int(1, 0));
                 if (CheckWin(new Vector2Int(1, 0)))
                 {
                     Debug.Log("Win");
                 }
                 else
                 {
                     tileGrid[roverPosition.x, roverPosition.y].tileType = "Ground";
                     roverPosition.x++;
                     tileGrid[roverPosition.x, roverPosition.y].tileType = "Rover";
                     UpdateGrid();
                     LooseEnergy();
                    SoundController.instance.PlaySoundRandomPitch(roverMove);
                }
             }
            else if (roverPosition.x < gridSize.x - 1 && tileGrid[roverPosition.x + 1, roverPosition.y].tileType == "Rock")
            {
                if (canBreakRocks)
                {
                    tileGrid[roverPosition.x, roverPosition.y].tileType = "Ground";
                    roverPosition.x++;
                    tileGrid[roverPosition.x, roverPosition.y].tileType = "Rover";
                    UpdateGrid();
                    LooseEnergy();
                    LooseEnergy();
                    SoundController.instance.PlaySoundRandomPitch(rockBreak);
                }
            }
        }
    }

    private void UpdateGrid()
    {
        if(roverPosition != roverStartPoint) tileGrid[roverStartPoint.x, roverStartPoint.y].tileType = "Base";
        if (roverPosition != itemPoint && hasItem) tileGrid[itemPoint.x, itemPoint.y].tileType = "Ground";
        foreach(MapTile tile in tileGrid)
        {
            tile.UpdateTile();
        }
    }

    private void LooseEnergy()
    {
        if (currentEnergy > 0) currentEnergy--;
        energySprite.fillAmount = (float)currentEnergy / startEnergy;
        if (currentEnergy == 0)
        {
            SoundController.instance.PlaySoundRandomPitch(gameLoose);
            DeathController.instance.TriggerDeath("Луноход не смог доставить необходимые материалы. Вы остались на луне и погибли от голода");
            Debug.Log("Loose");
        }
    }

    public void TurnOn()
    {
        active = true;
        gameUI.SetActive(true);
    }

    public void TurnOff()
    {
        active = false;
        gameUI.SetActive(false);
    }

    public void CheckItem(Vector2Int dir)
    {
        if (roverPosition + dir == itemPoint)
        {
            hasItem = true;
            SoundController.instance.PlaySoundRandomPitch(itemGet);
        }
    }

    private bool CheckWin(Vector2Int dir)
    {
        if(roverPosition + dir == roverStartPoint && hasItem)
        {
            SoundController.instance.PlaySoundRandomPitch(gameWin);
            gameWon = true;
            _animator.SetTrigger("Win");
            DayCounter.Instance.SetTrigger("Rover");
            Dispenser.Instance.SpawnOre();
            return true;
        }
        return false;
    }
}
