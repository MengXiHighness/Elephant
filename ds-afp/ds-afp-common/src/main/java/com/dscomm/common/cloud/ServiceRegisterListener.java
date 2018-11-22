package com.dscomm.common.cloud;




import java.io.IOException;

import javax.servlet.ServletContextEvent;
import javax.servlet.ServletContextListener;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.netflix.appinfo.ApplicationInfoManager;
import com.netflix.appinfo.InstanceInfo;
import com.netflix.appinfo.MyDataCenterInstanceConfig;
import com.netflix.config.ConfigurationManager;
import com.netflix.config.DynamicPropertyFactory;
import com.netflix.discovery.DefaultEurekaClientConfig;
import com.netflix.discovery.DiscoveryManager;

public class ServiceRegisterListener implements ServletContextListener {
	
	private static final Logger LOGGER = LoggerFactory.getLogger(ServiceRegisterListener.class);

    private  DynamicPropertyFactory configInstance = DynamicPropertyFactory
            .getInstance();
    
  
    public static final String RIBBON_CONFIG_FILE_NAME = "springcloud.properties";

    static{
    	System.setProperty("archaius.configurationSource.defaultFileName", RIBBON_CONFIG_FILE_NAME);
    }

    /**
     * * Notification that the web application initialization
     * * process is starting.
     * * All ServletContextListeners are notified of context
     * * initialization before any filter or servlet in the web
     * * application is initialized.
     *
     * @param sce
     */
    public void contextInitialized(ServletContextEvent sce) {
    	
        
       /* System.setProperty("archaius.configurationSource.defaultFileName", RIBBON_CONFIG_FILE_NAME);*/
       
        registerWithEureka();
    }

    public void registerWithEureka() {
        /**鍔犺浇鏈湴閰嶇疆鏂囦欢 鏍规嵁閰嶇疆鍒濆鍖栬繖鍙� Eureka Application Service 骞朵笖娉ㄥ唽鍒� Eureka Server*/
        DiscoveryManager.getInstance().initComponent(
                new ServiceInstanceConfig(),
                new DefaultEurekaClientConfig());

         
        /**鏈彴 Application Service 宸插惎鍔紝鍑嗗濂戒緧鏈嶇綉缁滆姹�*/
        ApplicationInfoManager.getInstance().setInstanceStatus(
                InstanceInfo.InstanceStatus.UP);

        /**Application Service 鐨� Eureka Server 鍒濆鍖栦互鍙婃敞鍐屾槸寮傛鐨勶紝闇�瑕佷竴娈垫椂闂� 姝ゅ绛夊緟鍒濆鍖栧強娉ㄥ唽鎴愬姛 鍙幓闄�*/
        String vipAddress = configInstance.getStringProperty(
                "eureka.vipAddress", "").getValue();
        InstanceInfo nextServerInfo = null;
        while (nextServerInfo == null) {
            try {
                nextServerInfo = DiscoveryManager.getInstance()
                        .getDiscoveryClient()
                        .getNextServerFromEureka(vipAddress, false);
            } catch (Throwable e) {
                System.out.println("Waiting for service to register with eureka..");
                try {
                    Thread.sleep(10000);
                } catch (InterruptedException e1) {
                    e1.printStackTrace();
                }


            }
        }
        System.out.println("Service started and ready to process requests..");

    }

    /**
     * * Notification that the servlet context is about to be shut down.
     * * All servlets and filters have been destroy()ed before any
     * * ServletContextListeners are notified of context
     * * destruction.
     *
     * @param sce
     */
    public void contextDestroyed(ServletContextEvent sce) {
        DiscoveryManager.getInstance().shutdownComponent();
    }
}