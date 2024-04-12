using UnityEngine;

namespace EWiresMiniGame
{
    public class WireCollider : MonoBehaviour
    {
        public EWiresGame eWiresGame;
        
        public void OnTriggerEnter(Collider col)
        {
            if (col.CompareTag("WireCollider") || col.CompareTag("Wire")) eWiresGame.ResetGame();
        }
    }
}