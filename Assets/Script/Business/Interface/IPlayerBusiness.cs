using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Script.Business.Interface
{
    internal interface IPlayerBusiness
    {
        /// <summary>
        /// Create new Player in order to select character
        /// </summary>
        /// <param name="playerSelectionPreviewPrefab">Prefab of the player selection view</param>
        /// <param name="tokenPrefab">Token prefab for the player</param>
        /// <param name="cursorPrefab">Cursor prefab for the player</param>
        /// <param name="currentTransform">Current transform of the Canvas</param>
        /// <param name="playerSelectionGridTransform">Transform of the player selection grid</param>
        /// <param name="cursorDetectionList">List of the cursor detection present in the game</param>
        /// <param name="colorBusiness">Logic code about Color</param>
        /// <param name="device">Device added</param>
        /// <param name="playerSelectGameObjectByDevice">Dictionary of device (Key) and List of player select gameobject (Value)</param>
        /// <param name="indexPlayer">Index of the player</param>
        void CreateNewPlayer(GameObject playerSelectionPreviewPrefab, GameObject tokenPrefab, GameObject cursorPrefab, Transform currentTransform, Transform playerSelectionGridTransform, List<CursorDetection> cursorDetectionList, IColorBusiness colorBusiness, InputDevice device, IDictionary<InputDevice, List<GameObject>> playerSelectGameObjectByDevice, int indexPlayer);

        /// <summary>
        /// Desactivate all device connected at players. It's possible to exclude the device of player
        /// </summary>
        /// <param name="inputDeviceByPlayerIndex">Dictionary of all input device by player index connected</param>
        /// <param name="playerIndexList">List of index player to exclude. Can be null</param>
        void DesactivateAllDeviceWithException(IDictionary<int, InputDevice> inputDeviceByPlayerIndex, List<int> playerIndexList);
        void SetupForNewGame(int playerIndex, List<object> characterSelectedAndDevice, bool isModeAI);

        /// <summary>
        /// Remove a player from the select character
        /// </summary>
        /// <param name="playerSelectGameObjectByDevice">Dictionary of device (Key) and List of player select gameobject (Value)</param>
        /// <param name="device">Device removed</param>
        /// <param name="cursorDetectionList">CursorDetection List already present in the select menu</param>
        /// <param name="indexPlayerConnectedArray">Index of all connected player</param>
        void RemovePlayer(IDictionary<InputDevice, List<GameObject>> playerSelectGameObjectByDevice, InputDevice device, List<CursorDetection> cursorDetectionList, int[] indexPlayerConnectedArray);

        /// <summary>
        /// Give the next player index available
        /// </summary>
        /// <param name="indexOfConnectedPlayer">Index of all connected player</param>
        /// <returns>The index player use for the new player</returns>
        int NextPlayerIndex(int[] indexOfConnectedPlayer);

        /// <summary>
        /// Get the character selected by the player during the selection character menu
        /// </summary>
        /// <param name="playerIndex">Index of the player</param>
        /// <param name="deviceAndCharacterPlayerByIndex">Dictionnay with all device and character by index player</param>
        /// <returns>The gameobject of the character selected</returns>
        GameObject GetCharacterSelectedByIndex(int playerIndex, IDictionary<int, List<object>> deviceAndCharacterPlayerByIndex);

        /// <summary>
        /// Reactivate all device who has been desactivate
        /// </summary>
        /// <param name="inputDeviceByPlayerIndex">All device by player index connected</param>
        void ReactivateAllDevice(IDictionary<int, InputDevice> inputDeviceByPlayerIndex);
    }
}
