import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import 'dart:convert';
import 'package:tracker/entities/adresse.dart';

class ThirdPage extends StatefulWidget {
  @override
  ThirdPageState createState() => ThirdPageState();
}

class ThirdPageState extends State<ThirdPage> {
   List<Adresse> list = List();
  var isLoading = false;

  _fetchData() async {
    setState(() {
      isLoading = true;
    });
    final response =
        await http.get("http://192.168.1.7:8080/gps/api/read.php");
    if (response.statusCode == 200) {
      list = (json.decode(response.body) as List)
          .map((data) => new Adresse.fromJson(data))
          .toList();
      setState(() {
        isLoading = false;
      });
    } else {
      throw Exception('Failed to load adresse');
    }
  }
  @override
  Widget build(BuildContext context) {
    return Scaffold(
        floatingActionButton: FloatingActionButton(
          onPressed: _fetchData,
          child: Icon(
            Icons.history,
          ),
        ),
        body:buildListView()
         
       );
  }

  ListView buildListView() {
    return ListView.builder(
      itemCount: list.length,
      itemBuilder: (BuildContext context, int index) {
        return Card(
          elevation: 14.0,
          shadowColor: Color(0x802196F3),
          child: ListTile(
              contentPadding: EdgeInsets.all(12),
              title: Text(
                list[index].adress,
                style: TextStyle(fontSize: 13),
              ),
              subtitle: Text(
                list[index].date,
                style: TextStyle(fontSize: 10),
              ),
              leading: Icon(Icons.history)
              ),
        );
      },
    );
  }
}
