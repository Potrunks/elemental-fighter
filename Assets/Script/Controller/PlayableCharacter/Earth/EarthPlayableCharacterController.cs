using Assets.Script.Business;
using Assets.Script.Data.Reference;
using Assets.Script.FiniteStateMachine;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script.Controller
{
    public class EarthPlayableCharacterController : PlayableCharacterController
    {
        [Header("InGame Data Supplementary")]
        [SerializeField]
        private GameObject _wallRockAlreadyInTheScene;

        #region MonoBehaviour Method
        private void Start()
        {
            currentState = new EarthIdlePlayableCharacterState();
            currentState.OnEnter(this);
        }
        #endregion

        #region Action
        public void OnCastEarthMediumElemental()
        {
            kvpPowerModelByPowerLevel.TryGetValue(PowerLevelReference.Medium, out GameObject mediumElementalToCast);
            elementalBusiness.InstantiateElementalUpperOrientation(mediumElementalToCast, gameObjectElementalSpawnPoint, this);
        }

        public void OnThrowMediumAtk()
        {
            characterBusiness.PushElemental(this, "ElementalProjectile", new List<PowerLevelReference> { PowerLevelReference.Medium }, 5f);
            characterBusiness.InflictedMeleeDamageAfterHitBoxContact(_hitBoxAtk, _hitBoxAtkRadius, this, isPushingAtk: true);
        }

        public void OnCastHeavyAtk()
        {
            kvpPowerModelByPowerLevel.TryGetValue(PowerLevelReference.Heavy, out GameObject heavyElementalToCast);
            elementalBusiness.InstantiateStaticElemental(heavyElementalToCast, gameObjectElementalSpawnPoint, this);
            characterBusiness.InflictedMeleeDamageAfterHitBoxContact(_hitBoxAtk, _hitBoxAtkRadius, this, isPushingAtk: true);
        }

        public void OnCastEarthSpecialElemental()
        {
            if (_wallRockAlreadyInTheScene != null)
            {
                _wallRockAlreadyInTheScene.GetComponent<PowerController>().TriggerSelfDestruct(1f);
            }
            kvpPowerModelByPowerLevel.TryGetValue(PowerLevelReference.Special, out GameObject specialElementalToCast);
            _wallRockAlreadyInTheScene = elementalBusiness.InstantiateStaticElemental(specialElementalToCast, gameObjectElementalSpawnPoint, this);
        }

        public void OnThrowSpecialAtk2()
        {
            characterBusiness.PushElemental(this, "ElementalProjectile", new List<PowerLevelReference> { PowerLevelReference.Special }, 5f, rigidbodyConstraints2DList: new List<RigidbodyConstraints2D> { RigidbodyConstraints2D.FreezePositionY, RigidbodyConstraints2D.FreezeRotation });
            characterBusiness.InflictedMeleeDamageAfterHitBoxContact(_hitBoxAtk, _hitBoxAtkRadius, this, isPushingAtk: true);
        }
        #endregion
    }
}
