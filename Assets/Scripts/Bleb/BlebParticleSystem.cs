using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlebParticleSystem : MonoBehaviour
{
    [Header("Particle")]
    [Tooltip("Particle System на префабе (drag here)")]
    public ParticleSystem ps;

    [Tooltip("—колько частиц выстрелить однократно при прикреплении")]
    public int burstCount = 30;

    [Tooltip("—корость посто€нной эмиссии, когда блЄб прикреплЄн (частиц/сек)")]
    public float continuousRate = 8f;

    [Tooltip("Tag паутины или оставь пустым и будет искать компонент RopeVerlet")]
    public string webTag = "Web";

    private ParticleSystem.EmissionModule emission;
    private bool currentlyAttached = false;

    void Awake()
    {
        if (ps == null)
            ps = GetComponentInChildren<ParticleSystem>();

        if (ps == null)
        {
            Debug.LogWarning($"[{name}] ParticleSystem не назначен и не найден.");
            enabled = false;
            return;
        }

        emission = ps.emission;
        // по умолчанию - не эмитим
        emission.rateOverTime = 0f;
        ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsWebCollision(collision.collider))
        {
            AttachToWeb();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (IsWebCollision(collision.collider))
        {
            DetachFromWeb();
        }
    }

    private bool IsWebCollision(Collider2D col)
    {
        if (col == null) return false;

        if (!string.IsNullOrEmpty(webTag))
        {
            if (col.gameObject.CompareTag(webTag)) return true;
        }

        return false;
    }

    private void AttachToWeb()
    {
        if (currentlyAttached) return;
        currentlyAttached = true;

        // немедленный радиальный "выстрел"
        ps.Emit(burstCount);

        // переход в посто€нную эмиссию
        emission.rateOverTime = continuousRate;
        if (!ps.isPlaying) ps.Play();
    }

    private void DetachFromWeb()
    {
        if (!currentlyAttached) return;
        currentlyAttached = false;

        // остановка посто€нной эмиссии (частицы, уже выпущенные Ч будут жить)
        emission.rateOverTime = 0f;
        // можно оставить ps.Play чтобы хвост плавно исчезал, или остановить:
        // ps.Stop(false, ParticleSystemStopBehavior.StopEmitting);
    }

    private void OnDestroy()
    {
        // при удалении объекта Ч остановим систему
        if (ps != null) ps.Stop();
    }
}
