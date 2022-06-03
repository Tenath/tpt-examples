package com.example.spbooks;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.web.bind.annotation.*;

// MVC
// Model - данные + логика работы с ними
// View - представление, интерфейс пользователя
// Controller - бизнес-логика приложения

@SpringBootApplication
@RestController
public class SpringtestApplication {
	public static void main(String[] args) {
		SpringApplication.run(SpringtestApplication.class, args);
	}

	@GetMapping("/hello")
	public String hello(@RequestParam(defaultValue = "KTA-19V")  String name)
	{
		return String.format("Hello %s",name);
	}
}
