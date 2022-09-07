using Assets.Script.Business.Interface;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Script.Business.Implementation
{
    internal class InputDeviceBusiness : IInputDeviceBusiness
    {
        public List<InputDevice> GetAllGamePadAndKeyBoardDevices(List<InputDevice> inputDeviceList)
        {
            return inputDeviceList.Where(d => d is Gamepad || d is Keyboard).ToList();
        }

        public List<InputDevice> KeepXPlayableDevices(List<InputDevice> inputDeviceList, int numberOfPlayer)
        {
            inputDeviceList.RemoveRange(numberOfPlayer, inputDeviceList.Count - numberOfPlayer);
            return inputDeviceList;
        }
    }
}
