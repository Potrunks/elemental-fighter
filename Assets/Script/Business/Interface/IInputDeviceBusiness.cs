using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace Assets.Script.Business.Interface
{
    internal interface IInputDeviceBusiness
    {
        /// <summary>
        /// Get all GamePad and KeyBoard devices connected to the game
        /// </summary>
        /// <param name="inputDeviceList">All devices connected to the game</param>
        /// <returns>A list of GamePad and KeyBoard devices</returns>
        List<InputDevice> GetAllGamePadAndKeyBoardDevices(List<InputDevice> inputDeviceList);

        /// <summary>
        /// Keep a number of connected devices
        /// </summary>
        /// <param name="inputDeviceList">List of devices targeted</param>
        /// <param name="numberOfPlayer">Number of player wanted</param>
        /// <returns>A list of devices with a number of player wanted</returns>
        List<InputDevice> KeepXPlayableDevices(List<InputDevice> inputDeviceList, int numberOfPlayer);

        /// <summary>
        /// Check if the player is using the device.
        /// </summary>
        bool CheckPlayerUsingDevice(InputAction.CallbackContext context, bool playerUsedDeviceBool);
    }
}
