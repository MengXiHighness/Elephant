package com.dscomm.common;

import java.util.List;

import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.cloud.client.loadbalancer.LoadBalanced;
import org.springframework.http.ResponseEntity;
import org.springframework.test.context.ContextConfiguration;
import org.springframework.test.context.junit4.SpringJUnit4ClassRunner;
import org.springframework.web.client.RestTemplate;

import com.dscomm.common.cloud.RestClient;
import com.dscomm.common.cloud.ServiceAddress;
import com.dscomm.common.cloud.ServiceAddressSelector;

import com.netflix.niws.loadbalancer.DiscoveryEnabledNIWSServerList;

@RunWith(SpringJUnit4ClassRunner.class)
@ContextConfiguration(locations = "classpath*:spring-afp-svc.xml")
public class cloudTest {
	
	@Autowired
	//@LoadBalanced
	private RestClient restcli;

	@Test
	public void test() {
		
		String result = restcli.getForObject("http://rest-service/","rest-service", String.class);
		
		System.out.println(result);
		String name = DiscoveryEnabledNIWSServerList.class.getName();
		// 选择出myclient对应服务全部可用地址
		List<ServiceAddress> list = ServiceAddressSelector.selectAvailableServers("rest-service");
		System.out.println(list);

		// 选择出myclient对应服务的一个可用地址(轮询), 返回null表示服务当前没有可用地址
		ServiceAddress addr = ServiceAddressSelector.selectOne("rest-service");
		System.out.println(addr);
		
		
	}

}
