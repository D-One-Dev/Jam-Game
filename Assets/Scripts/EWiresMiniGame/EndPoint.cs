using UnityEngine;

namespace EWiresMiniGame
{
    public class EndPoint : MonoBehaviour
    {
        [SerializeField] private EWiresGame wiresGame;
        [SerializeField] private int wireType;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Wire wire))
            {
                if (wire.wireType == wireType)
                {
                    wiresGame.isEndPointActive[wireType] = true;
                }
            }
        }
    }
}