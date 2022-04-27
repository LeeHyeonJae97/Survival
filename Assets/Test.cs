using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Test : MonoBehaviour
{
    private static readonly string[] operators = { "+", "-", "*", "/" };

    [SerializeField] Vector2Int _operandRange;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            string s = GetOperand().ToString();
            GenerateQuiz(Random.Range(2, 5), ref s);
            Debug.Log($"{s} = {RemoveRedundantBracket(s)} = {new System.Data.DataTable().Compute(s, null)}");
        }
    }

    private string GetOperator(int result)
    {
        return operators[Random.Range(result == 1 ? 1 : 0, operators.Length)];
    }

    private int GetOperand()
    {
        return Random.Range(_operandRange.x, _operandRange.y);
    }

    private void GenerateQuiz(int count, ref string operand)
    {
        if (count <= 0) return;

        // get operator with constraint about result
        int result = int.Parse(operand);
        string op = GetOperator(result);

        // get operands by operator
        switch (op)
        {
            case "+":
                {
                    int l = Random.Range(_operandRange.x, result);
                    string ls = l.ToString();
                    GenerateQuiz(--count, ref ls);
                    operand = "(" + ls + op + (result - l) + ")";
                    break;
                }

            case "-":
                {
                    int l = Random.Range(result + 1, _operandRange.y + 1);
                    string ls = l.ToString();
                    GenerateQuiz(--count, ref ls);
                    operand = "(" + ls + op + (l - result) + ")";
                    break;
                }

            case "*":
                {
                    List<int> operands = new List<int>();

                    for (int i = (result == 1 ? 1 : 2); i <= result; i++)
                    {
                        if (result % i == 0) operands.Add(i);
                    }

                    int l = operands[Random.Range(0, operands.Count)];
                    string ls = l.ToString();
                    GenerateQuiz(--count, ref ls);
                    operand = ls + op + (int.Parse(operand) / l);
                    break;
                }

            case "/":
                {
                    List<int> operands = new List<int>();

                    for (int i = _operandRange.x; i <= _operandRange.y; i++)
                    {
                        if (result * i <= _operandRange.y)
                        {
                            operands.Add(i);
                        }
                        else
                        {
                            break;
                        }
                    }

                    int r = operands[Random.Range(0, operands.Count)];
                    string rs = r.ToString();
                    GenerateQuiz(--count, ref rs);
                    operand = "(" + (int.Parse(operand) * r) + op + rs + ")";
                    break;
                }
        }
    }

    private string RemoveRedundantBracket(string s)
    {
        Stack<int> stack = new Stack<int>();

        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == '(')
            {
                stack.Push(i);
            }
            else if (s[i] == ')')
            {
                int index = stack.Pop();

                string removed = s.Remove(i, 1);
                removed = removed.Remove(index, 1);

                var dt = new System.Data.DataTable();

                if (dt.Compute(s, null).Equals(dt.Compute(removed, null)))
                {
                    s = removed;
                    i--;
                }
            }
        }
        return s;
    }
}
