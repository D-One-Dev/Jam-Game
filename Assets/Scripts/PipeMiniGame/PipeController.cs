using UnityEngine;
using UnityEngine.UI;

public class PipeController : MonoBehaviour, IInteractable
{
    [SerializeField] private Vector2Int cursorStartPoint;
    [SerializeField] private Vector2Int startPipe;
    [SerializeField] private Vector2Int endPipe;
    [SerializeField] private Pipe[] tiles;
    [SerializeField] private Image[] cursors;
    [SerializeField] private Vector2Int gridSize;
    [SerializeField] private int startAir;
    [SerializeField] private Image airSprite;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private Animator _animator;

    [SerializeField] private AudioClip cursorMove, pipeRotate, gameWin, gameLoose;

    private Vector2Int currentPipe = -Vector2Int.one;
    [SerializeField] private int currentAir;
    private Pipe[,] tileGrid;
    private Image[,] cursorGrid;
    private Controls _controls;
    private Vector2Int cursorPosition;
    private bool active;
    private bool gameWon;
    private void Awake()
    {
        _controls = new Controls();
        _controls.Gameplay.Up.performed += ctx => MoveUp();
        _controls.Gameplay.Down.performed += ctx => MoveDown();
        _controls.Gameplay.Left.performed += ctx => MoveLeft();
        _controls.Gameplay.Right.performed += ctx => MoveRight();
        _controls.Gameplay.Space.performed += ctx => Use();
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
        currentAir = startAir;
        cursorPosition = cursorStartPoint;
        tileGrid = new Pipe[gridSize.x, gridSize.y];
        cursorGrid = new Image[gridSize.x, gridSize.y];
        int i = 0;
        int j = 0;

        foreach (Pipe tile in tiles)
        {
            tileGrid[i, j] = tile;

            if (i < gridSize.x - 1) i++;
            else
            {
                i = 0;
                j++;
            }
        }

        i = 0;
        j = 0;
        foreach (Image cursor in cursors)
        {
            cursorGrid[i, j] = cursor;
            if (i < gridSize.x - 1) i++;
            else
            {
                i = 0;
                j++;
            }
        }

        UpdateCursor();
        UpdatePipes();
    }

    private void MoveUp()
    {
        if (PlayerInteraction.instance.playerStatus == 1 && active && !gameWon)
        {
            if(currentPipe == -Vector2Int.one)
            {
                if (cursorPosition.y > 0)
                {
                     cursorPosition.y--;
                     UpdateCursor();
                }
            }
        }
    }

    private void MoveDown()
    {
        if (PlayerInteraction.instance.playerStatus == 1 && active && !gameWon)
        {
            if (currentPipe == -Vector2Int.one)
            {
                if (cursorPosition.y < gridSize.y - 1)
                {
                    cursorPosition.y++;
                    UpdateCursor();
                }
            }
        }
    }

    private void MoveLeft()
    {
        if (PlayerInteraction.instance.playerStatus == 1 && active && !gameWon)
        {
            if (currentPipe == -Vector2Int.one)
            {
                if (cursorPosition.x > 0)
                {
                    cursorPosition.x--;
                    UpdateCursor();
                }
            }
            else
            {
                tileGrid[cursorPosition.x, cursorPosition.y].RotatePipe(false);
                UpdatePipes();
                SoundController.instance.PlaySoundRandomPitch(pipeRotate);
            }
        }
    }

    private void MoveRight()
    {
        if (PlayerInteraction.instance.playerStatus == 1 && active && !gameWon)
        {
            if (currentPipe == -Vector2Int.one)
            {
                if (cursorPosition.x < gridSize.x - 1)
                {
                    cursorPosition.x++;
                    UpdateCursor();
                }
            }
            else
            {
                tileGrid[cursorPosition.x, cursorPosition.y].RotatePipe(true);
                UpdatePipes();
                SoundController.instance.PlaySoundRandomPitch(pipeRotate);
            }
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

    public void UpdateCursor()
    {
        foreach(Image cursor in cursorGrid)
        {
            cursor.color = new Color(0f, 0f, 0f, 0f);
        }

        cursorGrid[cursorPosition.x, cursorPosition.y].color = Color.green;
        SoundController.instance.PlaySoundRandomPitch(cursorMove);
    }

    private void Use()
    {
        if (currentPipe == -Vector2Int.one)
        {
            currentPipe = cursorPosition;
            cursorGrid[cursorPosition.x, cursorPosition.y].color = Color.red;
        }
        else
        {
            currentPipe = -Vector2Int.one;
            cursorGrid[cursorPosition.x, cursorPosition.y].color = Color.green;
        }
    }

    private void UpdatePipes()
    {
        foreach(Pipe pipe in tileGrid)
        {
            pipe.updated = false;
            if (pipe.movable)
            {
                pipe.filled = false;
            }
        }
        UpdatePipe(startPipe);
        foreach(Pipe pipe in tileGrid) pipe.UpdateImage();
        CheckAir();
        if(currentAir > 0) CheckWin();
    }

    private void UpdatePipe(Vector2Int pos)
    {
        if (tileGrid[pos.x, pos.y].updated == false)
        {
            tileGrid[pos.x, pos.y].updated = true;
            if(pos.y > 0)
            {
                if(tileGrid[pos.x, pos.y].ways[0] && tileGrid[pos.x, pos.y - 1].ways[2])
                {
                    tileGrid[pos.x, pos.y - 1].filled = true;
                    UpdatePipe(new Vector2Int(pos.x, pos.y - 1));
                }
            }

            if (pos.y < gridSize.y - 1)
            {
                if (tileGrid[pos.x, pos.y].ways[2] && tileGrid[pos.x, pos.y + 1].ways[0])
                {
                    tileGrid[pos.x, pos.y + 1].filled = true;
                    UpdatePipe(new Vector2Int(pos.x, pos.y + 1));
                }
            }

            if (pos.x > 0)
            {
                if (tileGrid[pos.x, pos.y].ways[3] && tileGrid[pos.x - 1, pos.y].ways[1])
                {
                    tileGrid[pos.x - 1, pos.y].filled = true;
                    UpdatePipe(new Vector2Int(pos.x - 1, pos.y));
                }
            }

            if (pos.x < gridSize.x - 1)
            {
                if (tileGrid[pos.x, pos.y].ways[1] && tileGrid[pos.x + 1, pos.y].ways[3])
                {
                    tileGrid[pos.x + 1, pos.y].filled = true;
                    UpdatePipe(new Vector2Int(pos.x + 1, pos.y));
                }
            }
        }
    }

    private void CheckWin()
    {
        if (tileGrid[endPipe.x, endPipe.y].filled)
        {
            gameWon = true;
            Debug.Log("Win");
            SoundController.instance.PlaySoundRandomPitch(gameWin);
            _animator.SetTrigger("Win");
        }
    }

    private void CheckAir()
    {
        int air = -1;
        foreach(Pipe pipe in tileGrid)
        {
            if (pipe.filled) air++;
        }

        currentAir = startAir - air;

        airSprite.fillAmount = Mathf.Clamp(((float)currentAir / startAir), 0f, 1f);

        if(currentAir <= 0)
        {
            Debug.Log("Loose");
            SoundController.instance.PlaySoundRandomPitch(gameLoose);
            DeathController.instance.TriggerDeath("¬ы допустили утечку кислорода и задохнулись");
        }
    }
}
