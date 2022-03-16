using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using EventChannels;

[RequireComponent(typeof(CircleCollider2D), typeof(Animation))]
public class Log : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] LevelEventChannelSO levelEvents;
    [SerializeField] IntEventChannelSO maxKnifeCountEvent;
    [SerializeField] IntEventChannelSO knifeCountEvent;

    [Header("Properties")]
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Transform rotatingHolder;
    [SerializeField] Transform knifeHolder;
    [SerializeField] Transform appleHolder;

    LogDataSO data;
    CircleCollider2D circleCollider;
    Animation animation;
    AnimationCurve rotationCurve;
    GameObject explodeEffect;
    GameObject hitEffect;

    int Hp {
        get => hp;
        set {
            hp = value;
            knifeCountEvent.UpdateValue(hp);
            if (hp == 0)
                Explode();
        }
    }
    
    int hp;
    float time;
    bool stopped;

    void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        animation = GetComponent<Animation>();
    }

    void OnEnable()
    {
        maxKnifeCountEvent.OnValueUpdated += OnMaxKnivesUpdated;
    }

    void OnDisable()
    {
        maxKnifeCountEvent.OnValueUpdated -= OnMaxKnivesUpdated;
    }

    void Update()
    {
        var speed = rotationCurve.Evaluate(time);
        if (stopped)
            speed = Mathf.Lerp(speed, 0f, Time.deltaTime * 8f);
        rotatingHolder.Rotate(new Vector3(0f, 0f, speed * Mathf.Rad2Deg * Time.deltaTime));
        time += Time.deltaTime;
    }

    public void AssignData(LogDataSO data)
    {
        this.data = data;
        spriteRenderer.sprite = data.Sprite;
        circleCollider.radius = data.Radius;
        explodeEffect = data.DestroyEffect;
        hitEffect = data.HitEffect;
    }

    public void SetRotationCurve(AnimationCurve rotationCurve)
    {
        this.rotationCurve = rotationCurve;
    }

    public void Explode()
    {
        // Spawn effect
        Instantiate(explodeEffect, transform.position, Quaternion.identity);

        // Vibrate
        Vibration.VibratePeek();

        // Detach children
        foreach (Transform child in knifeHolder) {
            var knife = child.GetComponent<Knife>();
            knife.Release();
            knife.BlastAway();
        }

        foreach (Transform apple in appleHolder) {
            apple.GetComponent<Apple>().Explode();
        }

        knifeHolder.DetachChildren();

        // Notify
        levelEvents.DestroyLog();

        // Destroy self
        Destroy(gameObject);
    }

    public void Stop()
    {
        stopped = true;
    }

    public void Hide()
    {
        Destroy(gameObject);
    }

    void AttachKnife(Knife knife)
    {
        knife.transform.SetParent(knifeHolder, true);
        knife.Stick();
    }

    public void AddKnife(Knife knife, float angle)
    {
        // Stick and align knife
        knife.transform.position = transform.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * data.Radius;
        knife.transform.up = transform.position - knife.transform.position;
        AttachKnife(knife);
    }

    public void AddApple(Apple apple, float angle)
    {
        apple.transform.position = transform.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * (data.Radius + 0.1f);
        apple.transform.up = transform.position - apple.transform.position;
        apple.transform.SetParent(appleHolder, true);
    }

    public void HitKnife(Knife knife)
    {
        if (Hp > 1) {
            // Align by direction
            Vector3 direction = (knife.transform.position - transform.position).normalized;
            knife.transform.position = transform.position + direction * data.Radius;
            AttachKnife(knife);
            
            // Hit effect
            Instantiate(hitEffect, knife.transform.position, Quaternion.identity);
            Vibration.Vibrate(30);
        }else{
            knife.QueueDestroy();
        }

        // Reduce hp
        Hp--;

        // Play animation
        animation.Play("Hit");
    }

    void OnMaxKnivesUpdated(int number)
    {
        Hp = number;
    }
}
