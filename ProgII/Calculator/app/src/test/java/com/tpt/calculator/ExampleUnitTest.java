package com.tpt.calculator;

import org.junit.Test;

import static org.junit.Assert.*;

/**
 * Example local unit test, which will execute on the development machine (host).
 *
 * @see <a href="http://d.android.com/tools/testing">Testing documentation</a>
 */
public class ExampleUnitTest {
    @Test
    public void addition_isCorrect() {
        assertEquals(4, 2 + 2);
    }

    @Test
    public void TestAdd()
    {
        Calculator c = new Calculator();
        c.push("1");
        c.push("+");
        c.push("2");
        c.push("=");

        assertEquals("3.0", c.getValue());
    }
}