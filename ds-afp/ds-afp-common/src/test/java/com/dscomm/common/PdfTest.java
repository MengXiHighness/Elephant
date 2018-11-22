package com.dscomm.common;

import static org.junit.Assert.*;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.OutputStreamWriter;
import java.io.Writer;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.HashMap;
import java.util.Map;

import org.apache.commons.io.IOUtils;
import org.apache.pdfbox.cos.COSDocument;
import org.apache.pdfbox.io.RandomAccessFile;
import org.apache.pdfbox.io.RandomAccessRead;
import org.apache.pdfbox.pdfparser.PDFParser;
import org.apache.pdfbox.pdmodel.PDDocument;
import org.apache.pdfbox.pdmodel.PDPage;
import org.apache.pdfbox.pdmodel.encryption.InvalidPasswordException;
import org.apache.pdfbox.text.PDFTextStripper;
import org.junit.Test;

public class PdfTest {

	@Test
	public void test() throws IOException {

		// 开始提取页数
		int startPage = 1;
		// 结束提取页数
		int endPage = Integer.MAX_VALUE;
		// String filePath =
		// "D:\\DocSpace\\技术\\activiti\\J2EE文档\\JavaScript权威指南.pdf";
		String filePath = "D:\\DocSpace\\技术\\J2EE文档\\thymeleaf_3.0.5_中文参考手册.pdf";
		RandomAccessRead accessRead = new RandomAccessFile(new File(filePath), "rw");
		PDFParser parser = new PDFParser(accessRead);
		parser.parse();
		PDDocument pdDocument = parser.getPDDocument();

		PDFTextStripper pdfTextStripper = new PDFTextStripper();

		pdfTextStripper.setSortByPosition(false);
		endPage = pdDocument.getNumberOfPages();
		/*
		 * pdfTextStripper.setStartPage(startPage);
		 * pdfTextStripper.setEndPage(endPage);
		 */
		String reString = "";
		for (PDPage p : pdDocument.getPages()) {
			// reString += p.getContents().toString();//.getText(pdDocument);
			reString += IOUtils.toString(p.getContents(), "utf-8");
		}
		// String reString = pdfTextStripper.getText(pdDocument);
		accessRead.close();
		pdDocument.close();
		System.out.print("ok");
	}

	@Test
	public void testreadpdf(){
		try {
			this.testpdf();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
	
	
	public Map testpdf() throws InvalidPasswordException, IOException {
		// 分页查询保存到map里
		// 是否排序
		boolean sort = false;
		// pdf文件名
		// String pdfFile = file;
		// 输入文本文件名称
		String textFile = null;
		// 编码方式
		String encoding = "UTF-8";
		// 开始提取页数
		int startPage = 1;
		// 结束提取页数
		int endPage = 5;
		// 文件输入流，生成文本文件
		Writer output = null;
		// 内存中存储的PDF Document
		PDDocument document = null;
		StringBuilder sb = new StringBuilder();
		Map mpdf = new HashMap();
		try {
			try {
				// 首先当作一个URL来装载文件，如果得到异常再从本地文件系统//去装载文件
				// URL url = new URL(pdfFile);

				String filePath = "D:\\DocSpace\\技术\\J2EE文档\\thymeleaf_3.0.5_中文参考手册.pdf";
				// RandomAccessRead accessRead = new RandomAccessFile(new
				// File(filePath), "rw");
				// 注意参数已不是以前版本中的URL.而是File。
				File file = new File(filePath);
				document = PDDocument.load(file);
				// 获取PDF的文件名
				String fileName = file.getName();
				// 以原来PDF的名称来命名新产生的txt文件
				if (fileName.length() > 4) {
					File outputFile = new File(fileName.substring(0, fileName.length() - 4) + ".txt");
					textFile = outputFile.getName();
				}
			} catch (MalformedURLException e) {
				// 如果作为URL装载得到异常则从文件系统装载
				// 注意参数已不是以前版本中的URL.而是File。
				/*
				 * document = PDDocument.load(pdfFile); if (pdfFile.length() >
				 * 4) { textFile = pdfFile.substring(0, pdfFile.length() - 4) +
				 * ".txt"; }
				 */
			}
			// 文件输入流，写入文件倒textFile
			output = new OutputStreamWriter(new FileOutputStream(textFile), encoding);
			// PDFTextStripper来提取文本
			PDFTextStripper stripper = null;
			stripper = new PDFTextStripper();
			// 设置是否排序
			stripper.setSortByPosition(sort);
			// // 设置起始页
			// stripper.setStartPage(startPage);
			// // 设置结束页
			// stripper.setEndPage(endPage);

			for (int i = startPage; i <= document.getNumberOfPages(); i++) {
				stripper = new PDFTextStripper();
				stripper.setSortByPosition(sort);
				// 设置起始页
				stripper.setStartPage(i);
				// 设置结束页
				stripper.setEndPage(i);
				String textT = stripper.getText(document);
				sb.append(textT);
				// System.out.println("第" + i+"页");
				// System.out.println(
				// "开始--------------------------------------------------------------------");
				// System.out.println( textT);
				// System.out.println(
				// "--------------------------------------------------------------------结束");
				mpdf.put(i, textT);
			}
			// System.out.println(mpdf.size());
			// 调用PDFTextStripper的writeText提取并输出文本
			// stripper.writeText(document, output);
			// System.out.println(stripper.getEndPage());
			// System.out.println( "*****="+stripper.getText(document));
			// System.out.println("*****22=" + stripper.getTextLineMatrix());
			// System.out.println("*****33=" + stripper.getTextMatrix());
			// System.out.println("*****44=" + stripper.getArticleStart());
			// System.out.println("*****55=" + stripper.getArticleEnd());

		} finally {
			if (output != null) {
				// 关闭输出流
				output.close();
			}
			if (document != null) {
				// 关闭PDF Document
				document.close();
			}
		}
		String t1 = sb.toString();
		return mpdf;
	}

}
