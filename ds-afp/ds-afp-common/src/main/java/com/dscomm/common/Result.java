package com.dscomm.common;

import java.io.Serializable;

public class Result implements Serializable{

	private static final long serialVersionUID = 1L;
	public String ret=ResultStatus.OK;
	public String msg="";
	public Object dataStore=null;
	
	public static Result ok(Object dataStore){
		Result r = new Result();
		r.setRet(ResultStatus.OK);
		r.setDataStore(dataStore);
		r.setMsg("Success");
		return r;
	}
	
	public static Result error(String msg){
		Result r = new Result();
		r.setRet(ResultStatus.ERROR);
		r.setMsg(msg);
		return r;
	}
	
	public String getRet() {
		return ret;
	}
	public void setRet(String ret) {
		this.ret = ret;
	}
	public String getMsg() {
		return msg;
	}
	public void setMsg(String msg) {
		this.msg = msg;
	}
	public Object getDataStore() {
		return dataStore;
	}
	public void setDataStore(Object dataStore) {
		this.dataStore = dataStore;
	}
	
}
