package com.springboot.repository;

import java.util.ArrayList;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Repository;

import com.amazonaws.services.dynamodbv2.datamodeling.DynamoDBMapper;
import com.amazonaws.services.dynamodbv2.datamodeling.DynamoDBScanExpression;
import com.amazonaws.services.dynamodbv2.datamodeling.PaginatedScanList;
import com.springboot.model.Weather;


@Repository
public class DynamoDbRepository {

	//private static final Logger LOGGER = LoggerFactory.getLogger(DynamoDbRepository.class);

	@Autowired
	private DynamoDBMapper mapper;
	
	public void insertIntoDynamoDB(Weather weather) {
		mapper.save(weather);
		}
	
	public ArrayList<Weather> getAll() {
		PaginatedScanList<Weather> results = mapper.scan(Weather.class, new DynamoDBScanExpression());
		ArrayList<Weather> temps = new ArrayList<Weather>();
		results.forEach(w -> temps.add(w));
		return temps;
	}	
}
