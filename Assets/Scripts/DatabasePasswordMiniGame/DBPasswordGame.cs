using TMPro;
using UnityEngine;

namespace DatabasePasswordMiniGame
{
    public class DBPasswordGame : MonoBehaviour, IInteractable
    {
        [SerializeField] private TMP_Text tryCountText;
        [SerializeField] private TMP_Text passwordText;
        
        [SerializeField] private TMP_Text matchText;
        [SerializeField] private TMP_Text placeMatchText;
        
        private bool isActive, isEnteringPassword;
        private bool isFirstSymbol = true;

        public string password;
        private string _enteredPassword;

        private int tryCount = 5;
        
        private Controls _controls;

        private void Awake()
        {
            _controls = new Controls();
            _controls.Gameplay.Left.performed += ctx => OnAClick();
            _controls.Gameplay.Enter.performed += ctx => OnEnterClick();
        }
        
        private void OnEnable()
        {
            _controls.Enable();
        }
        private void OnDisable()
        {
            _controls.Disable();
        }

        private void Update()
        {
            if (isActive && isEnteringPassword)
            {
                if (!isFirstSymbol)
                {
                    passwordText.text += Input.inputString;
                    _enteredPassword += Input.inputString;
                }
                else isFirstSymbol = false;
            }
        }

        private void OnAClick()
        {
            if (isActive)
            {
                if (PlayerInteraction.instance.playerStatus != 1) return;
            
                isEnteringPassword = !isEnteringPassword;

                if (isEnteringPassword)
                {
                    passwordText.text = "Введенный пароль: ";
                    _enteredPassword = "";
                    isFirstSymbol = true;
                }
            }
        }

        private void OnEnterClick()
        {
            if (isActive)
            {
                if (PlayerInteraction.instance.playerStatus != 1 || isEnteringPassword) return;
            
                if (_enteredPassword != password)
                {
                    tryCount--;
                    tryCountText.text = "Осталось попыток: " + tryCount;

                    matchText.text = "Совпадений букв: " + CountMatchingChars(_enteredPassword, password);
                    placeMatchText.text = "Совпадений букв по месту: " + CountPlaceMatchingChars(_enteredPassword, password);

                    if (tryCount == 0)
                    {
                        //SoundController.instance.PlaySoundRandomPitch(gameLoose);
                        DeathController.instance.TriggerDeath("BRUH");
                        Debug.Log("Loose");
                    }
                }
                else
                {
                    print("thats right");
                    DayCounter.Instance.SetTrigger("BD");
                }
            }
        }
        
        private int CountMatchingChars(string str1, string str2)
        {
            int count = 0;
            for (int i = 0; i < str1.Length; i++)
            {
                if (str2.IndexOf(str1[i]) >= 0)
                {
                    count++;
                }
            }
            return count;
        }
        
        private int CountPlaceMatchingChars(string str1, string str2)
        {
            int count = 0;
            for (int i = 0; i < str1.Length; i++)
            {
                if (i < str2.Length && str1[i] == str2[i])
                {
                    count++;
                }
            }
            return count;
        }
        
        public void TurnOn()
        {
            Cursor.lockState = CursorLockMode.None;
            isActive = true;
        }

        public void TurnOff()
        {
            Cursor.lockState = CursorLockMode.Locked;
            isActive = false;
        }
    }
}