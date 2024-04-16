using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BeatType
{
    Corchea,
    Negra,
    Blanca,
    Redonda
}

public class BeatManager : MonoBehaviour
{
    public static BeatManager Instance { get; private set; }
    public bool simplifiedControllers;

    public delegate void OnBeatEvent(BeatType type);
    public static event OnBeatEvent OnPreBeat;
    public static event OnBeatEvent OnBeat;
    public static event OnBeatEvent OnPostBeat;

    //[SerializeField] public static bool isPlaying {  get; private set; }
    public bool isPlaying;
    [Range(0f, 1f)] public float margen;
    public bool onMargen;

    public float bpm;
    private float timer;
    private int counter;
    private int totalcounter;
    
    
    //auxiliares

    private bool canPre;
    private bool canPost;
    
    private void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
    
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this;
            DontDestroyOnLoad(this);
        } 
    }

    private void Start()
    {
        isPlaying = false;
        onMargen = false;
        timer = 0f;
        canPre = true;
        canPost = false;
    }
    
    void Update()
    {
        if (isPlaying)
        {
            timer += Time.deltaTime;
            
            if (timer >= ((60f / bpm) - (60f / bpm) * margen)&& canPre)
            {
                canPre = false;
                PreBeat();
                    
            }
            
            if (timer >= 60f/bpm)
            {
                timer = 0f;
                Beat();
                canPost = true;
                
            }
            
            if (timer >= (0f + (60f / bpm) * margen) && canPost)
            {
                canPost = false;
                PostBeat();
                canPre = true;
            }
            
        }
    }

    
    void PreBeat()
    {
        Debug.Log("PREE- Beat");
        onMargen = true;
        OnPreBeat(BeatType.Corchea);
        if (counter%2==0)
        {
            OnPreBeat(BeatType.Negra);
        }
        if (counter%4==0)
        {
            OnPreBeat(BeatType.Blanca);
        }
        if (counter%8 == 0)
        {
            OnPreBeat(BeatType.Redonda);
        }
    }
    void Beat()
    {
        Debug.Log("Beat");
        counter += 1;
        OnBeat(BeatType.Corchea);
        if (counter%2==0)
        {
            OnBeat(BeatType.Negra);
        }
        if (counter%4==0)
        {
            OnBeat(BeatType.Blanca);
        }
        if (counter%8 == 0)
        {
            OnBeat(BeatType.Redonda);
        }
    }
    void PostBeat()
    {
        Debug.Log("Post -Beat");
        onMargen = false;
        OnPostBeat(BeatType.Corchea);
        if (counter%2==0)
        {
            OnPostBeat(BeatType.Negra);
        }
        if (counter%4==0)
        {
            OnPostBeat(BeatType.Blanca);
        }
        if (counter%8 == 0)
        {
            OnPostBeat(BeatType.Redonda);
        }
    }

    public void PlayBeat()
    {
        isPlaying = true;
    }
}
