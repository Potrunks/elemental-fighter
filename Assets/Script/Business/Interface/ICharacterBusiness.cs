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
        /// Push an elemental power.
        /// </summary>
        /// <param name="pusher">Character who push the elemental power</param>
        /// <param name="elementalLayerName">Layer name of the elemental able to be pushed by the atk of the pusher</param>
        /// <param name="powerLevelToPushList">List of power level able to be push by the atk of the pusher</param>
        /// <param name="selfDestructTimer">Time for destruct the elemental power gameobject after pushing. If 0, the object never destruct</param>
        /// <param name="rigidbodyConstraints2DList">List of rigibody constraint 2D to apply to the elemental power gameobject. Only freeze PositionX, PositionY and Rotation is available. By default is null so the constraint is None</param>
        void PushElemental(PlayableCharacterController pusher, string elementalLayerName, IEnumerable<PowerLevelReference> powerLevelToPushList, float selfDestructTimer, IEnumerable<RigidbodyConstraints2D> rigidbodyConstraints2DList = null);

        /// <summary>
        /// Respawn the character on his own character spawn point.
        /// </summary>
        void RespawnPlayer(PlayableCharacterController characterToRespawn);

        /// <summary>
        /// Reset character after died. Update score enemy and check if end game or not.
        /// </summary>
        /// <returns>Return TRUE if the game is over because the enemy, of the character died, win the game.</returns>
        bool ResetCharacterAfterDeath(PlayableCharacterController characterDied);

        /// <summary>
        /// Update the victory point of the selected character. Can choose the number of victory point to add (1 by default).
        /// </summary>
        /// <returns>Return TRUE if the player win the game.</returns>
        bool UpdateCharacterScore(ScorePlayer scorePlayerToUpdate, int pointWon = 1);
    }
}
