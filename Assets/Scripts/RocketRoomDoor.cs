﻿using UnityEngine;

public class RocketRoomDoor : MonoBehaviour
{
    [SerializeField] private DoorCollider doorCollider;

    private void Start()
    {
        if (DayCounter.Instance.currentDay > 1) doorCollider.locked = false;
        else doorCollider.locked = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Item item))
        {
            if (item.itemName == "Шестеренка")
            {
                doorCollider.locked = false;
            }
        }
    }
}