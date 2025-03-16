
using System.Xml.Serialization;
namespace bibModelBudka.Model
{
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class Ksiazki {
    
    private KsiazkiKsiazka[] ksiazkaField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Ksiazka")]
    public KsiazkiKsiazka[] Ksiazka {
        get {
            return this.ksiazkaField;
        }
        set {
            this.ksiazkaField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class KsiazkiKsiazka {
    
    private int idField;
    
    private int idAutoraField;
    
    private string tytulField;
    
    private ushort rok_wydaniaField;
    
    private int idWydawcyField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public int id {
        get {
            return this.idField;
        }
        set {
            this.idField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public int IdAutora {
        get {
            return this.idAutoraField;
        }
        set {
            this.idAutoraField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string tytul {
        get {
            return this.tytulField;
        }
        set {
            this.tytulField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public ushort rok_wydania {
        get {
            return this.rok_wydaniaField;
        }
        set {
            this.rok_wydaniaField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public int IdWydawcy {
        get {
            return this.idWydawcyField;
        }
        set {
            this.idWydawcyField = value;
        }
    }
}
}