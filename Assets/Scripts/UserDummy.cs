using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SelectedDirection
{
    Forward,
    Left,
    Right
}

public class UserDummy : MonoBehaviour
{
    [Range(0f, 1.5f)]
    public float Speed = 4f;
    private float temp;
    private Coroutine rotationCoroutine;


    public bool RandomDirection = false;

    [SerializeField]
    private Button leftButton;
    [SerializeField]
    private Button forwardButton;
    [SerializeField]
    private Button righttButton;

    public static int topic = 0; //0 vacation, 1 elementary
    public static int number = 0; //0 started, 1 continued
    public static int subbiomeVacation = 0; //0 VB, 1 VF, 2 VC, 3 VS
    public static int subbiomeElementary = 0; //0 EN, 1 EC, 2 EP
    public static int generalChecker = 1; //0 not general, 1 general 
    public static int mainTrack = 0; //current track as numbers 0, 1, 2, 3


    private float tempSpeed;
    public void StopMovement()
    {
        Speed = 0f;
    }

    public void StartMovement()
    {
        Speed = tempSpeed;
    }

    private void Start()
    {
        leftButton.onClick.AddListener(() => OnButtonPressed(SelectedDirection.Left));
        righttButton.onClick.AddListener(() => OnButtonPressed(SelectedDirection.Right));
        forwardButton.onClick.AddListener(() => OnButtonPressed(SelectedDirection.Forward));

        tempSpeed = Speed;
    }

    private void OnDestroy()
    {
        if (rotationCoroutine != null)
        {
            StopCoroutine(rotationCoroutine);
            rotationCoroutine = null;
        }
    }

    void FixedUpdate()
    {
        transform.position = transform.position + direction * Speed;
    }

    [SerializeField]
    private Vector3 direction = new Vector3(-1f, 0f, 0f);



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
            Debug.Log("righttest");
            changeRightTrack();
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
            changeLeftTrack();

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
            changeForwardTrack();
        }

        // invoke selected direction at trackManager
        TrackManager.OnDirectionSelectedEvent.Invoke(selectedDirection);

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
        if (delta > 0)
        {
            while (count < delta)
            {
                transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y + stepSize, 0f);
                count += stepSize;
                yield return new WaitForEndOfFrame();
            }
        }
        else
        {
            while (count > delta)
            {
                transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y - stepSize, 0f);
                count -= stepSize;
                yield return new WaitForEndOfFrame();
            }
        }
    }
    void changeRightTrack()
    {
        if (topic == 0)
        {
            topic = 1;
            generalChecker = 1;
            number = 0;
            mainTrack = 0;
            //subbiomeElementary = 0;
            if (subbiomeVacation < 3)
            {
                subbiomeVacation = subbiomeVacation + 1;
            }
            else
            {
                subbiomeVacation = 0;
            }

            if (subbiomeElementary < 2)
            {
                subbiomeElementary = subbiomeElementary + 1;
            }
            else
            {
                subbiomeElementary = 0;
            }



        }
        else
        {
            topic = 0;
            generalChecker = 1;
            mainTrack = 0;
            number = 0;
            //subbiomeVacation = 0;
            if (subbiomeElementary < 2)
            {
                subbiomeElementary = subbiomeElementary + 1;
            }
            else
            {
                subbiomeElementary = 0;
            }

            if (subbiomeVacation < 3)
            {
                subbiomeVacation = subbiomeVacation + 1;
            }
            else
            {
                subbiomeVacation = 0;
            }
        }
    }

    void changeLeftTrack()
    {
        Debug.Log("changed Left");
        generalChecker = 0;
        if (topic == 0)
        {
            number = 0;
            if (subbiomeVacation < 3)
            {
                mainTrack = subbiomeVacation;
                subbiomeVacation = subbiomeVacation + 1;

            }
            else
            {
                mainTrack = subbiomeVacation;
                subbiomeVacation = 0;

            }
        }
        else
        {
            number = 0;
            if (subbiomeElementary < 2)
            {
                mainTrack = subbiomeElementary;
                subbiomeElementary = subbiomeElementary + 1;

            }
            else
            {
                mainTrack = subbiomeElementary;
                subbiomeElementary = 0;

            }
        }
    }

    void changeForwardTrack()
    {
        if (number == 0)
        {
            number = 1;
        }
        else
        {
            number = 0;
        }
    }
}
