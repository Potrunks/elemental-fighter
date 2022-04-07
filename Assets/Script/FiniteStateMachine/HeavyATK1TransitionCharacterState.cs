public class HeavyATK1TransitionCharacterState : CharacterState
{
    private ICharacterState nextState;
    public override ICharacterState CheckingStateModification(MovePlayer player)
    {
        if (player.isHurting == true)
        {
            // hurt state
            return nextState = new HurtCharacterState();
        }
        else
        {
            // attendre fin animation sauf pour hurt state
            if (player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                // idle state
                if (player.isGrounding == true)
                {
                    return nextState = new IdleCharacterState();
                }
                else
                {
                    // jump state
                    if (player.rb.velocity.y >= 0.1f)
                    {
                        return nextState = new JumpCharacterState();
                    }
                    // fall state
                    if (player.rb.velocity.y <= -0.1f)
                    {
                        return nextState = new FallCharacterState();
                    }
                }
            }
        }
        return nextState;
    }

    public override void OnEnter(MovePlayer player)
    {
        // check if grounded
        if (player.isGrounding == true)
        {
            // if true, heavy atk 1 transition
            player.animator.Play("HeavyATK1Transition");
        }
        else
        {
            // if false, air heavy atk 1 transition
            player.animator.Play("AirHeavyAttack1Transition");
        }
    }

    public override void OnExit(MovePlayer player)
    {

    }

    public override void PerformingInput(string action)
    {

    }
}
