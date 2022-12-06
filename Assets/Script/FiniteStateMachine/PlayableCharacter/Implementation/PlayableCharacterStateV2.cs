using Assets.Script.Data;

public abstract class PlayableCharacterStateV2 : IPlayableCharacterStateV2
{
    public abstract IPlayableCharacterStateV2 CheckingStateModification(PlayableCharacterController playableCharacterController);
    public abstract void OnEnter(PlayableCharacterController playableCharacterController);
    public abstract void OnExit(PlayableCharacterController playableCharacterController);
    public abstract void PerformingInput(PlayableCharacterActionReference action);
}
