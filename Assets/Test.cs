using KgmSlem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Test : MonoBehaviour
{
    private string[] operators = { "+", "-", "*", "/" };

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            string s = "";

            int count = Random.Range(1, 10);

            for (int i = 0; i < count; i++)
            {
                if (i == 0)
                {
                    s = Random.Range(0, 100) + operators[Random.Range(0, operators.Length)] + Random.Range(0, 100);
                }
                else
                {
                    string op = operators[Random.Range(0, operators.Length)];
                    int operand;

                    if (op.Equals("/"))
                    {
                        List<int> operands = new List<int>();

                        Debug.Log(new System.Data.DataTable().Compute(s, null).ToString());

                        int result = int.Parse(new System.Data.DataTable().Compute(s, null).ToString());
                        float sqrt = Mathf.Sqrt(result);

                        for (int j = 1; j < sqrt; j++)
                        {
                            if (result % j == 0) operands.Add(j);
                        }

                        operand = operands[Random.Range(0, operands.Count)];
                    }
                    else
                    {
                        operand = Random.Range(0, 100);
                    }

                    bool left = Random.Range(0, 2) == 0;

                    if (left)
                    {
                        s = operand + op + s;
                    }
                    else
                    {
                        s = s + op + operand;
                    }
                }
            }

            Debug.Log($"{s} = {new System.Data.DataTable().Compute(s, null)}");
        }
    }
}
