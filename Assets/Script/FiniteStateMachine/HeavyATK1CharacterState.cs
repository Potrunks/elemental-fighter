public class HeavyATK1CharacterState : CharacterState
{
    private ICharacterState nextState;
    private System.Random random = new System.Random();

    public override ICharacterState CheckingStateModification(MovePlayer player)
    {
        // Hurt state
        if (player.isHurting == true)
        {
            return nextState = new HurtCharacterState();
        }
        else
        {
            // Heavy ATK 1 Transition
            if (player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                return nextState = new HeavyATK1TransitionCharacterState();
            }
        }
        return nextState;
    }

    public override void OnEnter(MovePlayer player)
    {
        player.audioManager.Play("Fireball");
        if (player.enemyDamageCommand.currentHealth <= (player.enemyDamageCommand.maxHealth / 5))
        {
            player.audioManager.PlaySoundByIndexInListOfSound(player.audioManager.insultSounds, random.Next(0, player.audioManager.insultSounds.Length));
        }
        else
        {
            player.audioManager.PlaySoundByIndexInListOfSound(player.audioManager.heavyATKSounds, random.Next(0, player.audioManager.heavyATKSounds.Length));
        }
        if (player.isGrounding == true)
        {
            // Disable Velocity Player
            player.moveSpeed = player.stopMoveSpeed;
            // Heavy ATK 1 Action
            player.animator.Play("HeavyATK1");
        }
        else
        {
            // Air Heavy ATK 1 animation
            player.animator.Play("AirHeavyAttack1");
        }

    }

    public override void OnExit(MovePlayer player)
    {

    }

    public override void PerformingInput(string action)
    {

    }
}
