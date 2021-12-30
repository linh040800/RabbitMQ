using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Collections.Generic;

namespace Hub.BackgroundJob.Main
{
    public class ApiExplorerGroupNameCustom : IControllerModelConvention
    {
        /// <summary>
        /// 
        /// </summary>
        public const string API_PO = "api-PO";

        /// <summary>
        /// 
        /// </summary>
        public static List<string> groupName = new List<string>()
        {
            API_PO
        };

        public static Dictionary<string, List<string>> DictionaryActionRoute = new Dictionary<string, List<string>>();

        public void Apply(ControllerModel controller)
        {
            throw new NotImplementedException();
        }
    }
}
