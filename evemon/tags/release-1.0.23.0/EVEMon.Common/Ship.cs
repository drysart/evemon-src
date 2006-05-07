using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;
using System.IO.Compression;
using System.Xml.Serialization;

namespace EVEMon.Common
{
    public class Ship
    {
        private static Ship[] sm_ships = null;

        public static Ship[] GetShips()
        {
            if (sm_ships == null)
            {
                Assembly a = Assembly.GetExecutingAssembly();
                using (Stream s = a.GetManifestResourceStream("EVEMon.Common.eve-ships2.xml.gz"))
                using (GZipStream zs = new GZipStream(s, CompressionMode.Decompress))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(Ship[]));
                    sm_ships = (Ship[])xs.Deserialize(zs);
                }
            }
            return sm_ships;
        }

        private int m_id = -1;

        [XmlAttribute]
        public int Id
        {
            get { return m_id; }
            set { m_id = value; }
        }

        private string m_name = String.Empty;

        [XmlAttribute]
        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        private string m_description = String.Empty;

        [XmlAttribute]
        public string Description
        {
            get { return m_description; }
            set { m_description = value; }
        }

        private string m_race = String.Empty;

        [XmlAttribute]
        public string Race
        {
            get { return m_race; }
            set { m_race = value; }
        }

        private string m_type = String.Empty;

        [XmlAttribute]
        public string Type
        {
            get { return m_type; }
            set { m_type = value; }
        }

        private List<ShipProperty> m_properties = new List<ShipProperty>();

        [XmlArrayItem("prop")]
        public List<ShipProperty> Properties
        {
            get { return m_properties; }
        }

        private List<ShipRequiredSkill> m_requiredSkills = new List<ShipRequiredSkill>();

        [XmlArrayItem("skill")]
        public List<ShipRequiredSkill> RequiredSkills
        {
            get { return m_requiredSkills; }
        }
    }

    public class ShipProperty
    {
        private string m_name;

        [XmlAttribute]
        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        private string m_value;

        [XmlAttribute]
        public string Value
        {
            get { return m_value; }
            set { m_value = value; }
        }

        public override string ToString()
        {
            return m_name + ": " + m_value;
        }
    }

    public class ShipRequiredSkill
    {
        private string m_name;

        [XmlAttribute]
        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        private int m_level;

        [XmlAttribute]
        public int Level
        {
            get { return m_level; }
            set { m_level = value; }
        }
    }
}
