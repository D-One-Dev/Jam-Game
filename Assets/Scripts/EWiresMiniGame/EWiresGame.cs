using System.Collections.Generic;
using Sylphiette;
using UnityEngine;

namespace EWiresMiniGame
{
    public class EWiresGame : MonoBehaviour, IInteractable
    {
        [Header("Sound")] 
        [SerializeField] private AudioSource activatedElectricalPanelSound;
        [SerializeField] private AudioSource wireConnectSound;
        [SerializeField] private AudioSource wirePaveSound;
        
        [Header("Settings")]
        [SerializeField] private GameObject wirePathCollider;
        [SerializeField] private Wire[] wires;

        private List<GameObject> _wirePath = new List<GameObject>();

        public bool[] _isEndPointActive = {false, false, false, false};

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

        public void ResetGame()
        {
            for (int i = 0; i < wires.Length; i++)
            {
                wires[i].ResetPosition();
            }

            for (int i = 0; i < _wirePath.Count; i++)
            {
                Destroy(_wirePath[i]);
            }

            for (int i = 0; i < _isEndPointActive.Length; i++)
            {
                _isEndPointActive[i] = false;
            }

            _wirePath = new List<GameObject>();
        }

        private void MoveUp()
        {
            if (PlayerInteraction.instance.playerStatus != 1) return;
            
            if (_currentWire.transform.localPosition.y + 170 > 450) return;
            
            var previousPosition = _currentWire.transform.localPosition;
            
            _currentWire.transform.localPosition = new Vector3(_currentWire.transform.localPosition.x,
                _currentWire.transform.localPosition.y + 170, _currentWire.transform.localPosition.z);
            
            CreateWirePath(previousPosition);
        }

        private void MoveDown()
        {
            if (PlayerInteraction.instance.playerStatus != 1) return;
            
            if (_currentWire.transform.localPosition.y - 170 < -450) return;
            
            var previousPosition = _currentWire.transform.localPosition;

            _currentWire.transform.localPosition = new Vector3(_currentWire.transform.localPosition.x,
                _currentWire.transform.localPosition.y - 170, _currentWire.transform.localPosition.z);
            
            CreateWirePath(previousPosition);
        }
        private void MoveLeft()
        {
            if (PlayerInteraction.instance.playerStatus != 1) return;
            
            if (_currentWire.transform.localPosition.x - 170 < -520) return;
            
            var previousPosition = _currentWire.transform.localPosition;

            _currentWire.transform.localPosition = new Vector3(_currentWire.transform.localPosition.x - 170,
                _currentWire.transform.localPosition.y, _currentWire.transform.localPosition.z);
            
            CreateWirePath(previousPosition);
        }
        private void MoveRight()
        {
            if (PlayerInteraction.instance.playerStatus != 1) return;
            
            if (_currentWire.transform.localPosition.x + 170 > 520) return;

            var previousPosition = _currentWire.transform.localPosition;

            _currentWire.transform.localPosition = new Vector3(_currentWire.transform.localPosition.x + 170,
                _currentWire.transform.localPosition.y, _currentWire.transform.localPosition.z);
            
            CreateWirePath(previousPosition);
        }

        public void SetWireConnected(int type)
        {
            _isEndPointActive[type] = true;
            
            _currentWire.UnSelect();

            _currentWire = null;

            for (int i = 0; i < wires.Length; i++)
            {
                SelectWire(i);
                
                if (_currentWire != null) break;
            }

            //Game complete
            if (_currentWire == null)
            {
                SylphietteDialogueSystem.Instance.StartNextDialogue();
                activatedElectricalPanelSound.Play();
                print("Electric wires mini-game completed");
                DayCounter.Instance.SetTrigger("Wires");
                
                for (int i = 0; i < _wirePath.Count; i++)
                {
                    Destroy(_wirePath[i]);
                }
            }
        }
        
        private void CreateWirePath(Vector3 position)
        {
            GameObject wcollider = Instantiate(wirePathCollider, position, Quaternion.identity);

            wcollider.GetComponent<WireCollider>().eWiresGame = this;
            wcollider.transform.SetParent(transform, false);
            wcollider.transform.localScale = new Vector3(1, 1, 1);
            wcollider.transform.localPosition = position;
            
            _wirePath.Add(wcollider);
            
            wirePaveSound.Play();
        }

        private void SelectWire(int type)
        {
            if (PlayerInteraction.instance.playerStatus == 1)
            {
                if (_isEndPointActive[type]) return;
                
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