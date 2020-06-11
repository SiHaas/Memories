/*
 * @author Valentin Simonov / http://va.lent.in/
 */

using UnityEngine;
using TouchScript.Gestures.TransformGestures;

namespace TouchScript.Examples.CameraControl
{
    /// <exclude />
    public class CameraController : MonoBehaviour
    {
        public ScreenTransformGesture TwoFingerMoveGesture;
        public ScreenTransformGesture ManipulationGesture;
        public float PanSpeed = 200f;
        public float RotationSpeed = 1f;
        public float ZoomSpeed = 10f;
        public float actualYRotation = 0f;

        private Transform pivot;
        private Transform cam;

        private void Awake()
        {
            pivot = transform.Find("Pivot");
            cam = transform.Find("Pivot/Camera");
        }

        private void OnEnable()
        {
            TwoFingerMoveGesture.Transformed += twoFingerTransformHandler;
            ManipulationGesture.Transformed += manipulationTransformedHandler;
        }

        private void OnDisable()
        {
            TwoFingerMoveGesture.Transformed -= twoFingerTransformHandler;
            ManipulationGesture.Transformed -= manipulationTransformedHandler;

        }

        private void manipulationTransformedHandler(object sender, System.EventArgs e)
        {
            var rotation = Quaternion.Euler(ManipulationGesture.DeltaPosition.y/Screen.height*RotationSpeed, -ManipulationGesture.DeltaPosition.x/Screen.width*RotationSpeed,  ManipulationGesture.DeltaRotation);
            //pivot.localRotation *= rotation;
            //cam.transform.localPosition += Vector3.forward*(ManipulationGesture.DeltaScale - 1f)*ZoomSpeed;

           // Debug.Log(ManipulationGesture.DeltaPosition.x/Screen.width);

            //float roationScale = 200f;
            float nextYRotation = cam.transform.localRotation.eulerAngles.y + ManipulationGesture.DeltaPosition.x / Screen.width * RotationSpeed;
            Debug.Log(nextYRotation);
            rotationCalculation(nextYRotation);
            
           
        }
        private void rotationCalculation(float rotationValue)
        {
            int lastMovement = 0; 
            //Debug.Log(rotationValue);
            //actualYRotation = actualYRotation + rotationValue;
            //Debug.Log(actualYRotation);
            if (rotationValue < 390 && rotationValue > 300 )
            {
                lastMovement = 1;
                Debug.Log("left area");
                cam.transform.rotation = Quaternion.Euler(0f, rotationValue, 0f);
                
            } else if (rotationValue > -30 && rotationValue < 60)
            {
                lastMovement = 2;
                Debug.Log("right area");
                cam.transform.rotation = Quaternion.Euler(0f, rotationValue, 0f);
            } else
            {
                if (lastMovement == 1)
                {
                    if (rotationValue > 300)
                    {
                        //cam.transform.rotation = Quaternion.Euler(0f, rotationValue, 0f);
                    }
                    
                } else if (lastMovement == 2)
                    if (rotationValue < 60)
                    {
                        //cam.transform.rotation = Quaternion.Euler(0f, rotationValue, 0f);
                    }
            }
        }

        private void twoFingerTransformHandler(object sender, System.EventArgs e)
        {
            //pivot.localPosition += pivot.rotation*TwoFingerMoveGesture.DeltaPosition*PanSpeed;
        }
    }
}