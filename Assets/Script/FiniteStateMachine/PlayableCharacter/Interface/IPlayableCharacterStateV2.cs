using Assets.Script.Data.Reference;

public interface IPlayableCharacterStateV2
{
    void PerformingInput(PlayableCharacterActionReference action);
    IPlayableCharacterStateV2 CheckingStateModification(PlayableCharacterController playableCharacterController);
    void OnEnter(PlayableCharacterController playableCharacterController);
    void OnExit(PlayableCharacterController playableCharacterController);
}
