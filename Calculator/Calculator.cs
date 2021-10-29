using System;
using System.Collections.Generic;

namespace CalculatorNS
{
    public static class Calculator
    {
        public static decimal Calculate(string input)
        {
            decimal result = 0;

            // Logic here
            var array = input.Split(" ");
            var currentOperator = "";

            int bracketLevel = 0;
            string insideBracketExpression = "";

            for (int i = 0; i < array.Length; i++)
            {
                // If not inside bracket, do calculation
                if (bracketLevel == 0)
                {
                    if (result == 0 && currentOperator == string.Empty && decimal.TryParse(array[i], out decimal parsedDouble))
                    {
                        result = parsedDouble;
                        continue;
                    }
                    if (IsOperator(array[i]))
                    {
                        currentOperator = array[i];
                        continue;
                    }
                    // Calculate
                    if (IsOperand(array[i]) && result != 0)
                    {
                        result = CalculateWithOperator(currentOperator, Convert.ToDecimal(array[i]), result);
                        continue;
                    }
                }
                // If inside bracket, add expression to insideBracketExpression
                else
                {
                    // ignore open & close bracket so they are not included in 
                    // expression insideBracketExpression for calculation
                    if (array[i] != "(" && array[i] != ")") { insideBracketExpression += " " + array[i]; }
                }

                if (array[i] == "(")
                {
                    bracketLevel += 1;
                    continue;
                }

                if (array[i] == ")")
                {
                    bracketLevel -= 1;
                    decimal innerBracketResult = Calculate(insideBracketExpression);
                    //handle if bracket hits before any operator is assigned
                    result = currentOperator == "" ?
                        innerBracketResult :
                        CalculateWithOperator(currentOperator, innerBracketResult, result);
                }
            }
            return result;
        }

        /// <summary>
        /// Calculate based on operator and return result
        /// </summary>
        /// <param name="oprator"></param>
        /// <param name="digit"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private static decimal CalculateWithOperator(string oprator, decimal digit, decimal result)
        {
            switch (oprator)
            {
                case "+":
                    result = result += digit;
                    break;

                case "-":
                    result = result -= digit;
                    break;

                case "*":
                    result = result *= digit;
                    break;

                case "/":
                    result = result /= digit;
                    break;

                default:
                    break;
            }
            return result;
        }

        private static bool IsOperator(string paramOperator)
        {
            List<string> operatorList = new List<string> { "+", "-", "*", "/" };

            return operatorList.Contains(paramOperator);
        }

        private static bool IsOperand(string operand)
        {
            return Decimal.TryParse(operand, out decimal parsedOperand);
        }
    }
}
