package com.dscomm.common.junit;

import java.io.File;
import java.util.ArrayList;
import java.util.List;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.dscomm.common.utils.FilePathUtil;

public class ProfileConfigure {
	private Logger logger = LoggerFactory.getLogger(this.getClass());
	private String projectPath;// 项目绝对路径

	public ProfileConfigure() {
		this.projectPath = getProjectPath();
		logger.info("文件获取器初始化成功，项目绝对路径为：{}", this.projectPath);
	}

	/*
	 * 获取profile=test的配置,默认profile为test目录，将该目录下的所有配置文件的配置项获取来替换通用的配置中的${}
	 */
	public List<File> getProfileFiles() {
		String classes = projectPath + "/target/classes/dev/";
		List<File> propertityFiles = new ArrayList<>();
		findFiles(new File(classes), propertityFiles,false);
		return propertityFiles;
	}



	private String getProjectPath() {
		File file = new File(this.getClass().getClassLoader().getResource("").getFile());
		while (file != null && file.getParentFile() != null && !file.getName().equals("target")) {
			file = file.getParentFile();
			if (file.getName() != null && file.getName().equals("target")) {
				file = file.getParentFile();
				break;
			}
		}
		logger.info("项目绝对路径获取成功");
		return FilePathUtil.pathDecode(file.getAbsolutePath());
	}

	/**
	 * 获取类路径下的所有配置文件列表
	 *
	 * @return
	 */
	public List<File> getClasspathFiles() {
		String classes = projectPath + "/target/classes";
		String testClasses = projectPath + "/target/test-classes";
		List<File> classesFiles = new ArrayList<>();
	
		findFiles(new File(classes), classesFiles,true);
		findFiles(new File(testClasses), classesFiles,true);
		logger.info("类路径下配置文件列表获取成功：{}", classesFiles);
		return classesFiles;
	}

	// 递归遍历类路径下的所有class外的配置文件
	private void findFiles(File file, List<File> files,boolean isIgnoreDir) {
		if (file.isDirectory()) {

			String dir = file.getPath().substring(file.getPath().lastIndexOf(file.separatorChar) + 1);
			System.out.println("dir:" + dir);
			String[] ignoreDir = new String[]{"test","dev","prod"};
			if (isIgnoreDir && (dir.toLowerCase().equals(ignoreDir[0]) || dir.toLowerCase().equals(ignoreDir[1])
					|| dir.toLowerCase().equals(ignoreDir[2]))) {
				System.out.println("忽略的目录" + dir);
				// 忽略
			} else {
				File[] fs = file.listFiles();
				for (File f : fs) {					
					findFiles(f, files,isIgnoreDir);
				}
			}
		} else {
			if (file.getName().endsWith(".xml") || file.getName().endsWith(".properties")) {
				files.add(file);
			}
		}
	}

}
