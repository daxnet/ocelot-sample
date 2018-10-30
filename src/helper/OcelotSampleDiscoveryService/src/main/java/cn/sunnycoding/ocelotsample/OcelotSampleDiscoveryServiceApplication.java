package cn.sunnycoding.ocelotsample;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.cloud.netflix.eureka.server.EnableEurekaServer;

@EnableEurekaServer
@SpringBootApplication
public class OcelotSampleDiscoveryServiceApplication {

	public static void main(String[] args) {
		SpringApplication.run(OcelotSampleDiscoveryServiceApplication.class, args);
	}
}
