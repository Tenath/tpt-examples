package com.example.demo;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.boot.autoconfigure.jdbc.DataSourceAutoConfiguration;

// MVC
// Model - ������ + ������ ������ � ����
// View - �������������, ��������� ������������
// Controller - ������-������ ���������� 

/*@SpringBootApplication(exclude = {
	DataSourceAutoConfiguration.class
})*/
@SpringBootApplication
public class SpringAppApplication {

	public static void main(String[] args) {
		SpringApplication.run(SpringAppApplication.class, args);
	}

}
