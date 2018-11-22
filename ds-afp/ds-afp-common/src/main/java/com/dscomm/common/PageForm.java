/* 
 * @(#)PageForm.java
 *
 * Copyright 2017, 迪爱斯通信设备有限公司保留.
 */
package com.dscomm.common;


/**
 * PageForm 分页条件
 * 
 * @author qiushi
 */
public class PageForm {

	/** 成员变量：当前页码 */
	private int currentPage = 1;
	
	/** 成员变量：每页最大数据  */
	private int pageSize = 20;

	/** 成员变量：数据总条数  */
	private int totalSize = 0;
	
	/** 成员变量：总页数  */
	private int totalPage = 0;

	/**
	 * 获取类成员currentPage
	 * 
	 * @return {@link #currentPage}
	 */
	public int getCurrentPage() {
		return this.currentPage;
	}

	/**
	 * 设定类成员currentPage
	 * 
	 * @param currentPage
	 *            要设定的{@link #currentPage}
	 */
	public void setCurrentPage(int currentPage) {
		this.currentPage = currentPage;
	}

	/**
	 * 获取类成员pageSize
	 * 
	 * @return {@link #pageSize}
	 */
	public int getPageSize() {
		return this.pageSize;
	}

	/**
	 * 设定类成员pageSize
	 * 
	 * @param pageSize
	 *            要设定的{@link #pageSize}
	 */
	public void setPageSize(int pageSize) {
		this.pageSize = pageSize;
	}

	/**
	 * 获取类成员totalSize
	 * 
	 * @return {@link #totalSize}
	 */
	public int getTotalSize() {
		return this.totalSize;
	}

	/**
	 * 设定类成员totalSize
	 * 
	 * @param totalSize
	 *            要设定的{@link #totalSize}
	 */
	public void setTotalSize(int totalSize) {
		this.totalSize = totalSize;
	}

	/**
	 * 获取类成员totalPage
	 * 
	 * @return {@link #totalPage}
	 */
	public int getTotalPage() {
		return this.totalPage;
	}

	/**
	 * 设定类成员totalPage
	 * 
	 * @param totalPage
	 *            要设定的{@link #totalPage}
	 */
	public void setTotalPage(int totalPage) {
		this.totalPage = totalPage;
	}
	
}
