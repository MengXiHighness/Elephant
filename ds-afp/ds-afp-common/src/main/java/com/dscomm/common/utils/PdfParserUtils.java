package com.dscomm.common.utils;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;

import org.apache.pdfbox.io.RandomAccessRead;
import org.apache.pdfbox.pdfparser.PDFParser;
import org.apache.pdfbox.pdmodel.PDDocument;
import org.apache.pdfbox.text.PDFTextStripper;


public class PdfParserUtils implements DocumentParser {

	@Override
	public String parser(String filePath) {
		
		if(filePath==null)
			return null;
		File file=new File(filePath);
		if(!file.exists()){
			return null;
		}
		return parser(file);
	}

	@Override
	public String parser(File file) {
		FileInputStream fileInputStream=null;
		try {
			fileInputStream = new FileInputStream(file);
		} catch (FileNotFoundException e1) {
			e1.printStackTrace();
			return null;
		}
		PDFParser parser;
		String reString=null;
		try {
			parser = new PDFParser((RandomAccessRead) fileInputStream);
			parser.parse();
			PDDocument pdDocument=parser.getPDDocument();
			PDFTextStripper pdfTextStripper=new PDFTextStripper();
			reString=pdfTextStripper.getText(pdDocument);
		} catch (IOException e) {
			e.printStackTrace();
		}	
		return reString.trim();
	}

}
