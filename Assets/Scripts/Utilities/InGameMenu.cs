using Model.Game;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Utilities
{
    public class InGameMenu : MonoBehaviour
    {
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private Button changeNameButton;
        [SerializeField] private Button randomColorButton;
        [SerializeField] private GameObject panel;

        public void Show()
        {
            panel.SetActive(true);
        }

        public void Hide()
        {
            panel.SetActive(false);
        }

        private void ChangeName()
        {
            var value = inputField.text;
            PlayerPrefs.SetString("name", value);
            ChangeNameEvent.OnNameChanged(value);
        }

        private void RandomColor()
        {
            var color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            ChangeColorEvent.OnColorChanged(color);
        }

        public void OnClickChangeNameButton()
        {
            ChangeName();
        }
        
        public void OnClickRandomColorButton()
        {
            RandomColor();
        }
        
        private void Update()
        {
            changeNameButton.interactable = !string.IsNullOrEmpty(inputField.text);
        }
    }
}