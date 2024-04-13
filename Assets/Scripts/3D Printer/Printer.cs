using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace _3D_Printer
{
    public class Printer : MonoBehaviour, IInteractable
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
                if (items.Contains(item.itemName))
                {
                    Destroy(other.gameObject);

                    for (int i = 0; i < items.Count; i++)
                    {
                        if (items[i] == item.itemName)
                        {
                            isItemReceived[i] = true;
                        }
                    }

                    if (IsAllItemsReceived())
                    {
                        tag = "Minigame";

                        showItemsList.text =
                            "Требуемые предметы загружены, пожалуйста запустите процесс печати, следуя инстуркции";
                    }
                }
            }
        }

        public void SpawnOutputItem()
        {
            GameObject spawnedObject = Instantiate(outputItem, spawnPlace.position, Quaternion.identity);

            spawnedObject.transform.localScale = new Vector3(1, 1, 1);
            
            showItemsList.text = "Предмет успешно получен";
            DayCounter.Instance.SetTrigger("3D");
        }

        private bool IsAllItemsReceived()
        {
            for (int i = 0; i < isItemReceived.Count; i++)
            {
                if (!isItemReceived[i]) return false;
            }

            return true;
        }

        public void TurnOn() {}

        public void TurnOff() {}
    }
}