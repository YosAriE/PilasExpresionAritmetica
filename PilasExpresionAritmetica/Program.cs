using System;
using System.Collections.Generic;

class Program
{
    static int Precedence(char op)
    {
        switch (op)
        {
            case '+':
            case '-':
                return 1;
            case '*':
            case '/':
                return 2;
            case '^':
                return 3;
            default:
                return -1;
        }
    }

    static string InfixToPostfix(string expression)
    {
        string result = "";
        Stack<char> stack = new Stack<char>();

        foreach (char c in expression)
        {
            if (Char.IsDigit(c) || Char.IsLetter(c))
            {
                result += c;
            }
            else if (c == '(')
            {
                stack.Push(c);
            }
            else if (c == ')')
            {
                while (stack.Count > 0 && stack.Peek() != '(')
                {
                    result += stack.Pop();
                }
                if (stack.Count > 0 && stack.Peek() == '(')
                {
                    stack.Pop();
                }
            }
            else
            {
                while (stack.Count > 0 && Precedence(c) <= Precedence(stack.Peek()))
                {
                    result += stack.Pop();
                }
                stack.Push(c);
            }
        }

        while (stack.Count > 0)
        {
            result += stack.Pop();
        }

        return result;
    }

    static int EvaluatePostfix(string expression)
    {
        Stack<int> stack = new Stack<int>();

        foreach (char c in expression)
        {
            if (Char.IsDigit(c))
            {
                stack.Push(int.Parse(c.ToString()));
            }
            else
            {
                int operand2 = stack.Pop();
                int operand1 = stack.Pop();

                switch (c)
                {
                    case '+':
                        stack.Push(operand1 + operand2);
                        break;
                    case '-':
                        stack.Push(operand1 - operand2);
                        break;
                    case '*':
                        stack.Push(operand1 * operand2);
                        break;
                    case '/':
                        stack.Push(operand1 / operand2);
                        break;
                    case '^':
                        stack.Push((int)Math.Pow(operand1, operand2));
                        break;
                }
            }
        }

        return stack.Pop();
    }

    static void Main()
    {
        Console.WriteLine("Ingrese una expresión aritmética:");
        string infixExpression = Console.ReadLine();
        string postfixExpression = InfixToPostfix(infixExpression);
        int result = EvaluatePostfix(postfixExpression);

        Console.WriteLine("Expresión Infija: " + infixExpression);
        Console.WriteLine("Expresión Posfija: " + postfixExpression);
        Console.WriteLine("Resultado: " + result);
    }
}
