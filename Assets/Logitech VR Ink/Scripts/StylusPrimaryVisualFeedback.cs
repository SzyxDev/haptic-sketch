/* Copyright (c) Logitech Corporation. All rights reserved. Licensed under the MIT License.*/

namespace Logitech.Scripts
{
    using Logitech.XRToolkit.Actions;
    using UnityEngine;
    using Valve.VR;

    /// <summary>
    /// Updates a MeshRenderer that has a circle shader material to indicate pressure on the primary button of a Stylus.
    /// </summary>
    public class StylusPrimaryVisualFeedback : MonoBehaviour
    {
        [Header("Input")]
        public bool GetInputSourceFromStylusDetection = true;
        [Tooltip("If UseStylusDetection is false, set the SteamVR input source manually")]
        public SteamVR_Input_Sources ManualSteamVRInputSource;
        [SerializeField]
        private SteamVR_Action_Single _input;

        [SerializeField]
        private MeshRenderer _circleShaderMaterialMeshRenderer;
        [SerializeField]
        private AnimationCurve _mappedCurve;

        private AirDrawingAction _airDrawingAction;

        private void Start() {
            //_airDrawingAction = new AirDrawingAction();
        }

        private void Update()
        {
            SteamVR_Input_Sources inputSource = GetInputSourceFromStylusDetection
                ? PrimaryDeviceDetection.PrimaryDeviceBehaviourPose.inputSource
                : ManualSteamVRInputSource;

            float mappedPressureValue = _input.GetAxis(inputSource);
            if (mappedPressureValue > 0)
            {
                mappedPressureValue = _mappedCurve.Evaluate(mappedPressureValue);
                Debug.Log("Pressed");
                //_airDrawingAction.Update(true);
            }
            _circleShaderMaterialMeshRenderer.sharedMaterial.SetFloat("_BackgroundCutoff", 1 - mappedPressureValue);
        }
    }
}
