//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Scripts/Engine/Menu/MenuActions.inputactions
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

public partial class @MenuActions : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @MenuActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""MenuActions"",
    ""maps"": [
        {
            ""name"": ""KeyActions"",
            ""id"": ""99a1a63c-701c-4800-920d-f4664ff79701"",
            ""actions"": [
                {
                    ""name"": ""Escape"",
                    ""type"": ""Button"",
                    ""id"": ""3b87114d-c142-45b3-89df-e2f19db56189"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ba277a83-38e2-4f5e-bfea-40458722550d"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Escape"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // KeyActions
        m_KeyActions = asset.FindActionMap("KeyActions", throwIfNotFound: true);
        m_KeyActions_Escape = m_KeyActions.FindAction("Escape", throwIfNotFound: true);
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

    // KeyActions
    private readonly InputActionMap m_KeyActions;
    private IKeyActionsActions m_KeyActionsActionsCallbackInterface;
    private readonly InputAction m_KeyActions_Escape;
    public struct KeyActionsActions
    {
        private @MenuActions m_Wrapper;
        public KeyActionsActions(@MenuActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Escape => m_Wrapper.m_KeyActions_Escape;
        public InputActionMap Get() { return m_Wrapper.m_KeyActions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(KeyActionsActions set) { return set.Get(); }
        public void SetCallbacks(IKeyActionsActions instance)
        {
            if (m_Wrapper.m_KeyActionsActionsCallbackInterface != null)
            {
                @Escape.started -= m_Wrapper.m_KeyActionsActionsCallbackInterface.OnEscape;
                @Escape.performed -= m_Wrapper.m_KeyActionsActionsCallbackInterface.OnEscape;
                @Escape.canceled -= m_Wrapper.m_KeyActionsActionsCallbackInterface.OnEscape;
            }
            m_Wrapper.m_KeyActionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Escape.started += instance.OnEscape;
                @Escape.performed += instance.OnEscape;
                @Escape.canceled += instance.OnEscape;
            }
        }
    }
    public KeyActionsActions @KeyActions => new KeyActionsActions(this);
    public interface IKeyActionsActions
    {
        void OnEscape(InputAction.CallbackContext context);
    }
}