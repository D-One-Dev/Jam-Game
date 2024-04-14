using Sylphiette;
using UnityEngine;

public class RocketRoomDoor : MonoBehaviour
{
    [SerializeField] private DoorCollider doorCollider;

    private void Start()
    {
        if (DayCounter.Instance.currentDay > 2) doorCollider.locked = false;
        else doorCollider.locked = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Item item))
        {
            if (item.itemName == "Шестеренка")
            {
                doorCollider.locked = false;
                SylphietteDialogueSystem.Instance.StartNextDialogue();
                Destroy(other.gameObject);
            }
        }
    }
}