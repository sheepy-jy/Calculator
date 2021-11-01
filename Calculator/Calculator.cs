using System;
using System.Collections.Generic;

namespace CalculatorNS
{
    public static class Calculator
    {
        public static decimal Calculate(string input)
        {
            string[] array = input.Split(" ");
            var currentOperator = "";
            decimal finalResult = 0;

            int indexOpenBracket = Array.LastIndexOf(array, "(");
            if (indexOpenBracket != -1)
            {
                // Specify start index to handle multiple but not nested brackets
                int indexCloseBracket = Array.IndexOf(array, ")", indexOpenBracket);

                string insideBracket = "";
                for (int i = indexOpenBracket + 1; i < indexCloseBracket; i++)
                {
                    insideBracket += array[i] + (i + 1 == indexCloseBracket ? "" : " ");
                }
                decimal bracketResult = Calculate(insideBracket);
                string calculatedPart = "( " + insideBracket + " )";
                string newInput = input.Replace(calculatedPart, bracketResult.ToString());

                return Calculate(newInput);
            }

            int indexMultiply = Array.IndexOf(array, "*");
            if (indexMultiply != -1)
            {
                decimal firstArg = Convert.ToDecimal(array[indexMultiply - 1]);
                decimal secondArg = Convert.ToDecimal(array[indexMultiply + 1]);

                decimal result = firstArg * secondArg;
                string sumParts = firstArg.ToString() + " * " + secondArg.ToString();
                string newInput = input.Replace(sumParts, result.ToString());

                return Calculate(newInput);
            }

            int indexDivide = Array.IndexOf(array, "/");
            if (indexDivide != -1)
            {
                decimal firstArg = Convert.ToDecimal(array[indexDivide - 1]);
                decimal secondArg = Convert.ToDecimal(array[indexDivide + 1]);

                decimal divideResult = firstArg / secondArg;
                string sumParts = firstArg.ToString() + " / " + secondArg.ToString();
                string newInput = input.Replace(sumParts, divideResult.ToString());

                return Calculate(newInput);
            }

            // Calculation after all brackets & multiply/divide is solved
            for (int i = 0; i < array.Length; i++)
            {
                if (finalResult == 0 && currentOperator == string.Empty && decimal.TryParse(array[i], out decimal parsedDecimal))
                {
                    finalResult = parsedDecimal;
                    continue;
                }
                if (IsOperator(array[i]))
                {
                    currentOperator = array[i];
                    continue;
                }
                if (IsOperand(array[i]) && currentOperator != string.Empty)
                {
                    finalResult = CalculateWithOperator(currentOperator, Convert.ToDecimal(array[i]), finalResult);
                    continue;
                }
            }

            return finalResult;
        }

        //public static decimal Calculate(string input)
        //{
        //    decimal result = 0;
        //    bool isFirst = true;
        //    var array = input.Split(" ");
        //    var currentOperator = "";
        //    int openBracketCount = 0;
        //    int closeBracketCount = 0;
        //    string insideBracketExpression = "";

        //    for (int i = 0; i < array.Length; i++)
        //    {
        //        // If not inside bracket, do calculation
        //        if (openBracketCount == 0)
        //        {
        //            if (result == 0 && currentOperator == string.Empty && decimal.TryParse(array[i], out decimal parsedDouble))
        //            {
        //                result = parsedDouble;
        //                isFirst = false;
        //                continue;
        //            }
        //            if (IsOperator(array[i]))
        //            {
        //                currentOperator = array[i];
        //                continue;
        //            }
        //            // Calculate
        //            if (IsOperand(array[i]) && !isFirst && currentOperator != string.Empty)
        //            {
        //                result = CalculateWithOperator(currentOperator, Convert.ToDecimal(array[i]), result);
        //                continue;
        //            }
        //        }

        //        if (array[i] == "(")
        //        {
        //            openBracketCount += 1;
        //            isFirst = false;

        //            // If not nested bracket, skip adding to insideBracketExpression
        //            if (openBracketCount == 1) { continue; }
        //        }

        //        if (array[i] == ")")
        //        {
        //            closeBracketCount += 1;
        //            // If openBracketCount == closeBracketCount, then run recursion to calculate
        //            if (openBracketCount == closeBracketCount)
        //            {
        //                decimal innerBracketResult = Calculate(insideBracketExpression);

        //                // If else to Handle if bracket hits before any operator is assigned
        //                result = currentOperator == "" ?
        //                    innerBracketResult :
        //                    CalculateWithOperator(currentOperator, innerBracketResult, result);

        //                // Remove bracket count since out of bracket now
        //                openBracketCount -= 1;   
        //                closeBracketCount -= 1;
        //                // Have to clear expression to handle multiple seperate nested expression
        //                insideBracketExpression = "";   
        //            }
        //        }

        //        // If inside bracket, add expression to insideBracketExpression
        //        if (openBracketCount != 0)
        //        {
        //            insideBracketExpression += " " + array[i];
        //        }
        //    }
        //    return result;
        //}

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
