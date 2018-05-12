public interface IInputManager {
    bool isEnabled { get; set; }
    float GetAxis(int playerId, InputAction action);
    bool GetButton(int playerId, InputAction action);
    bool GetButtonDown(int playerId, InputAction action);
    bool GetButtonUp(int playerId, InputAction action);
};
