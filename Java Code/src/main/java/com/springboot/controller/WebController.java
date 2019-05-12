package com.springboot.controller;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileReader;
import java.io.IOException;
import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import com.springboot.model.Weather;
import com.springboot.repository.DynamoDbRepository;

@RestController
@RequestMapping("/dynamoDb")
public class WebController {
	
	@Autowired
	private DynamoDbRepository repository;
	
	private void readFile(String fileName) throws IOException {
		FileReader fr = new FileReader(fileName);
		BufferedReader br = new BufferedReader(fr);
		
		String str;
		
		while((str=br.readLine())!=null)
		{
			if(!str.isEmpty()) {
				Weather w = new Weather();
				w.setTemperature(Double.parseDouble(str));
				repository.insertIntoDynamoDB(w);
			}
		}
		
		br.close();
	}
	
	private void readDir() throws IOException {
		File folder = new File("/Users/vishakhasupekar/Downloads/logs/");
		File[] listOfFiles = folder.listFiles();

		for (File file : listOfFiles) {
		    if (file.isFile()) {
		    	
		        this.readFile(file.getAbsolutePath());
		    }
		}
	}
	
	@PostMapping
	public String insertIntoDynamoDB(@RequestBody Weather weather) {
		repository.insertIntoDynamoDB(weather);
		return "Successfully inserted into DynamoDB table";
	}
	
	@GetMapping
	@RequestMapping("/all")
	public List<Weather> getAll() {
		return repository.getAll();
	}
	
	@GetMapping
	@RequestMapping("/processData")
	public String processData() throws IOException {
		this.readDir();
		return "Successfully Processed Data";
	}
	
}
