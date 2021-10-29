using System;
using System.Collections.Generic;

namespace CalculatorNS
{
    public static class Calculator
    {
        public static decimal Calculate(string input)
        {
            decimal result = 0;
            var array = input.Split(" ");
            var currentOperator = "";
            int openBracketCount = 0;
            int closeBracketCount = 0;
            string insideBracketExpression = "";

            for (int i = 0; i < array.Length; i++)
            {
                // If not inside bracket, do calculation
                if (openBracketCount == 0)
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

                if (array[i] == "(")
                {
                    openBracketCount += 1;

                    // If 1 openBracket, dun add ( into insideBracketExpression
                    if (openBracketCount == 1) { continue; }
                }

                if (array[i] == ")")
                {
                    closeBracketCount += 1;
                    // If openBracketCount == closeBracketCount, then run recursion to calculate
                    if (openBracketCount == closeBracketCount)
                    {
                        decimal innerBracketResult = Calculate(insideBracketExpression);

                        // If else to Handle if bracket hits before any operator is assigned
                        result = currentOperator == "" ?
                            innerBracketResult :
                            CalculateWithOperator(currentOperator, innerBracketResult, result);

                        // Remove bracket count since out of bracket now
                        openBracketCount -= 1;   
                        closeBracketCount -= 1;
                        // Have to clear expression to handle multiple seperate nested expression
                        insideBracketExpression = "";   
                    }
                }

                // If inside bracket, add expression to insideBracketExpression
                if (openBracketCount != 0)
                {
                    insideBracketExpression += " " + array[i];
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
