using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Collections.Generic;

namespace PO.EventBus.Main
{
    public class ApiExplorerGroupNameCustom : IControllerModelConvention
    {
        /// <summary>
        /// 
        /// </summary>
        public const string API_CORE = "api-core";

        /// <summary>
        /// 
        /// </summary>
        public static List<string> groupName = new List<string>()
        {
            API_CORE
        };

        public static Dictionary<string, List<string>> DictionaryActionRoute = new Dictionary<string, List<string>>();

        public void Apply(ControllerModel controller)
        {
            throw new NotImplementedException();
        }
    }
}
