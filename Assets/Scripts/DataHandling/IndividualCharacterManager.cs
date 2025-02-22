using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class IndividualCharacterManager : MonoBehaviour
{
    private CharacterData currentCharacterData;

    [SerializeField] string currentCharacterFileName;
    [SerializeField] float currentCharacterMoveSpeed = 1;
    [SerializeField] uint currentCharacterHealth = 100;

    [SerializeField] CharacterMode currentCharacterMode;
    [SerializeField] bool isCharacterUnique = false;

    private StateData currentStateData;
    [SerializeField] PrimaryState currentPrimaryState = PrimaryState.PRIMARYSTATE_INVALID;
    [SerializeField] SecondaryState currentSecondaryState = SecondaryState.SECONDARYSTATE_INVALID;
    InteractingSubstate currentInteractingSubstate;
    DashingSubstate currentDashingSubstate;
    ThrowingSubstate currentThrowingSubstate;
    AttackingSubstate currentAttackingSubstate;
    CinematicSubstate currentCinematicSubstate;

    [SerializeField] float characterAnimationSpeed = 1;

    private AbilityData currentAbilityData;
    [SerializeField] bool canCharacterAttack = false;
    [SerializeField] bool isCharacterInvincible = false;

    [SerializeField] bool isCharacterHumanoid = false;

    private HumanoidData humanoidData;
    [SerializeField] HumanType characterHumanType;
    
    [SerializeField] BodyType characterBodyType;
    [SerializeField] Color characterBodyColor;
    [SerializeField] SkinType characterSkinType;
    [SerializeField] Color characterSkinColor;
    [SerializeField] HairType characterHairType;
    [SerializeField] Color characterHairColor;

    [SerializeField] string primaryWeaponName;
    [SerializeField] string secondaryWeaponName;
    public List<string> additionalWeaponsInventoryNames;

    private List<string> allWeaponsInventoryNames;

    public WeaponData primaryWeaponEquipment;
    public WeaponData secondaryWeaponEquipment;
    public List<WeaponData> weaponInventory;

    [HideInInspector] public string currentCharacterObjectSubtype;

    private List<GameObject> allCharactersSubtypes;

    public StateData currentCharacterStateChangeData;

    private CharacterStateMachineManager myStateMachine;

    public void Initialize(string characterSubtype, string currentCharacterFileName, CharacterMode currentCharacterMode, PrimaryState currentPrimaryState, SecondaryState currentSecondaryState, InteractingSubstate currentInteractingSubstate, DashingSubstate currentDashingSubstate, ThrowingSubstate currentThrowingSubstate, AttackingSubstate currentAttackingSubstate, CinematicSubstate currentCinematicSubstate,float animationSpeed, bool isCharacterUnique, bool characterCanAttack, bool characterInvinsible, bool isCharacterHumanoid, HumanType characterHumanType, BodyType characterBodyType, Color characterBodyColor, SkinType characterSkinType, Color characterSkinColor, HairType characterHairType, Color characterHairColor, WeaponData primaryWeapon, WeaponData secondaryWeapon, List<WeaponData> weaponInventory)
    {
        this.currentCharacterObjectSubtype = characterSubtype;
        this.currentCharacterFileName = currentCharacterFileName;
        this.currentCharacterMode = currentCharacterMode;
        this.currentPrimaryState = currentPrimaryState;
        this.currentSecondaryState = currentSecondaryState;
        this.currentInteractingSubstate = currentInteractingSubstate;
        this.currentDashingSubstate = currentDashingSubstate;
        this.currentThrowingSubstate = currentThrowingSubstate;
        this.currentAttackingSubstate = currentAttackingSubstate;
        this.currentCinematicSubstate = currentCinematicSubstate;
        this.characterAnimationSpeed = animationSpeed;
        this.canCharacterAttack = characterCanAttack;
        this.isCharacterInvincible = characterInvinsible;
        this.isCharacterHumanoid = isCharacterHumanoid;
        this.isCharacterUnique = isCharacterUnique;
        this.characterHumanType = characterHumanType;
        this.characterBodyType = characterBodyType;
        this.characterBodyColor = characterBodyColor;
        this.characterSkinType = characterSkinType;
        this.characterSkinColor = characterSkinColor;
        this.characterHairType = characterHairType;
        this.characterHairColor = characterHairColor;
        this.primaryWeaponEquipment = primaryWeapon;
        this.secondaryWeaponEquipment = secondaryWeapon;
        this.weaponInventory = weaponInventory;

        CheckForMyStateMachine();
    }


    // Getters
    public string GetCharacterFileName()
    {
        return currentCharacterFileName;
    }

    public float GetCharacterMoveSpeed()
    {
        return currentCharacterMoveSpeed;
    }

    public uint GetCharacterHealth()
    {
        return currentCharacterHealth;
    }
    
    public CharacterMode GetCharacterMode()
    {
        return currentCharacterMode;
    }

    public InteractingSubstate GetCurrentInteractingSubstate()
    {
        return currentInteractingSubstate;
    }

    public DashingSubstate GetCurrentDashingSubstate()
    {
        return currentDashingSubstate;
    }

    public ThrowingSubstate GetCurrentThrowingSubstate()
    {
        return currentThrowingSubstate;
    }

    public AttackingSubstate GetCurrentAttackingSubstate()
    {
        return currentAttackingSubstate;
    }

    public CinematicSubstate GetCurrentCinematicSubstate()
    {
        return currentCinematicSubstate;
    }

    public bool GetCanCharacterAttack()
    {
        return canCharacterAttack;
    }

    public bool GetIsCharacterInvincible()
    {
        return isCharacterInvincible;
    }

    public bool GetIsCharacterHumanoid()
    {
        return isCharacterHumanoid;
    }

    public bool GetIsCharacterUnique()
    {
        return isCharacterUnique;
    }

    public HumanType GetCharacterHumanType()
    {
        return characterHumanType;
    }

    public BodyType GetCharacterBodyType()
    {
        return characterBodyType;
    }
    
    public Color GetBodyColor()
    {
        return characterBodyColor;
    }

    public SkinType GetSkinType()
    {
        return characterSkinType;
    }

    public Color GetSkinColor()
    {
        return characterSkinColor;
    }

    public HairType GetCharacterHairType()
    {
        return characterHairType;
    }

    public Color GetHairColor()
    {
        return characterHairColor;
    }   

    public WeaponData GetPrimartWeaponData()
    {
        return primaryWeaponEquipment;
    }

    public WeaponData GetSecondaryWeaponData()
    {
        return secondaryWeaponEquipment;
    }

    public List<WeaponData> GetWeaponInventory()
    {
        return weaponInventory;
    }

    public string GetCharacterObjectSubType()
    {
        return currentCharacterObjectSubtype;
    }

    public CharacterStateMachineManager GetCharacterStateMachineManager()
    {
        return myStateMachine;
    }

    // Setters
    public void SetCharacterFileName(string value)
    {
        this.currentCharacterFileName = value;
    }
    
    public void SetCharacterMoveSpeed(float value)
    {
        this.currentCharacterMoveSpeed = value;
    }
    
    public void SetCharacterHealth(uint value)
    {
        this.currentCharacterHealth = value;
    }

    public void SetCharacterMode(CharacterMode chrMode)
    {
        this.currentCharacterMode = chrMode;
    }

    public void SetCurrentPrimaryState(PrimaryState primaryState)
    {
        this.currentPrimaryState = primaryState;
    }

    public void SetCurrentSecondaryState(SecondaryState secondaryState)
    {
        this.currentSecondaryState = secondaryState;
    }

    public void SetCurrentInteractingSubstate(InteractingSubstate interactingSubstate)
    {
        this.currentInteractingSubstate = interactingSubstate;
    }

    public void SetCurrentDashingSubstate(DashingSubstate dashingSubstate)
    {
        this.currentDashingSubstate = dashingSubstate;
    }

    public void SetCurrentThrowingSubstate(ThrowingSubstate throwingSubstate)
    {
        this.currentThrowingSubstate = throwingSubstate;
    }

    public void SetCurrentAttackingSubstate(AttackingSubstate attackingSubstate)
    {
        this.currentAttackingSubstate = attackingSubstate;
    }

    public void SetCurrentCinematicSubstate(CinematicSubstate cinematicSubstate)
    {
        this.currentCinematicSubstate = cinematicSubstate;
    }

    public void SetCanCharacterAttack(bool canAttack)
    {
        this.canCharacterAttack = canAttack;
    }

    public void SetIsCharacterInvincible(bool isInvincible)
    {
        this.isCharacterInvincible = isInvincible;
    }

    public void SetIsCharacterUnique(bool isUnique)
    {
        this.isCharacterUnique = isUnique;
    }

    public void SetAnimationSpeed(float animationSpeed)
    {
        this.characterAnimationSpeed = animationSpeed;
    }

    public void SetIsCharacterHumanoid(bool isHumanoid)
    {
        this.isCharacterHumanoid = isHumanoid;
    }

    public void SetCharacterHumanType(HumanType humanType)
    {
        this.characterHumanType = humanType;
    }

    public void SetCharacterBodyType(BodyType bodyType)
    {
        this.characterBodyType = bodyType;
    }

    public void SetCharacterHairType(HairType hairType)
    {
        this.characterHairType = hairType;
    }

    public void SetBodyColor(Color color)
    {
        this.characterBodyColor = color;
    }

    public void SetSkinColor(Color color)
    {
        this.characterSkinColor = color;
    }

    public void SetHairColor(Color color)
    {
        this.characterHairColor = color;
    }

    public void SetCharacterStateMachineManager(CharacterStateMachineManager stateMachine)
    {
        myStateMachine = stateMachine;
    }

    public void SetPrimaryWeapon(WeaponData weaponData)
    {
        primaryWeaponEquipment = weaponData;
    }
    
    public void SetSecondartWeapon(WeaponData weaponData)
    {
        secondaryWeaponEquipment = weaponData;
    }

    public void SetWeaponInventory(List<WeaponData> weaponsData)
    {
        weaponInventory = weaponsData;
    }

    public void SetCharacterObjectSubType(string characterObjectSubtype)
    {
        this.currentCharacterObjectSubtype = characterObjectSubtype;
    }

    //Switching
    public void SwitchCharacterMode(CharacterMode mode)
    {
        currentCharacterMode = mode;
    }

    public void SwitchPrimaryState(PrimaryState primaryState)
    {
        currentPrimaryState = primaryState;
    }

    public void SwitchSecondaryState(SecondaryState secondaryState)
    {
        currentSecondaryState = secondaryState;
    }

    public void SwitchInteractingSubstate(InteractingSubstate interactingSubstate)
    {
        currentInteractingSubstate = interactingSubstate;
    }

    public void SwitchDashingSubstate(DashingSubstate dashingSubstate)
    {
        currentDashingSubstate = dashingSubstate;
    }

    public void SwitchThrowingSubstate(ThrowingSubstate throwingSubstate)
    {
        currentThrowingSubstate = throwingSubstate;
    }

    public void SwitchAttackingSubstate(AttackingSubstate attackingSubstate)
    {
        currentAttackingSubstate = attackingSubstate;
    }

    public void SwitchCinematicSubstate(CinematicSubstate cinematicSubstate)
    {
        currentCinematicSubstate = cinematicSubstate;
    }

    ///Assigning

    public void SetStateDataFromInventory()
    {
        currentStateData = new StateData(currentPrimaryState, currentSecondaryState, currentInteractingSubstate, currentDashingSubstate, currentThrowingSubstate, currentAttackingSubstate, currentCinematicSubstate);
    }

    public void SetStateData(StateData stateData)
    {
        this.currentStateData = stateData;

        currentPrimaryState = stateData.GetPrimaryState();
        currentSecondaryState = stateData.GetSecondaryState();
        currentInteractingSubstate = stateData.GetInteractingSubstate();
        currentDashingSubstate = stateData.GetDashingSubstate();
        currentThrowingSubstate = stateData.GetThrowingSubstate();
        currentAttackingSubstate = stateData.GetAttackingSubstate();
        currentCinematicSubstate = stateData.GetCinematicSubstate();
    }

    public void SetAbilityDataFromInventory()
    {
        currentAbilityData = new AbilityData(canCharacterAttack, isCharacterInvincible);
    }
    
    public void SetAbilityData(AbilityData abilityData)
    {
        this.currentAbilityData = abilityData;

        this.canCharacterAttack = abilityData.GetCharacterCanAttack();
        this.isCharacterInvincible = abilityData.GetCharacterInvincible();
    }

    public void SetHumanoidDataFromInventory()
    {
        if (weaponInventory == null)
        {
            weaponInventory = new List<WeaponData>(0);
        }
        weaponInventory.Clear();
        WeaponsCreator weaponsStorage = GameObject.Find("WeaponsStorage").GetComponent<WeaponsCreator>();

        if (allWeaponsInventoryNames == null)
        {
            allWeaponsInventoryNames = new List<string>();
        }

        allWeaponsInventoryNames.Clear();

        if (primaryWeaponEquipment != null)
        {
            allWeaponsInventoryNames.Add(primaryWeaponName);
        }

        if (secondaryWeaponEquipment != null)
        {
            allWeaponsInventoryNames.Add(secondaryWeaponName);
        }

        //This is to make sure to add in the additional weapons inventory names added by designer into the private all weapons inventory list!
        foreach (string wpnName in additionalWeaponsInventoryNames)
        {
            allWeaponsInventoryNames.Add(wpnName);
        }

        //Only the private all weapons inventory names is the one that is checked for against the weapons storage to add to the character's inventory!
        foreach (WeaponData weapon in weaponsStorage.allWeapons)
        {
            uint weaponFoundTimesCount = 0;

            foreach (string currentWeaponName in allWeaponsInventoryNames)
            {
                if (currentWeaponName.Equals(weapon.GetWeaponName()))
                {
                    ++weaponFoundTimesCount;

                    //For every copy the weapon is saved with the name: WeaponName_Count!
                    WeaponData newWpnData = new WeaponData();

                    newWpnData.SetWeaponData(weapon);
                    newWpnData.SetWeaponName(newWpnData.GetWeaponName() + "__" + weaponFoundTimesCount);
                    weaponInventory.Add(newWpnData);

                    Debug.Log("Weapon: " + currentWeaponName + " found in Weapons Storage added to the " + currentCharacterFileName + "'s weapon inventory (" + weaponFoundTimesCount + ") times.");
                }
            }

            if (weaponFoundTimesCount == 0)
            {
                Debug.Log("No such weapon found in the Weapons Storage!!!");
            }
        }

        humanoidData = new HumanoidData(characterHumanType, characterBodyType, characterBodyColor, characterSkinType, characterSkinColor, characterHairType, characterHairColor, primaryWeaponName, secondaryWeaponName, weaponInventory);
    }
    
    public void SetHumanoidData(HumanoidData humanoidData)
    {
        this.humanoidData = humanoidData;

        characterHumanType = humanoidData.GetHumanType();
        characterBodyType = humanoidData.GetBodyType();
        characterBodyColor = humanoidData.GetBodyColor();
        characterSkinType = humanoidData.GetSkinType();
        characterSkinColor = humanoidData.GetSkinColor();
        characterHairType = humanoidData.GetHairType();
        characterHairColor = humanoidData.GetHairColor();
        
        primaryWeaponName = humanoidData.GetPrimaryWeaponName();
        secondaryWeaponName = humanoidData.GetSecondaryWeaponName();

        List<string> weaponNames = new List<string>();

        bool foundPrimaryOnce = false;
        bool foundSecondaryOnce = false;

        foreach (WeaponData weapon in humanoidData.GetWeaponInventory())
        {
            string wpnName = weapon.GetWeaponName();

            //Splitting the __ counted part because there could be multiple copies of a weapon!!
            string[] wpnNameSplit = wpnName.Split(new string[] { "__" }, StringSplitOptions.RemoveEmptyEntries);

            if (!foundPrimaryOnce && primaryWeaponName.Equals(wpnNameSplit[0]))
            {
                foundPrimaryOnce = true;
                weaponNames.Add(wpnNameSplit[0]);
                continue;
            }

            if (!foundSecondaryOnce && secondaryWeaponName.Equals(wpnNameSplit[0]))
            {
                foundSecondaryOnce = true;
                weaponNames.Add(wpnNameSplit[0]);
                continue;
            }

            additionalWeaponsInventoryNames.Add(wpnNameSplit[0]);
            weaponNames.Add(wpnNameSplit[0]);
        }

        //This is the true full list of weapon inventory!!
        allWeaponsInventoryNames = weaponNames;
    }

    public void GetAllCharacterTypePrefabs()
    {
        if (allCharactersSubtypes == null)
        {
            allCharactersSubtypes = new List<GameObject>();
        }

        allCharactersSubtypes.Clear();

        //Internal function runs based on relative path!!!
        string dataHandlingObjectSubTypePrefabDir = "Assets\\Prefabs\\DataHandling\\CharacterSubTypes\\";
        allCharactersSubtypes = FindPrefabs.FindPrefabsInPath(dataHandlingObjectSubTypePrefabDir);
    }

    public void SaveCharacter()
    {
        string dirCharacter = FilePaths.GetFullCharactersPath();
        Directory.CreateDirectory(dirCharacter);
        string dirFilePath = Path.Combine(dirCharacter, currentCharacterFileName);

        //Setting Weapons
        WeaponsCreator weaponsStorage = GameObject.Find("WeaponsStorage").GetComponent<WeaponsCreator>();
        
        if (weaponsStorage == null)
        {
            return;
        }

        weaponsStorage.LoadAllWeapons();

        if (weaponInventory == null)
        {
            weaponInventory = new List<WeaponData>();
        }

        weaponInventory.Clear();

        foreach (WeaponData weapon in weaponsStorage.GetAllWeapons())
        {
            if (weapon.GetWeaponName().Equals(primaryWeaponName))
            {
                primaryWeaponEquipment = weapon;
            }
            //look at
            //we can have same weapon twice dual wielded right?
            if(weapon.GetWeaponName().Equals(secondaryWeaponName))
            {
                secondaryWeaponEquipment = weapon;
            }
        }

        SetStateDataFromInventory();
        SetAbilityDataFromInventory();
        SetHumanoidDataFromInventory();

        currentCharacterData = new CharacterData(currentCharacterMoveSpeed, currentCharacterHealth, this.gameObject, currentCharacterObjectSubtype, currentCharacterMode, isCharacterUnique, currentStateData,
            characterAnimationSpeed, currentAbilityData, isCharacterHumanoid, humanoidData);

        SaveAndLoadFile.SerializeObject<CharacterData>(currentCharacterData, dirFilePath);

        string dirDumpPath = FilePaths.GetFullCharacterDumpsPath();
        string dumpFileName = currentCharacterFileName + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");

        Directory.CreateDirectory(dirDumpPath);

        string fullDumpPath = Path.Combine(dirDumpPath, dumpFileName);
        SaveAndLoadFile.SerializeObject<CharacterData>(currentCharacterData, fullDumpPath);
    }

    public void UpdateCharacterFromFile()
    {
        string dirCharacter = FilePaths.GetFullCharactersPath();
        Directory.CreateDirectory(dirCharacter);
        string dirFilePath = Path.Combine(dirCharacter, currentCharacterFileName);
        
        currentCharacterData = SaveAndLoadFile.DeSerializeObject<CharacterData>(dirFilePath);
        UpdateCharacter(currentCharacterData);
    }
    
    private GameObject UpdateCharacter(CharacterData currCharacterData)
    {
        ////Spawn object before changing data of objects if you don't want to mess with the prefabs
        GameObject characterObj = this.gameObject;
        IndividualCharacterManager characterManager = characterObj.GetComponent<IndividualCharacterManager>();

        ObjectData charObjData = currCharacterData.GetCharacterObjectData();

        characterObj.name = charObjData.GetObjectSubtype().ToString();

        characterManager.SetCharacterMoveSpeed(currCharacterData.GetCharacterMoveSpeed());
        characterManager.SetCharacterHealth(currCharacterData.GetCharacterHealth());

        ////Setting Object Data
        characterObj.transform.position = charObjData.GetPosition();
        characterObj.transform.rotation = charObjData.GetOrientation();
        characterObj.transform.localScale = charObjData.GetScale();

        characterObj.SetActive(charObjData.GetIsActive());

        //Managing sprite data.
        {
            SpriteData sprData = charObjData.GetSpriteData();
            SpriteRenderer characterObjSprRender = characterObj.GetComponent<SpriteRenderer>();

            if (characterObjSprRender != null)
            {
                characterObjSprRender.sprite.name = sprData.GetSpriteName();
                characterObjSprRender.color = sprData.GetSpriteColor();
                characterObjSprRender.flipX = sprData.GetSpriteFlipX();
                characterObjSprRender.flipY = sprData.GetSpriteFlipY();
                characterObjSprRender.sharedMaterial.name = sprData.GetSpriteMaterialName();


                characterObjSprRender.enabled = sprData.GetSpriteIsEnabled();

                characterObjSprRender.sortingLayerName = sprData.GetSpriteSortingLayerName();
                characterObjSprRender.sortingLayerID = sprData.GetSpriteSortingLayerID();
                characterObjSprRender.sortingOrder = sprData.GetSpriteSortingOrder();
                characterObjSprRender.renderingLayerMask = sprData.GetSpriteRenderingLayerMask();
            }
        }

        ////Managing collider data.
        //{
        //    //Destroy prior colliders (set up from the prefabs) that exist!!
        //    {
        //        Collider2D[] coll2DList = characterObj.GetComponents<Collider2D>();

        //        if (Application.isPlaying)
        //        {
        //            for (int i = 0; i < coll2DList.Length; ++i)
        //            {
        //                Destroy(coll2DList[i]);
        //            }
        //        }
        //        else
        //        {
        //            for (int i = 0; i < coll2DList.Length; ++i)
        //            {
        //                DestroyImmediate(coll2DList[i]);
        //            }
        //        }
        //    }


        //    ColliderData[] collDataList = charObjData.GetColliderData();
        //    if (collDataList.Length > 0)
        //    {
        //        foreach (ColliderData colliderData in collDataList)
        //        {
        //            if (colliderData is BoxColliderData)
        //            {
        //                BoxCollider2D spawnedBox = characterObj.AddComponent<BoxCollider2D>();
        //                BoxColliderData boxColliderData = colliderData as BoxColliderData;

        //                spawnedBox.offset = boxColliderData.GetOffset();
        //                spawnedBox.isTrigger = boxColliderData.GetIsTrigger();
        //                spawnedBox.enabled = boxColliderData.GetIsEnabled();
        //                spawnedBox.size = boxColliderData.GetColliderSize();
        //            }
        //            else if (colliderData is PolygonColliderData)
        //            {
        //                PolygonCollider2D spawnedPolygon = characterObj.AddComponent<PolygonCollider2D>();
        //                PolygonColliderData polygonColliderData = colliderData as PolygonColliderData;

        //                spawnedPolygon.offset = polygonColliderData.GetOffset();
        //                spawnedPolygon.isTrigger = polygonColliderData.GetIsTrigger();
        //                spawnedPolygon.enabled = polygonColliderData.GetIsEnabled();
        //                spawnedPolygon.points = polygonColliderData.GetPolygonColliderPointsList().ToArray();
        //            }
        //            else if (colliderData is CircleColliderData)
        //            {
        //                CircleCollider2D spawnedCircle = characterObj.AddComponent<CircleCollider2D>();
        //                CircleColliderData circleColliderData = colliderData as CircleColliderData;

        //                spawnedCircle.offset = circleColliderData.GetOffset();
        //                spawnedCircle.isTrigger = circleColliderData.GetIsTrigger();
        //                spawnedCircle.enabled = circleColliderData.GetIsEnabled();
        //                spawnedCircle.radius = circleColliderData.GetColliderRadius();
        //            }
        //            else if (colliderData is CapsuleColliderData)
        //            {
        //                CapsuleCollider2D spawnedCapsule = characterObj.AddComponent<CapsuleCollider2D>();
        //                CapsuleColliderData capsuleColliderData = colliderData as CapsuleColliderData;

        //                spawnedCapsule.offset = capsuleColliderData.GetOffset();
        //                spawnedCapsule.isTrigger = capsuleColliderData.GetIsTrigger();
        //                spawnedCapsule.enabled = capsuleColliderData.GetIsEnabled();
        //                spawnedCapsule.size = capsuleColliderData.GetColliderSize();
        //            }
        //        }
        //    }
        //}

        //Set Light Data Here
        {
            Light2dData lightData = charObjData.GetLight2DData();
            Light2D light2D = characterObj.GetComponent<Light2D>();

            if (light2D != null)
            {
                lightData.SetDataToLight2D(ref light2D);
            }

            LevelSwitcherData levelSwitcherData = charObjData.GetLevelSwitcherData();
            LevelSwitcher lvlSwitcher = characterObj.GetComponent<LevelSwitcher>();

            if (lvlSwitcher != null)
            {
                lvlSwitcher.newLevelName = levelSwitcherData.GetLevelName();
            }
        }

        //Character Mode
        characterManager.SwitchCharacterMode(currCharacterData.GetCharacterMode());

        characterManager.SetIsCharacterUnique(currCharacterData.GetIsCharacterUnique());

        characterManager.SetAnimationSpeed(currCharacterData.GetCharacterAnimationSpeed());

        //StateData
        characterManager.SetStateData(currCharacterData.GetStateData());

        //Ability Data
        characterManager.SetAbilityData(currCharacterData.GetCharacterAbilityData());

        //HumanoidData
        characterManager.SetHumanoidData(currCharacterData.GetCharacterHumanoidData());

        characterObj.layer = charObjData.GetLayer();

        return characterObj;
    }

    public void CheckForMyStateMachine()
    {
        if (myStateMachine == null)
        {
            myStateMachine = new CharacterStateMachineManager();
            //SUBSCRIBE TO STATE CHANGE
            myStateMachine.OnCurrentStateUpdated += OnMyStateChanged;
        }
    }

    public void AddStateDataToQueue(StateData stateData)
    {
        CheckForMyStateMachine();

        //ADD TO QUEUE
        myStateMachine.AddToStateChangeQueue(stateData);
    }

    public void RequestStateParse()
    {
        myStateMachine.RequestStateChange();
    }
    
    public void TrySetRandomStates()
    {
        StateData stateChangeData = new StateData();

        //Random States
        {
            PrimaryState primaryState = (PrimaryState)Randomiser.GetRandomInt(0, (int)PrimaryState.PRIMARYSTATE_COUNT);

            SecondaryState secondaryState = (SecondaryState)Randomiser.GetRandomInt(0, (int)SecondaryState.SECONDARYSTATE_COUNT);

            InteractingSubstate interactingSubstate = (InteractingSubstate)Randomiser.GetRandomInt(0, (int)InteractingSubstate.INTERACTINGSUBSTATE_COUNT);

            DashingSubstate dashingSubstate = (DashingSubstate)Randomiser.GetRandomInt(0, (int)DashingSubstate.DASHINGSUBSTATE_COUNT);

            ThrowingSubstate throwingSubstate = (ThrowingSubstate)Randomiser.GetRandomInt(0, (int)ThrowingSubstate.THROWINGSUBSTATE_COUNT);

            AttackingSubstate attackingSubstate = (AttackingSubstate)Randomiser.GetRandomInt(0, (int)AttackingSubstate.ATTACKINGSUBSTATE_COUNT);

            CinematicSubstate cinematicSubstate = (CinematicSubstate)Randomiser.GetRandomInt(0, (int)CinematicSubstate.CINEMATICSUBSTATE_COUNT);

            PauseMenuSubstate pauseMenuSubstate = (PauseMenuSubstate)Randomiser.GetRandomInt(0, (int)PauseMenuSubstate.PAUSEMENU_COUNT);
        }

        CheckForMyStateMachine();
        myStateMachine.RequestStateChange();
    }

    public void OnMyStateChanged(StateData stateChangeData)
    {
        Debug.Log("Successfully Changed States PRIMARY STATE :" + currentCharacterStateChangeData.GetPrimaryState() + "->" + stateChangeData.GetPrimaryState() + "\nSECONDARY STATE :" + currentCharacterStateChangeData.GetSecondaryState() + "->" + stateChangeData.GetSecondaryState() + "\nINTERACTING SUBSTATE :" + currentCharacterStateChangeData.GetInteractingSubstate() + "->" + "\nDASHING SUBSTATE :" + currentCharacterStateChangeData.GetDashingSubstate() + "->" + stateChangeData.GetDashingSubstate() + "\nTHROWING SUBSTATE :" + currentCharacterStateChangeData.GetThrowingSubstate() + "->" + stateChangeData.GetThrowingSubstate() + "\nCINEMATIC SUBSTATE :" + currentCharacterStateChangeData.GetCinematicSubstate() + "->" + stateChangeData.GetCinematicSubstate() + "\nPAUSEMENU SUBSTATE :" + currentCharacterStateChangeData.GetPauseMenuSubstate() + "->" + stateChangeData.GetPauseMenuSubstate());

        //SetCurrentPrimaryState(primaryState);
        //SetCurrentSecondaryState(secondaryState);
    }

    private void MoveToPath(Vector2[] path)
    {
        for (int i = 0; i < path.Length; ++i)
        {

        }
    }

    private void Update()
    {

    }
}
