using TMPro;
using UnityEngine;

public class CurrentTasks : MonoBehaviour
{
    [SerializeField] private TMP_Text taskText;
    void Start()
    {
        switch (DayCounter.Instance.currentDay)
        {
            case 1:
               taskText.text = "Задачи на день 1: \n" +
                  "Починить проводку\n" +
                  "Собрать материалы луноходом\n" +
                  "Переработать собранные материалы";
               break;
            case 2:
                taskText.text = "Задачи на день 2: \n" +
                   "Устранить утечку кислорода\n" +
                   "Взломать базу данных\n" +
                   "Распечатать деталь на 3Д принтере";
                break;
            case 3:
                taskText.text = "Задачи на день 3: \n" +
                   "Собрать материалы луноходом\n" +
                  "Переработать собранные материалы";
                break;
            case 4:
                taskText.text = "Задачи на день 4: \n" +
                   "Устранить утечку кислорода\n" +
                   "Взломать базу данных\n" +
                   "Распечатать деталь на 3Д принтере";
                break;
            case 5:
                taskText.text = "Задачи на день 5: \n" +
                  "Починить проводку\n" +
                  "Собрать материалы луноходом\n" +
                  "Переработать собранные материалы";
                break;
            case 6:
                taskText.text = "Задачи на день 6: \n" +
                   "Починить проводку\n" +
                   "Устранить утечку кислорода";
                break;
            case 7:
                taskText.text = "Задачи на день 7: \n" +
                   "Взломать базу данных\n" +
                   "Распечатать деталь на 3Д принтере\n" +
                   "Улететь домой";
                break;

        }
    }
}
