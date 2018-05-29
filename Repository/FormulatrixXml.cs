using System;
using Newtonsoft.Json;
using System.Xml;
using System.Xml.Linq;

namespace Repository
{
    public class FormulatrixXml
    {
        public XmlDocument  item;
        private string xmlString = string.Empty;
        public FormulatrixXml(string xmlString)
        {
            this.xmlString = xmlString;
        }

        public string validate()
        {

            string result = string.Empty;
            try
            {
                this.item = new XmlDocument();
                this.item.LoadXml("<root>"+this.xmlString+"</root>");
            }
            catch (Exception exc)
            {
                result = exc.Message;
            }
            return result;
        }

    }
}
