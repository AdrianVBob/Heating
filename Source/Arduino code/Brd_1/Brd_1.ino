
#include "ESP8266WiFi.h"
#include "ESP8266HTTPClient.h"
#include "DHT.h"

#define DHTPIN 15
#define DHTTYPE DHT11


const char* ssid = "NSA-Phone";
const char* pwd = "12345678";
float h;
float t;
HTTPClient http;

DHT dht(DHTPIN, DHTTYPE);
WiFiServer server(80);

String payload;

void setup() {
  pinMode(0,OUTPUT);
  Serial.begin(115200);
 
  pinMode(LED_BUILTIN, OUTPUT);
  WiFi.mode(WIFI_STA);
  
  Serial.println("Connecting...");
  Serial.println(ssid);
 
  WiFi.begin(ssid, pwd);
  Serial.println("Waiting to connect...");
 
  while(WiFi.status() != WL_CONNECTED){
    delay(500);
    Serial.print(".");
  }
  Serial.println();
  Serial.println(WiFi.localIP());
  Serial.println("Connected");
  
dht.begin();
server.begin();
}

void loop() {
h = dht.readHumidity();
t = dht.readTemperature();
if(isnan(h) || isnan(t)) {
    Serial.println("Failed to read from sensor");
    return;
  }
   Serial.println("temperature: "+String(t));
   Serial.println("humidity: "+String(h));
   WiFiClient client;
Serial.println("Call the endpoint");
http.begin("http://chsapi.azurewebsites.net/api/values/addToDb?t="+String(t)+
"&h="+String(h)+"&user=adrian&p=12345");
int httpCode=http.GET();
if(httpCode) {
   if(httpCode==200)
  {
    payload=http.getString();
   
    Serial.println(payload);
    Serial.println("test");

    if (payload=="0"){
      digitalWrite(0,HIGH);
    }
    
    if(payload == "1") {
      digitalWrite(0,LOW);
    }
    
  }
}
else 
{
  Serial.println("Failed, no connection or no HTTP server");
}
client.stop();
delay(20000);


}

