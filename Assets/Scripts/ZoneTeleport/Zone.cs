using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Zone : MonoBehaviour
{
    public bool hasOnEnter;
    public UnityEvent onEnter;
    public bool hasOnExit;
    public UnityEvent onExit;
    [Header("North")]
    [SerializeField] Zone m_North;
    [SerializeField] AudioClip m_NorthAudio;
    [SerializeField] float m_NorthDistance;
    [Header("East")]
    [SerializeField] Zone m_East;
    [SerializeField] AudioClip m_EastAudio;
    [SerializeField] float m_EastDistance;
    [Header("South")]
    [SerializeField] Zone m_South;
    [SerializeField] AudioClip m_SouthAudio;
    [SerializeField] float m_SouthDistance;
    [Header("West")]
    [SerializeField] Zone m_West;
    [SerializeField] AudioClip m_WestAudio;
    [SerializeField] float m_WestDistance;

    public Zone North
    {
        get { return m_North; }
    }
    public AudioClip NorthAudio
    {
        get { return m_NorthAudio; }
    }
    public float NorthDistance
    {
        get { return m_NorthDistance; }
    }
    public Zone East
    {
        get { return m_East; }
    }
    public AudioClip EastAudio
    {
        get { return m_EastAudio; }
    }
    public float EastDistance
    {
        get { return m_EastDistance; }
    }
    public Zone South
    {
        get { return m_South; }
    }
    public AudioClip SouthAudio
    {
        get { return m_SouthAudio; }
    }
    public float SouthDistance
    {
        get { return m_SouthDistance; }
    }
    public Zone West
    {
        get { return m_West; }
    }
    public AudioClip WestAudio
    {
        get { return m_WestAudio; }
    }
    public float WestDistance
    {
        get { return m_WestDistance; }
    }

}
