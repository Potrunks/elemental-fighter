using Assets.Script.Data.Reference;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script.Business.Interface
{
    public interface ICharacterBusiness
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

        /// <summary>
        /// Allow to move a rigibody when player use device.
        /// </summary>
        Vector2 MoveCharacter(Vector2 inputMoveValue, float moveSpeed, Rigidbody2D rigidbodyToMove, float smoothTime);

        /// <summary>
        /// Flip the model depending on velocity orientation.
        /// </summary>
        void CheckFlipCharacterModel(PlayableCharacterController controller);

        /// <summary>
        /// Verify if the character state have been change.
        /// </summary>
        void CheckCharacterStateChange(PlayableCharacterController controller);

        /// <summary>
        /// Check what kind of object is touching by the attack and apply the good treatment.
        /// </summary>
        void InflictedMeleeDamageAfterHitBoxContact(GameObject hitBox, float hitBoxRadius, PlayableCharacterController caster, bool isPushingAtk = false);

        /// <summary>
        /// Inflict damage to the enemy.
        /// </summary>
        void InflictedMeleeDamage(PlayableCharacterController enemy, PlayableCharacterController caster, bool isPushingAtk);

        /// <summary>
        /// Push an elemental power depending of this layer. Rigidbody constraint 2D is none by default.
        /// The only rigidbody constraint 2D available is PositionX, PositionY and Rotation.
        /// </summary>
        /// <param name="elementalLayerName">Layer name of the target elemental to push.</param>
        void PushElemental(PlayableCharacterController pusher, string elementalLayerName, IEnumerable<PowerLevelReference> powerLevelToPushList, IEnumerable<RigidbodyConstraints2D> rigidbodyConstraints2DList = null);
    }
}
