using Assets.Script.Business.Implementation;
using Assets.Script.Business.Interface;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class CursorMovement : MonoBehaviour
{
    [Header("Movement Properties")]
    [SerializeField]
    private Vector2 vector2ValueOnMovement;
    public float moveSpeed;

    [Header("Component")]
    [SerializeField]
    private Rigidbody2D rb2D;
    [SerializeField]
    private CursorDetection cursorDetection;
    private InputDevice device;

    private ICharacterBusiness characterBusiness = new CharacterBusiness();
    private ISelectCharacterMenuBusiness selectCharacterMenuBusiness = new SelectCharacterMenuBusiness();
    private IPlayerBusiness playerBusiness = new PlayerBusiness();

    public void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        cursorDetection = GetComponent<CursorDetection>();
        this.device = GetComponent<PlayerInput>().devices[0];
    }

    private void FixedUpdate()
    {
        Movement(vector2ValueOnMovement.x, vector2ValueOnMovement.y, moveSpeed);
    }

    /// <summary>
    /// Method call when player use button able to move the cursor
    /// </summary>
    /// <param name="context">This is the CallBackContext who give information about the state of the button</param>
    public void OnMove(InputAction.CallbackContext context)
    {
        vector2ValueOnMovement = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// Give velocity to the rigidbody of the cursor depending of X-Axis and Y-Axis
    /// </summary>
    /// <param name="xMovement">X-Axis value</param>
    /// <param name="yMovement">Y-Axis value</param>
    /// <param name="moveSpeed">Multiplicator Speed</param>
    private void Movement(float xMovement, float yMovement, float moveSpeed)
    {
        rb2D.velocity = new Vector2(xMovement * moveSpeed, yMovement * moveSpeed);
    }

    /// <summary>
    /// Drop the token from the cursor and confirm the choice
    /// </summary>
    /// <param name="context">This is the CallBackContext who give information about the state of the button</param>
    public void OnDropToken(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (cursorDetection.characterUnderCursor != null)
            {
                if (cursorDetection.characterUnderCursor.name == "SelectionBackButton")
                {
                    selectCharacterMenuBusiness.GoToMainMenu();
                }
                else if (cursorDetection.cursorHasToken == true)
                {
                    cursorDetection.cursorHasToken = false;
                    characterBusiness.ConfirmCharacter(cursorDetection.playerSlot, cursorDetection.characterUnderCursor);
                    selectCharacterMenuBusiness.VerifyAllPlayerConfirmedCharacterChoice(SelectCharacterManager.instance.cursorDetectionList, SelectCharacterManager.instance.readyPanelGameobject);
                }
            }
        }
    }

    /// <summary>
    /// Bring back the token from the cursor
    /// </summary>
    /// <param name="context">This is the CallBackContext who give information about the state of the button</param>
    public void OnBringBackToken(InputAction.CallbackContext context)
    {
        if (cursorDetection.cursorHasToken != true && context.performed)
        {
            cursorDetection.cursorHasToken = true;
            selectCharacterMenuBusiness.DisableReadyPanel(SelectCharacterManager.instance.readyPanelGameobject);
        }
    }

    /// <summary>
    /// When all players select a character, anyone player can start the game with this method
    /// </summary>
    /// <param name="context">This is the CallBackContext who give information about the state of the button</param>
    public void OnValidAllCharacterChoice(InputAction.CallbackContext context)
    {
        if (SelectCharacterManager.instance.readyPanelGameobject.activeInHierarchy == true && context.performed)
        {
            DOTween.Clear();
            selectCharacterMenuBusiness.StartFight(SelectCharacterManager.instance.playerSelectGameObjectByDevice, "StoneBattleground", characterBusiness, playerBusiness);
        }
    }

    /// <summary>
    /// When player call this method, the device is removed
    /// </summary>
    /// <param name="context">This is the CallBackContext who give information about the state of the button</param>
    public void onDisconnectDevice(InputAction.CallbackContext context)
    {
        if (context.performed && SelectCharacterManager.instance.inputDeviceList.Count > 1)
        {
            SelectCharacterManager.instance.onDeviceChangeDuringSelectCharacterMenu(this.device, InputDeviceChange.Removed);
        }
    }
}