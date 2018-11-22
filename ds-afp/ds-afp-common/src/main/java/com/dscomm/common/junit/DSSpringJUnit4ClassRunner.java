package com.dscomm.common.junit;

import org.junit.runners.model.InitializationError;
import org.springframework.test.context.junit4.SpringJUnit4ClassRunner;



import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.util.List;
import java.util.Map;
import java.util.Properties;
import org.apache.commons.io.IOUtils;
import org.junit.runners.model.Statement;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;


public class DSSpringJUnit4ClassRunner extends SpringJUnit4ClassRunner {

	private Logger logger = LoggerFactory.getLogger(this.getClass());

	public DSSpringJUnit4ClassRunner(Class<?> clazz) throws InitializationError {
		super(clazz);
		// TODO Auto-generated constructor stub
	}

	@Override
	protected Statement withBeforeClasses(Statement statement) {
		ProfileConfigure configFilesCatcher = new ProfileConfigure();

		// 获取filter文件
		List<File> filterFiles = configFilesCatcher.getProfileFiles();
		Properties properties = new Properties();
		FileInputStream fis = null;
		for (File filterFile : filterFiles) {
			try {
				fis = new FileInputStream(filterFile);
				// filter中的键值对
				properties.load(fis);
			} catch (IOException e) {
				logger.warn("filter文件加载失败", e);
				throw new RuntimeException("filter文件加载失败", e);
			} finally {
				closeIs(fis);

			}
		}
		// File filterFile = configFilesCatcher.getFilterFile();

		// 遍历类路径下的所有配置文件
		List<File> classpathFiles = configFilesCatcher.getClasspathFiles();
		for (File file : classpathFiles) {
			FileInputStream is = null;
			FileOutputStream fos = null;
			try {
				is = new FileInputStream(file);
				ByteArrayOutputStream baos = new ByteArrayOutputStream();
				IOUtils.copy(is, baos);
				// 将该配置文件中含有filter中的元素子串替换
				String content = new String(baos.toByteArray());
				//System.out.println("文件："+file.getPath());
				//System.out.println("文件内容："+content);
				for (Map.Entry<Object, Object> entry : properties.entrySet()) {
					String key = "${" + entry.getKey() + "}";
					if (content.contains(key)) {
						System.out.println("key:"+key+",value:"+entry.getValue());
						content = content.replace(key, (CharSequence) entry.getValue());
					}
				}
				// 配置文件替换完毕回写
				fos = new FileOutputStream(file);
				fos.write(content.getBytes());
				//System.out.println("保存后文件内容："+content.getBytes());
				logger.info("{}文件配置替换成功", file.getName());
			} catch (IOException e) {
				logger.warn("{} 文件的filter替换失败", file.getName());
				// 不影响下一个文件
				continue;
			} finally {
				closeIs(is);
				closeOs(fos);
			}
		}

		logger.info("filter文件配置替换完毕");
		// 执行SpringJUnit4ClassRunner原本逻辑
		return super.withBeforeClasses(statement);
	}

	private void closeIs(InputStream is) {
		if (is != null) {
			try {
				is.close();
				is = null;
			} catch (IOException e) {
				throw new RuntimeException(e);
			}
		}
	}

	private void closeOs(OutputStream os) {
		if (os != null) {
			try {
				os.close();
				os = null;
			} catch (IOException e) {
				throw new RuntimeException(e);
			}
		}
	}

}
