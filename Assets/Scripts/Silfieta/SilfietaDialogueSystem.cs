using System.Collections;
using UnityEngine;

namespace Silfieta
{
    public class SilfietaDialogueSystem : MonoBehaviour
    {
        [SerializeField] private TextViewer textViewer;
        
        public SilfietaDialogueBlock[] dialogueBlocks;

        public static SilfietaDialogueSystem Instance;

        private int _currentDialogue;
        
        private void Awake() => Instance = this;

        private void Start() => StartCoroutine(ShowDialogue());

        public void StartNextDialogue()
        {
            _currentDialogue++;
            StartCoroutine(ShowDialogue());
        }

        private IEnumerator ShowDialogue()
        {
            for (int i = 0; i < dialogueBlocks[_currentDialogue].messages.Length; i++)
            {
                textViewer.SendMessage(dialogueBlocks[_currentDialogue].messages[i]);

                while (!textViewer.isTextShown) yield return new WaitForSeconds(1f);

                yield return new WaitForSeconds(1f);
                textViewer.ClearText();
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}