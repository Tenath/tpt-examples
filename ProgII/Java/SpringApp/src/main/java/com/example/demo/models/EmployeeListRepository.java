package com.example.demo.models;

import java.util.ArrayList;
import java.util.Optional;

public class EmployeeListRepository implements EmployeeRepository {
	private ArrayList<Employee> data = new ArrayList<Employee>();

	private int findMaxId()
	{
		int max = Integer.MIN_VALUE;
		
		for(Employee e : data)
		{
			if(e.getID() > max)
			{
				max = e.getID();
			}
		}
		
		if(max < 0) max = 1;
		
		return max;
	}
	
	@Override
	public <S extends Employee> S save(S entity) {
		// Ищем следующий доступный идентификатор (максимальный текущий ID + 1)
		int nextId = findMaxId() + 1;
		// Выставляем найденное значение в качестве ID у добавляемой сущности
		entity.setID(nextId);
		// Добавляем сущность в список объектов
		data.add(entity);
		// Возвращаем саму же сущность
		return entity;
	}

	@Override
	public <S extends Employee> Iterable<S> saveAll(Iterable<S> entities) {
		for(S entity : entities)
		{
			save(entity);
		}
		
		return entities;
	}

	@Override
	public Optional<Employee> findById(Integer id) {
		Employee found = null;
		for(Employee e : data)
		{
			if(e.getID() == id)
			{
				found = e;
				break;
			}
		}
		
		return Optional.ofNullable(found);
	}

	@Override
	public boolean existsById(Integer id) {
		/*Employee found = null;
		for(Employee e : data)
		{
			if(e.getID() == id)
			{
				found = e;
				break;
			}
		}
		
		return found != null;*/
		return !(findById(id).isEmpty());
	}

	@Override
	public Iterable<Employee> findAll() {
		return data;
	}

	@Override
	public Iterable<Employee> findAllById(Iterable<Integer> ids) {
		
		return null;
	}

	@Override
	public long count() {
		return data.size();
	}

	@Override
	public void deleteById(Integer id) {
		// TODO Auto-generated method stub

	}

	@Override
	public void delete(Employee entity) {
		// TODO Auto-generated method stub

	}

	@Override
	public void deleteAllById(Iterable<? extends Integer> ids) {
		// TODO Auto-generated method stub

	}

	@Override
	public void deleteAll(Iterable<? extends Employee> entities) {
		// TODO Auto-generated method stub

	}

	@Override
	public void deleteAll() {
		// TODO Auto-generated method stub

	}

}
