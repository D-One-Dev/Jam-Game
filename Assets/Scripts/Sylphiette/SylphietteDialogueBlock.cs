using UnityEngine;

namespace Sylphiette
{
    [CreateAssetMenu(fileName = "SylphietteDialogue")]
    public class SylphietteDialogueBlock : ScriptableObject
    {
        [TextArea(3,10)] public string[] messages;
    }
}