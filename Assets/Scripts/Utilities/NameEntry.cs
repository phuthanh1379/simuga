using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities
{
    public class NameEntry : MonoBehaviour
    {
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private Button submitButton;

        private void SubmitName()
        {
            var value = inputField.text;
            FusionController.Instance.ConnectToRunner(value);
            PlayerPrefs.SetString("name", value);
        }

        public void OnClickSubmit()
        {
            SubmitName();
        }

        private void Update()
        {
            submitButton.interactable = !string.IsNullOrEmpty(inputField.text);
        }
    }
}