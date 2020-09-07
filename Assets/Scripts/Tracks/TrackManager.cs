using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrackManager : MonoBehaviour
{
    public static Action<Track> InstantiationEvent;
    public static Action<SelectedDirection> OnDirectionSelectedEvent;
    private GameObject nextTrackRightPrefab;
    private GameObject nextTrackLeftPrefab;
    private GameObject nextTrackForwardPrefab;

    private GameObject nextTrackRight;
    private GameObject nextTrackLeft;
    private GameObject nextTrackForward;

    [SerializeField]
    private float activationDelay = 4f;

    [SerializeField]
    private GameObject OriginalTrack;

    [SerializeField]
    private TextMeshProUGUI leftButton;
    [SerializeField]
    private TextMeshProUGUI middleButton;
    [SerializeField]
    private TextMeshProUGUI rightButton;

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

    private void Start()
    {
        InstantiationEvent += OnInstantiationTriggerEntered;
        OnDirectionSelectedEvent += OnDirectionSelectedActiveTrack;
    }

    private void OnDestroy()
    {
        InstantiationEvent -= OnInstantiationTriggerEntered;
        OnDirectionSelectedEvent -= OnDirectionSelectedActiveTrack;
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
        Track newTrack = null;
        if (track.Right != null) //rechts
        {
            newTrack = AddTrack(GetTrackPrefabRight(), track, track.Right, track.yAngle + 45f);
            nextTrackRight = newTrack.gameObject;
            tracks.Add(newTrack);
        }
        if (track.Left != null) //links
        {
            newTrack = AddTrack(GetTrackPrefabLeft(), track, track.Left, track.yAngle - 45f);
            nextTrackLeft = newTrack.gameObject;
            tracks.Add(newTrack);
        }
        newTrack = AddTrack(GetTrackPrefabForward(), track, track.Forward, track.yAngle, 12.5f);
        nextTrackForward = newTrack.gameObject;
        tracks.Add(newTrack); //geradeaus
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
        track.SetActive(false);

        return tr;
    }

    private GameObject GetTrackPrefabRight()
    {
        // ToDo get track prefabs 

        if (UserDummy.topic == 0)
        {
            nextTrackRightPrefab = EG0;
            rightButton.text = "Go to the Elementary School Area";
        }
        else
        {
            nextTrackRightPrefab = VG0;
            rightButton.text = "Go to the Vacation Area";
        }

        return nextTrackRightPrefab;
    }

    private GameObject GetTrackPrefabLeft()
    {
        // ToDo get track prefabs 
        if (UserDummy.topic == 0)
        {
            switch (UserDummy.subbiomeVacation)
            {
                case 0:
                    nextTrackLeftPrefab = VB0;
                    leftButton.text = "Go to the Beach";
                    break;
                case 1:
                    nextTrackLeftPrefab = VF0;
                    leftButton.text = "Go to the Forest";
                    break;
                case 2:
                    nextTrackLeftPrefab = VC0;
                    leftButton.text = "Go to the City";
                    break;
                case 3:
                    nextTrackLeftPrefab = VS0;
                    leftButton.text = "Go to the snowy Moutain";
                    break;
            }
        }
        else
        {
            switch (UserDummy.subbiomeElementary)
            {
                case 0:
                    nextTrackLeftPrefab = EN0;
                    leftButton.text = "Go to the Neighborhood";
                    break;
                case 1:
                    nextTrackLeftPrefab = EC0;
                    leftButton.text = "Go to the Classroom";
                    break;
                case 2:
                    nextTrackLeftPrefab = EP0;
                    leftButton.text = "Go to the Playground";
                    break;
            }
        }

        return nextTrackLeftPrefab;


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
                        nextTrackForwardPrefab = VB1;
                        middleButton.text = "Continue on the Beach";
                        SceneLogic.VB0.Invoke();
                        break;
                    case 1:
                        nextTrackForwardPrefab = VF1;
                        middleButton.text = "Continue in the Forest";
                        SceneLogic.VF0.Invoke();
                        break;
                    case 2:
                        nextTrackForwardPrefab = VC1;
                        middleButton.text = "Continue in the City";
                        SceneLogic.VC0.Invoke();
                        break;
                    case 3:
                        nextTrackForwardPrefab = VS1;
                        middleButton.text = "Continue on the snowy Mountain";
                        SceneLogic.VS0.Invoke();
                        break;
                }
                if (UserDummy.generalChecker == 1)
                {
                    nextTrackForwardPrefab = VG1;
                    middleButton.text = "Continue in the Vacation Area";
                    SceneLogic.VG0.Invoke();
                }
            }
            else
            {
                switch (UserDummy.mainTrack)
                {
                    case 0:
                        nextTrackForwardPrefab = VB0;
                        middleButton.text = "Continue on the Beach";
                        SceneLogic.VB1.Invoke();
                        break;
                    case 1:
                        nextTrackForwardPrefab = VF0;
                        middleButton.text = "Continue in the Forest";
                        SceneLogic.VF1.Invoke();
                        break;
                    case 2:
                        nextTrackForwardPrefab = VC0;
                        middleButton.text = "Continue in the City";
                        SceneLogic.VC1.Invoke();
                        break;
                    case 3:
                        nextTrackForwardPrefab = VS0;
                        middleButton.text = "Continue on the snowy Moutain";
                        SceneLogic.VS1.Invoke();
                        break;
                }
                if (UserDummy.generalChecker == 1)
                {
                    nextTrackForwardPrefab = VG0;
                    middleButton.text = "Continue in Vacation Area";
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
                        nextTrackForwardPrefab = EN1;
                        middleButton.text = "Continue in the Neighborhood";
                        SceneLogic.EN0.Invoke();
                        break;
                    case 1:
                        nextTrackForwardPrefab = EC1;
                        middleButton.text = "Continue in the Classroom";
                        SceneLogic.EC0.Invoke();
                        break;
                    case 2:
                        nextTrackForwardPrefab = EP1;
                        middleButton.text = "Continue in the Playground";
                        SceneLogic.EP0.Invoke();
                        break;
                }
                if (UserDummy.generalChecker == 1)
                {
                    nextTrackForwardPrefab = EG1;
                    middleButton.text = "Continue in the Elementary School Area";
                    SceneLogic.EG0.Invoke();
                }
            }
            else
            {
                switch (UserDummy.mainTrack)
                {
                    case 0:
                        nextTrackForwardPrefab = EN0;
                        middleButton.text = "Continue in the Neighborhood";
                        SceneLogic.EN1.Invoke();
                        break;
                    case 1:
                        nextTrackForwardPrefab = EC0;
                        middleButton.text = "Continue in the Classroom";
                        SceneLogic.EC1.Invoke();
                        break;
                    case 2:
                        nextTrackForwardPrefab = EP0;
                        middleButton.text = "Continue in the Playground";
                        SceneLogic.EP1.Invoke();
                        break;
                }
                if (UserDummy.generalChecker == 1)
                {
                    nextTrackForwardPrefab = EG0;
                    middleButton.text = "Continue in the Elementary School Area";
                    SceneLogic.EG1.Invoke();
                }
            }

        }
        return nextTrackForwardPrefab;
    }

    private void OnDirectionSelectedActiveTrack(SelectedDirection direction)
    {
        if (OriginalTrack != null)
        {
            StartCoroutine(DestroyOriginal(OriginalTrack, 10f));
            Debug.Log("Started Destroy Timer");
        }
        switch (direction)
        {
            case SelectedDirection.Forward:
                StartCoroutine(ActivateTrack(nextTrackForward, activationDelay));
                break;
            case SelectedDirection.Right:
                StartCoroutine(ActivateTrack(nextTrackRight, activationDelay));
                break;
            case SelectedDirection.Left:
                StartCoroutine(ActivateTrack(nextTrackLeft, activationDelay));
                break;
        }
    }

    /// <summary>
    /// activates the track of the selected direction after a specified delay
    /// </summary>
    /// <param name="trackObject">the track' gameobject to activate</param>
    /// <param name="delay">the amount of time to wait until activation</param>
    /// <returns></returns>
    private IEnumerator ActivateTrack(GameObject trackObject, float delay)
    {
        yield return new WaitForSeconds(delay);
        trackObject.SetActive(true);
    }
    private IEnumerator DestroyOriginal(GameObject startingTrack, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(startingTrack);
        Debug.Log("Destroyed");
    }
}


//public int topic = 0; //0 vacation, 1 elementary
//public int number = 0; //0 started, 1 continued
//public int subbiomeVacation = 0; //0 VB, 1 VF, 2 VC, 3 VS
//public int subbiomeElementary = 0; //0 EN, 1 EC, 2 EP
//public static int generalChecker = 1; //0 not general, 1 general