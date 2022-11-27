using Assets.Script.Controller;
using Assets.Script.Entities;
using UnityEngine;

namespace Assets.Script.Business.Interface
{
    public interface IElementalBusiness
    {
        /// <summary>
        /// Change the color of the elemental power depending on the index of the player.
        /// </summary>
        void SetElementalColorByPlayerIndex(GameObject elementalGameObject, int casterPlayerIndex);

        /// <summary>
        /// Allow to jump, from the ground, a earth elemental.
        /// </summary>
        void RockOutOfGround(PowerController rockPowerControllerInstantiated);

        /// <summary>
        /// Allow the player to cast an element.
        /// </summary>
        void PrepareCastElemental(PowerEntity powerToCast, GameObject spawnPoint, MovePlayer caster);

        /// <summary>
        /// Instantiate an elemental in the scene with an upper orientation.
        /// </summary>
        void InstantiateElementalUpperOrientation(GameObject elementalToCast, GameObject spawnPoint, PlayableCharacterController caster);

        /// <summary>
        /// Instantiate a static elemental in the scene. Without considering input rotation.
        /// </summary>
        void InstantiateStaticElemental(GameObject elementalToCast, GameObject spawnPoint, PlayableCharacterController caster);

        /// <summary>
        /// Check all object touched by the attack and apply appropriate treatment.
        /// </summary>
        void CheckObjectTouchedByAttack(GameObject hitBoxAtk, float hitBoxAtkRadius);

        /// <summary>
        /// Check if state of the elemental in the scene need to be changed.
        /// </summary>
        void CheckElementalStateChange(PowerController controller);

        /// <summary>
        /// Inflict damage to the collider if this is an enemy. Work only one time.
        /// </summary>
        void InflictedDamageAfterCollision(Collider2D colliderTouched, PlayableCharacterController caster, PowerController powerController, bool isPushingAtk = false);
    }
}
