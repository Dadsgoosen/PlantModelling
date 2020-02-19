using System;
using System.Collections.Generic;
using System.Reflection;

namespace PlantSimulator.Runtime.Parameters
{
    public class Parameters : IParameters
    {
        public string SettingsPath { get; private set; } = "appsettings.json";

        public Parameters(IDictionary<string, string> parameters)
        {
            SetParameters(parameters);
        }

        private void SetParameters(IDictionary<string, string> parameters)
        {
            PropertyInfo[] properties = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo property in properties)
            {
                string name = property.Name.ToLower();

                if (!parameters.ContainsKey(name) || parameters[name] == null)
                {
                    continue;
                }

                property.SetValue(this, parameters[name]);
            }
        }
    }
}