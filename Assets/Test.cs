using KgmSlem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Test : MonoBehaviour
{
    private static readonly string[] operators = { "+", "-", "*", "/" };

    private string Operator => operators[Random.Range(0, operators.Length)];
    private int Operand => Random.Range(_operandRange.x, _operandRange.y);
    private int OpCount => _round / 2 + 1;

    [SerializeField] Vector2Int _operandRange;
    [SerializeField] private int _round;
    private int _curRound;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            string s = Operand.ToString();
            Generate(Random.Range(2, 5), ref s);
            Debug.Log($"{s} = {new System.Data.DataTable().Compute(s, null)}");
        }
    }

    private void Generate(int count, ref string operand)
    {
        if (count <= 0) return;

        string op = Operator;

        switch (op)
        {
            case "+":
                {
                    int l = Operand;
                    string ls = l.ToString();
                    Generate(--count, ref ls);
                    operand = "(" + ls + op + (int.Parse(operand) - l) + ")";
                    break;
                }

            case "-":
                {
                    int l = Operand;
                    string ls = l.ToString();
                    Generate(--count, ref ls);
                    operand = "(" + ls + op + (l - int.Parse(operand)) + ")";
                    break;
                }

            case "*":
                {
                    List<int> operands = new List<int>();

                    int result = int.Parse(operand);

                    for (int j = (result == 1 ? 1 : 2); j <= result; j++)
                    {
                        if (result % j == 0) operands.Add(j);
                    }

                    int l = operands[Random.Range(0, operands.Count)];
                    string ls = l.ToString();
                    Generate(--count, ref ls);
                    operand = ls + op + (int.Parse(operand) / l);
                    break;
                }

            case "/":
                {
                    int r = Operand;
                    string rs = r.ToString();
                    Generate(--count, ref rs);
                    operand = "(" + (int.Parse(operand) * r) + op + rs + ")";
                    break;
                }
        }
    }

    //private void Legacy()
    //{
    //        string s = $"{Operand}";

    //        int count = Random.Range(1, 10);

    //        for (int i = 0; i < count; i++)
    //        {
    //            string op = Operator;

    //            switch (op)
    //            {
    //                case "+":
    //                case "-":
    //                    s = "(" + s + op + Operand + ")";
    //                    break;

    //                case "*":
    //                    s = s + op + Operand;
    //                    break;

    //                case "/":
    //                    List<int> operands = new List<int>();

    //                    int result = int.Parse(new System.Data.DataTable().Compute(s, null).ToString());

    //                    for (int j = (result == 1 ? 1 : 2); j <= result; j++)
    //                    {
    //                        if (result % j == 0) operands.Add(j);
    //                    }

    //                    s = s + op + operands[Random.Range(0, operands.Count)];
    //                    break;
    //            }
    //        }

    //        Debug.Log($"{s} = {new System.Data.DataTable().Compute(s, null)}");
    //}
}
