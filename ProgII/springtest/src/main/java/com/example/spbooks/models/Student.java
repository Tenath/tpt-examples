package com.example.spbooks.models;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.validation.constraints.Max;
import javax.validation.constraints.Min;
import javax.validation.constraints.Size;

import com.sun.istack.NotNull;

import lombok.AllArgsConstructor;
import lombok.Data;

// Задание 1:
// Сделать модель Book c полями: ID, Title, Publisher, Author, Year, Edition
// Сделать под неё контроллер с методом list
// Отобразить данные как несколько последовательных ненумерованных списков <ul>

@Entity
@Data
@AllArgsConstructor
public class Student {
	@Id
	@GeneratedValue(strategy=GenerationType.AUTO)
	private int ID;
	@NotNull
	@Size(min=1,max=40)
	private String FirstName;
	@NotNull
	@Size(min=1,max=40)
	private String LastName;
	@NotNull
	@Size(min=1,max=10)
	private String Gruppa;
	
	@NotNull
	@Min(1)
	@Max(5)
	private int MathScore;
	
	@NotNull
	@Min(1)
	@Max(5)
	private int PhysicsScore;
	
	public Student() {}
}
