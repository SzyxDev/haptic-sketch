namespace Logitech.XRToolkit.Interactions
{
    using Logitech.XRToolkit.Actions;
    using Logitech.XRToolkit.Providers;
    using Logitech.XRToolkit.Triggers;
    using Logitech.XRToolkit.Utils;
    using UnityEngine;

    /// <summary>
    /// Draws lines in 3D space with a controller and an analog input.
    /// </summary>
    public class SurfaceDrawing : MonoBehaviour
    {
        [SerializeField]
        private InputTrigger _drawingTrigger;
        [SerializeField]
        private AirDrawingAction _airDrawingAction;
        private bool _isSmoothActive;

        // Prevent drawing when aiming at UI.
        [SerializeField]
        private RaycastTrigger _preventAirDrawingOnRaycast;
        [SerializeField]
        private CollisionTrigger _preventAirDrawingOnCollision;

        [HideInInspector]
        public bool CanDraw;
        private EButtonEvent _buttonState = EButtonEvent.OnButtonUp;

        private void Update()
        {
            if (_drawingTrigger.IsValid() && _buttonState == EButtonEvent.OnButtonUp)
            {
                Debug.Log("1");
                _buttonState = EButtonEvent.OnButtonDown;
            }
            else if (_drawingTrigger.IsValid())
            {
                Debug.Log("2");
                _buttonState = EButtonEvent.OnButton;
            }

            if (CanDraw == false && _buttonState == EButtonEvent.OnButtonDown && !_preventAirDrawingOnRaycast.IsValid())
            {
                Debug.Log("3");
                CanDraw = true;
            }

            if (!_drawingTrigger.IsValid())
            {
                Debug.Log("4");
                CanDraw = false;
                _buttonState = EButtonEvent.OnButtonUp;
            }

            _airDrawingAction.Update(CanDraw && !_preventAirDrawingOnCollision.IsValid());

            Debug.Log("Can Draw Surface: " + CanDraw);
        }

        public void SetSmoothingActive()
        {
            _isSmoothActive = !_isSmoothActive;
            _airDrawingAction.SetSmoothingMode(_isSmoothActive);
        }

        private void OnDisable()
        {
            CanDraw = false;
        }
    }
}
