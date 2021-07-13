package com.example.spbooks.controllers;

import java.util.ArrayList;
import java.util.Optional;

import javax.validation.Valid;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.validation.BindingResult;
import org.springframework.web.bind.annotation.*;

import com.example.spbooks.models.Student;
import com.example.spbooks.models.StudentRepository;

// Обед до 12:20

@Controller
@RequestMapping(path="/student")
public class StudentController {
	//private ArrayList<Student> students;
	
	@Autowired
	private StudentRepository students;
	
	/*public StudentController()
	{
		students = new ArrayList<Student>();
		
		students.add(new Student(1, "Vasya","Pupkin","5A",4,3));
		students.add(new Student(2, "Petya", "Ivanov", "7B",5,5));
		
		if(studentRepository.count() == 0)
		{
			studentRepository.save(new Student(1, "Vasya","Pupkin","5A",4,3));
			studentRepository.save(new Student(2, "Petya", "Ivanov", "7B",5,5));
		}
		Student s = new Student();
		s.setID(1);
		s.setFirstName("Vasya");
		s.setLastName("Pupkin");
		s.setGroup("5A");
		s.setMathScore(4);
		s.setPhysicsScore(3);
	}*/
	
	@GetMapping(path="/listjson")
	public @ResponseBody Iterable<Student> getStudentList()
	{
		return students.findAll();
	}
	
	// Без @ResponseBody возвращает View (обработанный шаблон)
	@GetMapping(path="/list")
	public String viewStudentList(Model model)
	{
		// Задание: Сделать новый метод (путь /testdata) для добавления тестовых данных
		// из viewStudentList() его убрать
		
		
		//model.addAttribute("firstName",students.get(0).getFirstName());
		//model.addAttribute("lastName",students.get(0).getLastName());
		model.addAttribute("students",students.findAll());
		model.addAttribute("PageTitle", "Students list");
		model.addAttribute("BodyTemplate","studentlist");
		
		return "shared/layout";
	}
	
	@GetMapping(path="/testdata")
	public @ResponseBody String seedData()
	{
		students.deleteAll();		
		
		students.save(new Student(0, "Vasya","Pupkin","5A",4,3));
		students.save(new Student(0, "Petya", "Ivanov", "7B",5,5));
		
		return "";
	}
	
	@GetMapping(path="/add")
	public String addStudent(Student student,Model model)
	{
		model.addAttribute("BodyTemplate","studentadd");
		return "shared/layout";
	}
	
	@PostMapping(path="/add")
	public String addStudentCommit(
			@Valid Student student, BindingResult binding
			)
	{
		//return (binding.hasErrors()) ? "Error" : student.getFirstName();
		if(binding.hasErrors())
		{
			return "redirect:add";
		}
		
		students.save(student);
		return "redirect:list";
	}
	
	// Обед до 12:15
	
	@GetMapping(path="/validate")
	public @ResponseBody String validateStudentInfo(@Valid Student student, BindingResult binding)
	{
		//return student.getFirstName();
		return (binding.hasErrors()) ? "Error" : "OK";
	}
	
	@GetMapping(path="/edit")
	public String editStudent(@RequestParam int ID, Model model)
	{
		Student s = students.findById(ID).get();
		
		model.addAttribute("BodyTemplate","studentedit");
		model.addAttribute("student", s);
		return "shared/layout";
	}
	
	@PostMapping(path="/edit")
	public String editStudentCommit(@Valid Student student, BindingResult binding)
	{
		if(binding.hasErrors())
		{
			return "redirect:edit";
		}
		
		Student s = students.findById(student.getID()).get();
		
		s.setFirstName(student.getFirstName());
		s.setLastName(student.getLastName());
		s.setGruppa(student.getGruppa());
		s.setMathScore(student.getMathScore());
		s.setPhysicsScore(student.getPhysicsScore());
		
		students.save(s);
		return "redirect:list";
	}
	
	@GetMapping(path="/delete")
	public String deleteStudent(@RequestParam int ID, Model model)
	{
		students.deleteById(ID);
		return "redirect:list";
	}
}
