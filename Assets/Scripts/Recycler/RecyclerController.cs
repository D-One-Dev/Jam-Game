using UnityEngine;

public class RecyclerController : MonoBehaviour
{
    [SerializeField] private string[] day1Tasks;
    [SerializeField] private string[] day3Tasks;
    [SerializeField] private string[] day5Tasks;

    [SerializeField] private GameObject day2Object, day4Object, day6Object;

    private int currentDay;

    private bool[] day1Completed;
    private bool[] day3Completed;
    private bool[] day5Completed;

    private void Start()
    {
        day1Completed = new bool[day1Tasks.Length];
        day3Completed = new bool[day3Tasks.Length];
        day5Completed = new bool[day5Tasks.Length];

        currentDay = DayCounter.Instance.currentDay;

        switch (currentDay)
        {
            case 2:
                Instantiate(day2Object, transform.position, Quaternion.identity);
                break;
            case 4:
                Instantiate(day4Object, transform.position, Quaternion.identity);
                break;
            case 6:
                Instantiate(day6Object, transform.position, Quaternion.identity);
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        int obj = CheckObject(collision.gameObject);
        if (obj != -1)
        {
            switch (currentDay)
            {
                case 1:
                    day1Completed[obj] = true;
                    break;
                case 3:
                    day3Completed[obj] = true;
                    break;
                case 5:
                    day5Completed[obj] = true;
                    break;
                default:
                    break;
            }

            PickupController.instance.DropObjectConstant();

            CheckTask();
        }
    }

    private int CheckObject(GameObject collision)
    {
        switch (currentDay)
        {
            case 1:
                for(int i = 0; i < day1Tasks.Length; i++)
                {
                    if (collision.CompareTag(day1Tasks[i])) return i;
                }
                return -1;
            case 3:
                for (int i = 0; i < day3Tasks.Length; i++)
                {
                    if (collision.CompareTag(day3Tasks[i])) return i;
                }
                return -1;
            case 5:
                for (int i = 0; i < day5Tasks.Length; i++)
                {
                    if (collision.CompareTag(day5Tasks[i])) return i;
                }
                return -1;
            default:
                return -1;
        }
    }

    private void CheckTask()
    {
        bool completed = true;
        switch (currentDay)
        {
            case 1:
                foreach(bool task in day1Completed)
                {
                    if (!task)
                    {
                        completed = false;
                        break;
                    }
                }
                break;
            case 3:
                foreach (bool task in day3Completed)
                {
                    if (!task)
                    {
                        completed = false;
                        break;
                    }
                }
                break;
            case 5:
                foreach (bool task in day5Completed)
                {
                    if (!task)
                    {
                        completed = false;
                        break;
                    }
                }
                break;
            default:
                break;
        }
        if (completed)
        {
            Debug.Log("Items for recycler collected");
            DayCounter.Instance.SetTrigger("Recycle");
        }
    }
}
