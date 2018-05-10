#pragma warning disable 0649

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Rewired.Integration.PlayerControls {

    [RequireComponent(typeof(Rewired.InputManager))]
    public sealed class PlayerControlsInputManager : global::InputManager {

        private const string className = "Rewired Player Controls Input Manager";

        [Header("Action Mappings")]

        [SerializeField]
        [Tooltip("The string name of the Rewired Action to use for the game Attack action.")]
        private string _attackAction = "Attack";

        [SerializeField]
        [Tooltip("The string name of the Rewired Action to use for the game Confirm action.")]
        private string _confirmAction = "Confirm";

        [SerializeField]
        [Tooltip("The string name of the Rewired Action to use for the game Cancel action.")]
        private string _cancelAction = "Cancel";

        [SerializeField]
        [Tooltip("The string name of the Rewired Action to use for the game Start action.")]
        private string _startAction = "Start";

        [SerializeField]
        [Tooltip("The string name of the Rewired Action to use for the game Select action.")]
        private string _selectAction = "Select";

        [SerializeField]
        [Tooltip("The string name of the Rewired Action to use for the game MoveX action.")]
        private string _moveXAction = "MoveX";

        [SerializeField]
        [Tooltip("The string name of the Rewired Action to use for the game MoveY action.")]
        private string _moveYAction = "MoveY";

        [SerializeField]
        [Tooltip("The string name of the Rewired Action to use for the game Up action.")]
        private string _upAction = "Up";

        [SerializeField]
        [Tooltip("The string name of the Rewired Action to use for the game Down action.")]
        private string _downAction = "Down";

        [SerializeField]
        [Tooltip("The string name of the Rewired Action to use for the game Left action.")]
        private string _leftAction = "Left";

        [SerializeField]
        [Tooltip("The string name of the Rewired Action to use for the game Right action.")]
        private string _rightAction = "Right";

        private Dictionary<int, int> _actionIds;
        private bool _initialized;

        protected override void Awake() {
            base.Awake();

            if(!ReInput.isReady) {
                Debug.LogError(className + ": Rewired is not initialized. You must have an active Rewired Input Manager in the scene.");
                return;
            }

            _initialized = true;

            // Cache Rewired Action ids for speed
            _actionIds = new Dictionary<int, int>();
            AddRewiredActionId(_actionIds, _attackAction, global::InputAction.Attack);
            AddRewiredActionId(_actionIds, _confirmAction, global::InputAction.Confirm);
            AddRewiredActionId(_actionIds, _cancelAction, global::InputAction.Cancel);
            AddRewiredActionId(_actionIds, _startAction, global::InputAction.Start);
            AddRewiredActionId(_actionIds, _selectAction, global::InputAction.Select);
            AddRewiredActionId(_actionIds, _moveXAction, global::InputAction.MoveX);
            AddRewiredActionId(_actionIds, _moveYAction, global::InputAction.MoveY);
            AddRewiredActionId(_actionIds, _upAction, global::InputAction.Up);
            AddRewiredActionId(_actionIds, _downAction, global::InputAction.Down);
            AddRewiredActionId(_actionIds, _leftAction, global::InputAction.Left);
            AddRewiredActionId(_actionIds, _rightAction, global::InputAction.Right);

            // Set the singleton instance
            SetInstance(this);
        }

        protected override void OnEnable() {
            base.OnEnable();
            ReInput.ControllerConnectedEvent += OnControllerConnected;
            ReInput.ControllerDisconnectedEvent += OnControllerDisconnected;
        }

        protected override void OnDisable() {
            base.OnDisable();
            ReInput.ControllerConnectedEvent -= OnControllerConnected;
            ReInput.ControllerDisconnectedEvent -= OnControllerDisconnected;
        }

        // Public methods

        public override bool GetButton(int playerId, global::InputAction action) {
            if(!_initialized || !isEnabled) return false;
            return ReInput.players.GetPlayer(playerId).GetButton(_actionIds[(int)action]);
        }

        public override bool GetButtonDown(int playerId, global::InputAction action) {
            if(!_initialized || !isEnabled) return false;
            return ReInput.players.GetPlayer(playerId).GetButtonDown(_actionIds[(int)action]);
        }

        public override bool GetButtonUp(int playerId, global::InputAction action) {
            if(!_initialized || !isEnabled) return false;
            return ReInput.players.GetPlayer(playerId).GetButtonUp(_actionIds[(int)action]);
        }

        public override float GetAxis(int playerId, global::InputAction action) {
            if(!_initialized || !isEnabled) return 0f;
            return ReInput.players.GetPlayer(playerId).GetAxis(_actionIds[(int)action]);
        }

        // Private methods
        private void OnControllerConnected(Rewired.ControllerStatusChangedEventArgs args) {
            if(args.controllerType != ControllerType.Joystick) return;
            //CheckHideTouchControlsWhenJoystickConnected();
        }

        private void OnControllerDisconnected(Rewired.ControllerStatusChangedEventArgs args) {
            if(args.controllerType != ControllerType.Joystick) return;
           //CheckHideTouchControlsWhenJoystickConnected();
        }

        private static void AddRewiredActionId(Dictionary<int, int> actionIds, string actionName, global::InputAction action) {
            int id = GetRewiredActionId(actionName);
            if(id < 0) return; // invalid Action id
            actionIds.Add((int)action, id);
        }

        private static int GetRewiredActionId(string actionName) {
            if(string.IsNullOrEmpty(actionName)) return -1;
            int id = ReInput.mapping.GetActionId(actionName);
            if(id < 0) Debug.LogWarning(className + ": No Rewired Action found for Action name \"" + actionName + "\". The Action name must match exactly to an Action defined in the Rewired Input Manager.");
            return id;
        }

        private enum PlatformFlags {
            None = 0,
            Editor = 1,
            Windows = 1 << 1,
            OSX = 1 << 2,
            Linux = 1 << 3,
            IOS = 1 << 4,
            TVOS = 1 << 5,
            Android = 1 << 6,
            Windows8Store = 1 << 7,
            WindowsUWP10 = 1 << 8,
            WebGL = 1 << 9,
            PS4 = 1 << 10,
            PSVita = 1 << 11,
            Xbox360 = 1 << 12,
            XboxOne = 1 << 13,
            SamsungTV = 1 << 14,
            WiiU = 1 << 15,
            Nintendo3DS = 1 << 16,
            Switch = 1 << 17,
            AmazonFireTV = 1 << 18,
            RazerForgeTV = 1 << 19,
            Unknown = 1 << 31
        }
    }
}
