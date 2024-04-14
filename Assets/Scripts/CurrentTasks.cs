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
               taskText.text = "������ �� ���� 1: \n" +
                  "�������� ��������\n" +
                  "������� ��������� ���������\n" +
                  "������������ ��������� ���������";
               break;
            case 2:
                taskText.text = "������ �� ���� 2: \n" +
                   "��������� ������ ���������\n" +
                   "�������� ���� ������\n" +
                   "����������� ������ �� 3� ��������";
                break;
            case 3:
                taskText.text = "������ �� ���� 3: \n" +
                   "������� ��������� ���������\n" +
                  "������������ ��������� ���������";
                break;
            case 4:
                taskText.text = "������ �� ���� 4: \n" +
                   "��������� ������ ���������\n" +
                   "�������� ���� ������\n" +
                   "����������� ������ �� 3� ��������";
                break;
            case 5:
                taskText.text = "������ �� ���� 5: \n" +
                  "�������� ��������\n" +
                  "������� ��������� ���������\n" +
                  "������������ ��������� ���������";
                break;
            case 6:
                taskText.text = "������ �� ���� 6: \n" +
                   "�������� ��������\n" +
                   "��������� ������ ���������";
                break;
            case 7:
                taskText.text = "������ �� ���� 7: \n" +
                   "�������� ���� ������\n" +
                   "����������� ������ �� 3� ��������\n" +
                   "������� �����";
                break;

        }
    }
}
