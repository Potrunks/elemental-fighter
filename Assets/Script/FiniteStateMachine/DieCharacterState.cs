public class DieCharacterState : CharacterState
{
    private ICharacterState nextState;
    private System.Random random = new System.Random();

    public override ICharacterState CheckingStateModification(MovePlayer player)
    {
        if (player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            //GameObject.Destroy(player.gameObject);
            player.playerDamageCommand.ResetPlayer(player, player.enemy);
            return nextState = new IdleCharacterState();
        }
        return nextState;
    }

    public override void OnEnter(MovePlayer player)
    {
        player.animator.Play("Die");
        player.audioManager.PlaySoundByIndexInListOfSound(player.audioManager.dieSounds, random.Next(0, player.audioManager.dieSounds.Length));
    }

    public override void OnExit(MovePlayer player)
    {

    }

    public override void PerformingInput(string action)
    {

    }
}
