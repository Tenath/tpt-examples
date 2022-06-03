package com.example.spbooks.models;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.validation.constraints.Min;
import javax.validation.constraints.Size;

import com.sun.istack.NotNull;

import lombok.AllArgsConstructor;
import lombok.Data;

@Entity
@Data
@AllArgsConstructor
public class Book {
	@Id
	@GeneratedValue(strategy=GenerationType.AUTO)
	private int ID;
	@NotNull
	@Size(min=1,max=40)
	private String Title;
	
	@NotNull
	@Size(min=1,max=40)
	private String Publisher;
	
	@NotNull
	@Size(min=1,max=15)
	private String ISBN;
	
	@NotNull
	@Size(min=1,max=100)
	private String Author;
	
	@NotNull
	@Min(1)
	private int Edition;
	
	@NotNull
	@Min(1970)
	private int YearPublished;
	
	public Book() {}
}
