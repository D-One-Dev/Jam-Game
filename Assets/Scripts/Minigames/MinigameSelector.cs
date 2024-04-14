using UnityEngine;

public class MinigameSelector : MonoBehaviour
{
    [SerializeField] private GameObject[] maps;
    [SerializeField] private IInteractable[] scripts;
    private IInteractable currentScript;

    public void TurnOn()
    {
        int day = PlayerPrefs.GetInt("Day", 1);
        if (maps[day - 1] != null)
        {
            maps[day - 1].SetActive(true);
            currentScript = maps[day - 1].GetComponent<IInteractable>();
            if (currentScript == null) Debug.LogError("Cannot find IInteractable script on object " + gameObject);
            else currentScript.TurnOn();
        }
    }

    public void TurnOff()
    {
        if(currentScript != null) currentScript.TurnOff();
    }
}
