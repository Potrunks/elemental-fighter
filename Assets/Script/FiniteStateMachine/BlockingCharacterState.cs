using UnityEngine;

public class BlockingCharacterState : PlayableCharacterState
{
    private IPlayableCharacterState nextState;
    private System.Random random = new System.Random();

    public override IPlayableCharacterState CheckingStateModification(MovePlayer player)
    {
        // go to idle
        if (player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            player.isHurtingByPushAttack = false;
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
        player.audioManager.Play("Blocking");
        player.audioManager.PlaySoundByIndexInListOfSound(player.audioManager.blockingSounds, random.Next(0, player.audioManager.blockingSounds.Length));
        player.enemyMovePlayer.audioManager.PlaySoundByIndexInListOfSound(player.enemyMovePlayer.audioManager.astonishmentSounds, random.Next(0, player.enemyMovePlayer.audioManager.astonishmentSounds.Length));
        DamageCommand playerDamageCommand = player.GetComponent<DamageCommand>();
        // Push effect
        playerDamageCommand.DisplayBlockingEffect();
        // Move player with push
        if (player.isFlipLeft == true)
        {
            if (playerDamageCommand.GetIsAttackedFromBehind() == true)
            {
                player.rb.velocity = PushLeft(playerDamageCommand.blockPush);
            } else
            {
                player.rb.velocity = PushRight(playerDamageCommand.blockPush);
            }
        }
        else
        {
            if (playerDamageCommand.GetIsAttackedFromBehind() == true)
            {
                player.rb.velocity = PushRight(playerDamageCommand.blockPush);
            }
            else
            {
                player.rb.velocity = PushLeft(playerDamageCommand.blockPush);
            }
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

    private Vector2 PushLeft(float force)
    {
        return Vector2.left * force;
    }

    private Vector2 PushRight(float force)
    {
        return Vector2.right * force;
    }
}
