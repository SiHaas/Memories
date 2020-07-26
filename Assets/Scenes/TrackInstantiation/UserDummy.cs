using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserDummy : MonoBehaviour
{
    [Range(0f, 1.5f)]
    public float Speed = 1f;
    private float temp;
    private Coroutine rotationCoroutine;


    public bool RandomDirection = false;

    [SerializeField]
    private Button leftButton;
    [SerializeField]
    private Button forwardButton;
    [SerializeField]
    private Button righttButton;

    private void Start()
    {
        leftButton.onClick.AddListener(() => OnButtonPressed(SelectedDirection.Left));
        righttButton.onClick.AddListener(() => OnButtonPressed(SelectedDirection.Right));
        forwardButton.onClick.AddListener(() => OnButtonPressed(SelectedDirection.Forward));
    }

    private void OnDestroy()
    {
        if(rotationCoroutine != null)
        {
            StopCoroutine(rotationCoroutine);
            rotationCoroutine = null;
        }
    }

    void FixedUpdate()
    {
        transform.position = transform.position + direction * Speed;
    }

    private Vector3 direction = new Vector3(0f, 0f, -1f);

    private enum SelectedDirection
    {
        Forward,
        Left, 
        Right       
    }

    private void OnButtonPressed(SelectedDirection selected)
    {
        selectedDirection = selected;
        leftButton.gameObject.SetActive(false);
        righttButton.gameObject.SetActive(false);
        forwardButton.gameObject.SetActive(false);

        TurnTowardsTrack();
    }

    private SelectedDirection selectedDirection;
    private Track currentTrack;
    private Vector3 referencePosition;
    private void OnTriggerEnter(Collider other)
    {
        Track track = other.GetComponentInParent<Track>();
        if (track != null)
        {
            if (other.name.Equals("InstantiationTrigger"))
            {
                TrackManager.InstantiationEvent.Invoke(track);
                return;
            }

            if (other.name.Equals("DirectionTrigger"))
            {
                selectedDirection = SelectedDirection.Forward;

                referencePosition = other.transform.position;
                currentTrack = track;

                if (RandomDirection)
                {
                    float val = Random.Range(0f, 1f);
                    
                    if (val > 0.66f) // right
                    {
                        selectedDirection = SelectedDirection.Right;
                    }
                    else if (val < 0.33f) // left
                    {
                        selectedDirection = SelectedDirection.Left;
                    }

                    TurnTowardsTrack();
                }
                else
                {
                    temp = Speed;
                    Speed = 0f;
                    leftButton.gameObject.SetActive(true);
                    righttButton.gameObject.SetActive(true);
                    forwardButton.gameObject.SetActive(true);
                }
                
                return;
            }        
        }
    }

    private void TurnTowardsTrack()
    {
        // turn towards
        Vector3 dir;
        if (selectedDirection == SelectedDirection.Right) // right
        {
            dir = GetDirection(referencePosition, currentTrack.Right.position);

            // adjust rotation
            if (rotationCoroutine != null)
            {
                StopCoroutine(rotationCoroutine);
            }
            rotationCoroutine = StartCoroutine(AdjustRotation(45f));
        }
        else if (selectedDirection == SelectedDirection.Left) // left
        {
            dir = GetDirection(referencePosition, currentTrack.Left.position);

            // adjust rotation
            if (rotationCoroutine != null)
            {
                StopCoroutine(rotationCoroutine);
            }
            rotationCoroutine = StartCoroutine(AdjustRotation(-45f));
        }
        else // forward
        {
            dir = GetDirection(referencePosition, currentTrack.Forward.position);
        }
        
        // adjust position
        transform.position = referencePosition;

        // adjust direction
        direction = dir;

        if (!RandomDirection)
            Speed = temp;
    }

    private Vector3 GetDirection(Vector3 colliderPos, Vector3 target)
    {
        Vector3 dir = target - colliderPos;
        dir.y = 0f;
        return Vector3.Normalize(dir);
    }

    private IEnumerator AdjustRotation(float delta)
    {
        float stepSize = 0.75f;
        float count = 0f; // hilfsvariable, da Unity intern die -45 gern in 315 etc übersetzt, machts das manchmal etwas nervig..
        if(delta > 0)
        {
            while(count < delta)
            {
                transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y + stepSize, 0f);
                count += stepSize;
                yield return new WaitForEndOfFrame();
            }
        }
        else
        {
            while(count > delta)
            {
                transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y - stepSize, 0f);
                count -= stepSize;
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
