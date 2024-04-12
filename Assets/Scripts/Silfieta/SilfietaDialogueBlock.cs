using UnityEngine;

namespace Silfieta
{
    [CreateAssetMenu(fileName = "SilfietaDialogue")]
    public class SilfietaDialogueBlock : ScriptableObject
    {
        [TextArea(3,10)] public string[] messages;
    }
}