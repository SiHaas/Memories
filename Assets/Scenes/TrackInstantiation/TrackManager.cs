using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
    public static Action<Track> InstantiationEvent;
    private GameObject nextTrackRight;
    private GameObject nextTrackLeft;
    private GameObject nextTrackForward;

    [SerializeField]
    private GameObject VG0;
    [SerializeField]
    private GameObject VG1;
    [SerializeField]
    private GameObject VB0;
    [SerializeField]
    private GameObject VB1;
    [SerializeField]
    private GameObject VF0;
    [SerializeField]
    private GameObject VF1;
    [SerializeField]
    private GameObject VC0;
    [SerializeField]
    private GameObject VC1;
    [SerializeField]
    private GameObject VS0;
    [SerializeField]
    private GameObject VS1;
    [SerializeField]
    private GameObject EG0;
    [SerializeField]
    private GameObject EG1;
    [SerializeField]
    private GameObject EN0;
    [SerializeField]
    private GameObject EN1;
    [SerializeField]
    private GameObject EC0;
    [SerializeField]
    private GameObject EC1;
    [SerializeField]
    private GameObject EP0;
    [SerializeField]
    private GameObject EP1;


    [SerializeField]
    private List<GameObject> trackPrefabs;
    private int currentPrefabIdx = 0;

    // this is also the start track
    [SerializeField]
    private Track currentTrack;

    private List<Track> tracks;

    private Vector3 crossPoint = new Vector3(-25f, 0f, 0f);


    private void Awake()
    {
        tracks = new List<Track>();
        if (currentTrack != null)
        {
            tracks.Add(currentTrack);
        }
    }

    void Start()
    {
        InstantiationEvent += OnInstantiationTriggerEntered;
    }

    private void OnInstantiationTriggerEntered(Track enteredTrack)
    {
        // generate new tracks
        List<Track> newTracks = InstantiateNextTracks(enteredTrack);

        // update current track
        currentTrack = enteredTrack;

        // cleanup old tracks
        for (int i = 0; i < tracks.Count; i++)
        {
            if (tracks[i] != currentTrack)
            {
                Destroy(tracks[i].gameObject);
                tracks.RemoveAt(i);
                i--;
            }
        }

        // add new tracks
        tracks.AddRange(newTracks);
    }

    private List<Track> InstantiateNextTracks(Track track) //hier könnte ich pro Richtung unterschiedliche Funktionen verwenden)
    {
        List<Track> tracks = new List<Track>();
        if (track.Right != null) //rechts
        {
            tracks.Add(AddTrack(GetTrackPrefabRight(), track, track.Right, track.yAngle + 45f));
        }
        if (track.Left != null) //links
        {
            tracks.Add(AddTrack(GetTrackPrefabLeft(), track, track.Left, track.yAngle - 45f));
        }
        tracks.Add(AddTrack(GetTrackPrefabForward(), track, track.Forward, track.yAngle, 12.5f)); //geradeaus
        return tracks;
    }

    private Track AddTrack(GameObject objectToInstantiate, Track trackReference, Transform trackTarget, float yAngle, float junctionLength = 15f)
    {
        // get point reference of the trackReference to place the new track
        Vector3 trackCrossPoint = trackReference.transform.position + Quaternion.Euler(0f, trackReference.yAngle, 0f) * crossPoint;

        float trackHalfLength = 35f;

        Vector3 directionNormalized = Vector3.Normalize(trackCrossPoint - trackTarget.position);
        Vector3 delta = directionNormalized * (trackHalfLength + junctionLength - 0.5f);

        GameObject track = Instantiate(objectToInstantiate, trackCrossPoint, Quaternion.Euler(0f, yAngle, 0f), transform);
        track.transform.position = new Vector3(trackCrossPoint.x - delta.x, trackCrossPoint.y, trackCrossPoint.z - delta.z);

        Track tr = track.GetComponent<Track>();
        tr.yAngle = yAngle; // we store the new angle to this track, because it's important to know the angle for further track generation

        return tr;
    }

    private GameObject GetTrackPrefabRight()
    {
        // ToDo get track prefabs 

        if (UserDummy.topic == 0)
        {
            nextTrackRight = EG0;
        }
        else
        {
            nextTrackRight = VG0;
        }

        return nextTrackRight;
    }
    private GameObject GetTrackPrefabLeft()
    {
        // ToDo get track prefabs 
        if (UserDummy.topic == 0)
        {
            switch (UserDummy.subbiomeVacation)
            {
                case 0:
                    nextTrackLeft = VB0;
                    break;
                case 1:
                    nextTrackLeft = VF0;
                    break;
                case 2:
                    nextTrackLeft = VC0;
                    break;
                case 3:
                    nextTrackLeft = VS0;
                    break;
            }
        }
        else
        {
            switch (UserDummy.subbiomeElementary)
            {
                case 0:
                    nextTrackLeft = EN0;
                    break;
                case 1:
                    nextTrackLeft = EC0;
                    break;
                case 2:
                    nextTrackLeft = EP0;
                    break;
            }
        }

        return nextTrackLeft;


    }

    private GameObject GetTrackPrefabForward()
    {
        // ToDo get track prefabs 
        if (UserDummy.topic == 0)
        {
            if (UserDummy.number == 0)
            {
                switch (UserDummy.mainTrack)
                {
                    case 0:
                        nextTrackForward = VB1;
                        SceneLogic.VB0.Invoke();
                        break;
                    case 1:
                        nextTrackForward = VF1;
                        SceneLogic.VF0.Invoke();
                        break;
                    case 2:
                        nextTrackForward = VC1;
                        SceneLogic.VC0.Invoke();
                        break;
                    case 3:
                        nextTrackForward = VS1;
                        SceneLogic.VS0.Invoke();
                        break;
                }
                if (UserDummy.generalChecker == 1)
                {
                    nextTrackForward = VG1;
                    SceneLogic.VG0.Invoke();
                }
            }
            else
            {
                switch (UserDummy.mainTrack)
                {
                    case 0:
                        nextTrackForward = VB0;
                        SceneLogic.VB1.Invoke();
                        break;
                    case 1:
                        nextTrackForward = VF0;
                        SceneLogic.VF1.Invoke();
                        break;
                    case 2:
                        nextTrackForward = VC0;
                        SceneLogic.VC1.Invoke();
                        break;
                    case 3:
                        nextTrackForward = VS0;
                        SceneLogic.VS1.Invoke();
                        break;
                }
                if (UserDummy.generalChecker == 1)
                {
                    nextTrackForward = VG0;
                    SceneLogic.VG1.Invoke();
                }
            }
        }
        else
        {
            if (UserDummy.number == 0)
            {
                switch (UserDummy.mainTrack)
                {
                    case 0:
                        nextTrackForward = EN1;
                        SceneLogic.EN0.Invoke();
                        break;
                    case 1:
                        nextTrackForward = EC1;
                        SceneLogic.EC0.Invoke();
                        break;
                    case 2:
                        nextTrackForward = EP1;
                        SceneLogic.EP0.Invoke();
                        break;
                }
                if (UserDummy.generalChecker == 1)
                {
                    nextTrackForward = EG1;
                    SceneLogic.EG0.Invoke();
                }
            }
            else
            {
                switch (UserDummy.mainTrack)
                {
                    case 0:
                        nextTrackForward = EN0;
                        SceneLogic.EN1.Invoke();
                        break;
                    case 1:
                        nextTrackForward = EC0;
                        SceneLogic.EC1.Invoke();
                        break;
                    case 2:
                        nextTrackForward = EP0;
                        SceneLogic.EP1.Invoke();
                        break;
                }
                if (UserDummy.generalChecker == 1)
                {
                    nextTrackForward = EG0;
                    SceneLogic.EG1.Invoke();
                }
            }

        }




            return nextTrackForward;
    }

}


//public int topic = 0; //0 vacation, 1 elementary
//public int number = 0; //0 started, 1 continued
//public int subbiomeVacation = 0; //0 VB, 1 VF, 2 VC, 3 VS
//public int subbiomeElementary = 0; //0 EN, 1 EC, 2 EP
//public static int generalChecker = 1; //0 not general, 1 general