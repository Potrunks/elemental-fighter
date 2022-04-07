using UnityEngine;

public class HurtCharacterState : CharacterState
{
    private ICharacterState nextState;
    public override ICharacterState CheckingStateModification(MovePlayer player)
    {
        // When damage is taken, go to Idle State
        if (player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            if (player.GetComponent<DamageCommand>().currentHealth <= 0)
            {
                return nextState = new DieCharacterState();
            }
            else
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
        else
        {
            if (player.isHurting == true)
            {
                return nextState = new HurtCharacterState();
            }
        }
        return nextState;
    }

    public override void OnEnter(MovePlayer player)
    {
        // Play animation
        player.moveSpeed = player.stopMoveSpeed;
        // isHurting go to False value
        player.isHurting = false;
        player.animator.Play("Hurt", -1, 0.0f);
    }

    public override void OnExit(MovePlayer player)
    {

    }

    public override void PerformingInput(string action)
    {

    }
}
