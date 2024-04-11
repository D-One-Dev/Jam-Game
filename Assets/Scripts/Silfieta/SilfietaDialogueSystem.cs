using System.Collections;
using UnityEngine;

namespace Silfieta
{
    public class SilfietaDialogueSystem : MonoBehaviour
    {
        [SerializeField] private TextViewer textViewer;
        
        public SilfietaDialogue[] dialogues;

        private int _currentDialogue;
        
        private void Start() => StartCoroutine(StartDialogue());

        private IEnumerator StartDialogue()
        {
            for (int i = 0; i < dialogues[_currentDialogue].messages.Length; i++)
            {
                textViewer.SendMessage(dialogues[_currentDialogue].messages[i]);
                yield return new WaitForSeconds(3);
            }
        }
    }
}