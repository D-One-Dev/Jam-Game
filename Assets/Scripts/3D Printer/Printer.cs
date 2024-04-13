using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace _3D_Printer
{
    public class Printer : MonoBehaviour
    {
        [SerializeField] private List<string> items = new List<string>();
        
        [SerializeField] private List<bool> isItemReceived;
        
        [SerializeField] private TMP_Text showItemsList;

        [SerializeField] private GameObject outputItem;
        [SerializeField] private Transform spawnPlace;

        private void Start()
        {
            showItemsList.text = "Требуются следующие предметы для переработки:";

            for (int i = 0; i < items.Count; i++)
            {
                showItemsList.text += "\n" + items[i];
                isItemReceived.Add(false);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Item item))
            {
                if (items.Contains(item.name))
                {
                    Destroy(other.gameObject);

                    for (int i = 0; i < items.Count; i++)
                    {
                        if (items[i] == item.name)
                        {
                            isItemReceived[i] = true;
                        }
                    }

                    if (IsAllItemsReceived())
                    {
                        GameObject spawnedObject = Instantiate(outputItem, spawnPlace.position, Quaternion.identity);

                        spawnedObject.transform.localScale = new Vector3(1, 1, 1);
                    }
                }
            }
        }

        private bool IsAllItemsReceived()
        {
            for (int i = 0; i < isItemReceived.Count; i++)
            {
                if (!isItemReceived[i]) return false;
            }

            return true;
        }
    }
}