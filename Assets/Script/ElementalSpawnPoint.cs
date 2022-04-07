using UnityEngine;

public class ElementalSpawnPoint : MonoBehaviour
{
    public float rotation, absTransformX, absTransformY;
    public void Rotate(bool flipX) {
        transform.Rotate(0f, rotation, 0f);
        if (flipX == true) {
            transform.localPosition = new Vector3(-absTransformX, -absTransformY, 0f);
        } else {
            transform.localPosition = new Vector3(absTransformX, -absTransformY, 0f);
        }
    }
}
