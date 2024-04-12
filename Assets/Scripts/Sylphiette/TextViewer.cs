using System.Collections;
using TMPro;
using UnityEngine;

namespace Sylphiette
{
    public class TextViewer : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        
        public string sample =
            "Я возмущён уровнем обслуживания, который предоставляет это казино! Мне бы очень не хотелось переходить на личности, вы уж извините," +
            " но хватает ли вашего уровня компетенции для работы в данном заведении?";

        public bool isTextShown;

        public void Show(string str) => StartCoroutine(View(str));

        private IEnumerator View(string str)
        {
            isTextShown = false;
            
            foreach (var sym in str)
            {
                text.text += sym;

                yield return new WaitForSeconds(0.1f);
            }

            isTextShown = true;
        }

        public void ClearText() => text.text = "";
    }
}