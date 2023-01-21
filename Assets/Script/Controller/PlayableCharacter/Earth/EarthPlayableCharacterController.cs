using Assets.Script.Business;
using Assets.Script.Data;
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
        [SerializeField]
        private GameObject _groundLineAlreadyInTheScene;

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
            _characterBusiness.PushElementalProjectile(this, new List<PowerLevelReference> { PowerLevelReference.Medium }, 5f);
            _characterBusiness.InflictedMeleeDamageAfterHitBoxContact(_hitBoxAtk, _hitBoxAtkRadius, this, isPushingAtk: true);
        }

        public void OnCastHeavyAtk()
        {
            if (_groundLineAlreadyInTheScene != null)
            {
                _groundLineAlreadyInTheScene.GetComponent<PowerController>().TriggerSelfDestruct(0.5f);
            }
            kvpPowerModelByPowerLevel.TryGetValue(PowerLevelReference.Heavy, out GameObject heavyElementalToCast);
            _groundLineAlreadyInTheScene = elementalBusiness.InstantiateStaticElemental(heavyElementalToCast, gameObjectElementalSpawnPoint, this);
            _characterBusiness.InflictedMeleeDamageAfterHitBoxContact(_hitBoxAtk, _hitBoxAtkRadius, this, isPushingAtk: true);
        }

        public void OnCastEarthSpecialElemental()
        {
            if (_wallRockAlreadyInTheScene != null)
            {
                _wallRockAlreadyInTheScene.GetComponent<PowerController>().TriggerSelfDestruct(0.5f);
            }
            kvpPowerModelByPowerLevel.TryGetValue(PowerLevelReference.Special, out GameObject specialElementalToCast);
            _wallRockAlreadyInTheScene = elementalBusiness.InstantiateStaticElemental(specialElementalToCast, gameObjectElementalSpawnPoint, this);
        }

        public void OnThrowSpecialAtk2()
        {
            _characterBusiness.PushElementalProjectile(this, new List<PowerLevelReference> { PowerLevelReference.Special }, 5f, rigidbodyConstraints2DList: new List<RigidbodyConstraints2D> { RigidbodyConstraints2D.FreezePositionY, RigidbodyConstraints2D.FreezeRotation });
            _characterBusiness.InflictedMeleeDamageAfterHitBoxContact(_hitBoxAtk, _hitBoxAtkRadius, this, isPushingAtk: true);
        }
        #endregion
    }
}
