using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

namespace Model.Logger
{
    public class LoggerItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text label;
        [SerializeField] private LayoutElement layoutElement;
        [SerializeField] private RectTransform rectTransform;
        
        public float Height => layoutElement.preferredHeight;

        public void Setup(string message, Color color, float y)
        {
            label.text = message;
            label.color = color;
            rectTransform.anchoredPosition = new Vector2(0, y);
        }

        public Tween MoveBy(float y)
            => rectTransform.DOAnchorPosY(y, .5f).SetRelative(true).SetEase(Ease.OutQuad);
    }
}