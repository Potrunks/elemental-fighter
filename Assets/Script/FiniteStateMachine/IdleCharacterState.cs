using UnityEngine;

public class IdleCharacterState : CharacterState
{
    private ICharacterState nextState;
    public override ICharacterState CheckingStateModification(MovePlayer player)
    {
        // Hurting
        if (player.isHurting == true)
        {
            return nextState = new HurtCharacterState();
        }
        else
        {
            // Fall State
            if (player.rb.velocity.y <= -0.01f)
            {
                return nextState = new FallCharacterState();
            }
            else
            {
                // Verify if blocking
                if (player.isBlockingAttack == true)
                {
                    return nextState = new BlockCharacterState();
                }
                else
                {
                    // Move Forward
                    if (player.rb.velocity.x > 0.1f || player.rb.velocity.x < -0.1f)
                    {
                        return nextState = new MoveForwardCharacterState();
                    }
                }
            }
        }
        return nextState;
    }

    public override void OnEnter(MovePlayer player)
    {
        player.moveSpeed = player.normalMoveSpeed;
        player.animator.Play("Idle");
    }

    public override void OnExit(MovePlayer player)
    {

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
    }
}
