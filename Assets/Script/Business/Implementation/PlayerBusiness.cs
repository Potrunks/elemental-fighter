using Assets.Script.Business.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Script.Business.Implementation
{
    internal class PlayerBusiness : IPlayerBusiness
    {
        public void CreateNewPlayer(GameObject playerSelectionPreviewPrefab, GameObject tokenPrefab, GameObject cursorPrefab, Transform currentTransform, Transform playerSelectionGridTransform, List<CursorDetection> cursorDetectionList, IColorBusiness colorBusiness, InputDevice device, IDictionary<InputDevice, List<GameObject>> playerSelectGameObjectByDevice, int indexPlayer)
        {
            GameObject newPlayerSelectionPreview = GameObject.Instantiate(playerSelectionPreviewPrefab, playerSelectionGridTransform);
            GameObject newToken = GameObject.Instantiate(tokenPrefab, currentTransform);
            GameObject newCursor = GameObject.Instantiate(cursorPrefab, currentTransform);
            CursorDetection cursorDetection = newCursor.GetComponent<CursorDetection>();
            PlayerInput playerInput = newCursor.GetComponent<PlayerInput>();
            playerInput.SwitchCurrentControlScheme(new InputDevice[] { device });
            cursorDetection.playerSlot = newPlayerSelectionPreview;
            cursorDetection.token = newToken;
            TextMeshProUGUI playerIndexOnSelectionPreview = newPlayerSelectionPreview.transform.Find("PlayerIndex").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI playerIndexOnToken = newToken.transform.Find("PlayerIndex").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI playerIndexOnCursor = newCursor.transform.Find("PlayerIndex").GetComponent<TextMeshProUGUI>();
            cursorDetection.indexPlayer = indexPlayer;
            string playerInputIndex = indexPlayer.ToString();
            playerIndexOnSelectionPreview.text = "P" + playerInputIndex;
            playerIndexOnToken.text = "P" + playerInputIndex;
            playerIndexOnCursor.text = playerIndexOnToken.text;
            newPlayerSelectionPreview.gameObject.name = "Player" + playerInputIndex + "Selection";
            newToken.gameObject.name = "Player" + playerInputIndex + "Token";
            newCursor.gameObject.name = "Player" + playerInputIndex + "Cursor";
            Color colorOfPlayerIndex = colorBusiness.SetColorByPlayerIndex(playerInputIndex);
            playerIndexOnToken.color = colorOfPlayerIndex;
            playerIndexOnCursor.color = colorOfPlayerIndex;
            playerIndexOnSelectionPreview.color = colorOfPlayerIndex;
            cursorDetectionList.Add(cursorDetection);
            playerSelectGameObjectByDevice.Add(device, new List<GameObject> { newPlayerSelectionPreview, newToken, newCursor });
        }

        public void DesactivateAllDeviceWithException(IDictionary<int, InputDevice> inputDeviceByPlayerIndex, List<int> playerIndexList)
        {
            List<InputDevice> inputDeviceListToDesactivate = new List<InputDevice>();
            if (playerIndexList != null)
            {
                inputDeviceListToDesactivate = inputDeviceByPlayerIndex.Where(id => !playerIndexList.Contains(id.Key)).Select(id => id.Value).ToList();
            }
            else
            {
                inputDeviceListToDesactivate = inputDeviceByPlayerIndex.Values.ToList();
            }
            foreach (InputDevice device in inputDeviceListToDesactivate)
            {
                InputSystem.DisableDevice(device);
            }
        }

        public GameObject GetCharacterSelectedByIndex(int playerIndex, IDictionary<int, List<object>> deviceAndCharacterPlayerByIndex)
        {
            GameObject character;
            try
            {
                character = (GameObject)deviceAndCharacterPlayerByIndex.First(dacp => dacp.Key == playerIndex + 1).Value.First(o => o is GameObject);
                return character;
            }
            catch (Exception e)
            {
                if (e is InvalidOperationException)
                {
                    Debug.Log("No player with index = " + playerIndex + " in the game");
                }
                return null;
            }
        }

        public int NextPlayerIndex(int[] indexConnectedPlayerArray)
        {
            for (int i = 0; i < indexConnectedPlayerArray.Length; i++)
            {
                if (indexConnectedPlayerArray[i] == 0)
                {
                    indexConnectedPlayerArray[i] = i + 1;
                    return i + 1;
                }
            }
            return 0;
        }

        public void ReactivateAllDevice(IDictionary<int, InputDevice> inputDeviceByPlayerIndex)
        {
            inputDeviceByPlayerIndex.Values
                .Where(d => d.enabled != true)
                .ToList()
                .ForEach(d =>
                            {
                                InputSystem.EnableDevice(d);
                            }
                         );
        }

        public void RemovePlayer(IDictionary<InputDevice, List<GameObject>> playerSelectGameObjectByDevice, InputDevice device, List<CursorDetection> cursorDetectionList, int[] indexPlayerConnectedArray)
        {
            List<GameObject> playerSelectGameObjectListToDelete = playerSelectGameObjectByDevice[device];
            GameObject playerCursor = playerSelectGameObjectListToDelete.Where(psgo => psgo.tag == "CursorSelection").First();
            CursorDetection playerCursorDetectionToDelete = playerCursor.GetComponent<CursorDetection>();
            foreach(GameObject gameObject in playerSelectGameObjectListToDelete)
            {
                GameObject.Destroy(gameObject);
            }
            indexPlayerConnectedArray[playerCursorDetectionToDelete.indexPlayer - 1] = 0;
            cursorDetectionList.Remove(playerCursorDetectionToDelete);
            playerSelectGameObjectByDevice.Remove(device);
        }

        public void SetupForNewGame(int playerIndex, List<object> characterSelectedAndDevice, bool isModeAI)
        {
            GameManager.instance.deviceAndCharacterPlayerByIndex.Add(playerIndex, characterSelectedAndDevice);
            GameManager.instance.selectedMode.Add(playerIndex - 1, isModeAI);
        }
    }
}
