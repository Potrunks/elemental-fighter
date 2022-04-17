public class BlockCharacterState : CharacterState
{
    private ICharacterState nextState;
    public override ICharacterState CheckingStateModification(MovePlayer player)
    {
        // Verify if blocking ATK
        if (player.isHurting == true)
        {
            return nextState = new BlockingCharacterState();
        }
        else
        {
            // Verify if is not blocking
            if (player.isBlockingAttack == false)
            {
                return nextState = new IdleCharacterState();
            }
        }
        return nextState;
    }

    public override void OnEnter(MovePlayer player)
    {
        // Disable Velocity Player
        player.moveSpeed = player.stopMoveSpeed;
        // Blocking Animation
        player.isBlockingAttack = true;
        player.animator.Play("BlockIdle");
    }

    public override void OnExit(MovePlayer player)
    {

    }

    public override void PerformingInput(string action)
    {

    }
}
