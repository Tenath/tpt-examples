package com.example.spbooks;

import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.security.config.annotation.web.builders.HttpSecurity;
import org.springframework.security.config.annotation.web.configuration.EnableWebSecurity;
import org.springframework.security.config.annotation.web.configuration.WebSecurityConfigurerAdapter;
import org.springframework.security.core.userdetails.User;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.security.provisioning.InMemoryUserDetailsManager;

@Configuration
@EnableWebSecurity
public class WebSecurityConfig extends WebSecurityConfigurerAdapter {
	@Override
	protected void configure(HttpSecurity http) throws Exception
	{
		http.csrf().disable().authorizeRequests().antMatchers("/student*/**", "/book*/**").authenticated()
		.anyRequest().permitAll().and().formLogin().permitAll().and().logout().permitAll();
		
		http.httpBasic();
	}
	
	@Bean
	@Override
	public UserDetailsService userDetailsService()
	{
		UserDetails user = User.withDefaultPasswordEncoder()
				.username("opilane")
				.password("Passw0rd")
				.roles("admin")
				.build();
		
		return new InMemoryUserDetailsManager(user);
	}
}
