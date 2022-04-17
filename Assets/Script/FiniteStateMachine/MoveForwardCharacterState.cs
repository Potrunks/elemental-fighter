public class MoveForwardCharacterState : CharacterState
{
    private ICharacterState nextState;
    public override ICharacterState CheckingStateModification(MovePlayer player)
    {
        if (player.isHurting == false)
        {
            // Idle State
            if ((player.rb.velocity.x <= 0.1f && player.spriteRenderer.flipX == false)
            || (player.rb.velocity.x >= -0.1f && player.spriteRenderer.flipX == true))
            {
                nextState = new IdleCharacterState();
            }
            else if (player.isGrounding == false)
            {
                // Fall State
                if (player.rb.velocity.y <= -0.1f)
                {
                    return nextState = new FallCharacterState();
                }
            }
        }
        else
        {
            // hurt state
            return nextState = new HurtCharacterState();
        }
        return nextState;
    }

    public override void OnEnter(MovePlayer player)
    {
        player.audioManager.Play("MoveForward");
        player.animator.Play("Run");
    }

    public override void OnExit(MovePlayer player)
    {
        player.audioManager.Stop("MoveForward");
    }

    public override void PerformingInput(string action)
    {
        // Jump
        if (action.Equals("Jumping"))
        {
            nextState = new JumpCharacterState();
        }
        // Light ATK 1
        if (action.Equals("LightATK"))
        {
            nextState = new LightATK1CharacterState();
        }
        // Block
        if (action.Equals("Block"))
        {
            nextState = new BlockCharacterState();
        }
        // Medium ATK 1
        if (action.Equals("MediumATK"))
        {
            nextState = new MediumATK1CharacterState();
        }
        // Heavy ATK 1
        if (action.Equals("HeavyATK"))
        {
            nextState = new HeavyATK1CharacterState();
        }
        // Dash
        if (action.Equals("Dash"))
        {
            nextState = new DashCharacterState();
        }
    }
}
