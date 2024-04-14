using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace _3D_Printer
{
    public class Printer : MonoBehaviour, IInteractable
    {
        [SerializeField] private List<string> day2Items = new List<string>();
        [SerializeField] private List<string> day4Items = new List<string>();
        [SerializeField] private List<string> day7Items = new List<string>();

        [SerializeField] private List<bool> isItemReceived;
        
        [SerializeField] private TMP_Text showItemsList;

        [SerializeField] private GameObject day2OutputItem;
        [SerializeField] private GameObject day4OutputItem;
        [SerializeField] private GameObject day7OutputItem;
        [SerializeField] private Transform spawnPlace;

        private void Start()
        {
            Debug.Log(DayCounter.Instance.currentDay);
            switch (DayCounter.Instance.currentDay)
            {
                case 2:
                    showItemsList.text = "Требуются следующие предметы для переработки:";

                    for (int i = 0; i < day2Items.Count; i++)
                    {
                        showItemsList.text += "\n" + day2Items[i];
                        isItemReceived.Add(false);
                    }
                    break;
                case 4:
                    showItemsList.text = "Требуются следующие предметы для переработки:";

                    for (int i = 0; i < day4Items.Count; i++)
                    {
                        showItemsList.text += "\n" + day4Items[i];
                        isItemReceived.Add(false);
                    }
                    break;
                case 7:
                    showItemsList.text = "Требуются следующие предметы для переработки:";

                    for (int i = 0; i < day7Items.Count; i++)
                    {
                        showItemsList.text += "\n" + day7Items[i];
                        isItemReceived.Add(false);
                    }
                    break;
                default:
                    showItemsList.text = "";
                    break;
            }

            //showItemsList.text = "Требуются следующие предметы для переработки:";

            //for (int i = 0; i < items.Count; i++)
            //{
            //    showItemsList.text += "\n" + items[i];
            //    isItemReceived.Add(false);
            //}
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Item item))
            {
                switch (DayCounter.Instance.currentDay)
                {
                    case 2:
                        if (day2Items.Contains(item.itemName))
                        {
                            Destroy(other.gameObject);

                            for (int i = 0; i < day2Items.Count; i++)
                            {
                                if (day2Items[i] == item.itemName)
                                {
                                    isItemReceived[i] = true;
                                }
                            }

                            if (IsAllItemsReceived())
                            {
                                tag = "Minigame";

                                showItemsList.text = "Требуемые предметы загружены, пожалуйста запустите процесс печати, следуя инструкции";
                            }
                        }
                        break;
                    case 4:
                        if (day4Items.Contains(item.itemName))
                        {
                            Destroy(other.gameObject);

                            for (int i = 0; i < day4Items.Count; i++)
                            {
                                if (day4Items[i] == item.itemName)
                                {
                                    isItemReceived[i] = true;
                                }
                            }

                            if (IsAllItemsReceived())
                            {
                                tag = "Minigame";

                                showItemsList.text = "Требуемые предметы загружены, пожалуйста запустите процесс печати, следуя инструкции";
                            }
                        }
                        break;
                    case 7:
                        if (day7Items.Contains(item.itemName))
                        {
                            Destroy(other.gameObject);

                            for (int i = 0; i < day7Items.Count; i++)
                            {
                                if (day7Items[i] == item.itemName)
                                {
                                    isItemReceived[i] = true;
                                }
                            }

                            if (IsAllItemsReceived())
                            {
                                tag = "Minigame";

                                showItemsList.text = "Требуемые предметы загружены, пожалуйста запустите процесс печати, следуя инструкции";
                            }
                        }
                        break;
                    default:
                        break;
                }

                //if (items.Contains(item.itemName))
                //{
                //    Destroy(other.gameObject);

                //    for (int i = 0; i < items.Count; i++)
                //    {
                //        if (items[i] == item.itemName)
                //        {
                //            isItemReceived[i] = true;
                //        }
                //    }

                //    if (IsAllItemsReceived())
                //    {
                //        tag = "Minigame";

                //        showItemsList.text = "Требуемые предметы загружены, пожалуйста запустите процесс печати, следуя инструкции";
                //    }
                //}
            }
        }

        public void SpawnOutputItem()
        {
            GameObject spawnedObject;
            switch (DayCounter.Instance.currentDay)
            {
                case 2:
                    spawnedObject = Instantiate(day2OutputItem, spawnPlace.position, Quaternion.identity);

                    spawnedObject.transform.localScale = new Vector3(1, 1, 1);

                    showItemsList.text = "Предмет успешно получен";
                    DayCounter.Instance.SetTrigger("3D");
                    break;
                case 4:
                    spawnedObject = Instantiate(day4OutputItem, spawnPlace.position, Quaternion.identity);

                    spawnedObject.transform.localScale = new Vector3(1, 1, 1);

                    showItemsList.text = "Предмет успешно получен";
                    DayCounter.Instance.SetTrigger("3D");
                    break;
                case 7:
                    spawnedObject = Instantiate(day7OutputItem, spawnPlace.position, Quaternion.identity);

                    spawnedObject.transform.localScale = new Vector3(1, 1, 1);

                    showItemsList.text = "Предмет успешно получен";
                    DayCounter.Instance.SetTrigger("3D");
                    break;
                default:
                    break;
            }
            //GameObject spawnedObject = Instantiate(outputItem, spawnPlace.position, Quaternion.identity);

            //spawnedObject.transform.localScale = new Vector3(1, 1, 1);
            
            //showItemsList.text = "Предмет успешно получен";
            //DayCounter.Instance.SetTrigger("3D");
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