using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Script.Business
{
    public class InputDeviceBusiness : IInputDeviceBusiness
    {
        public bool CheckPlayerUsingDevice(InputAction.CallbackContext context, bool playerUsedDeviceBool)
        {
            if (context.started)
            {
                return true;
            }
            else if (context.canceled)
            { 
                return false;
            }
            return playerUsedDeviceBool;
        }

        public List<InputDevice> GetAllGamePadAndKeyBoardDevices(List<InputDevice> inputDeviceList)
        {
            return inputDeviceList.Where(device => SystemInfo.deviceType == DeviceType.Desktop ? device is Gamepad || device is Keyboard : device is Gamepad).ToList();
        }

        public List<InputDevice> KeepXPlayableDevices(List<InputDevice> inputDeviceList, int numberOfPlayer)
        {
            inputDeviceList.RemoveRange(numberOfPlayer, inputDeviceList.Count - numberOfPlayer);
            return inputDeviceList;
        }
    }
}
