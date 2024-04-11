using System.Collections;
using TMPro;
using UnityEngine;

namespace Silfieta
{
    public class TextViewer : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        
        public string sample =
            "Я возмущён уровнем обслуживания, который предоставляет это казино! Мне бы очень не хотелось переходить на личности, вы уж извините," +
            " но хватает ли вашего уровня компетенции для работы в данном заведении?";

        public void SendMessage(string str) => StartCoroutine(View(str));

        private IEnumerator View(string str)
        {
            foreach (var sym in str)
            {
                text.text += sym;

                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}