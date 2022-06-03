package com.company;

import java.security.InvalidParameterException;
import java.time.LocalDate;

public class Employee {
    private int ID;
    protected String FirstName;
    protected String LastName;
    protected LocalDate Birthday;
    protected double Salary;

    private static int NextID = 1;

    public Employee(String first_name, String last_name, LocalDate birthday)
    {
        ID = NextID++;
        setFirstName(first_name);
        setLastName(last_name);
        Birthday = birthday;
        Salary = 0.0;
    }

    public int getID() { return ID; }
    public String getFirstName() { return FirstName; }
    public String getLastName() { return LastName; }
    public LocalDate getBirthday() { return Birthday; }

    public void setFirstName(String first_name)
    {
        if(first_name.length() == 0) { throw new InvalidParameterException("Empty first name parameter"); }
        FirstName = first_name;
    }

    public void setLastName(String last_name)
    {
        if(last_name.length() == 0) { throw new InvalidParameterException("Empty last name parameter"); }
        LastName = last_name;
    }

    public void setBirthday(LocalDate birthday)
    {
        Birthday = birthday;
    }

    public double getSalary() { return Salary; }
    public void setSalary(double salary)
    {
        if(salary < 0 || !Double.isFinite(salary))
            throw new InvalidParameterException("Invalid salary argument");

        Salary = salary;
    }
}
