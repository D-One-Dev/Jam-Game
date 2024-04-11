using TMPro;
using UnityEngine;

public class DayCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text dayText;
    [SerializeField] private Animator daySceenAnim;
    private bool roverGameCompleted = true, signalGameCompleted = true, repairGameCompleted = true;
    public static DayCounter Instance;
    public int currentDay = 1;
    private void Awake()
    {
        dayText.text = "Δενό " + currentDay;
    }
    void Start()
    {
        Instance = this;
    }
    
    public void GoToNextDay()
    {
        if(roverGameCompleted && signalGameCompleted && repairGameCompleted)
        {
            currentDay++;
            dayText.text = "Δενό " + currentDay;
            daySceenAnim.SetTrigger("FadeIn");
        }
    }
}
