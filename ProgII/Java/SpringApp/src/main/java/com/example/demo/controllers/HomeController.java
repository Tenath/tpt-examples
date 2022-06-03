package com.example.demo.controllers;

import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.RequestMapping;

@Controller
public class HomeController {
	@RequestMapping(path="/")
	public String index(Model model)
	{
		model.addAttribute("PageTitle","SpringApp home page");
		model.addAttribute("BodyTemplate", "../static/index");
		return "layout";
	}
}
