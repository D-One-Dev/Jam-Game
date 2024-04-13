using UnityEngine;

public class RecyclerController : MonoBehaviour
{
    [SerializeField] private string[] day2Tasks;
    [SerializeField] private string[] day4Tasks;
    [SerializeField] private string[] day6Tasks;

    [SerializeField] private GameObject day3Object, day5Object, day7Object;

    private int currentDay;

    private bool[] day2Completed;
    private bool[] day4Completed;
    private bool[] day6Completed;

    private void Start()
    {
        day2Completed = new bool[day2Tasks.Length];
        day4Completed = new bool[day4Tasks.Length];
        day6Completed = new bool[day6Tasks.Length];

        currentDay = DayCounter.Instance.currentDay;

        switch (currentDay)
        {
            case 3:
                Instantiate(day3Object, transform.position, Quaternion.identity);
                break;
            case 5:
                Instantiate(day5Object, transform.position, Quaternion.identity);
                break;
            case 7:
                Instantiate(day7Object, transform.position, Quaternion.identity);
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
                case 2:
                    day2Completed[obj] = true;
                    break;
                case 4:
                    day4Completed[obj] = true;
                    break;
                case 6:
                    day6Completed[obj] = true;
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
            case 2:
                for(int i = 0; i < day2Tasks.Length; i++)
                {
                    if (collision.CompareTag(day2Tasks[i])) return i;
                }
                return -1;
            case 4:
                for (int i = 0; i < day4Tasks.Length; i++)
                {
                    if (collision.CompareTag(day4Tasks[i])) return i;
                }
                return -1;
            case 6:
                for (int i = 0; i < day6Tasks.Length; i++)
                {
                    if (collision.CompareTag(day6Tasks[i])) return i;
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
            case 2:
                foreach(bool task in day2Completed)
                {
                    if (!task)
                    {
                        completed = false;
                        break;
                    }
                }
                break;
            case 4:
                foreach (bool task in day4Completed)
                {
                    if (!task)
                    {
                        completed = false;
                        break;
                    }
                }
                break;
            case 6:
                foreach (bool task in day6Completed)
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
        }
    }
}
