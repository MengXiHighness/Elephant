/* 
 * @(#)DataStore.java
 *
 * Copyright 2017, 迪爱斯通信设备有限公司保留.
 */
package com.dscomm.common;

import java.util.HashMap;

/**
 * DataStore  数据输出结构
 * 
 * @author jn
 */
public class DataStore {

	private Object data = null;
	
	private Query query = null;
	
	private HashMap<String,Enum> enums = new HashMap<String,Enum>();

	public HashMap<String, Enum> getEnums() {
		return enums;
	}

	public void setEnums(HashMap<String, Enum> enums) {
		this.enums = enums;
	}

	/**
	 * 获取类成员data
	 * 
	 * @return {@link #data}
	 */
	public Object getData() {
		return this.data;
	}

	/**
	 * 设定类成员data
	 * 
	 * @param data
	 *            要设定的{@link #data}
	 */
	public void setData(Object data) {
		this.data = data;
	}	

	/**
	 * 获取类成员query
	 * 
	 * @return {@link #query}
	 */
	public Query getQuery() {
		return this.query;
	}

	/**
	 * 设定类成员query
	 * 
	 * @param query
	 *  要设定的{@link #query}
	 */
	public void setQuery(Query query) {
		this.query = query;
	}

}
