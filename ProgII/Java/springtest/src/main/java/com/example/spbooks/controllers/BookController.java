package com.example.spbooks.controllers;

import javax.validation.Valid;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.validation.BindingResult;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseBody;

import com.example.spbooks.models.Book;
import com.example.spbooks.models.BookRepository;
import com.example.spbooks.models.Student;

@Controller
@RequestMapping(path="/book")
public class BookController {
	@Autowired
	private BookRepository books;
	
	@GetMapping(path="/listjson")
	public @ResponseBody Iterable<Book> getBookList()
	{
		return books.findAll();
	}
	
	@GetMapping(path="/testdata")
	public @ResponseBody String seedData()
	{
		books.deleteAll();		
		
		books.save(new Book(0, "The C++ Programming Language","Addison-Wesley",
				"978-0321563842","Bjarne Stroustrup",4,2013));
		books.save(new Book(0, "Java in a Nutshell","O'Reilly",
				"978-1449370824","Benjamin J. Evans and David Flanagan",6,2015));
		
		return "Added test data";
	}
	
	@GetMapping(path="/list")
	public String viewBooksList(Model model)
	{
		model.addAttribute("books",books.findAll());
		model.addAttribute("PageTitle", "Books");
		model.addAttribute("BodyTemplate","books");
		
		return "shared/layout";
	}
	
	@GetMapping(path="/delete")
	public @ResponseBody String deleteBook(@RequestParam int ID, Model model)
	{
		books.deleteById(ID);
		return "OK";
	}
	
	@PostMapping(path="/update")
	public @ResponseBody String editBook(@Valid Book book, BindingResult binding)
	{
		if(binding.hasErrors())	{ return "Error"; }
		
		Book b = books.findById(book.getID()).get();
		
		b.setTitle(book.getTitle());
		b.setPublisher(book.getPublisher());
		b.setAuthor(book.getAuthor());
		b.setISBN(book.getISBN());
		b.setYearPublished(book.getYearPublished());
		b.setEdition(book.getEdition());
		
		books.save(b);
		return "OK";
	}
	
	@PostMapping(path="/create")
	public @ResponseBody String addBook(@Valid Book book, BindingResult binding)
	{
		if(binding.hasErrors())	{ return "Error"; }
		
		books.save(book);
		return "OK";
	}
}
