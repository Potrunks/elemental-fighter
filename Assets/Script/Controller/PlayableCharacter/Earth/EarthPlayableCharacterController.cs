using Assets.Script.Business;
using Assets.Script.Data.Reference;
using Assets.Script.FiniteStateMachine;
using UnityEngine;

namespace Assets.Script.Controller
{
    public class EarthPlayableCharacterController : PlayableCharacterController
    {
        [Header("InGame Data")]
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
            characterBusiness.PushElemental(this, "EarthElemental");
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
        #endregion
    }
}
