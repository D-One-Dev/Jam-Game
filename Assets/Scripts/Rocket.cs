using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Item item))
        {
            if (item.itemName == "Ключ доступа")
            {
                sceneLoader.StartSceneLoading("Game End");
            }
        }
    }
}