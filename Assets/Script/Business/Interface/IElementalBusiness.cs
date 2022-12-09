using Assets.Script.Entities;
using UnityEngine;

namespace Assets.Script.Business
{
    public interface IElementalBusiness
    {
        /// <summary>
        /// Change the color of the elemental power depending on the index of the player.
        /// </summary>
        void SetElementalColorByPlayerIndex(GameObject elementalGameObject, int casterPlayerIndex);

        /// <summary>
        /// Instantiate an elemental in the scene with an upper orientation.
        /// </summary>
        void InstantiateElementalUpperOrientation(GameObject elementalToCast, GameObject spawnPoint, PlayableCharacterController caster);

        /// <summary>
        /// Instantiate a static elemental in the scene. Without considering input rotation.
        /// </summary>
        GameObject InstantiateStaticElemental(GameObject elementalToCast, GameObject spawnPoint, PlayableCharacterController caster);

        /// <summary>
        /// Inflicted damage on every enemy touched by elemental hit box.
        /// </summary>
        void InflictedElementalDamageAfterHitBoxContact(GameObject hitBoxAtk, float hitBoxAtkRadius, bool isPushingAtk, PlayableCharacterController caster, PowerEntity powerEntity);

        /// <summary>
        /// Check if state of the elemental in the scene need to be changed.
        /// </summary>
        void CheckElementalStateChange(PowerController controller);

        /// <summary>
        /// Inflicted damage to the target after collision
        /// </summary>
        /// <param name="colliderTouched">Collider touched by the elemental power</param>
        /// <param name="caster">PlayableCharacterController of the caster</param>
        /// <param name="powerControllerCasted">PowerController of the elemental power casted</param>
        /// <param name="isTriggerAfterCollision">The collider of the elemental power casted is trigger or not after collision</param>
        /// <param name="isPushingAtk">The elemental power can push the enemy touched by the attack. False by default</param>
        void InflictedDamageAfterCollision(Collider2D colliderTouched, PlayableCharacterController caster, PowerController powerControllerCasted, bool isTriggerAfterCollision, bool destructPowerAfterNoEnemyCollision = true, bool isPushingAtk = false);
    }
}
