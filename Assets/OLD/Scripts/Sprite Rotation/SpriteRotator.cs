using UnityEngine;

public class SpriteRotator : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite frontSprite, rightSprite, leftSprite, backSprite;
    [SerializeField] private Camera cam;
    void FixedUpdate()
    {
        Vector3 dir = transform.position - cam.transform.position;
        dir.y = 0;
        dir.Normalize();
        Vector3 cross = Vector3.Cross(Vector3.forward, dir);
        float sign = Mathf.Sign(Vector3.Dot(cross, Vector3.up));
        float angle = sign * Vector3.Angle(Vector3.forward, dir);

        if(angle > -45f && angle <= 45f) _spriteRenderer.sprite = frontSprite;
        else if(angle > 45f && angle <= 135f) _spriteRenderer.sprite = rightSprite;
        else if(angle > -135f && angle <= -45f) _spriteRenderer.sprite = leftSprite;
        else _spriteRenderer.sprite = backSprite;

        transform.LookAt(new Vector3(cam.transform.position.x, transform.position.y, cam.transform.position.z));
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - 180f, transform.eulerAngles.z);
    }
}