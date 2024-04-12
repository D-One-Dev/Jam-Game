using UnityEngine;

namespace EWiresMiniGame
{
    public class Wire : MonoBehaviour
    {
        [SerializeField] private GameObject selectIcon;
        
        public int wireType;

        public void Select() => selectIcon.SetActive(true);
        
        public void UnSelect() => selectIcon.SetActive(false);
    }
}