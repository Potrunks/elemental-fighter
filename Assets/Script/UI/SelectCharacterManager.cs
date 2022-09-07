using Assets.Script.Business.Implementation;
using Assets.Script.Business.Interface;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectCharacterManager : MonoBehaviour
{
    [Header("Selection Prefab")]
    [SerializeField]
    private GameObject tokenPrefab;
    [SerializeField]
    private GameObject cursorPrefab;
    [SerializeField]
    private GameObject playerSelectionPreviewPrefab;

    [Header("UI Zone")]
    [SerializeField]
    private GameObject playerSelectionGrid;
    public GameObject readyPanelGameobject;

    [Header("Game Settings")]
    [SerializeField]
    private int maxPlayer = 4;
    public int[] indexPlayerConnectedArray;
    public List<CharacterPreview> characterPreviewList;

    [Header("Connected Player Data")]
    public List<CursorDetection> cursorDetectionList;
    public List<InputDevice> inputDeviceList;
    public IDictionary<InputDevice, List<GameObject>> playerSelectGameObjectByDevice = new Dictionary<InputDevice, List<GameObject>>();

    [HideInInspector]
    public static SelectCharacterManager instance;
    private IInputDeviceBusiness inputDeviceBusiness = new InputDeviceBusiness();
    private IColorBusiness colorBusiness = new ColorBusiness();
    private IPlayerBusiness playerBusiness = new PlayerBusiness();
    private ISelectCharacterMenuBusiness selectCharacterMenuBusiness = new SelectCharacterMenuBusiness();
    private ICursorBusiness cursorBusiness = new CursorBusiness();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        indexPlayerConnectedArray = new int[maxPlayer];
        for (int i = 0; i < indexPlayerConnectedArray.Length; i++)
        {
            indexPlayerConnectedArray[i] = 0;
        }

        InputSystem.onDeviceChange += onDeviceChangeDuringSelectCharacterMenu;

        inputDeviceList = inputDeviceBusiness.GetAllGamePadAndKeyBoardDevices(InputSystem.devices.ToList());
        if (inputDeviceList.Count > maxPlayer)
        {
            inputDeviceBusiness.KeepXPlayableDevices(inputDeviceList, maxPlayer);
        }
        foreach (InputDevice device in inputDeviceList)
        {
            playerBusiness.CreateNewPlayer(playerSelectionPreviewPrefab, tokenPrefab, cursorPrefab, transform, playerSelectionGrid.transform, cursorDetectionList, colorBusiness, device, playerSelectGameObjectByDevice, playerBusiness.NextPlayerIndex(indexPlayerConnectedArray));
        }
        cursorBusiness.SetAsLastSiblingAllCursor(GameObject.FindGameObjectsWithTag("CursorSelection"));
    }

    /// <summary>
    /// Event action call when a device change during this the select character menu
    /// </summary>
    /// <param name="device">Device concern</param>
    /// <param name="state">State of the device concern</param>
    public void onDeviceChangeDuringSelectCharacterMenu(InputDevice device, InputDeviceChange state)
    {
        switch (state)
        {
            case InputDeviceChange.Added:
                if (inputDeviceList.Count < maxPlayer)
                {
                    inputDeviceList.Add(device);
                    playerBusiness.CreateNewPlayer(playerSelectionPreviewPrefab, tokenPrefab, cursorPrefab, transform, playerSelectionGrid.transform, cursorDetectionList, colorBusiness, device, playerSelectGameObjectByDevice, playerBusiness.NextPlayerIndex(indexPlayerConnectedArray));
                    cursorBusiness.SetAsLastSiblingAllCursor(GameObject.FindGameObjectsWithTag("CursorSelection"));
                    selectCharacterMenuBusiness.DisableReadyPanel(readyPanelGameobject);
                }
                break;

            case InputDeviceChange.Removed:
                inputDeviceList.Remove(device);
                playerBusiness.RemovePlayer(playerSelectGameObjectByDevice, device, cursorDetectionList, indexPlayerConnectedArray);
                selectCharacterMenuBusiness.VerifyAllPlayerConfirmedCharacterChoice(cursorDetectionList, readyPanelGameobject);
                break;
        }
    }
}
