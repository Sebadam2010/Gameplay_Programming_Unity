using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineScript : MonoBehaviour
{
    private PlayableDirector director;
    public GameObject controlPanel;

    // Start is called before the first frame update
    void Awake()
    {
        director = GetComponent<PlayableDirector>();
        director.played += Director_Played;
        director.stopped += Director_Stopped;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Director_Played(PlayableDirector obj)
    {
        controlPanel.SetActive(false);
    }

    private void Director_Stopped(PlayableDirector obj)
    {
        controlPanel.SetActive(true);
    }

    public void StartTimeline()
    {
        director.Play();
    }
}
