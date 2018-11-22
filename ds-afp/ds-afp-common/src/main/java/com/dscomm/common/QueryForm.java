/* 
 * @(#)QueryForm.java
 *
 * Copyright 2017, 迪爱斯通信设备有限公司保留.
 */
package com.dscomm.common;

/**
 * QueryForm 数据库查询条件
 * 
 * @author qiushi
 */
public class QueryForm {

	private Object queryData = null;

	/**
	 * 获取类成员queryData
	 * 
	 * @return {@link #queryData}
	 */
	public Object getQueryData() {
		return this.queryData;
	}

	/**
	 * 设定类成员queryData
	 * 
	 * @param queryData
	 *            要设定的{@link #queryData}
	 */
	public void setQueryData(Object queryData) {
		this.queryData = queryData;
	}
}
