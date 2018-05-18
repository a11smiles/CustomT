using System;
using System.Collections.Generic;
using Newtonsoft;

namespace CustomT
{
    public static class Parser
    {
        public static CustomT Parse(string input, string name = null)
        {
            if (string.IsNullOrWhiteSpace(input))
                return null;
            else if (input == "n")
                return new CustomT() { Name = name, Type = CustomTDef.number };
            else if (input == "s")
                return new CustomT() { Name = name, Type = CustomTDef.text };
            else if (input == "b")
                return new CustomT() { Name = name, Type = CustomTDef.boolean };
            else if (input.StartsWith("[") && input.EndsWith("]"))
            {
                input = input.Substring(1, input.Length - 2);

                string[] strArray = input.Split(new char[] { ',', ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                
                var custT = new CustomT() { Type = CustomTDef.tObject, Children = new List<CustomT>() };
                for (int i = 0; i < strArray.Length; i++)
                {
                    string[] objArray = strArray[i].Split(new char[] { ':', ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                    if (objArray.Length == 1)
                    {
                        var subChild = new CustomT()
                        {
                            Type = CustomTDef.tObject,
                            Name = objArray[0],
                            Children = new List<CustomT>()
                        };

                        subChild.Children.Add(Parse(strArray[++i]));
                        custT.Children.Add(subChild);
                    }
                    else
                    {
                        custT.Children.Add(Parse(objArray[1], objArray[0]));
                    }
                }

                return custT;
            }
            else {
                throw new FormatException();
            }
        }
    }
}