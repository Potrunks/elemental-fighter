using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Script.Business.Interface
{
    internal interface ISelectCharacterMenuBusiness
    {
        /// <summary>
        /// Verify if all players have confirmed the player choice. If all player have confirmed his choice, a ready panel appear
        /// </summary>
        /// <param name="cursorDetections">All cursor detection of all players</param>
        /// <param name="readyPanel">Ready panel to appear</param>
        void VerifyAllPlayerConfirmedCharacterChoice(List<CursorDetection> cursorDetections, GameObject readyPanel);

        /// <summary>
        /// Disable the ready panel
        /// </summary>
        /// <param name="readyPanelGameobject">Disable the ready panel</param>
        void DisableReadyPanel(GameObject readyPanelGameobject);

        /// <summary>
        /// Load the fighting scene with a dictionary with cursor, token and select character preview by device
        /// </summary>
        /// <param name="playerSelectGameObjectByDevice">Cursor, token and select character preview by device</param>
        /// <param name="fightingScene">Fighting Scene</param>
        void StartFight(IDictionary<InputDevice, List<GameObject>> playerSelectGameObjectByDevice, string fightingScene, ICharacterBusiness characterBusiness, IPlayerBusiness playerBusiness);

        /// <summary>
        /// Allow to go to main menu from select character menu
        /// </summary>
        void GoToMainMenu();
    }
}
