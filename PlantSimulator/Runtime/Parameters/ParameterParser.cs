using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PlantSimulator.Runtime.Parameters
{
    public class ParameterParser : IParameterParser
    {
        private static readonly Regex ValidationRegex = new Regex("^[\\w]*=[a-zA-Z.,-_]{1,}$");

        public Parameters Parse(string[] args)
        {
            IDictionary<string, string> parameters = new Dictionary<string, string>(args.Length);

            foreach (string arg in args)
            {
                if (!Validate(arg))
                {
                    continue;
                }

                string[] split = SplitArg(arg);

                parameters.Add(split[0], split[1]);
            }

            return new Parameters(parameters);
        }

        private bool Validate(string arg)
        {
            return ValidationRegex.IsMatch(arg);
        }

        private string[] SplitArg(string arg)
        {
            string[] split = arg.Split('=');

            if (split.Length != 2 || string.IsNullOrEmpty(split[1]))
            {
                throw new ArgumentException($"Argument {arg} is invalid");
            }

            return split;
        }
    }
}