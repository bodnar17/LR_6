using System;

namespace ArithmeticCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введiть арифметичнi вирази, роздiленi символом ';':");
            string input = Console.ReadLine();
            string[] expressions = input.Split(';');

            foreach (string expression in expressions)
            {
                try
                {
                    double result = EvaluateExpression(expression);
                    Console.WriteLine($"Результат виразу '{expression}': {result}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Помилка обчислення виразу '{expression}': {ex.Message}");
                }
            }
        }

        static double EvaluateExpression(string expression)
        {
            string[] tokens = expression.Split(' ');
            double result = 0;
            string operation = "";
            bool expectingOperand = true;

            foreach (string token in tokens)
            {
                if (string.IsNullOrWhiteSpace(token))
                    continue;

                if (expectingOperand)
                {
                    if (double.TryParse(token, out double operand))
                    {
                        if (operation == "")
                            result = operand;
                        else
                        {
                            result = PerformOperation(result, operand, operation);
                            operation = "";
                        }
                    }
                    else
                    {
                        throw new Exception($"Невiдомий операнд: {token}");
                    }
                    expectingOperand = false;
                }
                else
                {
                    if (IsOperator(token))
                    {
                        operation = token;
                        expectingOperand = true;
                    }
                    else
                    {
                        throw new Exception($"Невiдомий оператор: {token}");
                    }
                }
            }

            return result;
        }

        static double PerformOperation(double leftOperand, double rightOperand, string operation)
        {
            double result = 0;

            switch (operation)
            {
                case "+":
                    result = leftOperand + rightOperand;
                    break;
                case "-":
                    result = leftOperand - rightOperand;
                    break;
                case "*":
                    result = leftOperand * rightOperand;
                    break;
                case "/":
                    result = leftOperand / rightOperand;
                    break;
                default:
                    throw new Exception($"Невiдома операцiя: {operation}");
            }

            return result;
        }

        static bool IsOperator(string token)
        {
            return token == "+" || token == "-" || token == "*" || token == "/";
        }
    }
}
