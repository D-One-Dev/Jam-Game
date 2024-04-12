using UnityEngine;

namespace EWiresMiniGame
{
    public class EndPoint : MonoBehaviour
    {
        [SerializeField] private EWiresGame wiresGame;
        [SerializeField] private int wireType;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Wire"))
            {
                wiresGame.SetWireConnected(wireType);
            }
        }
    }
}