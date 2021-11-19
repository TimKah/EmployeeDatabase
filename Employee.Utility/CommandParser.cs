using Employee.Domain.Models;
using System;
using System.Collections.Generic;

namespace Employee.Utility
{
    public class CommandParser
    {
        public static IDictionary<AllowedVariables, object> ParseVariables(string[] args)
        {
            if (args.Length % 2 != 0)
            {
                throw new Exception();
            }

            IDictionary<AllowedVariables, object> receivedArgs = new Dictionary<AllowedVariables, object>();

            for (int i = 0; i < args.Length; i += 2)
            {
                string variableName = args[i].Replace("--", "").ToLower();

                if (variableName.Equals(AllowedVariables.EmployeeId.GetEnumDescription()))
                {
                    if (int.TryParse(args[i + 1], out int value))
                    {
                        receivedArgs[AllowedVariables.EmployeeId] = value;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                else if (variableName.Equals(AllowedVariables.EmployeeName.GetEnumDescription()))
                {
                    if (args[i + 1].Length <= 128)
                    {
                        receivedArgs[AllowedVariables.EmployeeName] = args[i + 1];
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                else if (variableName.Equals(AllowedVariables.EmployeeSalary.GetEnumDescription()))
                {
                    if (int.TryParse(args[i + 1], out int value))
                    {
                        receivedArgs[AllowedVariables.EmployeeSalary] = value;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                else if (variableName.Equals(AllowedVariables.SimulatedTimeUtc.GetEnumDescription()))
                {
                    if (DateTime.TryParse(args[i + 1], out DateTime value))
                    {
                        receivedArgs[AllowedVariables.SimulatedTimeUtc] = value.ToUniversalTime();
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    throw new ArgumentException($"Argument {args[i]} is not supported");
                }
            }

            return receivedArgs;
        }
    }
}
