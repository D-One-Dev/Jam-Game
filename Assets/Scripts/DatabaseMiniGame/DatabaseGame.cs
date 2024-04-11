using UnityEngine;

public class DatabaseGame : MonoBehaviour
{
    [SerializeField] private RectTransform cursor;
    [SerializeField] private RectTransform leftBorder, rightBorder;
    [SerializeField] private RectTransform[] greenZones;
    [SerializeField] private float cursorMoveSpeed;
    private Controls _controls;
    private void Awake()
    {
        _controls = new Controls();
        _controls.Gameplay.Space.performed += ctx => Catch();
    }
    private void OnEnable()
    {
        _controls.Enable();
    }
    private void OnDisable()
    {
        _controls.Disable();
    }

    private void FixedUpdate()
    {
        cursor.position += new Vector3(cursorMoveSpeed, 0f, 0f);
        if (cursor.position.x < leftBorder.position.x || cursor.position.x > rightBorder.position.x) cursorMoveSpeed *= -1;
    }

    private void Catch()
    {
        if(PlayerInteraction.instance.playerStatus == 1)
        {
            RectTransform zone = CheckCollision();
            if (CheckCollision() != null)
            {
                zone.sizeDelta = new Vector2(zone.sizeDelta.x / 1.5f, zone.sizeDelta.y);
            }
            else
            {
                Debug.Log("Loose");
            }
        }
    }

    private RectTransform CheckCollision()
    {
        foreach (RectTransform zone in greenZones)
        {
            if (cursor.localPosition.x >= zone.localPosition.x - zone.rect.width && cursor.localPosition.x <= zone.localPosition.x + zone.rect.width)
            { 
                Debug.Log(cursor.localPosition.x + "||" + (zone.localPosition.x - zone.rect.width) + "||" + (zone.localPosition.x + zone.rect.width));
                return zone;
            }
        }
        return null;
    }
}