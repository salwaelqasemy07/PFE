class Adresse {
  final String adress;
  final String latitude;
  final String longitude;
  final String date;
  Adresse({this.adress, this.latitude, this.longitude, this.date});

  Adresse.fromJson(Map<String, dynamic> json)
      : adress = json['adress'] ?? '',
        latitude = json['latitude'] ?? '',
        longitude = json['longitude'] ?? '',
        date = json['date'] ?? '';
}
