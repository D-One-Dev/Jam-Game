using UnityEngine;

namespace EWiresMiniGame
{
    public class WireCollider : MonoBehaviour
    {
        public void OnTriggerEnter(Collider col)
        {
            if (col.CompareTag("WireCollider")) print("Collide"); //reset
        }
    }
}