using System;
using UnityEngine;

public class Dispenser : MonoBehaviour
{
    [SerializeField] private Transform spawnPlace;

    [SerializeField] private GameObject plastic, diamondOre, metalOre;
    [SerializeField] private GameObject recycledPlastic, recycledDiamond, recycledMetal;

    public static Dispenser Instance;

    private void Awake() => Instance = this;

    private void Start()
    {
        switch (DayCounter.Instance.currentDay)
        {
            case 2:
                Instantiate(recycledPlastic, spawnPlace.position, Quaternion.identity);
                break;
            case 4:
                Instantiate(recycledDiamond, spawnPlace.position, Quaternion.identity);
                break;
            case 6:
                Instantiate(recycledMetal, spawnPlace.position, Quaternion.identity);
                break;
            case 7:
                Instantiate(recycledMetal, spawnPlace.position, Quaternion.identity);
                break;
        }
    }

    public void SpawnOre()
    {
        GameObject spawnedObject;
        
        switch (DayCounter.Instance.currentDay)
        {
            case 1:
                print("spawned");
                spawnedObject = Instantiate(plastic, spawnPlace.position, Quaternion.identity);
                //spawnedObject.transform.localScale = new Vector3(1, 1, 1);
                break;
            case 3:
                spawnedObject = Instantiate(diamondOre, spawnPlace.position, Quaternion.identity);
                //spawnedObject.transform.localScale = new Vector3(1, 1, 1);
                break;
            case 5:
                spawnedObject = Instantiate(metalOre, spawnPlace.position, Quaternion.identity);
                //spawnedObject.transform.localScale = new Vector3(1, 1, 1);
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Item item))
        {
            if (item.itemName == "Бур")
            {
                //Улучшение бура для лунахода
                Destroy(other.gameObject);
            }
        }
    }
}