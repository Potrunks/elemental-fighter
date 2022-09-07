using UnityEngine;

namespace Assets.Script.Business.Interface
{
    internal interface ICharacterBusiness
    {
        /// <summary>
        /// Confirm the character choice
        /// </summary>
        /// <param name="playerslot">Player slot of interest</param>
        /// <param name="characterUnderCursor">Character under cursor confirmed by player</param>
        void ConfirmCharacter(GameObject playerslot, GameObject characterUnderCursor);

        /// <summary>
        /// Allow to show the character under the cursor in a player slot
        /// </summary>
        /// <param name="playerSlot">Slot of the player</param>
        /// <param name="characterUnderPlayerCursor">Character under the cursor to display in player slot</param>
        void ShowCharacterInPlayerSlot(GameObject playerSlot, GameObject characterUnderPlayerCursor);

        /// <summary>
        /// Change color of character sprite selected by the player
        /// </summary>
        /// <param name="playerIndex">Index of the player</param>
        /// <param name="spriteRenderer">SpriteRenderer of the character selected by the player</param>
        void SetSpriteRendererColorByIndexPlayer(int playerIndex, SpriteRenderer spriteRenderer);
        GameObject GetRandomCharacter();
    }
}
