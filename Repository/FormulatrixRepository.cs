using System;
using System.Collections.Generic;
using Services;
using System.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Xml;

namespace Repository
{
    public class FormulatrixRepository : IFormulatrix,IDisposable
    {
        //Storage Contexts 
        public Dictionary<TypeNames, object> itemsContext;
       
        // Json Utility
        FormulatrixJson formulatrixJson;
        // XML Utility
        FormulatrixXml formulatrixXml;

        public void Initialize()
        {
            itemsContext = new Dictionary<TypeNames, object>();
        }

        private bool isDuplicatedKey(int itemType,string itemName){

            return itemsContext.Where(a => a.Key.itemName == itemName && a.Key.itemType == itemType).Count()>0;
        }

        private void jsonRegister(string itemName, string itemContent, int itemType){
            this.formulatrixJson = new FormulatrixJson(itemContent);
            if (string.IsNullOrEmpty(this.formulatrixJson.validate()))
            {


                // Check Duplicated Key
                if (itemsContext.Count() > 0)
                {
                    if (this.isDuplicatedKey(itemType, itemName))
                    {
                        throw new Exception("Duplicated !");
                    }
                }
                // Adding item Json to Dictionary 
                itemsContext.Add(new TypeNames { itemName = itemName, itemType = 1 }, this.formulatrixJson.item);

            }
            else
            {
                throw new Exception("Error format JSON String !");
            }
        }

        private void xmlRegister(string itemName, string itemContent, int itemType){
            this.formulatrixXml = new FormulatrixXml(itemContent);
            if (string.IsNullOrEmpty(this.formulatrixXml.validate()))
            {


                // Check Duplicated Key
                if (itemsContext.Count() > 0)
                {
                    if (this.isDuplicatedKey(itemType, itemName))
                    {
                        throw new Exception("Duplicated !");
                    }
                }
                // Adding item Json to Dictionary 
                itemsContext.Add(new TypeNames { itemName = itemName, itemType = 2 }, this.formulatrixXml.item);

            }
            else
            {
                throw new Exception("Error format XML String !");
            }
        }

        public void Register(string itemName, string itemContent, int itemType)
        {
            //Validation
            if(string.IsNullOrWhiteSpace(itemName)){
                throw new Exception("item name mandatory !");
            }

            if (string.IsNullOrWhiteSpace(itemContent))
            {
                throw new Exception("item content mandatory !");
            }

            if (itemType!=1 && itemType != 2)
            {
                throw new Exception("item type mandatory and should be entering 1 (json) or 2 (xml) !");
            }

            // Validate document item Json & XML
            if(itemType==1){ // Json Item 

                this.jsonRegister( itemName,  itemContent,  itemType);

            }else if(itemType==2){ // XML item
                this.xmlRegister(itemName, itemContent, itemType);
            }
        }


        public int GetType(string itemName)
        {
            if (this.itemsContext.Count() > 0)
            {
                var docs = this.itemsContext.Where(a => a.Key.itemName == itemName);

                if (docs.Count() > 0)
                {
                    return this.itemsContext.Where(a => a.Key.itemName == itemName).First().Key.itemType;
                }
            }

            return 0;
        }

        public string Retrieve(string itemName)
        {
            if (this.itemsContext.Count() > 0)
            {
                var docs = this.itemsContext.Where(a => a.Key.itemName == itemName);

                if (docs.Count() > 0)
                {
                    var doc = docs.First();
                    if (doc.Value != null)
                    {
                        if (doc.Key.itemType == 1)
                        {
                            return JsonConvert.SerializeObject(doc.Value);
                        }
                        else if (doc.Key.itemType == 2)
                        {
                            XmlDocument xmlDoc = (XmlDocument)doc.Value;
                            StringWriter sw = new StringWriter();
                            XmlTextWriter xw = new XmlTextWriter(sw);
                            xmlDoc.WriteTo(xw);
                            return sw.ToString();
                        }
                    }
                }
            }
            return string.Empty;
        }

        public void Deregister(string itemName)
        {
            if (this.itemsContext.Count() > 0)
            {
                var docs = this.itemsContext.Where(a => a.Key.itemName == itemName);

                if (docs.Count() > 0)
                {
                    var doc = this.itemsContext.Where(a => a.Key.itemName == itemName).First();
                    this.itemsContext.Remove(doc.Key);
                }
            }

        }

        public void Dispose()
        {
            itemsContext.Clear();
        }
    }
}
