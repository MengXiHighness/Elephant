package com.dscomm.common.cloud;



import java.lang.management.ManagementFactory;
import java.net.InetAddress;
import java.net.UnknownHostException;
import java.util.Set;

import javax.management.MBeanServer;
import javax.management.ObjectName;
import javax.management.Query;

import com.netflix.appinfo.MyDataCenterInstanceConfig;

public class ServiceInstanceConfig extends MyDataCenterInstanceConfig {
	
	/**
	 * Eureka鐩戞帶椤甸潰Status鍐呭睍绀哄湴鍧�
	 */
	@Override
	public String getInstanceId() {
		try {
			return InetAddress.getLocalHost().getHostAddress() + ":" + getNonSecurePort();
		} catch (UnknownHostException e) {
			return super.getInstanceId();
		}
	}


    /**
     * 浼樺厛浣跨敤ip 鏇挎崲 hostname
     * @param refresh
     * @return
     */
    @Override
    public String getHostName(boolean refresh) {
        try {
            return InetAddress.getLocalHost().getHostAddress();
        } catch (UnknownHostException e) {
            return super.getHostName(refresh);
        }
    }


    /***
     *鑾峰彇鏈満鍚姩涓璽omcat绔彛鍙�
     * @return
     */
    @Override
    public int getNonSecurePort(){
        int tomcatPort;
        try {
            MBeanServer beanServer = ManagementFactory.getPlatformMBeanServer();
            Set<ObjectName> objectNames = beanServer.queryNames(new ObjectName("*:type=Connector,*"),
                    Query.match(Query.attr("protocol"), Query.value("HTTP/1.1")));

            tomcatPort = Integer.valueOf(objectNames.iterator().next().getKeyProperty("port"));
        }catch (Exception e){
            return super.getNonSecurePort();
        }
        return tomcatPort;
    }
}

