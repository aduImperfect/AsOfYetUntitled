using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

public class StateData : IXmlSerializable
{
    [XmlAttribute("PrimaryState")]
    private PrimaryState primaryState;

    [XmlAttribute("SecondaryState")]
    private SecondaryState secondaryState;

    [XmlAttribute("InteractingSubState")]
    private InteractingSubstate interactingSubstate;
    
    [XmlAttribute("DashingSubState")]
    private DashingSubstate dashingSubstate;

    [XmlAttribute("ThrowingSubstate")]
    private ThrowingSubstate throwingSubstate;
    
    [XmlAttribute("AttackingSubstate")]
    private AttackingSubstate attackingSubstate;

    [XmlAttribute("CinematicSubstate")]
    private CinematicSubstate cinematicSubstate;
    
    private PauseMenuSubstate pauseMenuSubState;

    public StateData()
    {
        primaryState = PrimaryState.PRIMARYSTATE_INVALID;
        secondaryState = SecondaryState.SECONDARYSTATE_INVALID;
        interactingSubstate = InteractingSubstate.INTERACTINGSUBSTATE_INVALID;
        dashingSubstate = DashingSubstate.DASHINGSUBSTATE_INVALID;
        throwingSubstate = ThrowingSubstate.THROWINGSUBSTATE_INVALID;
        attackingSubstate = AttackingSubstate.ATTACKINGSUBSTATE_INVALID;
        cinematicSubstate = CinematicSubstate.CINEMATICSUBSTATE_INVALID;
        pauseMenuSubState = PauseMenuSubstate.PAUSEMENU_INVALID;
    }

    public StateData(PrimaryState primaryState, SecondaryState secondaryState, InteractingSubstate interactingSubstate, DashingSubstate dashingSubstate, ThrowingSubstate throwingSubstate, AttackingSubstate attackingSubstate, CinematicSubstate cinematicSubstate)
    {
        this.primaryState = primaryState;
        this.secondaryState = secondaryState;
        this.interactingSubstate = interactingSubstate;
        this.dashingSubstate = dashingSubstate;
        this.throwingSubstate = throwingSubstate;
        this.attackingSubstate = attackingSubstate;
        this.cinematicSubstate = cinematicSubstate;
        this.pauseMenuSubState = PauseMenuSubstate.PAUSEMENU_INVALID;
    }

    //Getters
    public PrimaryState GetPrimaryState()
    {
        return primaryState;
    }

    public SecondaryState GetSecondaryState()
    {
        return secondaryState;
    }
    
    public InteractingSubstate GetInteractingSubstate()
    {
        return interactingSubstate;
    }
    
    public DashingSubstate GetDashingSubstate()
    {
        return dashingSubstate;
    }
    
    public ThrowingSubstate GetThrowingSubstate()
    {
        return throwingSubstate;
    }
    
    public AttackingSubstate GetAttackingSubstate()
    {
        return attackingSubstate;
    }
    
    public CinematicSubstate GetCinematicSubstate()
    {
        return cinematicSubstate;
    }

    public PauseMenuSubstate GetPauseMenuSubstate()
    {
        return pauseMenuSubState;
    }

    
    //Setters
    public void SetPrimaryState(PrimaryState primaryState)
    {
        this.primaryState = primaryState;
    }

    public void SetSecondaryState(SecondaryState secondaryState)
    {
        this.secondaryState = secondaryState;
    }

    public void SetInteractingSubstate(InteractingSubstate interactingSubstate)
    {
        this.interactingSubstate = interactingSubstate;
    }

    public void SetDashingSubstate(DashingSubstate dashingSubstate)
    {
        this.dashingSubstate = dashingSubstate;
    }
    
    public void SetThrowingSubstate(ThrowingSubstate throwingSubstate)
    {
        this.throwingSubstate = throwingSubstate;
    }
    
    public void SetAttackingSubstate(AttackingSubstate attackingSubstate)
    {
        this.attackingSubstate = attackingSubstate;
    }

    public void SetCinematicSubstate(CinematicSubstate cinematicSubstate)
    {
        this.cinematicSubstate = cinematicSubstate;
    }


    public XmlSchema GetSchema()
    {
        return null;
    }

    public void ReadXml(XmlReader reader)
    {
        reader.ReadStartElement("PrimaryState");
        primaryState = StringParserWrapper.GetEnumPrimaryState(reader.ReadString());
        reader.ReadEndElement();
        
        reader.ReadStartElement("SecondaryState");
        secondaryState = StringParserWrapper.GetEnumSecondaryState(reader.ReadString());
        reader.ReadEndElement();

        reader.ReadStartElement("InteractingSubstate");
        interactingSubstate = StringParserWrapper.GetEnumInteractingSubstate(reader.ReadString());
        reader.ReadEndElement();

        reader.ReadStartElement("DashingSubstate");
        dashingSubstate = StringParserWrapper.GetEnumDashingSubstate(reader.ReadString());
        reader.ReadEndElement();

        reader.ReadStartElement("ThrowingSubstate");
        throwingSubstate = StringParserWrapper.GetEnumThrowingSubstate(reader.ReadString());
        reader.ReadEndElement();

        reader.ReadStartElement("AttackingSubstate");
        attackingSubstate = StringParserWrapper.GetEnumAttackingSubstate(reader.ReadString());
        reader.ReadEndElement();
        
        reader.ReadStartElement("CinematicSubstate");
        cinematicSubstate = StringParserWrapper.GetEnumCinematicSubstate(reader.ReadString());
        reader.ReadEndElement();
    }

    public void WriteXml(XmlWriter writer)
    {
        writer.WriteStartElement("PrimaryState");
        writer.WriteString(primaryState.ToString());
        writer.WriteEndElement();
        
        writer.WriteStartElement("SecondaryState");
        writer.WriteString(secondaryState.ToString());
        writer.WriteEndElement();
        
        writer.WriteStartElement("InteractingSubstate");
        writer.WriteString(interactingSubstate.ToString());
        writer.WriteEndElement();
        
        writer.WriteStartElement("DashingSubstate");
        writer.WriteString(dashingSubstate.ToString());
        writer.WriteEndElement();
        
        writer.WriteStartElement("ThrowingSubstate");
        writer.WriteString(throwingSubstate.ToString());
        writer.WriteEndElement();

        writer.WriteStartElement("AttackingSubstate");
        writer.WriteString(attackingSubstate.ToString());
        writer.WriteEndElement();

        writer.WriteStartElement("CinematicSubstate");
        writer.WriteString(cinematicSubstate.ToString());
        writer.WriteEndElement();
    }
}