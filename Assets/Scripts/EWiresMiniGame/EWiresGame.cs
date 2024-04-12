using UnityEngine;

namespace EWiresMiniGame
{
    public class EWiresGame : MonoBehaviour, IInteractable
    {
        [SerializeField] private Wire[] wires;

        public bool[] isEndPointActive = {false, false, false, false};

        private Wire _currentWire;

        private Controls _controls;

        private void Awake()
        {
            _controls = new Controls();
            
            _controls.Gameplay.One.performed += ctx => SelectWire(0);
            _controls.Gameplay.Two.performed += ctx => SelectWire(1);
            _controls.Gameplay.Three.performed += ctx => SelectWire(2);
            _controls.Gameplay.Four.performed += ctx => SelectWire(3);
            
            _controls.Gameplay.Up.performed += ctx => MoveUp();
            _controls.Gameplay.Down.performed += ctx => MoveDown();
            _controls.Gameplay.Left.performed += ctx => MoveLeft();
            _controls.Gameplay.Right.performed += ctx => MoveRight();
            
            UnSelectAll();
            wires[0].Select();
            _currentWire = wires[0];
        }

        private void MoveUp()
        {
            if (PlayerInteraction.instance.playerStatus != 1) return;
            
            if (_currentWire.transform.localPosition.y + 170 > 450) return;

            _currentWire.transform.localPosition = new Vector3(_currentWire.transform.localPosition.x,
                _currentWire.transform.localPosition.y + 170, _currentWire.transform.localPosition.z);
        }
        private void MoveDown()
        {
            if (PlayerInteraction.instance.playerStatus != 1) return;
            
            if (_currentWire.transform.localPosition.y - 170 < -450) return;

            _currentWire.transform.localPosition = new Vector3(_currentWire.transform.localPosition.x,
                _currentWire.transform.localPosition.y - 170, _currentWire.transform.localPosition.z);
        }
        private void MoveLeft()
        {
            if (PlayerInteraction.instance.playerStatus != 1) return;
            
            if (_currentWire.transform.localPosition.x - 170 < -440) return;

            _currentWire.transform.localPosition = new Vector3(_currentWire.transform.localPosition.x - 170,
                _currentWire.transform.localPosition.y, _currentWire.transform.localPosition.z);
        }
        private void MoveRight()
        {
            if (PlayerInteraction.instance.playerStatus != 1) return;
            
            if (_currentWire.transform.localPosition.x + 170 > 440) return;

            _currentWire.transform.localPosition = new Vector3(_currentWire.transform.localPosition.x + 170,
                _currentWire.transform.localPosition.y, _currentWire.transform.localPosition.z);
        }

        private void SelectWire(int type)
        {
            if (PlayerInteraction.instance.playerStatus == 1)
            {
                UnSelectAll();
                wires[type].Select();

                _currentWire = wires[type];
            }
        }

        private void UnSelectAll()
        {
            for (int i = 0; i < wires.Length; i++)
            {
                wires[i].UnSelect();
            }
        }
        
        private void OnEnable()
        {
            _controls.Enable();
        }
        private void OnDisable()
        {
            _controls.Disable();
        }

        public void TurnOn()
        {
            Cursor.lockState = CursorLockMode.None;
        }

        public void TurnOff()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}