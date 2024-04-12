using TMPro;
using UnityEngine;

public class DayTextShower : MonoBehaviour
{
    [SerializeField] private TMP_Text dayText;
    void Awake()
    {
        int currentDay = PlayerPrefs.GetInt("Day", 1);
        dayText.text = "Δενό " + currentDay;
    }
}
