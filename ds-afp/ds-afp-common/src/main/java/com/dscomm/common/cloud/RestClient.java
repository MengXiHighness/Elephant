package com.dscomm.common.cloud;

import java.util.Map;

import org.springframework.http.ResponseEntity;
import org.springframework.lang.Nullable;

public interface RestClient {

	<T> T getForObject(String url,String serviceName, Class<T> responseType);
	<T> T getForObject(String url,String serviceName, Class<T> responseType, Map<String, ?> uriVariables);
	<T> T getForObject(String url,String serviceName, Class<T> responseType, Object... uriVariables);
	
	<T> ResponseEntity<T> getForEntity(String url,String serviceName, Class<T> responseType, Object... uriVariables);
	<T> ResponseEntity<T> getForEntity(String url, String serviceName,Class<T> responseType, Map<String, ?> uriVariables);
	<T> T postForObject(String url,String serviceName, @Nullable Object request, Class<T> responseType,	Object... uriVariables);
	<T> T postForObject(String url,String serviceName, @Nullable Object request, Class<T> responseType,	Map<String, ?> uriVariables);
	<T> ResponseEntity<T> postForEntity(String url,String serviceName, @Nullable Object request,Class<T> responseType, Object... uriVariables) ;
	<T> ResponseEntity<T> postForEntity(String url,String serviceName, @Nullable Object request,Class<T> responseType, Map<String, ?> uriVariables);
	
	void put(String url,String serviceName, @Nullable Object request, Object... uriVariables);
	void put(String url,String serviceName, @Nullable Object request, Map<String, ?> uriVariables);
	
	void delete(String url, String serviceName,Object... uriVariables);
	void delete(String url,String serviceName, Map<String, ?> uriVariables);
	
}
