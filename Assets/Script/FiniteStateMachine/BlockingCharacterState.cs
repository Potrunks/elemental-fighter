using UnityEngine;

public class BlockingCharacterState : CharacterState
{
    private ICharacterState nextState;
    public override ICharacterState CheckingStateModification(MovePlayer player)
    {
        // go to idle
        if (player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            // continu to block
            if (player.isBlockingAttack == true)
            {
                return nextState = new BlockCharacterState();
            }
            else
            {
                if (player.isGrounding == true)
                {
                    return nextState = new IdleCharacterState();
                }
            }
        }

        return nextState;
    }

    public override void OnEnter(MovePlayer player)
    {
        // Push effect
        player.GetComponent<DamageCommand>().DisplayBlockingEffect();
        // Move player with push
        if (player.spriteRenderer.flipX == true)
        {
            player.rb.velocity = Vector2.right * player.GetComponent<DamageCommand>().blockPush;
        }
        else
        {
            player.rb.velocity = Vector2.left * player.GetComponent<DamageCommand>().blockPush;
        }
        // play animation Blocking
        player.animator.Play("Blocking");
    }

    public override void OnExit(MovePlayer player)
    {
        // go to false value for is Hurting
        player.isHurting = false;
    }

    public override void PerformingInput(string action)
    {

    }
}
