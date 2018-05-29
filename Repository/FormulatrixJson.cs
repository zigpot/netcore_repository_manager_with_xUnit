using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Repository
{
    public class FormulatrixJson
    {
        public object item;
        private string jsonString = string.Empty;
        public FormulatrixJson(string jsonString)
        {
            this.jsonString = jsonString;
        }

        public string validate(){

            string result = string.Empty;
            try
            {
                this.item = JsonConvert.DeserializeObject(this.jsonString);
            }
            catch(Exception exc){
                result = exc.Message;
            }
            return result;
        }
    }
}
