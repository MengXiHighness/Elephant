package com.dscomm.common.cloud.aop;

import java.lang.reflect.Field;
import java.lang.reflect.Method;

import org.aopalliance.intercept.MethodInterceptor;
import org.aopalliance.intercept.MethodInvocation;
import org.aspectj.lang.JoinPoint;
import org.springframework.stereotype.Service;

import com.dscomm.common.cloud.LoadBalanced;

@Service
public class LoadBalancedInterceptor implements MethodInterceptor {
	

	@Override
	public Object invoke(MethodInvocation invocation) throws Throwable {

		Method invokeMethod = invocation.getMethod();  
        Object[] args = invocation.getArguments();  
        
		return null;
	}

}
