package com.dscomm.common;

import java.util.ArrayList;
import java.util.List;

public class Enum {
	private String key = "";
	
	private List<EnumItem> enumItems = new ArrayList<EnumItem>();

	public String getKey() {
		return key;
	}

	public void setKey(String key) {
		this.key = key;
	}

	public List<EnumItem> getEnumItems() {
		return enumItems;
	}

	public void setEnumItems(List<EnumItem> enumItems) {
		this.enumItems = enumItems;
	}
	
}
