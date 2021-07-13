package com.example.spbooks.controllers;

import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.*;

@Controller
public class HomeController {
	@RequestMapping(path="/")
	public String index(Model model)
	{
		model.addAttribute("PageTitle", "Home page");
		model.addAttribute("BodyTemplate","../static/index");
		return "shared/layout";
	}
}
