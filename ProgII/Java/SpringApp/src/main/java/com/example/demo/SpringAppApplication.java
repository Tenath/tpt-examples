package com.example.demo;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.boot.autoconfigure.jdbc.DataSourceAutoConfiguration;

// MVC
// Model - данные + логика работы с ними
// View - представление, интерфейс пользователя
// Controller - бизнес-логика приложения 

/*@SpringBootApplication(exclude = {
	DataSourceAutoConfiguration.class
})*/
@SpringBootApplication
public class SpringAppApplication {

	public static void main(String[] args) {
		SpringApplication.run(SpringAppApplication.class, args);
	}

}
