package com.dscomm.common.utils;

import java.io.UnsupportedEncodingException;

public class FilePathUtil {

	public static String pathDecode(String filePath){
		try {
			return java.net.URLDecoder.decode(filePath,"urf-8");
		} catch (UnsupportedEncodingException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return "";
	}
}
