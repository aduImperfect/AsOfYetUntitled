using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using UnityEngine;

public class CharacterData : IXmlSerializable
{
    //TODO List!
    //Remove CharacterName and make sure to add a counter specific naming value using ObjectName.
    //Remove CharacterLayer
    //Adding of AttributeData -  for the differents types of attributes that can be used - health, move speed, animation speed etc.
    //Adding of FamilyBackgroundData - including mother's name, father's name, sibling's list, emotionalvaluelistforeachcharacterconnection (all being values of: (-1, 0, +1)), netemotionalvalue (represented by the total emotionalvalues - can be: (<0, 0, >0)), dialoguepaths based on netemotionalvalue (so can be a negative, neutral, or positive dialogue path) - need to make sure to take care of all cases of dialogues for all characters, goalpaths - the same treatment as the dialogue paths which will also be depending on the netemotionalvalue.
    //Adding of GoalData - for the various pathways that a character can take.
    //Adding of CameraData(/CamData along with an isCamera bool check?) - Camera will then be considered as being a Character. This will let us then make sure that the camera can use the goalpaths to function.. same as an NPC, or an Enemy for example.
    [XmlAttribute("CharacterMoveSpeed")]
    float characterMoveSpeed;

    [XmlAttribute("CharacterHealth")]
    uint characterHealth;

    [XmlAttribute("ObjectData")]
    ObjectData characterObjectData;

    [XmlAttribute("CharacterMode")]
    CharacterMode characterMode;

    [XmlAttribute("IsUnique")]
    bool isCharacterUnique;

    [XmlAttribute("CharacterStateData")]
    StateData characterStateData;

    [XmlAttribute("CharacterAnimationSpeed")]
    float characterAnimationSpeed;

    //List<GoalData> goalsList;

    [XmlAttribute("CharacterAbilityData")]
    AbilityData characterAbilityData;

    [XmlAttribute("IsCharacterHumanoid")]
    bool isCharacterHumanoid;
    
    [XmlAttribute("CharacterHumanoidData")]
    HumanoidData characterHumanoidData;

    //bool isCamera;
    //CamData ;

    public CharacterData()
    {
        characterMoveSpeed = 1;
        characterHealth = 100;
        characterObjectData = new ObjectData();
        characterMode = CharacterMode.CHARACTER_INVALID;
        isCharacterUnique = false;
        characterStateData = new StateData();
        characterAnimationSpeed = 0;
        characterAbilityData = new AbilityData();
        isCharacterHumanoid = false;
        characterHumanoidData = new HumanoidData();
    }

    public CharacterData(float characterMoveSpeed, uint characterHealth, GameObject characterObj, string subtype, CharacterMode characterMode, bool isUnique, StateData stateData, float animationSpeed, AbilityData abilityData, bool isHumanoid, HumanoidData humanoidData)
    {
        this.characterMoveSpeed = characterMoveSpeed;
        this.characterHealth = characterHealth;
        characterObjectData = new ObjectData(characterObj, subtype);
        this.characterMode = characterMode;
        isCharacterUnique = isUnique;
        characterStateData = stateData;
        characterAnimationSpeed = animationSpeed;
        //characterLayer = characterObj.layer;
        characterAbilityData = abilityData;
        isCharacterHumanoid = false;
        characterHumanoidData = humanoidData;
    }
    
    public CharacterData(float characterMoveSpeed, uint characterHealth, string subtype, CharacterMode characterMode, bool isUnique, StateData stateData, float animationSpeed, AbilityData abilityData, bool isHumanoid, HumanoidData humanoidData)
    {
        this.characterMoveSpeed = characterMoveSpeed;
        this.characterHealth = characterHealth;
        characterObjectData = new ObjectData(subtype);
        this.characterMode = characterMode;
        isCharacterUnique = isUnique;
        characterStateData = stateData;
        characterAnimationSpeed = animationSpeed;
        //characterLayer = LayerMask.NameToLayer("Character");
        characterAbilityData = abilityData;
        isCharacterHumanoid = false;
        characterHumanoidData = humanoidData;
    }

    //Calculator
    private float GetAnimatorSpeed(Animator animator)
    {
        if (animator != null)
        {
            return animator.GetComponent<Animator>().speed;
        }
        return 0;
    }

    //Getters
    public float GetCharacterMoveSpeed() 
    {
        return characterMoveSpeed; 
    }
    
    public uint GetCharacterHealth() 
    {
        return characterHealth; 
    }

    public ObjectData GetCharacterObjectData()
    {
        return characterObjectData;
    }

    public CharacterMode GetCharacterMode()
    {
        return characterMode;
    }

    public bool GetIsCharacterUnique()
    {
        return isCharacterUnique;
    }
    public StateData GetStateData() 
    {
        return characterStateData;
    }

    public float GetCharacterAnimationSpeed()
    {
        return characterAnimationSpeed;
    }

    public AbilityData GetCharacterAbilityData()
    {
        return characterAbilityData;
    }

    public bool GetIsCharacterHumanoid() 
    {
        return isCharacterHumanoid;
    }

    public HumanoidData GetCharacterHumanoidData()
    {
        return characterHumanoidData;
    }

    //Setters
    public void SetCharacterMoveSpeed(float value)
    {
        characterMoveSpeed = value;
    }

    public void SetCharacterHealth(uint value)
    {
        characterHealth = value;
    }

    public void SetCharacterObjectData(ObjectData objectData)
    {
        characterObjectData = objectData;
    }

    public void SetCharacterMode(CharacterMode characterMode)
    {
        this.characterMode = characterMode;
    }

    public void SetIsCharacterUnique(bool isUnique)
    {
        this.isCharacterUnique = isUnique;
    }

    public void SetCharacterStateData(StateData stateData)
    {
        characterStateData = stateData;
    }

    public void SetCharacterAnimationSpeed(float speed)
    {
        characterAnimationSpeed = speed;
    }

    public void SetCharacterAbilityData(AbilityData abilityData)
    {
        characterAbilityData = abilityData;
    }

    public void SetIsCharacterHumanoid(bool isHumanoid)
    {
        isCharacterHumanoid = isHumanoid;
    }

    public void SetCharacterHumaniodData(HumanoidData humanoidData)
    {
        characterHumanoidData = humanoidData;
    }

    public XmlSchema GetSchema()
    {
        return null;
    }

    public void ReadXml(XmlReader reader)
    {
        reader.ReadStartElement("CharacterData");
        
        reader.ReadStartElement("CharacterMoveSpeed");
        characterMoveSpeed = StringParserWrapper.GetFloat(reader.ReadString());
        reader.ReadEndElement();
        
        reader.ReadStartElement("CharacterHealth");
        characterHealth = StringParserWrapper.GetUInt(reader.ReadString());
        reader.ReadEndElement();
        
        characterObjectData.ReadXml(reader);

        reader.ReadStartElement("CharacterMode");
        characterMode = (StringParserWrapper.GetEnumCharacterMode(reader.ReadString()));
        reader.ReadEndElement();
        
        reader.ReadStartElement("IsCharacterUnique");
        isCharacterUnique = StringParserWrapper.GetBool(reader.ReadString());
        reader.ReadEndElement();

        reader.ReadStartElement("CharacterStateData");
        characterStateData.ReadXml(reader);
        reader.ReadEndElement();

        reader.ReadStartElement("CharacterAnimationSpeed");
        characterAnimationSpeed = StringParserWrapper.GetFloat(reader.ReadString());
        reader.ReadEndElement();

        reader.ReadStartElement("CharacterAbilityData");
        characterAbilityData.ReadXml(reader);
        reader.ReadEndElement();

        reader.ReadStartElement("IsCharacterHumanoid");
        isCharacterHumanoid = StringParserWrapper.GetBool(reader.ReadString());
        reader.ReadEndElement();

        reader.ReadStartElement("CharacterHumanoidData");
        characterHumanoidData.ReadXml(reader);
        reader.ReadEndElement();

        reader.ReadEndElement();
    }

    public void WriteXml(XmlWriter writer)
    {
        writer.WriteStartElement("CharacterMoveSpeed");
        writer.WriteString(characterMoveSpeed.ToString());
        writer.WriteEndElement();
        
        writer.WriteStartElement("CharacterHealth");
        writer.WriteString(characterHealth.ToString());
        writer.WriteEndElement();

        writer.WriteStartElement("ObjectData");
        characterObjectData.WriteXml(writer);
        writer.WriteEndElement();

        writer.WriteStartElement("CharacterMode");
        writer.WriteString(characterMode.ToString());
        writer.WriteEndElement();

        writer.WriteStartElement("IsCharacterUnique");
        writer.WriteString(isCharacterUnique.ToString());
        writer.WriteEndElement();

        writer.WriteStartElement("CharacterStateData");
        characterStateData.WriteXml(writer);
        writer.WriteEndElement();

        writer.WriteStartElement("CharacterAnimationSpeed");
        writer.WriteString(characterAnimationSpeed.ToString());
        writer.WriteEndElement();

        writer.WriteStartElement("CharacterAbilityData");
        characterAbilityData.WriteXml(writer);
        writer.WriteEndElement();

        writer.WriteStartElement("IsCharacterHumanoid");
        writer.WriteString(isCharacterHumanoid.ToString());
        writer.WriteEndElement();

        writer.WriteStartElement("CharacterHumanoidData");
        characterHumanoidData.WriteXml(writer);
        writer.WriteEndElement();
    }
}
