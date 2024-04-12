using System.Collections;
using UnityEngine;

namespace Sylphiette
{
    public class SylphietteDialogueSystem : MonoBehaviour
    {
        [SerializeField] private TextViewer textViewer;
        
        public SylphietteDialogueBlock[] dialogueBlocks;

        public static SylphietteDialogueSystem Instance;

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
                textViewer.Show(dialogueBlocks[_currentDialogue].messages[i]);

                while (!textViewer.isTextShown) yield return new WaitForSeconds(1f);

                yield return new WaitForSeconds(1f);
                textViewer.ClearText();
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}