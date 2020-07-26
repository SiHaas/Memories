using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
    public static Action<Track> InstantiationEvent;

    [SerializeField]
    private GameObject trackCrossingPrefab;

    [SerializeField]
    private List<GameObject> trackPrefabs;
    private int currentPrefabIdx = 0;

    // this is also the start track
    [SerializeField]
    private Track currentTrack;

    private List<Track> tracks;

    private Vector3 crossPoint = new Vector3(0f, 0f, 0f); //(-25f, 0f, 0f)


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
        //Debug.Log("Triggered");
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

    private List<Track> InstantiateNextTracks(Track track)
    {
        List<Track> tracks = new List<Track>();
        if (track.Right != null)
        {
            tracks.Add(AddTrack(GetTrackPrefab(), track, track.Right, track.yAngle)); //+ 45f
        }
        if (track.Left != null)
        {
            tracks.Add(AddTrack(GetTrackPrefab(), track, track.Left, track.yAngle)); // - 45f
        }
        tracks.Add(AddTrack(GetTrackPrefab(), track, track.Forward, track.yAngle)); //, 12.5f
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

    private GameObject GetTrackPrefab()
    {
        // ToDo get track prefabs 





        return trackCrossingPrefab;
    }

}
