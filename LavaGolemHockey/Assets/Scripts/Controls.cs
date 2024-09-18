//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.6.3
//     from Assets/Scripts/Controls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @Controls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""LeftControls"",
            ""id"": ""6ad9a99e-56fd-4f84-8933-3d4f73106287"",
            ""actions"": [
                {
                    ""name"": ""LSMove"",
                    ""type"": ""Value"",
                    ""id"": ""8657dbc8-bfc6-4472-bf97-6acca9a93002"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Join"",
                    ""type"": ""Button"",
                    ""id"": ""b1709bcd-1aa8-4d35-8897-c16e4e327146"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ShootTackle"",
                    ""type"": ""Button"",
                    ""id"": ""a76a9e39-3c3b-421c-b700-cbe3f52aaa64"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""LeftPass"",
                    ""type"": ""Button"",
                    ""id"": ""11607ce9-2d10-4b0f-a12f-d38b36b66231"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap"",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b71270aa-cf4f-46ac-8971-eb59532eca68"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LSMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""788c2c2f-522b-4596-8bb7-27055ae83dbc"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LSMove"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""15e9824c-eafc-4534-8faf-30b1fb465573"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LSMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""c3a7cec9-c979-4a96-90b4-38f0cb12baea"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LSMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""1093f2bc-57ee-42b3-b50b-39b355ddafa2"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LSMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""5157433c-62dd-439a-a16d-50884079a540"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LSMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""f20caea9-a9a6-4e55-a3f8-8d92d6ad89c1"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""955cace4-f80a-4e7a-b9fb-ff094333b3e4"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7b8e76a0-a861-4c73-89cc-5c0c85d8d576"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShootTackle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c430829c-e1c3-4352-a536-834a2205814f"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftPass"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4b581ccd-4324-4cc5-aaf2-d3fb244bde91"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftPass"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""RightControls"",
            ""id"": ""7f3ddb75-a9c8-4fbf-b8a1-f5038c88dcac"",
            ""actions"": [
                {
                    ""name"": ""RSMove"",
                    ""type"": ""Value"",
                    ""id"": ""3344631f-c8d8-4ca8-bf09-38dee489f479"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""RightPass"",
                    ""type"": ""Button"",
                    ""id"": ""6357344b-6c62-4d83-bb83-acfd8557ff67"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ShootTackle"",
                    ""type"": ""Button"",
                    ""id"": ""c0ff47a2-c567-4b38-af03-418b30240170"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""0fb15c05-57ec-4a95-8d73-076e7f81e20b"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RSMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""UDLR"",
                    ""id"": ""be8acb0a-6119-40c6-bc4a-12795018a612"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RSMove"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""8a69e79b-2a2e-40f1-ab1d-8623c13f1e65"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RSMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""d9f64917-f5b0-4bb4-a1ba-85d44518cfed"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RSMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""721589e1-6ccc-48ca-bb2d-eed2cf7c45b8"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RSMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""319deb9b-d745-4769-9d87-dbcda48c869c"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RSMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""0a8c896c-61a1-47ce-8657-ff0a7a7b6960"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightPass"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9a781808-666e-4e1b-bb89-7d6461ef168e"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShootTackle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // LeftControls
        m_LeftControls = asset.FindActionMap("LeftControls", throwIfNotFound: true);
        m_LeftControls_LSMove = m_LeftControls.FindAction("LSMove", throwIfNotFound: true);
        m_LeftControls_Join = m_LeftControls.FindAction("Join", throwIfNotFound: true);
        m_LeftControls_ShootTackle = m_LeftControls.FindAction("ShootTackle", throwIfNotFound: true);
        m_LeftControls_LeftPass = m_LeftControls.FindAction("LeftPass", throwIfNotFound: true);
        // RightControls
        m_RightControls = asset.FindActionMap("RightControls", throwIfNotFound: true);
        m_RightControls_RSMove = m_RightControls.FindAction("RSMove", throwIfNotFound: true);
        m_RightControls_RightPass = m_RightControls.FindAction("RightPass", throwIfNotFound: true);
        m_RightControls_ShootTackle = m_RightControls.FindAction("ShootTackle", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // LeftControls
    private readonly InputActionMap m_LeftControls;
    private List<ILeftControlsActions> m_LeftControlsActionsCallbackInterfaces = new List<ILeftControlsActions>();
    private readonly InputAction m_LeftControls_LSMove;
    private readonly InputAction m_LeftControls_Join;
    private readonly InputAction m_LeftControls_ShootTackle;
    private readonly InputAction m_LeftControls_LeftPass;
    public struct LeftControlsActions
    {
        private @Controls m_Wrapper;
        public LeftControlsActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @LSMove => m_Wrapper.m_LeftControls_LSMove;
        public InputAction @Join => m_Wrapper.m_LeftControls_Join;
        public InputAction @ShootTackle => m_Wrapper.m_LeftControls_ShootTackle;
        public InputAction @LeftPass => m_Wrapper.m_LeftControls_LeftPass;
        public InputActionMap Get() { return m_Wrapper.m_LeftControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(LeftControlsActions set) { return set.Get(); }
        public void AddCallbacks(ILeftControlsActions instance)
        {
            if (instance == null || m_Wrapper.m_LeftControlsActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_LeftControlsActionsCallbackInterfaces.Add(instance);
            @LSMove.started += instance.OnLSMove;
            @LSMove.performed += instance.OnLSMove;
            @LSMove.canceled += instance.OnLSMove;
            @Join.started += instance.OnJoin;
            @Join.performed += instance.OnJoin;
            @Join.canceled += instance.OnJoin;
            @ShootTackle.started += instance.OnShootTackle;
            @ShootTackle.performed += instance.OnShootTackle;
            @ShootTackle.canceled += instance.OnShootTackle;
            @LeftPass.started += instance.OnLeftPass;
            @LeftPass.performed += instance.OnLeftPass;
            @LeftPass.canceled += instance.OnLeftPass;
        }

        private void UnregisterCallbacks(ILeftControlsActions instance)
        {
            @LSMove.started -= instance.OnLSMove;
            @LSMove.performed -= instance.OnLSMove;
            @LSMove.canceled -= instance.OnLSMove;
            @Join.started -= instance.OnJoin;
            @Join.performed -= instance.OnJoin;
            @Join.canceled -= instance.OnJoin;
            @ShootTackle.started -= instance.OnShootTackle;
            @ShootTackle.performed -= instance.OnShootTackle;
            @ShootTackle.canceled -= instance.OnShootTackle;
            @LeftPass.started -= instance.OnLeftPass;
            @LeftPass.performed -= instance.OnLeftPass;
            @LeftPass.canceled -= instance.OnLeftPass;
        }

        public void RemoveCallbacks(ILeftControlsActions instance)
        {
            if (m_Wrapper.m_LeftControlsActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ILeftControlsActions instance)
        {
            foreach (var item in m_Wrapper.m_LeftControlsActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_LeftControlsActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public LeftControlsActions @LeftControls => new LeftControlsActions(this);

    // RightControls
    private readonly InputActionMap m_RightControls;
    private List<IRightControlsActions> m_RightControlsActionsCallbackInterfaces = new List<IRightControlsActions>();
    private readonly InputAction m_RightControls_RSMove;
    private readonly InputAction m_RightControls_RightPass;
    private readonly InputAction m_RightControls_ShootTackle;
    public struct RightControlsActions
    {
        private @Controls m_Wrapper;
        public RightControlsActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @RSMove => m_Wrapper.m_RightControls_RSMove;
        public InputAction @RightPass => m_Wrapper.m_RightControls_RightPass;
        public InputAction @ShootTackle => m_Wrapper.m_RightControls_ShootTackle;
        public InputActionMap Get() { return m_Wrapper.m_RightControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(RightControlsActions set) { return set.Get(); }
        public void AddCallbacks(IRightControlsActions instance)
        {
            if (instance == null || m_Wrapper.m_RightControlsActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_RightControlsActionsCallbackInterfaces.Add(instance);
            @RSMove.started += instance.OnRSMove;
            @RSMove.performed += instance.OnRSMove;
            @RSMove.canceled += instance.OnRSMove;
            @RightPass.started += instance.OnRightPass;
            @RightPass.performed += instance.OnRightPass;
            @RightPass.canceled += instance.OnRightPass;
            @ShootTackle.started += instance.OnShootTackle;
            @ShootTackle.performed += instance.OnShootTackle;
            @ShootTackle.canceled += instance.OnShootTackle;
        }

        private void UnregisterCallbacks(IRightControlsActions instance)
        {
            @RSMove.started -= instance.OnRSMove;
            @RSMove.performed -= instance.OnRSMove;
            @RSMove.canceled -= instance.OnRSMove;
            @RightPass.started -= instance.OnRightPass;
            @RightPass.performed -= instance.OnRightPass;
            @RightPass.canceled -= instance.OnRightPass;
            @ShootTackle.started -= instance.OnShootTackle;
            @ShootTackle.performed -= instance.OnShootTackle;
            @ShootTackle.canceled -= instance.OnShootTackle;
        }

        public void RemoveCallbacks(IRightControlsActions instance)
        {
            if (m_Wrapper.m_RightControlsActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IRightControlsActions instance)
        {
            foreach (var item in m_Wrapper.m_RightControlsActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_RightControlsActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public RightControlsActions @RightControls => new RightControlsActions(this);
    public interface ILeftControlsActions
    {
        void OnLSMove(InputAction.CallbackContext context);
        void OnJoin(InputAction.CallbackContext context);
        void OnShootTackle(InputAction.CallbackContext context);
        void OnLeftPass(InputAction.CallbackContext context);
    }
    public interface IRightControlsActions
    {
        void OnRSMove(InputAction.CallbackContext context);
        void OnRightPass(InputAction.CallbackContext context);
        void OnShootTackle(InputAction.CallbackContext context);
    }
}
