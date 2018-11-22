package com.dscomm.common.cloud;

import java.net.URI;
import java.util.Map;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.springframework.http.HttpMethod;
import org.springframework.http.ResponseEntity;
import org.springframework.lang.Nullable;
import org.springframework.web.client.HttpMessageConverterExtractor;
import org.springframework.web.client.RequestCallback;
import org.springframework.web.client.ResponseExtractor;
import org.springframework.web.client.RestClientException;
import org.springframework.web.client.RestTemplate;


public class RestClientImpl extends RestTemplate implements RestClient {

		private static final Log logger = LogFactory.getLog(RestClient.class);
		
		private String SelectService(String url,String serviceName){
			ServiceAddress addr = ServiceAddressSelector.selectOne(serviceName);			
			url = url.replaceFirst(serviceName, addr.getHost()+":"+addr.getPort());
			return url;
		}
		
		
		
		public <T> T getForObject(String url,String serviceName, Class<T> responseType) throws RestClientException {
			
			return getForObject(SelectService(url,serviceName),responseType);
		}
		
		public <T> T getForObject(String url,String serviceName, Class<T> responseType, Map<String, ?> uriVariables) throws RestClientException {
			
			return getForObject( SelectService(url,serviceName), responseType,  uriVariables);
		}		
		public <T> T getForObject(String url,String serviceName, Class<T> responseType, Object... uriVariables) throws RestClientException {
			
			return getForObject(SelectService(url,serviceName), responseType, uriVariables);
		}
		
		public <T> ResponseEntity<T> getForEntity(String url,String serviceName, Class<T> responseType, Object... uriVariables)
				throws RestClientException {

			return getForEntity(SelectService(url,serviceName), responseType,  uriVariables);
		}
		
		public <T> ResponseEntity<T> getForEntity(String url, String serviceName,Class<T> responseType, Map<String, ?> uriVariables)
				throws RestClientException {
			
			return getForEntity(SelectService(url,serviceName),  responseType,  uriVariables);
		}

		public <T> T postForObject(String url,String serviceName, @Nullable Object request, Class<T> responseType,
				Object... uriVariables) throws RestClientException {
		
			return postForObject(SelectService(url,serviceName),  request, responseType,uriVariables);
		}
		public <T> T postForObject(String url,String serviceName, @Nullable Object request, Class<T> responseType,
				Map<String, ?> uriVariables) throws RestClientException {
			
			return postForObject(SelectService(url,serviceName), request, responseType,	uriVariables);
		}
		
		public <T> ResponseEntity<T> postForEntity(String url,String serviceName, @Nullable Object request,
				Class<T> responseType, Object... uriVariables) throws RestClientException {			
			return postForEntity( SelectService(url,serviceName),  request, responseType, uriVariables);
		}
		
		public <T> ResponseEntity<T> postForEntity(String url,String serviceName, @Nullable Object request,
				Class<T> responseType, Map<String, ?> uriVariables) throws RestClientException {			
			return postForEntity(SelectService(url,serviceName), request, responseType, uriVariables);
		}
		
		public void put(String url,String serviceName, @Nullable Object request, Object... uriVariables)
				throws RestClientException {
			put(SelectService(url,serviceName), request, uriVariables);
		}
		public void put(String url,String serviceName, @Nullable Object request, Map<String, ?> uriVariables)
				throws RestClientException {
			put(SelectService(url,serviceName), request, uriVariables);
		}
		
		public void delete(String url, String serviceName,Object... uriVariables) throws RestClientException {
			delete(SelectService(url,serviceName), uriVariables);
		}	
		public void delete(String url,String serviceName, Map<String, ?> uriVariables) throws RestClientException {
			delete(SelectService(url,serviceName), uriVariables);
		}



}
