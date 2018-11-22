package com.dscomm.common.utils;

import java.io.File;

public interface DocumentParser {
	String parser(String filePath);
	String parser(File file);
}
