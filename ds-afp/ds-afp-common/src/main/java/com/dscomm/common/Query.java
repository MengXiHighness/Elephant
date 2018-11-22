/* 
 * @(#)Query.java
 *
 * Copyright 2017, 迪爱斯通信设备有限公司保留.
 */
package com.dscomm.common;


/**
 * Query  查询条件
 * 
 * @author qiushi
 */
public class Query {

	private QueryForm queryForm = null;
	
	private PageForm pageForm = null;

	/**
	 * 获取类成员queryForm
	 * 
	 * @return {@link #queryForm}
	 */
	public QueryForm getQueryForm() {
		return this.queryForm;
	}

	/**
	 * 设定类成员queryForm
	 * 
	 * @param queryForm
	 *            要设定的{@link #queryForm}
	 */
	public void setQueryForm(QueryForm queryForm) {
		this.queryForm = queryForm;
	}

	/**
	 * 获取类成员pageForm
	 * 
	 * @return {@link #pageForm}
	 */
	public PageForm getPageForm() {
		return this.pageForm;
	}

	/**
	 * 设定类成员pageForm
	 * 
	 * @param pageForm
	 *            要设定的{@link #pageForm}
	 */
	public void setPageForm(PageForm pageForm) {
		this.pageForm = pageForm;
	}

	
}
