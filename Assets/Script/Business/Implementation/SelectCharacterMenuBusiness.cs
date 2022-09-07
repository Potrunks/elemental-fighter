using Assets.Script.Business.Interface;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Linq;

namespace Assets.Script.Business.Implementation
{
    internal class SelectCharacterMenuBusiness : ISelectCharacterMenuBusiness
    {
        private static readonly System.Random random = new System.Random();

        public void DisableReadyPanel(GameObject readyPanelGameobject)
        {
            readyPanelGameobject.SetActive(false);
        }

        public void GoToMainMenu()
        {
            SceneManager.LoadScene("Menu");
        }

        public void StartFight(IDictionary<InputDevice, List<GameObject>> playerSelectGameObjectByDevice, string fightingScene, ICharacterBusiness characterBusiness, IPlayerBusiness playerBusiness)
        {
            GameManager.instance.selectedMode.Clear();
            GameManager.instance.deviceAndCharacterPlayerByIndex.Clear();
            foreach (KeyValuePair<InputDevice, List<GameObject>> deviceAndGameObject in playerSelectGameObjectByDevice)
            {
                CursorDetection cursorDetection = deviceAndGameObject.Value.Where(go => go.tag == "CursorSelection").First().GetComponent<CursorDetection>();
                int playerIndex = cursorDetection.indexPlayer;
                GameObject characterSelected = cursorDetection.characterUnderCursor.GetComponent<CharacterPreviewData>().characterModel;
                if (characterSelected == null)
                {
                    characterSelected = characterBusiness.GetRandomCharacter();
                }
                List<object> characterSelectedAndDevice = new List<object> { characterSelected, deviceAndGameObject.Key };
                playerBusiness.SetupForNewGame(playerIndex, characterSelectedAndDevice, false);
            }
            if (playerSelectGameObjectByDevice.Count == 1)
            {
                int indexPlayerAI = playerBusiness.NextPlayerIndex(SelectCharacterManager.instance.indexPlayerConnectedArray);
                List<object> randomCharAndDevice = new List<object> { characterBusiness.GetRandomCharacter(), null };
                playerBusiness.SetupForNewGame(indexPlayerAI, randomCharAndDevice, true);
            }
            InputSystem.onDeviceChange -= SelectCharacterManager.instance.onDeviceChangeDuringSelectCharacterMenu;
            GameManager.instance.currentTime = GameManager.instance.timeCondition;
            SceneManager.LoadScene(fightingScene);
        }

        public void VerifyAllPlayerConfirmedCharacterChoice(List<CursorDetection> cursorDetections, GameObject readyPanel)
        {
            bool allPlayerConfirmedCharacterChoice = true;
            if (cursorDetections.Count == 0)
            {
                allPlayerConfirmedCharacterChoice = false;
                return;
            }
            foreach (CursorDetection cursorDetection in cursorDetections)
            {
                if (cursorDetection.cursorHasToken == true)
                {
                    allPlayerConfirmedCharacterChoice = false;
                    break;
                }
            }
            if (allPlayerConfirmedCharacterChoice == true)
            {
                readyPanel.SetActive(true);
            }
        }
    }
}
