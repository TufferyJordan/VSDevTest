using System;
using UnityEngine;
using UnityEngine.UI;


namespace Test
{
    public class Tester : MonoBehaviour
    {
        public Button showAdButton;

        public Button startGameButton;

        public Button endGameButton;

        public Button GDPRConsentConfirmButton;

        public Toggle GDPRConsentToggle;

        public GameObject GDPRPanel;

        private void Awake()
        {
            showAdButton.onClick.AddListener(ShowAdClick);
            startGameButton.onClick.AddListener(StartGameClick);
            endGameButton.onClick.AddListener(EndGameClick);
            GDPRConsentConfirmButton.onClick.AddListener(GDPRConsentConfirm);

            VoodooSauce.SetAdDisplayConditions(60, 3); 
        }

        private void ShowAdClick()
        {
            VoodooSauce.ShowAd("f4280fh0318rf0h2"); 
        }

        private void StartGameClick()
        {
            VoodooSauce.StartGame(); 
        }

        private void EndGameClick()
        {
            VoodooSauce.EndGame(); 
        }

        private void GDPRConsentConfirm()
        {
            GDPRPanel.SetActive(false);
            VoodooSauce.SetGDPRConsent(GDPRConsentToggle.isOn);
        }
    }
}