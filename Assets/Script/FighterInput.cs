using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterInput : MonoBehaviour
{
    private Fighter fighter;

    InputSystem input;

    void Awake() {
        fighter = GetComponent<Fighter>();
        input = new InputSystem();
    }

    // Update is called once per frame
    void Update()
    {
        if (!input.InputFound) {
            input.WaitSignal();
        } else {
            if (input.Left()) { fighter.WalkLeft(); }
            else if (input.Right()) { fighter.WalkRight(); }
            else { fighter.StopWalk(); }

            if (input.LeftPunch()) { fighter.PunchLeft(); }
            if (input.RightPunch()) { fighter.PunchRight(); }
            if (input.MetaPunch()) { fighter.MetaPunch(); }
        }
    }

    public void TestKey() {
        if (Input.GetKey(KeyCode.JoystickButton0)) { Debug.Log(0); }
        if (Input.GetKey(KeyCode.JoystickButton1)) { Debug.Log(1); }
        if (Input.GetKey(KeyCode.JoystickButton2)) { Debug.Log(2); }
        if (Input.GetKey(KeyCode.JoystickButton3)) { Debug.Log(3); }
        if (Input.GetKey(KeyCode.JoystickButton4)) { Debug.Log(4); }
        if (Input.GetKey(KeyCode.JoystickButton5)) { Debug.Log(5); }
        if (Input.GetKey(KeyCode.JoystickButton6)) { Debug.Log(6); }
        if (Input.GetKey(KeyCode.JoystickButton7)) { Debug.Log(7); }
        if (Input.GetKey(KeyCode.JoystickButton8)) { Debug.Log(8); }
        if (Input.GetKey(KeyCode.JoystickButton9)) { Debug.Log(9); }
        if (Input.GetKey(KeyCode.JoystickButton10)) { Debug.Log(10); }
        if (Input.GetKey(KeyCode.JoystickButton11)) { Debug.Log(11); }
        if (Input.GetKey(KeyCode.JoystickButton12)) { Debug.Log(12); }
        if (Input.GetKey(KeyCode.JoystickButton13)) { Debug.Log(13); }
        if (Input.GetKey(KeyCode.JoystickButton14)) { Debug.Log(14); }
        if (Input.GetKey(KeyCode.JoystickButton15)) { Debug.Log(15); }
        if (Input.GetKey(KeyCode.JoystickButton16)) { Debug.Log(16); }
        if (Input.GetKey(KeyCode.JoystickButton17)) { Debug.Log(17); }
        if (Input.GetKey(KeyCode.JoystickButton18)) { Debug.Log(18); }
        if (Input.GetKey(KeyCode.JoystickButton19)) { Debug.Log(19); }
    }

    private class InputSystem {
        static List<InputType> takenInput;
        KeyCodeSet keySet;
        private InputType inputType = InputType.None;
        private enum InputType { None, Keyboard1, Keyboard2, JoyStick }

        public bool InputFound { get {return inputType != InputType.None; } }

        public InputSystem() {
            if (takenInput == null) { takenInput = new List<InputType>(); }
        }

        public void WaitSignal() {
            if (Keyboard1KeyCodeSet.AnyKey) {
                if (!takenInput.Contains(InputType.Keyboard1)) {
                    takenInput.Add(InputType.Keyboard1);
                    inputType = InputType.Keyboard1;

                    keySet = new Keyboard1KeyCodeSet();
                    return;
                }
            } else if (Keyboard2KeyCodeSet.AnyKey) {
                if (!takenInput.Contains(InputType.Keyboard2)) {
                    takenInput.Add(InputType.Keyboard2);
                    inputType = InputType.Keyboard2;

                    keySet = new Keyboard2KeyCodeSet();
                    return;
                }
            } else if (JoyStickKeyCodeSet.AnyKey) {
                if (!takenInput.Contains(InputType.JoyStick)) {
                    takenInput.Add(InputType.JoyStick);
                    inputType = InputType.JoyStick;

                    keySet = new JoyStickKeyCodeSet();
                    return;
                }
            }
        }

        // public 
        public bool Left() {
            return Input.GetKey(keySet.Left);
        }
        public bool Right() {
            return Input.GetKey(keySet.Right);
        }
        public bool LeftPunch() {
            return Input.GetKey(keySet.LeftPunch);
        }
        public bool RightPunch() {
            return Input.GetKey(keySet.RightPunch);
        }
        public bool MetaPunch() {
            return Input.GetKey(keySet.MetaPunch);
        }
    }

    interface KeyCodeSet {
        KeyCode Left { get; }
        KeyCode Right { get; }
        KeyCode LeftPunch { get; }
        KeyCode RightPunch { get; }
        KeyCode MetaPunch { get; }
    }

    private class Keyboard1KeyCodeSet: KeyCodeSet {
        static KeyCode left = KeyCode.A;
        static KeyCode right = KeyCode.D;
        static KeyCode leftPunch = KeyCode.C;
        static KeyCode rightPunch = KeyCode.V;
        static KeyCode metaPunch = KeyCode.F;

        static public bool AnyKey { get { return Input.GetKey(left) || Input.GetKey(right) || Input.GetKey(leftPunch) || Input.GetKey(rightPunch) || Input.GetKey(metaPunch); } }

        public KeyCode Left { get { return left; } }
        public KeyCode Right { get { return right; } }
        public KeyCode LeftPunch { get { return leftPunch; } }
        public KeyCode RightPunch { get { return rightPunch; } }
        public KeyCode MetaPunch { get { return metaPunch; } }
    }

    private class Keyboard2KeyCodeSet: KeyCodeSet {
        static KeyCode left = KeyCode.LeftArrow;
        static KeyCode right = KeyCode.RightArrow;
        static KeyCode leftPunch = KeyCode.K;
        static KeyCode rightPunch = KeyCode.L;
        static KeyCode metaPunch = KeyCode.O;

        static public bool AnyKey { get { return Input.GetKey(left) || Input.GetKey(right) || Input.GetKey(leftPunch) || Input.GetKey(rightPunch) || Input.GetKey(metaPunch); } }

        public KeyCode Left { get { return left; } }
        public KeyCode Right { get { return right; } }
        public KeyCode LeftPunch { get { return leftPunch; } }
        public KeyCode RightPunch { get { return rightPunch; } }
        public KeyCode MetaPunch { get { return metaPunch; } }
    }

    private class JoyStickKeyCodeSet: KeyCodeSet {
        static KeyCode left = KeyCode.JoystickButton7;
        static KeyCode right = KeyCode.JoystickButton5;
        static KeyCode leftPunch = KeyCode.JoystickButton15;
        static KeyCode rightPunch = KeyCode.JoystickButton13;
        static KeyCode metaPunch = KeyCode.JoystickButton12;

        static public bool AnyKey { get { return Input.GetKey(left) || Input.GetKey(right) || Input.GetKey(leftPunch) || Input.GetKey(rightPunch) || Input.GetKey(metaPunch); } }

        public KeyCode Left { get { return left; } }
        public KeyCode Right { get { return right; } }
        public KeyCode LeftPunch { get { return leftPunch; } }
        public KeyCode RightPunch { get { return rightPunch; } }
        public KeyCode MetaPunch { get { return metaPunch; } }
    }
}
