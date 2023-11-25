using System;
using UnityEngine;

public class HomeAlarm : MonoBehaviour
{
    public event Action CrookInEvent;
    public event Action CrookOutEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Crook crook))
            CrookInEvent?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Crook crook))
            CrookOutEvent?.Invoke();
    }
}