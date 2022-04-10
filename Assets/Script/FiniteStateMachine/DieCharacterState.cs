using UnityEngine;
public class DieCharacterState : CharacterState
{
    private ICharacterState nextState;
    public override ICharacterState CheckingStateModification(MovePlayer player)
    {
        if (player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            //GameObject.Destroy(player.gameObject);
            player.GetComponent<DamageCommand>().ResetPlayer(player, player.enemy);
            return nextState = new IdleCharacterState();
        }
        return nextState;
    }

    public override void OnEnter(MovePlayer player)
    {
        player.animator.Play("Die");
    }

    public override void OnExit(MovePlayer player)
    {

    }

    public override void PerformingInput(string action)
    {

    }
}
