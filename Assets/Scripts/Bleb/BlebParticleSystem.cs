using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlebParticleSystem : MonoBehaviour
{
    public ParticleSystem particles;     // Сам Particle System
    public float burstInterval = 3f;     // Интервал между вспышками
    public bool OnWeb = false;         // Блёб находится на паутине?

    private Coroutine burstRoutine;

    private void Start()
    {
        if (particles == null)
            particles = GetComponentInChildren<ParticleSystem>();
    }

    public void SetOnWeb(bool value)
    {
        OnWeb = value;

        if (OnWeb)
        {
            if (burstRoutine == null)
                burstRoutine = StartCoroutine(BurstLoop());
        }
        else
        {
            if (burstRoutine != null)
            {
                StopCoroutine(burstRoutine);
                burstRoutine = null;
            }
        }
    }

    private IEnumerator BurstLoop()
    {
        while (OnWeb)
        {
            particles.Emit(10);   // запускаем разовый бурст
            yield return new WaitForSeconds(burstInterval);
        }
    }
}
