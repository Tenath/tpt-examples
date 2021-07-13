package com.tpt.calculator;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

public class MainActivity extends AppCompatActivity {
    Calculator calc;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        calc = new Calculator();
    }

    public void onBtnClick(View view)
    {
        Button btn = (Button)view;
        String txt = btn.getText().toString();

        calc.push(txt);

        TextView tv = findViewById(R.id.calcValue);
        tv.setText(calc.getValue());
    }
}