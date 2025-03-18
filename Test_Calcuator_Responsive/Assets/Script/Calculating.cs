using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Calculating : MonoBehaviour
{
       public TMP_InputField display;  // Reference to the text field
    private string equation = ""; // Stores the full equation
    private List<string> tokens = new List<string>(); // Stores numbers and operators

    public void OnNumberClick(string number)
    {
        if (tokens.Count > 0 && float.TryParse(tokens[tokens.Count - 1], out _))
        {
            // Append to the last number if it's a number
            tokens[tokens.Count - 1] += number;
        }
        else
        {
            // Add a new number
            tokens.Add(number);
        }

        UpdateDisplay();
    }

    public void OnOperatorClick(string op)
    {
        if (tokens.Count > 0 && !IsOperator(tokens[tokens.Count - 1]))
        {
            tokens.Add(op);
        }
        UpdateDisplay();
    }

    public void OnEqualClick()
    {
        if (tokens.Count >= 3)
        {
            float result = EvaluateExpression(tokens);
            tokens.Clear();
            tokens.Add(result.ToString());
            display.text = result.ToString();
        }
    }

    public void OnClearClick()
    {
        tokens.Clear();
        display.text = "";
    }

    private void UpdateDisplay()
    {
        display.text = string.Join(" ", tokens);
    }

    private bool IsOperator(string value)
    {
        return value == "+" || value == "-" || value == "x" || value == "/";
    }

    private float EvaluateExpression(List<string> expressionTokens)
    {
        float result = float.Parse(expressionTokens[0]);

        for (int i = 1; i < expressionTokens.Count - 1; i += 2)
        {
            string op = expressionTokens[i];
            float nextNumber = float.Parse(expressionTokens[i + 1]);

            switch (op)
            {
                case "+": result += nextNumber; break;
                case "-": result -= nextNumber; break;
                case "x": result *= nextNumber; break;
                case "/": 
                    if (nextNumber != 0) result /= nextNumber;
                    else return 0; // Handle division by zero error
                    break;
            }
        }

        return result;
    }
}
