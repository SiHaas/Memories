
using UnityEngine;
using TouchScript.Gestures.TransformGestures;

namespace TouchScript.Gestures.TransformGestures
{
    /// <exclude />
    public class CameraMovement : MonoBehaviour
    {
       // public ScreenTransformGesture TwoFingerMoveGesture;
        public ScreenTransformGesture ManipulationGesture;
        public float PanSpeed = 200f;
        public float RotationSpeed = 100f;
        public float ZoomSpeed = 10f;
        public float actualYRotation = 0f;

        private Transform playerCamera;

        private void Awake()
        {
            //playerCamera = transform.Find("Scene/Camera");
            playerCamera = transform;
        }

        private void OnEnable()
        {
            //TwoFingerMoveGesture.Transformed += twoFingerTransformHandler;
            ManipulationGesture.Transformed += manipulationTransformedHandler;
        }

        private void OnDisable()
        {
            //TwoFingerMoveGesture.Transformed -= twoFingerTransformHandler;
            ManipulationGesture.Transformed -= manipulationTransformedHandler;

        }

        private void manipulationTransformedHandler(object sender, System.EventArgs e)
        {
            var rotation = Quaternion.Euler(ManipulationGesture.DeltaPosition.y / Screen.height * RotationSpeed, -ManipulationGesture.DeltaPosition.x / Screen.width * RotationSpeed, ManipulationGesture.DeltaRotation);
            //pivot.localRotation *= rotation;
            //cam.transform.localPosition += Vector3.forward*(ManipulationGesture.DeltaScale - 1f)*ZoomSpeed;

            // Debug.Log(ManipulationGesture.DeltaPosition.x/Screen.width);

            //float roationScale = 200f;
            float nextYRotation = playerCamera.transform.localRotation.eulerAngles.y + ManipulationGesture.DeltaPosition.x / Screen.width * RotationSpeed;
            float nextXRotation = playerCamera.transform.localRotation.eulerAngles.x  - ManipulationGesture.DeltaPosition.y / Screen.height * RotationSpeed;
            Debug.Log(nextYRotation);
            rotationCalculation(nextXRotation, nextYRotation);


        }
        private void rotationCalculation(float rotationValueX, float rotationValueY)
        {
            //int lastMovement = 0;
            //Debug.Log(rotationValue);
            //actualYRotation = actualYRotation + rotationValue;
            //Debug.Log(actualYRotation);
            if (rotationValueY > 240)
            {
                //lastMovement = 1;
                //Debug.Log("left border");
                rotationValueY = 240;
                

            }
            else if (rotationValueY < 120)
            {
                rotationValueY = 120;
                //lastMovement = 2;
                //Debug.Log("right border");
          
            }


            if (rotationValueX < 320 && rotationValueX > 60)
            {
                rotationValueX = 320;
            } else if (rotationValueX > 40 && rotationValueX < 300) { 
                rotationValueX = 40;
            }
            Debug.Log(rotationValueX);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationValueX, rotationValueY, 0f);
            //else
            //{
            //if (lastMovement == 1)
            //{
            //if (rotationValue > 300)
            //{
            //cam.transform.rotation = Quaternion.Euler(0f, rotationValue, 0f);
            //}

            //}
            // else if (lastMovement == 2)
            //if (rotationValue < 60)
            //{
            //cam.transform.rotation = Quaternion.Euler(0f, rotationValue, 0f);
            //}
            //}
        }

        //private void twoFingerTransformHandler(object sender, System.EventArgs e)
        //{
            //pivot.localPosition += pivot.rotation*TwoFingerMoveGesture.DeltaPosition*PanSpeed;
        //}
    }
}