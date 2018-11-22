package com.dscomm.common.cloud.aop;

import java.lang.reflect.Field;
import java.lang.reflect.Method;

import org.springframework.stereotype.Component;
import org.springframework.stereotype.Service;
import org.springframework.web.client.RestTemplate;

import com.dscomm.common.cloud.LoadBalanced;

import org.aspectj.lang.JoinPoint;

@Service
public class LoadBalancedAdvices {
	public void before(JoinPoint joinPoint) {
		Object target = joinPoint.getTarget();
		String methodName = joinPoint.getSignature().getName();
		System.out.println(target + "-------" + methodName);
		Method method;
		Field field;
		try {
			field = target.getClass().getField(methodName);
			if (field != null) {
				boolean isExist = field.isAnnotationPresent(LoadBalanced.class);
				if (isExist) {
					Object[] args = joinPoint.getArgs();
				}
			}

		} catch (NoSuchFieldException | SecurityException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

	}

}
