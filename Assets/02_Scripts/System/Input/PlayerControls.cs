// GENERATED AUTOMATICALLY FROM 'Assets/02_Scripts/System/Input/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""0d591860-e460-46dd-a3e6-671aa64ca166"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""49c3cd4e-50fd-454b-971a-36715fbc91ce"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Rotate"",
                    ""type"": ""PassThrough"",
                    ""id"": ""2fdf1204-269a-4df8-a414-26bf038f12fb"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ActivatePowerUp"",
                    ""type"": ""Button"",
                    ""id"": ""a8dc81d1-93a0-4f15-96b9-e1f35f516585"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""8a44b34c-3cb8-46ab-9235-85e70d3d0002"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""007e2556-65a8-473c-9722-4faded84be78"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Skill1"",
                    ""type"": ""Button"",
                    ""id"": ""304c72ee-f2ed-4ab1-8ec8-960f66b02e79"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Skill2"",
                    ""type"": ""Button"",
                    ""id"": ""497f7f01-7df4-4640-ab55-611cf8d07f16"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Skill3"",
                    ""type"": ""Button"",
                    ""id"": ""8d1161a3-e5ea-4443-8fd1-923edfa8bd0e"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Skill4"",
                    ""type"": ""Button"",
                    ""id"": ""095ebc34-6d83-43a5-a007-20ad7e9144b1"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Charge"",
                    ""type"": ""Button"",
                    ""id"": ""860ffea1-ebda-4ca1-b6b5-59385a92a9df"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""CenterCamera"",
                    ""type"": ""Button"",
                    ""id"": ""3f0651a4-79d1-4d75-b13a-f9c1e0fd74ab"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""2b37d1ad-fe27-4fc8-aa29-ef58443a89c7"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a98c3121-dcb6-45cc-b95b-7679dfa6da22"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8a023282-d76d-4030-9476-7c3d5ccdfa03"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""ActivatePowerUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""666a5262-b0cf-4bc9-88a8-8d38b7a474ec"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ActivatePowerUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""baec401d-6ff8-4c8d-9aa4-b9efbcffecf9"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ActivatePowerUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""wasdKeys"",
                    ""id"": ""c14f9cd8-913c-40b0-ac09-e9a85fb7bed6"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Up"",
                    ""id"": ""973f8379-f909-4521-92a3-2091804577fe"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Down"",
                    ""id"": ""3e92b97b-83f2-4194-b46b-fc4cbcffbbc9"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left"",
                    ""id"": ""62b25473-ed11-4b80-9660-7f04cf19d5f3"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Right"",
                    ""id"": ""50244d5c-9c2a-4cdf-845c-3249bbfde9a5"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""55565c4e-2f1a-4342-99e2-b40932efd8f8"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bc645fe7-e4ea-432c-a5d1-488cc3a7fcd8"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""939b7dc7-8d6f-4b2b-b364-fe415066da5d"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""Skill1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5145e25a-5f34-47fd-b402-01e3312dfa64"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Skill1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d995cc9d-13ab-492f-ab97-0be9a2a04932"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""Skill1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0e70b0e6-bcfe-4e23-badd-96fb6e04c094"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""Skill2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b297393a-c648-438e-bead-fc1ec0396624"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Skill2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d16d4b6f-c6ae-4ef7-8e67-b3c99b9c0d1e"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""Skill2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5352cccf-3c6f-46a5-bdb5-8db5f049686c"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""Skill3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4ab38ee3-cb62-422a-a6d1-7c76c0e973b9"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Skill3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""358ddab8-57dc-41ab-ac6d-c70685a0919f"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""Skill3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b8d2a8eb-b555-47b1-9d91-658b3b6a584a"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""Skill4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0643772d-9463-4f87-8f76-5ca42b040505"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Skill4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8b670691-b4f0-47e9-916d-b6f6d79fc25e"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""Skill4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ec945e99-fe3c-492a-a39a-3ae26524441b"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fb8d1d9e-4a53-4b65-b1f9-d20fa0eb97d6"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""Charge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""712d7f10-7adc-45f0-9759-181d44eb844b"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Charge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b0d52d44-f625-4bb5-8b2b-f4992d259b11"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CenterCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""uiControls"",
            ""id"": ""e18a87e5-b7cf-472e-bb52-12a680308cf5"",
            ""actions"": [
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""b43fa7b8-1c3e-47b8-8f21-e7556d612f03"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""cf71d3a9-9491-4cc5-9304-2460ac08e5b6"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cedcc12e-a8f5-43fe-8c63-e57d3fd6a76d"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Cheats"",
            ""id"": ""51c67e37-5558-4c43-92b3-70433e3e10c6"",
            ""actions"": [
                {
                    ""name"": ""GodMode"",
                    ""type"": ""Button"",
                    ""id"": ""87f82733-e05e-4668-b7e5-d112fdb608bc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RemoveSkillChargtime"",
                    ""type"": ""Button"",
                    ""id"": ""dc8db5d2-c30b-4fa2-9e38-b06520f57e9b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Fullhealth"",
                    ""type"": ""Button"",
                    ""id"": ""c7dd912d-db96-4b1f-a8eb-c251bfc9d8d9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DestroyAllEnemies"",
                    ""type"": ""Button"",
                    ""id"": ""1bd07c21-7ad1-44ca-9e01-ea886760f3fd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""NoDashCooldown"",
                    ""type"": ""Button"",
                    ""id"": ""ba47ebcf-21b1-4a3d-af4f-7a152306516b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ChargeSuperSkill"",
                    ""type"": ""Button"",
                    ""id"": ""5150f6a6-62cb-4496-9399-c2ddf017e289"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TeleportTest"",
                    ""type"": ""Button"",
                    ""id"": ""bb0ea735-faa7-4f2f-9d71-671c103e3a33"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""eeb4f43b-d8b3-438f-b2b4-6e79bad83579"",
                    ""path"": ""<Keyboard>/f2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""RemoveSkillChargtime"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e6e23a3b-f083-435a-a10d-b9cb1f19ce94"",
                    ""path"": ""<Keyboard>/f7"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""TeleportTest"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4185b9f5-2870-4781-85bc-dfede82c69de"",
                    ""path"": ""<Keyboard>/f1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""GodMode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""22183587-b3c6-4ebd-9b4d-48ffa0f50017"",
                    ""path"": ""<Keyboard>/f3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""Fullhealth"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d0bb7f36-1831-4579-b6dd-ce660214b026"",
                    ""path"": ""<Keyboard>/f4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""DestroyAllEnemies"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b79be796-ceac-4ad6-afb9-62ceab24b890"",
                    ""path"": ""<Keyboard>/f5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""NoDashCooldown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""06281a3f-58e1-4302-b786-a82e76371ff6"",
                    ""path"": ""<Keyboard>/f6"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""ChargeSuperSkill"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Keyboard and mouse"",
            ""bindingGroup"": ""Keyboard and mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_Movement = m_Gameplay.FindAction("Movement", throwIfNotFound: true);
        m_Gameplay_Rotate = m_Gameplay.FindAction("Rotate", throwIfNotFound: true);
        m_Gameplay_ActivatePowerUp = m_Gameplay.FindAction("ActivatePowerUp", throwIfNotFound: true);
        m_Gameplay_Dash = m_Gameplay.FindAction("Dash", throwIfNotFound: true);
        m_Gameplay_Look = m_Gameplay.FindAction("Look", throwIfNotFound: true);
        m_Gameplay_Skill1 = m_Gameplay.FindAction("Skill1", throwIfNotFound: true);
        m_Gameplay_Skill2 = m_Gameplay.FindAction("Skill2", throwIfNotFound: true);
        m_Gameplay_Skill3 = m_Gameplay.FindAction("Skill3", throwIfNotFound: true);
        m_Gameplay_Skill4 = m_Gameplay.FindAction("Skill4", throwIfNotFound: true);
        m_Gameplay_Charge = m_Gameplay.FindAction("Charge", throwIfNotFound: true);
        m_Gameplay_CenterCamera = m_Gameplay.FindAction("CenterCamera", throwIfNotFound: true);
        // uiControls
        m_uiControls = asset.FindActionMap("uiControls", throwIfNotFound: true);
        m_uiControls_Pause = m_uiControls.FindAction("Pause", throwIfNotFound: true);
        // Cheats
        m_Cheats = asset.FindActionMap("Cheats", throwIfNotFound: true);
        m_Cheats_GodMode = m_Cheats.FindAction("GodMode", throwIfNotFound: true);
        m_Cheats_RemoveSkillChargtime = m_Cheats.FindAction("RemoveSkillChargtime", throwIfNotFound: true);
        m_Cheats_Fullhealth = m_Cheats.FindAction("Fullhealth", throwIfNotFound: true);
        m_Cheats_DestroyAllEnemies = m_Cheats.FindAction("DestroyAllEnemies", throwIfNotFound: true);
        m_Cheats_NoDashCooldown = m_Cheats.FindAction("NoDashCooldown", throwIfNotFound: true);
        m_Cheats_ChargeSuperSkill = m_Cheats.FindAction("ChargeSuperSkill", throwIfNotFound: true);
        m_Cheats_TeleportTest = m_Cheats.FindAction("TeleportTest", throwIfNotFound: true);
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

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_Movement;
    private readonly InputAction m_Gameplay_Rotate;
    private readonly InputAction m_Gameplay_ActivatePowerUp;
    private readonly InputAction m_Gameplay_Dash;
    private readonly InputAction m_Gameplay_Look;
    private readonly InputAction m_Gameplay_Skill1;
    private readonly InputAction m_Gameplay_Skill2;
    private readonly InputAction m_Gameplay_Skill3;
    private readonly InputAction m_Gameplay_Skill4;
    private readonly InputAction m_Gameplay_Charge;
    private readonly InputAction m_Gameplay_CenterCamera;
    public struct GameplayActions
    {
        private @PlayerControls m_Wrapper;
        public GameplayActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Gameplay_Movement;
        public InputAction @Rotate => m_Wrapper.m_Gameplay_Rotate;
        public InputAction @ActivatePowerUp => m_Wrapper.m_Gameplay_ActivatePowerUp;
        public InputAction @Dash => m_Wrapper.m_Gameplay_Dash;
        public InputAction @Look => m_Wrapper.m_Gameplay_Look;
        public InputAction @Skill1 => m_Wrapper.m_Gameplay_Skill1;
        public InputAction @Skill2 => m_Wrapper.m_Gameplay_Skill2;
        public InputAction @Skill3 => m_Wrapper.m_Gameplay_Skill3;
        public InputAction @Skill4 => m_Wrapper.m_Gameplay_Skill4;
        public InputAction @Charge => m_Wrapper.m_Gameplay_Charge;
        public InputAction @CenterCamera => m_Wrapper.m_Gameplay_CenterCamera;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovement;
                @Rotate.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRotate;
                @Rotate.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRotate;
                @Rotate.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRotate;
                @ActivatePowerUp.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnActivatePowerUp;
                @ActivatePowerUp.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnActivatePowerUp;
                @ActivatePowerUp.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnActivatePowerUp;
                @Dash.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDash;
                @Look.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLook;
                @Skill1.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSkill1;
                @Skill1.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSkill1;
                @Skill1.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSkill1;
                @Skill2.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSkill2;
                @Skill2.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSkill2;
                @Skill2.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSkill2;
                @Skill3.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSkill3;
                @Skill3.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSkill3;
                @Skill3.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSkill3;
                @Skill4.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSkill4;
                @Skill4.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSkill4;
                @Skill4.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSkill4;
                @Charge.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCharge;
                @Charge.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCharge;
                @Charge.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCharge;
                @CenterCamera.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCenterCamera;
                @CenterCamera.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCenterCamera;
                @CenterCamera.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCenterCamera;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Rotate.started += instance.OnRotate;
                @Rotate.performed += instance.OnRotate;
                @Rotate.canceled += instance.OnRotate;
                @ActivatePowerUp.started += instance.OnActivatePowerUp;
                @ActivatePowerUp.performed += instance.OnActivatePowerUp;
                @ActivatePowerUp.canceled += instance.OnActivatePowerUp;
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @Skill1.started += instance.OnSkill1;
                @Skill1.performed += instance.OnSkill1;
                @Skill1.canceled += instance.OnSkill1;
                @Skill2.started += instance.OnSkill2;
                @Skill2.performed += instance.OnSkill2;
                @Skill2.canceled += instance.OnSkill2;
                @Skill3.started += instance.OnSkill3;
                @Skill3.performed += instance.OnSkill3;
                @Skill3.canceled += instance.OnSkill3;
                @Skill4.started += instance.OnSkill4;
                @Skill4.performed += instance.OnSkill4;
                @Skill4.canceled += instance.OnSkill4;
                @Charge.started += instance.OnCharge;
                @Charge.performed += instance.OnCharge;
                @Charge.canceled += instance.OnCharge;
                @CenterCamera.started += instance.OnCenterCamera;
                @CenterCamera.performed += instance.OnCenterCamera;
                @CenterCamera.canceled += instance.OnCenterCamera;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);

    // uiControls
    private readonly InputActionMap m_uiControls;
    private IUiControlsActions m_UiControlsActionsCallbackInterface;
    private readonly InputAction m_uiControls_Pause;
    public struct UiControlsActions
    {
        private @PlayerControls m_Wrapper;
        public UiControlsActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Pause => m_Wrapper.m_uiControls_Pause;
        public InputActionMap Get() { return m_Wrapper.m_uiControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UiControlsActions set) { return set.Get(); }
        public void SetCallbacks(IUiControlsActions instance)
        {
            if (m_Wrapper.m_UiControlsActionsCallbackInterface != null)
            {
                @Pause.started -= m_Wrapper.m_UiControlsActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_UiControlsActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_UiControlsActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_UiControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
            }
        }
    }
    public UiControlsActions @uiControls => new UiControlsActions(this);

    // Cheats
    private readonly InputActionMap m_Cheats;
    private ICheatsActions m_CheatsActionsCallbackInterface;
    private readonly InputAction m_Cheats_GodMode;
    private readonly InputAction m_Cheats_RemoveSkillChargtime;
    private readonly InputAction m_Cheats_Fullhealth;
    private readonly InputAction m_Cheats_DestroyAllEnemies;
    private readonly InputAction m_Cheats_NoDashCooldown;
    private readonly InputAction m_Cheats_ChargeSuperSkill;
    private readonly InputAction m_Cheats_TeleportTest;
    public struct CheatsActions
    {
        private @PlayerControls m_Wrapper;
        public CheatsActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @GodMode => m_Wrapper.m_Cheats_GodMode;
        public InputAction @RemoveSkillChargtime => m_Wrapper.m_Cheats_RemoveSkillChargtime;
        public InputAction @Fullhealth => m_Wrapper.m_Cheats_Fullhealth;
        public InputAction @DestroyAllEnemies => m_Wrapper.m_Cheats_DestroyAllEnemies;
        public InputAction @NoDashCooldown => m_Wrapper.m_Cheats_NoDashCooldown;
        public InputAction @ChargeSuperSkill => m_Wrapper.m_Cheats_ChargeSuperSkill;
        public InputAction @TeleportTest => m_Wrapper.m_Cheats_TeleportTest;
        public InputActionMap Get() { return m_Wrapper.m_Cheats; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CheatsActions set) { return set.Get(); }
        public void SetCallbacks(ICheatsActions instance)
        {
            if (m_Wrapper.m_CheatsActionsCallbackInterface != null)
            {
                @GodMode.started -= m_Wrapper.m_CheatsActionsCallbackInterface.OnGodMode;
                @GodMode.performed -= m_Wrapper.m_CheatsActionsCallbackInterface.OnGodMode;
                @GodMode.canceled -= m_Wrapper.m_CheatsActionsCallbackInterface.OnGodMode;
                @RemoveSkillChargtime.started -= m_Wrapper.m_CheatsActionsCallbackInterface.OnRemoveSkillChargtime;
                @RemoveSkillChargtime.performed -= m_Wrapper.m_CheatsActionsCallbackInterface.OnRemoveSkillChargtime;
                @RemoveSkillChargtime.canceled -= m_Wrapper.m_CheatsActionsCallbackInterface.OnRemoveSkillChargtime;
                @Fullhealth.started -= m_Wrapper.m_CheatsActionsCallbackInterface.OnFullhealth;
                @Fullhealth.performed -= m_Wrapper.m_CheatsActionsCallbackInterface.OnFullhealth;
                @Fullhealth.canceled -= m_Wrapper.m_CheatsActionsCallbackInterface.OnFullhealth;
                @DestroyAllEnemies.started -= m_Wrapper.m_CheatsActionsCallbackInterface.OnDestroyAllEnemies;
                @DestroyAllEnemies.performed -= m_Wrapper.m_CheatsActionsCallbackInterface.OnDestroyAllEnemies;
                @DestroyAllEnemies.canceled -= m_Wrapper.m_CheatsActionsCallbackInterface.OnDestroyAllEnemies;
                @NoDashCooldown.started -= m_Wrapper.m_CheatsActionsCallbackInterface.OnNoDashCooldown;
                @NoDashCooldown.performed -= m_Wrapper.m_CheatsActionsCallbackInterface.OnNoDashCooldown;
                @NoDashCooldown.canceled -= m_Wrapper.m_CheatsActionsCallbackInterface.OnNoDashCooldown;
                @ChargeSuperSkill.started -= m_Wrapper.m_CheatsActionsCallbackInterface.OnChargeSuperSkill;
                @ChargeSuperSkill.performed -= m_Wrapper.m_CheatsActionsCallbackInterface.OnChargeSuperSkill;
                @ChargeSuperSkill.canceled -= m_Wrapper.m_CheatsActionsCallbackInterface.OnChargeSuperSkill;
                @TeleportTest.started -= m_Wrapper.m_CheatsActionsCallbackInterface.OnTeleportTest;
                @TeleportTest.performed -= m_Wrapper.m_CheatsActionsCallbackInterface.OnTeleportTest;
                @TeleportTest.canceled -= m_Wrapper.m_CheatsActionsCallbackInterface.OnTeleportTest;
            }
            m_Wrapper.m_CheatsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @GodMode.started += instance.OnGodMode;
                @GodMode.performed += instance.OnGodMode;
                @GodMode.canceled += instance.OnGodMode;
                @RemoveSkillChargtime.started += instance.OnRemoveSkillChargtime;
                @RemoveSkillChargtime.performed += instance.OnRemoveSkillChargtime;
                @RemoveSkillChargtime.canceled += instance.OnRemoveSkillChargtime;
                @Fullhealth.started += instance.OnFullhealth;
                @Fullhealth.performed += instance.OnFullhealth;
                @Fullhealth.canceled += instance.OnFullhealth;
                @DestroyAllEnemies.started += instance.OnDestroyAllEnemies;
                @DestroyAllEnemies.performed += instance.OnDestroyAllEnemies;
                @DestroyAllEnemies.canceled += instance.OnDestroyAllEnemies;
                @NoDashCooldown.started += instance.OnNoDashCooldown;
                @NoDashCooldown.performed += instance.OnNoDashCooldown;
                @NoDashCooldown.canceled += instance.OnNoDashCooldown;
                @ChargeSuperSkill.started += instance.OnChargeSuperSkill;
                @ChargeSuperSkill.performed += instance.OnChargeSuperSkill;
                @ChargeSuperSkill.canceled += instance.OnChargeSuperSkill;
                @TeleportTest.started += instance.OnTeleportTest;
                @TeleportTest.performed += instance.OnTeleportTest;
                @TeleportTest.canceled += instance.OnTeleportTest;
            }
        }
    }
    public CheatsActions @Cheats => new CheatsActions(this);
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    private int m_KeyboardandmouseSchemeIndex = -1;
    public InputControlScheme KeyboardandmouseScheme
    {
        get
        {
            if (m_KeyboardandmouseSchemeIndex == -1) m_KeyboardandmouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard and mouse");
            return asset.controlSchemes[m_KeyboardandmouseSchemeIndex];
        }
    }
    public interface IGameplayActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnRotate(InputAction.CallbackContext context);
        void OnActivatePowerUp(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnSkill1(InputAction.CallbackContext context);
        void OnSkill2(InputAction.CallbackContext context);
        void OnSkill3(InputAction.CallbackContext context);
        void OnSkill4(InputAction.CallbackContext context);
        void OnCharge(InputAction.CallbackContext context);
        void OnCenterCamera(InputAction.CallbackContext context);
    }
    public interface IUiControlsActions
    {
        void OnPause(InputAction.CallbackContext context);
    }
    public interface ICheatsActions
    {
        void OnGodMode(InputAction.CallbackContext context);
        void OnRemoveSkillChargtime(InputAction.CallbackContext context);
        void OnFullhealth(InputAction.CallbackContext context);
        void OnDestroyAllEnemies(InputAction.CallbackContext context);
        void OnNoDashCooldown(InputAction.CallbackContext context);
        void OnChargeSuperSkill(InputAction.CallbackContext context);
        void OnTeleportTest(InputAction.CallbackContext context);
    }
}
