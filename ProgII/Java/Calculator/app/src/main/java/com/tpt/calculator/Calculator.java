package com.tpt.calculator;

public class Calculator {
    public enum CalcOp
    {
        Add,
        Sub,
        Mul,
        Div,
        Mod,
        None // специальное значение на случай, когда операция ещё не выбрана
    }

    private String Value = "0";
    private String OldValue = "";
    private String Memory = "";
    private CalcOp Operation = CalcOp.None;

    public void push(String btnSymbol)
    {
        switch(btnSymbol)
        {
            case "0":
            case "1":
            case "2":
            case "3":
            case "4":
            case "5":
            case "6":
            case "7":
            case "8":
            case "9":
                handleNumber(btnSymbol); break;
            case "C":
                handleReset(); break;
            // Самостоятельно: . (точка), <- (backspace), +/- (смена знака)
            case ".":
                handleDot(); break;
            case "<-":
                handleBackspace(); break;
            case "+/-":
                handleSign(); break;
            case "=":
                handleEquals(); break;

            // Кто справится, можно реализовать операции
            case "+":
                handleOp(CalcOp.Add); break;
            case "-":
                handleOp(CalcOp.Sub); break;
            case "*":
                handleOp(CalcOp.Mul); break;
            case "/":
                handleOp(CalcOp.Div); break;
            case "%":
                handleOp(CalcOp.Mod); break;
            case "MC":
            case "MR":
            case "MS":
                handleMemory(btnSymbol);
                break;

            // При выборе операции после второго числа сразу считаем результат
            // и ставим его в качестве первого числа (т.е. предыдущего значения)

            // Если все текущие операции реализованы, добавить также функции
            // памяти (MC,MR,M+,M-,MS)

            // Доп. задание: сделать unit-тесты
            // (см папку  com.example.calculator (test) и класс ExampleUnitTest)
        }
    }

    public void handleMemory(String cmd)
    {
        switch (cmd)
        {
            case "MC":
                Memory=""; break;
            case "MR":
                if(Memory.length() > 0) Value = Memory;
                break;
            case "MS":
                Memory = Value; break;
        }
    }

    public void handleNumber(String n)
    {
        if(Value.equals("0")) { Value=n; }
        else { Value+=n; }
    }

    public void handleReset()
    {
        Value = "0";
        OldValue = "";
        Operation = CalcOp.None;
    }

    public void handleDot()
    {
        if(!Value.contains(".")) { Value += "."; }
    }

    public void handleBackspace()
    {
        if(!Value.equals("0"))
        {
            if(Value.length() == 1) Value = "0";
            else if(Value.contains("-") && Value.length() == 2) Value = "0";
            else Value = Value.substring(0,Value.length()-1);
        }
    }

    public void handleSign()
    {
        if(Value.contains("-"))
        {
            Value = Value.substring(1,Value.length());
        }
        else if(Value.equals("0")) return;
        else
        {
            Value = "-" + Value;
        }
    }

    public void handleOp(CalcOp op)
    {
        OldValue = Value;
        Value = "0";
        Operation = op;
    }

    public void handleEquals()
    {
        double value2 = Double.parseDouble(Value);
        double value = Double.parseDouble(OldValue);
        double result = 0.0;

        switch(Operation)
        {
            case Add: result = value+value2; break;
            case Sub: result = value-value2; break;
            case Mul: result = value*value2; break;
            case Div: result = value/value2; break;
            case Mod: result = value%value2; break;
            case None: return;
        }

        Value = Double.toString(result);
    }

    public String getValue() { return Value; }
}
