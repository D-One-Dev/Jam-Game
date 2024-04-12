using UnityEngine;

namespace EWiresMiniGame
{
    public class Wire : MonoBehaviour
    {
        [SerializeField] private GameObject selectIcon;

        private TrailRenderer _trailRenderer;
        
        public int wireType;

        private Vector3 _defaultPosition;

        private void Start()
        {
            _defaultPosition = transform.position;
            _trailRenderer = GetComponent<TrailRenderer>();
        }

        public void ResetPosition()
        {
            transform.position = _defaultPosition;
            _trailRenderer.Clear();
        }

        public void Select() => selectIcon.SetActive(true);
        
        public void UnSelect() => selectIcon.SetActive(false);
    }
}