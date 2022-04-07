public class LightATK1TransitionCharacterState : CharacterState
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
        if(player.isGrounding == true){
            // if grounded, transition normal
            player.animator.Play("LightATK1Transition");
        } else {
            // if not grounded, air transition
            player.animator.Play("AirAttack1Transition");
        }
    }

    public override void OnExit(MovePlayer player)
    {
        
    }

    public override void PerformingInput(string action)
    {
        // Light ATK 2
        if (action.Equals("LightATK"))
        {
            nextState = new LightATK2CharacterState();
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
    }
}
