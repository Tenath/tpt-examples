package com.example.demo.controllers;

import java.time.LocalDate;
//import java.time.format.DateTimeFormatter;
import java.util.ArrayList;

import javax.validation.Valid;

import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.validation.BindingResult;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseBody;
import org.springframework.web.server.ResponseStatusException;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import com.example.demo.models.Employee;
import com.example.demo.models.EmployeeListRepository;
import com.example.demo.models.EmployeeRepository;

@Controller
@RequestMapping(path = "/employee")
public class EmployeeController {
	//ArrayList<Employee> data = new ArrayList<Employee>();
	//EmployeeRepository employees = new EmployeeListRepository();
	@Autowired
	private EmployeeRepository employees;
	
	@GetMapping(path="/hello")
	@ResponseBody
	public String hello()
	{
		return "Hello world!";
	}
	
	/*@RequestMapping(path="/")
	public String index(Model model)
	{
		model.addAttribute("PageTitle","SpringApp home page");
		model.addAttribute("BodyTemplate", "../static/index");
		return "layout";
	}*/
	
	@GetMapping(path="/testdata")
	@ResponseBody
	public String test_data()
	{		
		if(employees.count() == 0)
		{			
			employees.save(new Employee(0, "Vasya","Pupkin",LocalDate.of(1990, 1, 1), 1000.0));
			employees.save(new Employee(0, "Masha","Pupkina",LocalDate.of(1993, 4, 6), 1200.0));
			employees.save(new Employee(0, "Petya","Ivanov",LocalDate.of(1992, 9, 30), 1400.0));
		}
		
		return "Test data added";
	}
	
	@GetMapping(path="/listjson")
	@ResponseBody
	public Iterable<Employee> get_employees()
	{
		return employees.findAll();
	}
	
	@GetMapping(path="/list")
	public String get_employee_table(Model model)
	{
		model.addAttribute("PageTitle","Employee list");
		model.addAttribute("BodyTemplate", "employeelist");
		model.addAttribute("employees",employees.findAll());
		return "layout";
	}
	
	@GetMapping(path="/edit")
	public String editEmployee(@RequestParam int id, Model model)
	{
		Employee e = employees.findById(id).get();
		
		model.addAttribute("PageTitle","Edit employee");
		model.addAttribute("BodyTemplate", "employee_edit");
		model.addAttribute("employee", e);
		return "layout";
	}
	
	@GetMapping(path="/add")
	public String addEmployee(Model model)
	{
		model.addAttribute("PageTitle","Add employee");
		model.addAttribute("BodyTemplate", "employee_add");
		model.addAttribute("employee",new Employee());
		return "layout";
	}
	
	@PostMapping(path="/editCommit")
	public String editEmployeeCommit(@Valid Employee employee, BindingResult binding)
	{
		if(binding.hasErrors())
		{
			return "redirect:edit";
		}
		
		Employee existing_employee = employees.findById(employee.getID()).get();
		
		existing_employee.setFirstName(employee.getFirstName());
		existing_employee.setLastName(employee.getLastName());
		existing_employee.setBirthday(employee.getBirthday());
		existing_employee.setSalary(employee.getSalary());
		
		employees.save(existing_employee);
		return "redirect:list";
	}
	
	@PostMapping(path="/addCommit")
	public String addEmployeeCommit(Employee employee, BindingResult binding)
	{
		if(binding.hasErrors())
		{
			return "redirect:add";
		}

		employees.save(employee);
		return "redirect:list";
	}
	
	@GetMapping(path="/delete")
	public String deleteEmployee(@RequestParam int id, Model model)
	{
		employees.deleteById(id);
		return "redirect:list";
	}
	
	// Задание: сгенерировать преставление данных в виде XML (пока что обойтись)
	// известными вам средствами
	@GetMapping(path="/listxml", produces="application/xml")
	@ResponseBody
	public String get_employees_xml()
	{
		// ...
		String xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n";
		xml+="<Employees>\n";
		//xml+="<Employees id=\""+id+"\">\n";
		// foreach
		/*for(Employee e : data)
		{
			xml+="\t<Employee>\n";
			xml+=(e.getID()+"1234");
		}
		
		for(int i=0; i<data.size(); i++)
		{
			Employee e = data.get(i);
		}*/
		
		xml+="</Employees>";
		
		return xml;
	}
	
	@GetMapping(path="/getEmployee")
	@ResponseBody
	public Employee getEmployee(@RequestParam int ID, @RequestParam String firstname)
	{
		Employee found = employees.findById(ID).get();
		
		if(found == null)
		{
			throw new ResponseStatusException(HttpStatus.NOT_FOUND);
		}
		
		return found;
		
		//return "redirect:listjson";
	}
	
	// Задание 2: Добавить метод addEmployee, который принимает в качестве параметров
	// данные работника, и добавляет запись в data
	
	/*111public String addEmployee(String date)
	{
		LocalDate birthday = LocalDate.parse(date, DateTimeFormatter.ofPattern("dd.MM.yyyy"));
	}*/
	// Задание 2.5: При добавлении присваивать ID автоматически (найти максимальный ID
	// у текущих работников, и добавить +1)
	
	// http://localhost:8080/employee/getEmployee?ID=1&FirstName=Vasya&LastName=Pupkin
}
