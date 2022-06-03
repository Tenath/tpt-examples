package com.example.demo.models;

import java.time.LocalDate;

import javax.persistence.*;
import javax.validation.constraints.NotNull;
import javax.validation.constraints.Size;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@AllArgsConstructor
@NoArgsConstructor
@Entity
public class Employee {
	@Id
	@GeneratedValue(strategy=GenerationType.AUTO)
	private int ID;
	
	@NotNull
	@Size(max=40)
	protected String FirstName;
	
	@NotNull
	@Size(max=60)
	protected String LastName;
	
	@NotNull
	protected LocalDate Birthday;
	
	protected double Salary;
}
