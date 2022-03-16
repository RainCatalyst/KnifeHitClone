using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using EventChannels;

[RequireComponent(typeof(Rigidbody2D), typeof(Animation))]
public class Knife : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] LevelEventChannelSO levelEvents;

    [Header("Properties")]
    [SerializeField] float destroyTime = 2f;

    [Header("References")]
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Collider2D baseCollider;

    public bool IsStuck => stuck;

    bool stuck;
    Rigidbody2D rb;
    KnifeDataSO data;
    Animation animation;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animation = GetComponent<Animation>();
    }

    public void AssignData(KnifeDataSO data)
    {
        this.data = data;
        spriteRenderer.sprite = data.Sprite;
    }

    public void Ready()
    {
        animation.Play("Ready");
    }

    public void Throw(float velocity)
    {
        rb.simulated = true;
        rb.velocity = Vector2.up * velocity;
    }

    public void Stick()
    {
        stuck = true;
        rb.simulated = true;
        rb.bodyType = RigidbodyType2D.Static;
        baseCollider.isTrigger = false;
    }

    public void Release()
    {
        stuck = false;
        baseCollider.enabled = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 6f;
        QueueDestroy();
    }

    public void BlastAway()
    {
        rb.velocity = new Vector2(transform.up.x, transform.up.y) * 6f + new Vector2(Random.Range(-16f, 16f), Random.Range(5f, 13f));
        rb.angularVelocity = Random.Range(-360f, 360f);
    }

    public void Deflect()
    {
        rb.velocity = -rb.velocity * 0.35f + new Vector2(Random.Range(-2.5f, 2.5f), Random.Range(0.1f, -0.5f));
        rb.angularVelocity = Random.Range(-360f, 360f);
        QueueDestroy();
    }

    public void QueueDestroy()
    {
        Destroy(gameObject, destroyTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (stuck)
            return;
        
        Log log;
        if (other.gameObject.TryGetComponent(out log)) {
            log.HitKnife(this);
            animation.Play("Hit");
        }

        Knife knife;
        if (other.gameObject.TryGetComponent(out knife)) {
            if (knife.IsStuck) {
                Release();
                Deflect();
                Vibration.VibratePeek();
                levelEvents.DeflectKnife();
            }
        }

        Apple apple;
        if (other.gameObject.TryGetComponent(out apple)) {
            apple.Explode();
            levelEvents.DestroyApple();
        }
    }
}
