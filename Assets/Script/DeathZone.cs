using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        MovePlayer player = other.GetComponent<MovePlayer>();
        GameObject enemy = player.enemy;
        player.GetComponent<DamageCommand>().ResetPlayer(player, enemy);
    }
}
