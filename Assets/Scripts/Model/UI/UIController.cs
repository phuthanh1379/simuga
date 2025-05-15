using System;
using UnityEngine;
using Utilities;

namespace Model.UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private GameObject menu;
        [SerializeField] private InGameMenu inGameMenu;

        private bool _isOpenMenu;
        private bool _isOpenInGameMenu;

        private void Awake()
        {
            _isOpenMenu = true;
            _isOpenInGameMenu = false;
        }

        private void Start()
        {
            ShowMenu();
            inGameMenu.Hide();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SelectInGameMenu();
            }
        }

        public void ShowMenu()
        {
            menu.SetActive(true);
        }

        public void HideMenu()
        {
            menu.SetActive(false);
        }

        private void SelectInGameMenu()
        {
            if (_isOpenInGameMenu)
            {
                inGameMenu.Hide();
            }
            else
            {
                inGameMenu.Show();
            }
            
            _isOpenInGameMenu = !_isOpenInGameMenu;
        }
    }
}