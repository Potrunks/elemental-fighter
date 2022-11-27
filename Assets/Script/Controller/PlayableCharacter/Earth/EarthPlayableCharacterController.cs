using Assets.Script.Data.Reference;
using Assets.Script.FiniteStateMachine;
using UnityEngine;

namespace Assets.Script.Controller
{
    public class EarthPlayableCharacterController : PlayableCharacterController
    {
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
        }

        public void OnCastHeavyAtk()
        {
            kvpPowerModelByPowerLevel.TryGetValue(PowerLevelReference.Heavy, out GameObject heavyElementalToCast);
            elementalBusiness.InstantiateStaticElemental(heavyElementalToCast, gameObjectElementalSpawnPoint, this);
        }

        public void OnCastEarthSpecialElemental()
        {
            kvpPowerModelByPowerLevel.TryGetValue(PowerLevelReference.Special, out GameObject specialElementalToCast);
            elementalBusiness.InstantiateStaticElemental(specialElementalToCast, gameObjectElementalSpawnPoint, this);
        }
        #endregion
    }
}
