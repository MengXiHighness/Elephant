package com.dscomm.common;

import java.util.HashSet;
import java.util.Random;
import java.util.Set;
import java.util.concurrent.TimeUnit;

import redis.clients.jedis.Jedis;
import redis.clients.jedis.JedisSentinelPool;

public class ThreadRedis implements Runnable{

	private static JedisSentinelPool jedisSentinelPool;
	public  void init(){
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
	
	@Override
	public void run() {
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

				System.out.println(e.getMessage()+ e);
			} finally {
				try {
					if (jedis != null) {
						jedis.close();
						//jedisSentinelPool.returnBrokenResource(jedis);
					}
				} catch (Exception ex) {
					System.out.println(ex.getMessage());
					jedis = null;
				}
			}
		}
		
	}

}
