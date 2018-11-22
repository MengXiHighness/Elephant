package com.dscomm.common;

import java.io.IOException;
import java.util.HashSet;
import java.util.Random;
import java.util.Set;
import java.util.concurrent.TimeUnit;

import org.junit.Test;

import redis.clients.jedis.Jedis;
import redis.clients.jedis.JedisSentinelPool;

public class redisTest {

	JedisSentinelPool jedisSentinelPool;
	private void init(){
		String masterName = "mymaster";
		Set<String> sentinelSet = new HashSet<>();
		sentinelSet.add("192.168.6.205:26379");
		sentinelSet.add("192.168.6.206:26379");
		sentinelSet.add("192.168.6.207:26379");
		String pwd = "123456";
		jedisSentinelPool = new JedisSentinelPool(masterName, sentinelSet, pwd);
		int counter = 1;
		Jedis jedis = null;
	}
	
	@Test
	public void testLoop(){
		ThreadRedis ThreadRedis = new ThreadRedis();
		ThreadRedis.init();
		for(int i=0;i<10;i++){
			Thread th = new Thread(new ThreadRedis());
			
			th.start();
		}
		try {
			System.in.read();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
	
	@Test
	public void test() {
		if(jedisSentinelPool==null)
			init();
		
		int counter = 1;
		Jedis jedis = null;
		while (true) {
			counter++;
			try {
				jedis = jedisSentinelPool.getResource();
				int index = new Random().nextInt(1000000);
				String key = "k_" + index;
				String value = "v_" + index;
				jedis.set(key, value);

				if (counter % 200 == 0) {
					System.out.println(key + " value is " + jedis.get(key));
					// logger.info("{} value is {}",key,jedis.get(key));
				}
				TimeUnit.MILLISECONDS.sleep(10);
			} catch (Exception e) {

				System.out.printf(e.getMessage(), e);
			} finally {
				try {
					if (jedis != null) {
						jedis.close();
					}
				} catch (Exception ex) {
					jedis = null;
							
					System.out.println(ex.getMessage());
				}
			}
		}
	}

}
