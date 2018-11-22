package com.dscomm.common.cloud;

public class ServiceAddress {
    private int port;
    private String host;

    public ServiceAddress() {
    }

    public ServiceAddress(int port, String host) {
        this.port = port;
        this.host = host;
    }

    public int getPort() {
        return port;
    }

    public void setPort(int port) {
        this.port = port;
    }

    public String getHost() {
        return host;
    }

    public void setHost(String host) {
        this.host = host;
    }

    /**
     * 将服务地址转换为 http://主机名:端口/ 的格式
     * @return
     */
    @Override
    public String toString() {
        StringBuilder sb = new StringBuilder(15 + host.length());
        sb.append("http://").append(host).append(":").append(port).append("/");

        return sb.toString();
    }
}
