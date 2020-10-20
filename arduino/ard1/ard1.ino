#include <TinyGPS++.h>
#include <SoftwareSerial.h>
SoftwareSerial compim(7, 8);
TinyGPSPlus gps;  //Creates a new instance of the TinyGPS object
void setup()
{
  Serial.begin(9600);
  compim.begin(9600);
}
void getadress(){
  if (gps.encode(Serial.read())) {
      if (gps.location.isValid()) {
        compim.print(gps.location.lat(), 6);
        compim.print(",");
        compim.print(gps.location.lng(), 6);
        compim.print(",");
        compim.println();
      }
      else
        Serial.println("Location Invalid");
    }
  }
void loop()
{
  while (Serial.available() > 0) {
    getadress();
  }     
}
