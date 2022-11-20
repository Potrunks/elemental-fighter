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
    }
}
